import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { ApplicationEmpSingleDDComponent } from 'src/Application/Components/application-emp-single-dd/application-emp-single-dd.component';
import { ApplicationMultiTreeDropdownComponent } from 'src/Application/Components/application-multi-tree-dropdown/application-multi-tree-dropdown.component';
import { DesignationModel, DesignationResponseModel, AttributeModel } from '../../master-model';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { ValidationMessage } from '../../validation-message';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observer, forkJoin, Observable, of } from 'rxjs';
import { EntityOrgunitTreeVM, EntityOrgunitDepartmentModel } from 'src/app/landing/Modules/common-model';
import { isNullOrUndefined } from 'util';
import { MasterService } from '../../master.service';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.css']
})
export class DesignationComponent extends WebComponents.ApplicationComponent implements OnInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  @ViewChild(ApplicationMultiTreeDropdownComponent, { static: true, read: ApplicationMultiTreeDropdownComponent })
  private treeDropdown: ApplicationMultiTreeDropdownComponent;
  private m_editData: DesignationResponseModel;

  public DesignationConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'DesignationID', fieldName: '', columnClass: 'width100' },
      { fieldId: 'Designation', fieldName: 'Designation', columnClass: 'text-nowrap' },
      { fieldId: 'DesignationCode', fieldName: 'Designation Code', columnClass: 'text-nowrap' },
      { fieldId: 'EntityOrgDeptDesc', fieldName: 'Entity/Org Units/Departments', columnClass: 'text-nowrap' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'DesignationID',
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
  public DesignationForm: FormGroup;
  public TreeSelectedCB = this.SelectedValueInTree.bind(this);

  private DesignationObs: Observer<any>;
  private SelectedTreeValue: Array<EntityOrgunitTreeVM> = [];
  private TreeData: Array<EntityOrgunitTreeVM>;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private masterService: MasterService,
    private commonService: CommonService,
    private uiSweetAlertService: UiSweetAlertService,
  ) {
    super(Components.DesignationComponent, applicationService, changeDetection, viewContainerRef);
    this.DesignationObs = {
      next: (data: any) => {
        this.HandleDesignationResp(data);
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  public ngOnInit() {
    this.CreateDesignationForm();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    forkJoin([
      this.GetAllDesignation(1, this.DesignationConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' })
    ]).subscribe(
      (data: any[]) => {
        this.HandleDesignationResp(data[0]);
      },
      (error) => {
        this.HideLoader();
      }
    )
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllDesignation(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.DesignationObs
      );
    });
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }

  private SelectedValueInTree(data: EntityOrgunitTreeVM) {
    if (this.SelectedTreeValue.length != 0) {
      if (this.SelectedTreeValue.find(
        x => this.CompareTreeItem(x, data)) != undefined) {
        return true;
      }
    }
    return false;
  }
  private CompareTreeItem(source: EntityOrgunitDepartmentModel, dest: EntityOrgunitTreeVM): boolean {
    if (source.DepartmentId == dest.DepartmentId &&
      source.OrgUnitId == dest.OrgUnitId &&
      source.EntityId == dest.EntityId) {
      return true;
    }
    return false;
  }

  private CompareTreeModel(source: EntityOrgunitTreeVM, dest: EntityOrgunitTreeVM): boolean {
    if (source.DepartmentId == dest.DepartmentId &&
      source.OrgUnitId == dest.OrgUnitId &&
      source.EntityId == dest.EntityId) {
      return true;
    }
    return false;
  }
  private GetAllDesignation(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllDesignation(paginationModel);
  }
  private HandleDesignationResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.uiDataTableComponent.ConstructRow([], 0);
    }
    else {
      this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
    }
  }

  private CreateDesignationForm() {
    this.DesignationForm = this.fb.group({
      Name: ['', [Validators.required]],
      Code: ['', [Validators.required]],
      IsDoNotUse: [false],
      Id: []
    });
  }

  public OpenDesignationPopup(isAddMode: boolean, data: DesignationResponseModel) {
    this.isAddMode = isAddMode;
    this.m_editData = data;
    this.SelectedTreeValue = [];
    let desgId = 0;
    let apiList = [];
    if (isNullOrUndefined(this.TreeData)) {
      apiList.push(this.commonService.GetEntityOrgunitDeptTree(0));
    }
    else {
      apiList.push(of(this.TreeData))
    }
    if (isAddMode) {
      this.CreateDesignationForm();
    }
    else {
      desgId = data.DesignationID;
      apiList.push(this.masterService.GetDesignationById(desgId))
    }
    this.ShowLoader();
    forkJoin(apiList).subscribe(
      (data) => {
        this.HideLoader();
        if (!isNullOrUndefined(data)) {
          if (data.length > 1) {
            this.m_editData = data[1];
            this.DesignationForm.patchValue({
              Name: this.m_editData.Designation,
              Code: this.m_editData.DesignationCode,
              Id: this.m_editData.DesignationID,
              IsDoNotUse: this.m_editData.IsDoNotUse
            } as DesignationModel);
            this.SelectedTreeValue = this.m_editData.entityOrgunitDepartment.map(
              x => {
                return {
                  EntityId: x.EntityId,
                  OrgUnitId: x.OrgUnitId,
                  DepartmentId: x.DepartmentId
                } as EntityOrgunitTreeVM;
              }
            );
          }
          if (data.length > 0) {
            this.treeDropdown.UpdateTree(data[0]);
            this.TreeData = data[0];
          }
          this.OpenModelPopup("#addDesignation");
        }
      },
      (error) => {
        this.HideLoader();
      }
    )
  }

  public OnTreeValueSelect(isSelected: boolean, data: EntityOrgunitTreeVM) {
    let i = this.SelectedTreeValue.findIndex(x => this.CompareTreeModel(x, data));
    if (isSelected) {
      if (i == -1) {
        this.SelectedTreeValue.push(data);
      }
    } else {
      if (i != -1) {
        this.SelectedTreeValue.splice(i, 1);
      }
    }
  }

  public DesignationOp() {
    this.ShowLoader();
    if (this.DesignationForm.invalid) {
      this.triggerValidation(this.DesignationForm);
      this.HideLoader();
      return;
    }
    let model: DesignationModel = this.DesignationForm.value as DesignationModel;
    if (!isNullOrUndefined(this.SelectedTreeValue)) {
      // Add Tree Value in model
      model.DTAttributesMap = this.SelectedTreeValue.map(x => {
        return {
          EntityId: x.EntityId,
          OrgUnitId: x.OrgUnitId,
          DeptId: x.DepartmentId
        } as AttributeModel;
      });
    }
    let apiObs: Observable<any>;
    if (this.isAddMode) {
      apiObs = this.masterService.AddDesignation(model);
    }
    else {
      model.Id = this.m_editData.DesignationID;
      apiObs = this.masterService.UpdateDesignation(model);
    }

    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addDesignation");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Department data saved successfully");
        this.GetAllDesignation(1, this.DesignationConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.DesignationObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

}
