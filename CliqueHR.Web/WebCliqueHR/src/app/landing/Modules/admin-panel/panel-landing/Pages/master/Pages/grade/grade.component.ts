import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild, AfterViewInit } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { GradeType } from '../../master-model';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterService } from '../../master.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { ValidationMessage } from '../../validation-message';
import { Observer, Observable } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { CustomValidator } from 'src/Application/Utilitis/CustomValidator';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';

@Component({
  selector: 'app-grade',
  templateUrl: './grade.component.html',
  styleUrls: ['./grade.component.css']
})
export class GradeComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  private m_editData:GradeType;

  public GradeConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '', columnClass: 'width80' },
      { fieldId: 'TypeName', fieldName: 'Grade', columnClass: 'text-nowrap' },
      { fieldId: 'MinSalary', fieldName: 'Min Salary'},
      { fieldId: 'MaxSalary', fieldName: 'Max Salary'}
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };
  public GradeList: Array<GradeType>;
  public isAddMode: boolean;
  public validationMassages = ValidationMessage.data;
  public GradeForm: FormGroup;
  private GradeObs: Observer<any>;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private masterService: MasterService,
    private uiSweetAlertService: UiSweetAlertService,
    private fb: FormBuilder
  ) {
    super(Components.GradeComponent, applicationService, changeDetection, viewContainerRef);
    this.GradeObs = {
      next: (data: any) => {
        this.HideLoader();
        if (isNullOrUndefined(data)) {
          this.uiDataTableComponent.ConstructRow([], 0);
        }
        else {
          this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
        }
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  public ngOnInit() {
    this.CreateGradeForm();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    this.GetAllGradeType(1, this.GradeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
      this.GradeObs
    );
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllGradeType(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.GradeObs
      );
    });
  }
  protected TriggerCommand(request: WebInterface.CommandRequest) {
    
  }
  private CreateGradeForm() {
    this.GradeForm = this.fb.group({
      TypeName: ['', [Validators.required]],
      MinSalary: ['', [Validators.required, Validators.min(1)]],
      MaxSalary: ['', [Validators.required]],
      IsDoNotUse: [false],
      Id:[]
    },
    {
      validator: CustomValidator.MinMaxControlValue("MinSalary","MaxSalary")
    });
  }

  private GetAllGradeType(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllGradeType(paginationModel);
  }
  public OpenGradePopup(isAddMode: boolean, data: GradeType) {
    this.isAddMode = isAddMode;
    if (isAddMode) {
      this.CreateGradeForm();
    }
    else {
      this.m_editData = data;
      this.GradeForm.patchValue(data);
    }
    this.OpenModelPopup("#addGrade");
  }

  public GradeOp() {
    this.ShowLoader();
    if (this.GradeForm.invalid) {
      this.triggerValidation(this.GradeForm);
      this.HideLoader();
      return;
    }
    let model: GradeType = this.GradeForm.value as GradeType;
    let apiObs: Observable<any>;
    if (this.isAddMode) {
      apiObs = this.masterService.AddGradeType(model);
    }
    else {
      apiObs = this.masterService.UpdateGradeType(model);
    }
    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addGrade");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Grade data saved successfully");
        this.GetAllGradeType(1, this.GradeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.GradeObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

}
