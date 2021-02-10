import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, AfterViewInit, ViewChild } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { ValidationMessage } from '../../validation-message';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MasterService } from '../../master.service';
import { isNullOrUndefined } from 'util';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { CenterTypeModel } from '../../master-model';
import { Observable, Observer } from 'rxjs';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';

@Component({
  selector: 'app-center-type',
  templateUrl: './center-type.component.html',
  styleUrls: ['./center-type.component.css']
})
export class CenterTypeComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  private m_editData: CenterTypeModel;

  public CenterTypeConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '', columnClass: 'width100' },
      { fieldId: 'CenterTypeName', fieldName: 'Centre Type', columnClass: 'text-nowrap' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };

  public CenterTypeForm: FormGroup;
  public validationMassages = ValidationMessage.data;
  public CenterTypeList: Array<CenterTypeModel>;
  public isAddMode: boolean;

  private CentreTypeObs: Observer<any>;
  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private masterService: MasterService,
    private uiSweetAlertService: UiSweetAlertService,
  ) {
    super(Components.CenterTypeComponent, applicationService, changeDetection, viewContainerRef);
    this.CentreTypeObs = {
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
    this.CreateCenterTypeForm();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    this.GetAllCenterType(1, this.CenterTypeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
      this.CentreTypeObs
    );
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllCenterType(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.CentreTypeObs
      )
    });
  }
  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }
  private CreateCenterTypeForm() {
    this.CenterTypeForm = this.fb.group({
      CenterTypeName: ['', [Validators.required]],
      IsDoNotUse: [false],
      Id:[]
    });
  }

  private GetAllCenterType(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllCenterType(paginationModel);
  }
  public OpenCenterTypePopup(isAddMode: boolean, data: CenterTypeModel) {
    this.isAddMode = isAddMode;
    if (isAddMode) {
      this.CreateCenterTypeForm();
    }
    else {
      this.m_editData = data;
      this.CenterTypeForm.patchValue(data);
    }
    this.OpenModelPopup("#addCentreType");
  }

  public CurrencyOp() {
    this.ShowLoader();
    if (this.CenterTypeForm.invalid) {
      this.triggerValidation(this.CenterTypeForm);
      this.HideLoader();
      return;
    }
    let model: CenterTypeModel = this.CenterTypeForm.value as CenterTypeModel;
    let apiObs: Observable<any>;
    if (this.isAddMode) {
      apiObs = this.masterService.AddCenterTypeData(model);
    }
    else {
      apiObs = this.masterService.UpdateCenterTypeData(model);
    }
    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addCentreType");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Centre Type data saved successfully");
        this.GetAllCenterType(1, this.CenterTypeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.CentreTypeObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

}
