import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild, AfterViewInit } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { ApplicationEmpSingleDDComponent } from 'src/Application/Components/application-emp-single-dd/application-emp-single-dd.component';
import { ApplicationSingleTreeDropdownComponent } from 'src/Application/Components/application-single-tree-dropdown/application-single-tree-dropdown.component';
import { Department } from '../../company-model';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { ValidationMessage } from '../../validation-message';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observer, Observable, forkJoin } from 'rxjs';
import { CompanyService } from '../../company.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { isNullOrUndefined } from 'util';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { EntityOrgunitTreeVM } from 'src/app/landing/Modules/common-model';
import { CommonService } from 'src/app/landing/Modules/common.service';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css']
})
export class DepartmentsComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  @ViewChild(ApplicationEmpSingleDDComponent, { static: true, read: ApplicationEmpSingleDDComponent })
  private empDropdown: ApplicationEmpSingleDDComponent;
  @ViewChild(ApplicationSingleTreeDropdownComponent, { static: true, read: ApplicationSingleTreeDropdownComponent })
  private treeDropdown: ApplicationSingleTreeDropdownComponent;
  private m_editData: Department;

  public DepartmentConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '' },
      { fieldId: 'Name', fieldName: 'Department Name', columnClass: 'text-nowrap' },
      { fieldId: 'Code', fieldName: 'Department Code' },
      { fieldId: 'ParentUnit', fieldName: 'Parent Department' },
      { fieldId: 'HOD', fieldName: 'Head of Department', columnClass: 'text-nowrap' },
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };
  public treeConfig: WebInterface.TreeDropdown = {
    DisplayProperty: "Name",
    ChildProperty: "Childs",
    HideSearch: true,
    isEditable: true,
    ValueProperty: "",
    Placeholder: "Select"
  };
  public isAddMode: boolean;
  public validationMassages = ValidationMessage.data;
  public DepartmentForm: FormGroup;
  public TreeSelectedCB = this.SelectedValueInTree.bind(this);

  private DepartmentObs: Observer<any>;
  private SelectedTreeValue: EntityOrgunitTreeVM;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private companyService: CompanyService,
    private commonService: CommonService,
    private uiSweetAlertService: UiSweetAlertService,
  ) {
    super(Components.DepartmentsComponent, applicationService, changeDetection, viewContainerRef);
    this.DepartmentObs = {
      next: (data: any) => {
        this.HandleDepartmentResp(data);
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    forkJoin([
      this.GetAllDepartment(1, this.DepartmentConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' })
    ]).subscribe(
      (data: any[]) => {
        this.HandleDepartmentResp(data[0]);
      },
      (error) => {
        this.HideLoader();
      }
    )
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllDepartment(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.DepartmentObs
      );
    });
  }

  ngOnInit() {
    this.CreateDepartmentForm();
  }

  private SelectedValueInTree(data: EntityOrgunitTreeVM) {
    if (!isNullOrUndefined(this.m_editData)) {
      if (this.m_editData.ParentDepartmentId != 0) {
        if (data.DepartmentId == this.m_editData.ParentDepartmentId) {
          this.SelectedTreeValue = data;
          return true;
        }
      }
      else if (this.m_editData.ParentOrgUnitId != 0) {
        if (data.OrgUnitId == this.m_editData.ParentOrgUnitId) {
          this.SelectedTreeValue = data;
          return true;
        }
      }
      else if (data.EntityId == this.m_editData.ParentEntityId && data.OrgUnitId == 0 && data.DepartmentId == 0) {
        this.SelectedTreeValue = data;
        return true;
      }
    }
    return false;
  }
  private GetAllDepartment(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.companyService.GetDepartments(paginationModel);
  }
  private HandleDepartmentResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.uiDataTableComponent.ConstructRow([], 0);
    }
    else {
      this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
    }
  }

  private CreateDepartmentForm() {
    this.DepartmentForm = this.fb.group({
      Name: ['', [Validators.required]],
      Code: ['', [Validators.required]],
      IsDoNotUse: [false],
      Id: []
    });
  }

  public OpenDepartmentPopup(isAddMode: boolean, data: Department) {
    this.ShowLoader();
    this.isAddMode = isAddMode;
    let deptId = 0;
    if (isAddMode) {
      this.DepartmentForm.get("Code").enable({ emitEvent: true });
      this.DepartmentForm.get("Name").enable({ emitEvent: true });
      this.CreateDepartmentForm();
      this.companyService.GetDepartmentCode().subscribe(
        (data: string) => {
          if (!isNullOrUndefined(data) && data.trim() != '') {
            this.DepartmentForm.get("Code").setValue(data);
            this.DepartmentForm.get("Code").disable({ emitEvent: true });
          }
        },
        (error) => {
          this.validationMessage = ValidationBuilder.build(error);
          this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
        }
      )
    }
    else {
      this.m_editData = data;
      this.DepartmentForm.get("Code").disable({ emitEvent: true });
      this.DepartmentForm.get("Name").disable({ emitEvent: true });
      this.empDropdown.SetSelectedValue({ Text: this.m_editData.HOD, Value: this.m_editData.HODId });
      this.DepartmentForm.patchValue(data);
      deptId = this.m_editData.Id;
    }
    this.commonService.GetEntityOrgunitDeptTree(deptId).subscribe(
      (data: Array<EntityOrgunitTreeVM>) => {
        this.HideLoader();
        this.treeDropdown.UpdateTree(data);
      },
      (error) => {
        this.HideLoader();
      }
    );
    this.OpenModelPopup("#addEditDepartments");
  }

  public OnTreeValueSelect(data: EntityOrgunitTreeVM) {
    this.SelectedTreeValue = data;
  }

  public DepartmentOp() {
    this.ShowLoader();
    if (this.DepartmentForm.invalid) {
      this.triggerValidation(this.DepartmentForm);
      this.HideLoader();
      return;
    }
    let model: Department = this.DepartmentForm.value as Department;
    if (!isNullOrUndefined(this.SelectedTreeValue)) {
      model.ParentEntityId = this.SelectedTreeValue.EntityId;
      model.ParentOrgUnitId = this.SelectedTreeValue.OrgUnitId;
      model.ParentDepartmentId = this.SelectedTreeValue.DepartmentId;
    }
    let selectedUnitHead = this.empDropdown.GetSelectedValue();
    if (!isNullOrUndefined(selectedUnitHead)) {
      model.HODId = Number(selectedUnitHead.Value);
    }
    if (!this.isAddMode) {
      model.Id = this.m_editData.Id;
    }
    let apiObs: Observable<any> = this.companyService.AddUpdateDepartment(model);

    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addEditDepartments");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Department data saved successfully");
        this.GetAllDepartment(1, this.DepartmentConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.DepartmentObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }
}
