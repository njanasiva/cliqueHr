import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild, AfterViewInit } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MasterService } from '../../master.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { EmployeeType } from '../../master-model';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { ValidationMessage } from '../../validation-message';
import { Observer, Observable } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';

@Component({
  selector: 'app-employee-type',
  templateUrl: './employee-type.component.html',
  styleUrls: ['./employee-type.component.css']
})
export class EmployeeTypeComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  private m_editData: EmployeeType;

  public EmployeeTypeConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '' },
      { fieldId: 'TypeName', fieldName: 'Employee Type', columnClass: 'text-nowrap' },
      { fieldId: 'SelfService', fieldName: 'Self Service' },
      { fieldId: 'MinAge', fieldName: 'Min. age for employment' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };

  public EmployeeTypeForm: FormGroup;
  public validationMassages = ValidationMessage.data;
  public EmployeeTypeList: Array<EmployeeType>;
  public isAddMode: boolean;

  private EmployeeTypeObs: Observer<any>;
  
  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private masterService: MasterService,
    private uiSweetAlertService: UiSweetAlertService,
  ) {
    super(Components.EmployeeTypeComponent, applicationService, changeDetection, viewContainerRef);
    this.EmployeeTypeObs = {
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
    this.GetAllEmployeeType(1, this.EmployeeTypeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
      this.EmployeeTypeObs
    );
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllEmployeeType(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.EmployeeTypeObs
      )
    });
  }
  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }
  private CreateCenterTypeForm() {
    this.EmployeeTypeForm = this.fb.group({
      TypeName: ['', [Validators.required]],
      MinAge: ['', [Validators.required, Validators.min(18)]],
      SelfService: [false],
      IsDoNotUse: [false],
      Id:[]
    });
  }

  private GetAllEmployeeType(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllEmployeeType(paginationModel);
  }
  public OpenEmployeeTypePopup(isAddMode: boolean, data: EmployeeType) {
    this.isAddMode = isAddMode;
    if (isAddMode) {
      this.CreateCenterTypeForm();
    }
    else {
      this.m_editData = data;
      this.EmployeeTypeForm.patchValue(data);
    }
    this.OpenModelPopup("#addEmpType");
  }

  public EmployeeTypeOp() {
    this.ShowLoader();
    if (this.EmployeeTypeForm.invalid) {
      this.triggerValidation(this.EmployeeTypeForm);
      this.HideLoader();
      return;
    }
    let model: EmployeeType = this.EmployeeTypeForm.value as EmployeeType;
    let apiObs: Observable<any>;
    if (this.isAddMode) {
      apiObs = this.masterService.AddEmployeeType(model);
    }
    else {
      apiObs = this.masterService.UpdateEmployeeType(model);
    }
    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addEmpType");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("EmployeeType data saved successfully");
        this.GetAllEmployeeType(1, this.EmployeeTypeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.EmployeeTypeObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

}
