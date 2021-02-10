import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild, AfterViewInit } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { OrgUnits } from '../../company-model';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { ValidationMessage } from '../../validation-message';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observer, forkJoin, Observable } from 'rxjs';
import { CompanyService } from '../../company.service';
import { isNullOrUndefined } from 'util';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { ApplicationEmpSingleDDComponent } from 'src/Application/Components/application-emp-single-dd/application-emp-single-dd.component';
import { ApplicationSingleTreeDropdownComponent } from 'src/Application/Components/application-single-tree-dropdown/application-single-tree-dropdown.component';
import { EntityOrgunitTreeVM } from 'src/app/landing/Modules/common-model';

@Component({
  selector: 'app-org-units',
  templateUrl: './org-units.component.html',
  styleUrls: ['./org-units.component.css']
})
export class OrgUnitsComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  @ViewChild(ApplicationEmpSingleDDComponent, { static: true, read: ApplicationEmpSingleDDComponent })
  private empDropdown: ApplicationEmpSingleDDComponent;
  @ViewChild(ApplicationSingleTreeDropdownComponent, { static: true, read: ApplicationSingleTreeDropdownComponent })
  private treeDropdown: ApplicationSingleTreeDropdownComponent;
  private m_editData: OrgUnits;

  public OrgUnitConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '' },
      { fieldId: 'Name', fieldName: 'Unit Name', columnClass: 'text-nowrap' },
      { fieldId: 'Code', fieldName: 'Unit Code' },
      { fieldId: 'ParentUnit', fieldName: 'Parent Unit', columnClass: 'text-nowrap' },
      { fieldId: 'UnitHead', fieldName: 'Unit Head', columnClass: 'text-nowrap' },
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
  public OrgUnitList: Array<OrgUnits>;
  public isAddMode: boolean;
  public validationMassages = ValidationMessage.data;
  public OrgUnitForm: FormGroup;
  public TreeSelectedCB = this.SelectedValueInTree.bind(this);

  private OrgUnitObs: Observer<any>;
  private SelectedTreeValue: EntityOrgunitTreeVM;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private companyService: CompanyService,
    private uiSweetAlertService: UiSweetAlertService,
  ) {
    super(Components.OrgUnitsComponent, applicationService, changeDetection, viewContainerRef);
    this.OrgUnitObs = {
      next: (data: any) => {
        this.HandleOrgUnitResp(data);
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  ngOnInit() {
    this.CreateOrgUnitForm();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    forkJoin([
      this.GetAllOrgUnits(1, this.OrgUnitConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' })
    ]).subscribe(
      (data: any[]) => {
        this.HandleOrgUnitResp(data[0]);
      },
      (error) => {
        this.HideLoader();
      }
    )
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllOrgUnits(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.OrgUnitObs
      );
    });
  }
  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }
  private SelectedValueInTree(data: EntityOrgunitTreeVM) {
    if (!isNullOrUndefined(this.m_editData)) {
      if (data.EntityId == this.m_editData.ParentEntityId) {
        if (isNullOrUndefined(this.m_editData.ParentOrgUnitId) || this.m_editData.ParentOrgUnitId == 0) {
          this.SelectedTreeValue = data;
          return true;
        }
        else if (this.m_editData.ParentOrgUnitId == data.OrgUnitId) {
          this.SelectedTreeValue = data;
          return true;
        }
      }
    }
    return false;
  }
  private GetAllOrgUnits(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.companyService.GetOrgUnits(paginationModel);
  }
  private HandleOrgUnitResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.uiDataTableComponent.ConstructRow([], 0);
    }
    else {
      this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
    }
  }
  private CreateOrgUnitForm() {
    this.OrgUnitForm = this.fb.group({
      Name: ['', [Validators.required]],
      Code: ['', [Validators.required]],
      IsDoNotUse: [false],
      Id: []
    });
  }

  public OpenOrgUnitPopup(isAddMode: boolean, data: OrgUnits) {
    this.isAddMode = isAddMode;
    let orgId = 0;
    if (isAddMode) {
      this.OrgUnitForm.get("Code").enable({ emitEvent: true });
      this.OrgUnitForm.get("Name").enable({ emitEvent: true });
      this.CreateOrgUnitForm();
    }
    else {
      this.m_editData = data;
      this.OrgUnitForm.get("Code").disable({ emitEvent: true });
      this.OrgUnitForm.get("Name").disable({ emitEvent: true });
      this.empDropdown.SetSelectedValue({ Text: this.m_editData.UnitHead, Value: this.m_editData.UnitHeadId });
      this.OrgUnitForm.patchValue(data);
      orgId = this.m_editData.Id;
    }
    this.ShowLoader();
    this.companyService.GetEntityOrgunitTree(orgId).subscribe(
      (data: Array<EntityOrgunitTreeVM>) => {
        this.HideLoader();
        this.treeDropdown.UpdateTree(data);
      },
      (error) => {
        this.HideLoader();
      }
    );
    this.OpenModelPopup("#addeditOrgUnit");
  }

  public OnTreeValueSelect(data: EntityOrgunitTreeVM) {
    this.SelectedTreeValue = data;
  }

  public OrgUnitOp() {
    this.ShowLoader();
    if (this.OrgUnitForm.invalid) {
      this.triggerValidation(this.OrgUnitForm);
      this.HideLoader();
      return;
    }
    let model: OrgUnits = this.OrgUnitForm.value as OrgUnits;
    if (!isNullOrUndefined(this.SelectedTreeValue)) {
      model.ParentEntityId = this.SelectedTreeValue.EntityId;
      model.ParentOrgUnitId = this.SelectedTreeValue.OrgUnitId;
    }
    let selectedUnitHead = this.empDropdown.GetSelectedValue();
    if (!isNullOrUndefined(selectedUnitHead)) {
      model.UnitHeadId = Number(selectedUnitHead.Value);
    }
    if (!this.isAddMode) {
      model.Id = this.m_editData.Id;
    }
    let apiObs: Observable<any> = this.companyService.AddUpdateOrgUnits(model);

    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addeditOrgUnit");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("OrgUnit data saved successfully");
        this.GetAllOrgUnits(1, this.OrgUnitConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.OrgUnitObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

}
