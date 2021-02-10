import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild, AfterViewInit } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MasterService } from '../../master.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { BandType, GradeType } from '../../master-model';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { ValidationMessage } from '../../validation-message';
import { Observer, Observable, forkJoin } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { ApplicationMultiselectComponent } from 'src/Application/Components/application-multiselect/application-multiselect.component';

@Component({
  selector: 'app-band',
  templateUrl: './band.component.html',
  styleUrls: ['./band.component.css']
})
export class BandComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  private m_editData: BandType;

  @ViewChild(ApplicationMultiselectComponent,{read: ApplicationMultiselectComponent, static: true})
  private applicationMultiselectComponent: ApplicationMultiselectComponent;

  public GradeConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '' },
      { fieldId: 'TypeName', fieldName: 'Band Name', columnClass: 'text-nowrap' },
      { fieldId: 'GradeMapping', fieldName: 'Grades MappingGrade' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };
  public BandList: Array<BandType>;
  public isAddMode: boolean;
  public validationMassages = ValidationMessage.data;
  public BandForm: FormGroup;
  private BandObs: Observer<any>;

  public GradeList:Array<GradeType> = [];

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private masterService: MasterService,
    private uiSweetAlertService: UiSweetAlertService,
    private fb: FormBuilder
  ) {
    super(Components.BandComponent, applicationService, changeDetection, viewContainerRef);
    this.BandObs = {
      next: (data: any) => {
        this.HandleBandResp(data);
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  public ngOnInit() {
    this.CreateBandForm();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    forkJoin([
        this.masterService.GetGradeList(),
        this.GetAllBandType(1, this.GradeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' })
      ]).subscribe(
        (data:any[]) => {
          this.GradeList = data[0] || [];
          this.HandleBandResp(data[1]);
        },
        (error) => {
          this.HideLoader();
        }
      )
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllBandType(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.BandObs
      );
    });
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }
  private HandleBandResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.uiDataTableComponent.ConstructRow([], 0);
    }
    else {
      this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
    }
  }
  private CreateBandForm() {
    this.BandForm = this.fb.group({
      TypeName: ['', [Validators.required]],
      IsDoNotUse: [false],
      Id: []
    });
  }

  private GetAllBandType(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllBandType(paginationModel);
  }

  private ResetGradeList(){
    this.GradeList.forEach((grd:any) => {
      grd.Selected = false;
    });
  }

  public OpenBandPopup(isAddMode: boolean, data: BandType) {
    this.isAddMode = isAddMode;
    this.ResetGradeList();
    if (isAddMode) {
      this.CreateBandForm();
    }
    else {
      this.m_editData = data;
      if(!isNullOrUndefined(this.m_editData.GradeMapping) && this.m_editData.GradeMapping.trim() != ''){
        let selectedGrades = this.m_editData.GradeMapping.split('|');
        this.GradeList.forEach((grd:any) => {
          if(selectedGrades.findIndex(x => x == grd.Id) != -1){
            grd.Selected = true;
          }
          else {
            grd.Selected = false;
          }
        });
      }
      this.BandForm.patchValue(data);
    }
    this.applicationMultiselectComponent.Refresh(this.GradeList);
    this.OpenModelPopup("#addBand");
  }

  public BandOp() {
    this.ShowLoader();
    if (this.BandForm.invalid) {
      this.triggerValidation(this.BandForm);
      this.HideLoader();
      return;
    }
    let model: BandType = this.BandForm.value as BandType;
    let gradeMapping = this.GradeList.filter((x:any) => x.Selected == true);
    model.GradeMapping = gradeMapping.map(x => x.Id).join('|');
    let apiObs: Observable<any>;
    if (this.isAddMode) {
      apiObs = this.masterService.AddBandType(model);
    }
    else {
      apiObs = this.masterService.UpdateBandType(model);
    }
    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addBand");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Brand data saved successfully");
        this.GetAllBandType(1, this.GradeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.BandObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

  public OnGradeSelect(event) {
    let data: any = this.GradeList.find(grd => grd.Id == event.Id);
    data.Selected = true;
  }

  public OnGradeUnSelect(event) {
    let data: any = this.GradeList.find(grd => grd.Id == event.Id);
    data.Selected = false;
  }

  public OnGradeSelectAll(event) {
    this.GradeList.forEach((grd:any) => {
      grd.Selected = true;
    });
  }

  public OnGradeUnSelectAll(event) {
    this.ResetGradeList();
  }

}
