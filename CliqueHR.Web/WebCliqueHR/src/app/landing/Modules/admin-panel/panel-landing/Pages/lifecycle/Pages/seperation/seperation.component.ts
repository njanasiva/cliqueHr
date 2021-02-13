import { Component, OnInit, ViewChild, Inject, AfterViewInit, ViewContainerRef, ChangeDetectorRef, ViewChildren, QueryList } from '@angular/core';
import { UiDataTableConfig, SortType, UiMultiselectData, UiMultiSelectOptions, UiMultiSelectOutPut } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { ValidationMessage } from 'src/app/landing/Modules/admin-panel/admin-panel-services/lifecycle-validation';
import { LifeCycleService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/lifecycle.service';
import { Components } from 'src/Application/Types/Constants';
import { isNullOrUndefined } from 'util';
import { forkJoin, Observable, of } from 'rxjs';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';

@Component({
  selector: 'app-seperation',
  templateUrl: './seperation.component.html',
  styleUrls: ['./seperation.component.css']
})
export class SeperationComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;
  addForm: FormGroup;
  rows: FormArray;
  itemForm: FormGroup;
  public NoticePeriodDetailArrayList = new Array<any>();
  public SeperationReasonArrayList = new Array<any>();
  public SeperationTaskArrayList = new Array<any>();
  public orgunit = new Array<UiMultiselectData>();
  public centerType = new Array<UiMultiselectData>();
  public region = new Array<UiMultiselectData>();
  public location = new Array<UiMultiselectData>();
  public employeeType = new Array<UiMultiselectData>();
  public position = new Array<UiMultiselectData>();
  public costCentre = new Array<UiMultiselectData>();
  public grade = new Array<UiMultiselectData>();
  public entity = new Array<UiMultiselectData>();
  public department = new Array<UiMultiselectData>();
  public employeegroup = new Array<UiMultiselectData>();
  public designation = new Array<UiMultiselectData>();
  public daytype = new Array<UiMultiselectData>();
  public interviewtype = new Array<UiMultiselectData>();
  public taskownername = new Array<UiMultiselectData>();
  public escaltelevel = new Array<UiMultiselectData>();
  public orgEntityDepartment = new Array<UiMultiSelectOptions>();
  public selectedOrgEntityDepartment = new Array<UiMultiSelectOptions>();
  public separationtypename = new Array<UiMultiselectData>();
  public isSeparationtypenameRequiredError: boolean = false;

  public SeparationFromList = new Array<any>();
  public TaskOwnerList = new Array<any>();
  public EscalateLevelList = new Array<any>();
  public DayTypeNameList = new Array<any>();
  public InterviewTypeNameList = new Array<any>();

  public NoticePeriodDetailForm: FormGroup;
  public SeperationReasonForm: FormGroup;
  public SeperationTaskForm: FormGroup;
  public SeparationForm: FormGroup;

  public isAddMode: boolean = true;
  public popupHeading: string = '';
  public showForm: boolean = false;
  public showDiv: boolean = false;
  public centreTypeSelectedValues: string = '';
  public regionSelectedValues: string = '';
  public locationSelectedValues: string = '';
  public employeeTypeSelectedValues: string = '';
  public positionSelectedValues: string = '';
  public costCentreSelectedValues: string = '';
  public gradeSelectedValues: string = '';
  public entitySelectedValues: string = '';
  public donotUse: boolean = false;
  public orgPlaceHolder: string = 'Entity/Org Unit/Department';
  public separationtypeId: number = 0;
  public employeegroupSelectedValues: string = '';
  public orgunitSelectedValues: string = '';
  public departmentSelectedValues: string = '';
  public designationSelectedValues: string = '';
  public RelievingDateTypeId: boolean = false;
  public ApproveManagerType: boolean = false;
  public RetirementAge: number = 0;
  public RetirementDays: number = 0;


  public SeperationConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'NoticePeriodDetailId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'NoticePeriodName', fieldName: 'Notice Period Name', columnClass: 'text-nowrap width100' },
      { fieldId: 'NoticePeriodDays', fieldName: 'Notice Period Days', columnClass: 'text-nowrap width100' },
      { fieldId: 'ConfirmationDays', fieldName: 'ConfirmationDays', columnClass: 'text-nowrap width100' },
      { fieldId: 'PublishTo', fieldName: 'Publish to', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'NoticePeriodDetailId', direction: 'asc' },
    UniqueRowCol: 'NoticePeriodDetailId',
    isEditable: true
  };

  public SeperationReasonConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'SeparationReasonId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'SeparationReason', fieldName: 'Seperation Reason', columnClass: 'text-nowrap width100' },
      { fieldId: 'SeparationTypeName', fieldName: 'Seperation Type', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'SeparationReasonId', direction: 'asc' },
    UniqueRowCol: 'SeparationReasonId',
    isEditable: true
  };

  public SeperationTasksConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'SeparationTaskId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'SeparationTask', fieldName: 'Seperation Tasks', columnClass: 'text-nowrap width100' },
      { fieldId: 'SeparationTypeName', fieldName: 'Seperation Form', columnClass: 'text-nowrap width100' },
      { fieldId: 'TaskOwnerName', fieldName: 'Task Owner', columnClass: 'text-nowrap width100' },
      { fieldId: 'EscalateLevel', fieldName: 'Escalate to', columnClass: 'text-nowrap width100' },
      { fieldId: 'InitiateTaskDays', fieldName: 'Initiate Task', columnClass: 'text-nowrap width100' },
      { fieldId: 'EscalatePostDay', fieldName: 'Escalate Post(in days)', columnClass: 'text-nowrap width100' },
      { fieldId: 'Location', fieldName: 'Applicable to', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'SepTasksId', direction: 'asc' },
    UniqueRowCol: 'SepTasksId',
    isEditable: true
  };


  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private commonService: CommonService,
    private LifeCycleService: LifeCycleService,
    private uiSweetAlertService: UiSweetAlertService
  ) {
    super(Components.SeperationComponent, applicationService, changeDetection, viewContainerRef);
    this.addForm = this.fb.group({
      items: [null, Validators.required],
    });

    this.rows = this.fb.array([]);
  }


  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateNoticePeriodDetail();
    this.CreateSeparationReasonDetail();
    this.CreateSeparationTasksDetail();
    this.CreateSeparation();
    this.addForm.addControl('rows', this.rows);
  }
  keyPress(event){
    console.log(event, "click");
    const key = event.keyCode;
    console.log(key);
    if (key >= 15 && key <= 64) {
      event.preventDefault();
    }
  }
  keyPressNumbers(event) {
    console.log(event, "click");
    const key = event.keyCode;
    console.log(key, "length");
    // Only Numbers 0-9
    if ((key < 48 || key > 57)) {
      event.preventDefault();
      return false;
    } else {
      return true;
    }
  }
  onRemoveRow(rowIndex: number) {
    this.rows.removeAt(rowIndex);
  }

  createFormGroup(data: any): FormGroup {
    let fieldData = this.loadValues(data.fieldtype);
    let formData = this.fb.group({
      fieldname: data.fieldname,
      fieldtype: data.fieldtype,
      fieldvalueid: data.fieldvalueid,
      fieldvalue: data.fieldvalue,
      mandatory: data.mandatory,
      enable: data.enable,
      fieldvalues: fieldData,
      isTextBox: data.isTextBox == 'true' ? true : false,
      isDropDown: data.isDropDown == 'true' ? true : false,
      isDisabledText: data.isDisabledText == 'true' ? true : false,
      disabledContent: data.disabledContent
    });
    return formData;
  }

  onAddRow() {
    if (this.rows.length < 7) {
      this.rows.push(this.createItemFormGroup());
    }
    else {
      this.uiSweetAlertService.ShowAlert("Maximum of 6 rows can only be added!");
    }
  }

  loadFieldValue(index, val) {
    let value = parseInt(val);
    let items = this.rows.value[index];
    if (value == 1 || value == 2) {
      items.isTextBox = false;
      items.isDropDown = true;
      items.isDisabledText = false;
      items.fieldvalues = this.loadValues(value);
    }
    else if (value == 3 || value == 4) {
      items.isTextBox = true;
      items.isDropDown = false;
      items.isDisabledText = false;
    }
    else if (value > 4) {
      items.isTextBox = false;
      items.isDropDown = false;
      items.isDisabledText = true;
      items.disabledContent = value == 5 ? "Date Picker" : (value == 6) ? "Upto 75 chars" : "File upload";
    }
    this.rows.at(index).patchValue(items);
    console.log(index);
    console.log(val);
  }

  createItemFormGroup(): FormGroup {
    return this.fb.group({
      fieldname: null,
      fieldtype: 0,
      fieldvalueid: 0,
      fieldvalue: null,
      mandatory: false,
      enable: false,
      fieldvalues: new Array<UiMultiselectData>(),
      isTextBox: false,
      isDropDown: true,
      isDisabledText: false,
      disabledContent: ''
    });
  }

  loadValues(type: number) {
    let dropDownValues = new Array<UiMultiselectData>();
    switch (type) {
      case 1:
        let _data = new UiMultiselectData();
        _data.Text = "Upto 25 character";
        _data.Value = 1;
        dropDownValues.push(_data);
        _data = new UiMultiselectData();
        _data.Text = "Email";
        _data.Value = 2;
        dropDownValues.push(_data);
        break;
      case 2:
        let data = new UiMultiselectData();
        data.Text = "No Limit";
        data.Value = 1;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "1 Digit";
        data.Value = 2;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "1 Digit";
        data.Value = 3;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "2 Digit";
        data.Value = 4;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "3 Digit";
        data.Value = 5;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "4 Digit";
        data.Value = 6;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "5 Digit";
        data.Value = 7;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "Percentage";
        data.Value = 8;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "Mobile";
        data.Value = 9;
        dropDownValues.push(data);
        data = new UiMultiselectData();
        data.Text = "Telephone";
        data.Value = 10;
        dropDownValues.push(data);
        break;
    }

    return dropDownValues;
  }

  ngAfterViewInit(): void {
    try {
      this.LoadSeparationDetails();
      this.LoadMultiSelectDropdownList();
      this.LoadUserDefinedUI();
      this.LoadGrid();
    } catch (error) {
      this.HideLoader();
    }
  }

  LoadNoticePeriodDetailGrid() {
    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
        this.NoticePeriodNameDetailList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.NoticePeriodDetailsResp(data);
          },
          (error) => {
            console.log(error);
            this.HideLoader();
          });
      })
    );
  }

  LoadSeparateReasonGrid() {
    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[1].fetchObs.subscribe(x => {
        this.GetSeperationReasonList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.SeperationReasonResp(data);
          },
          (error) => {
            console.log(error);
            this.HideLoader();
          });
      })
    );
  }

  LoadSeparateTaskGrid() {
    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[2].fetchObs.subscribe(x => {
        this.GetSeperationTaskList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.SeperationTaskResp(data);
          },
          (error) => {
            console.log(error);
            this.HideLoader();
          });
      })
    );
  }

  LoadGrid() {
    this.ShowLoader();
    this.LoadNoticePeriodDetailGrid();
    this.LoadSeparateReasonGrid();
    this.LoadSeparateTaskGrid();
    forkJoin([
      this.NoticePeriodNameDetailList(1, this.SeperationConfig.PaginationPageSize, { fieldId: 'NoticePeriodDetailId', direction: 'asc' }),
      this.GetSeperationReasonList(1, this.SeperationReasonConfig.PaginationPageSize, { fieldId: 'SeparationReasonId', direction: 'asc' }),
      this.GetSeperationTaskList(1, this.SeperationTasksConfig.PaginationPageSize, { fieldId: 'SepTasksId', direction: 'asc' }),
    ]).subscribe(
      (data: any) => {
        this.HideLoader();
        this.NoticePeriodDetailsResp(data[0]);
        this.SeperationReasonResp(data[1]);
        this.SeperationTaskResp(data[2]);
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      });
  }

  LoadUserDefinedUI() {
    this.LifeCycleService.GetSeparationUserDefined({ 'UserId': 1 }).subscribe(
      (data: any) => {
        if (data) {
          if (data.Table) {
            this.SeparationForm.patchValue(data.Table[0]);
            //this.SeparationResp(data);
          }
          if (data.Table1) {
            let values = data.Table1;
            values.forEach(element => {
              this.rows.push(this.createFormGroup(element));
            });
            if (this.rows.value && this.rows.value.length && this.rows.value.length > 0) {
              //fieldvalues
              for (let i = 0; i < this.rows.value.length; i++) {
                this.rows.value[i].fieldvalues = this.loadValues(this.rows.value[i].fieldtype);
                this.rows.at(i).patchValue(this.rows.value[i]);
              }
            }
          }
        }
      },
      (error: any) => {
        console.log(error);
      }
    )
  }
  private CreateSeparation() {
    this.SeparationForm = this.fb.group({
      RelievingDateTypeId: 1,
      RetirementAge: 0,
      RetirementDays: 0,
      AllowUpTo: false,
      SeparationDaysUpto: 0,
      AllowManagerResignOnBehalf: false,
      EditExitDate:false,
      EditRecoveryDates:false,
      RaiseTermination:false,
      AutomaticallyTriggerRetirementWorkflow:false,
      UserDefinedField: ''
    });
  }

  private SeparationResp(data: any) {
    if (data.Table) {
      this.ApproveManagerType = data.Table[0].ApproveManagerType;
      this.RelievingDateTypeId = data.Table[0].RelievingDateTypeId;
      this.RetirementAge = data.Table[0].RetirementAge;
      this.RetirementDays = data.Table[0].RetirementDays;
    }
    else {
      this.ApproveManagerType = false;
      this.RelievingDateTypeId = false;
      this.RetirementAge = 0;
      this.RetirementDays = 0;
    }
  }

  private LoadSeparationDetails() {
    let paginationModel: any = {
      UserId: 1
    }
    this.LifeCycleService.GETMasterForSeparationTaskList(paginationModel).subscribe(
      (data: any) => {
        if (data && data != undefined) {
          if (data.Table[0]) {
            this.SeparationFromList = data.Table;
          }
          if (data.Table[1]) {
            this.TaskOwnerList = data.Table1;
          }
          if (data.Table[2]) {
            this.EscalateLevelList = data.Table2;
          }
          if (data.Table[3]) {
            this.DayTypeNameList = data.Table3;
          }
          if (data.Table[4]) {
            this.InterviewTypeNameList = data.Table4;
          }
        }
        console.log(data);
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      }
    );
  }

  private LoadMultiSelectDropdownList() {
    let paginationModel: any = {
      UserId: 1
    }
    this.LifeCycleService.GetConfirmationMasterList(paginationModel).subscribe(
      (data: any) => {
        if (data && data != undefined) {
          if (data.CentreType) {
            this.centerType = data.CentreType;
          }
          if (data.Region) {
            this.region = data.Region;
          }
          if (data.Location) {
            this.location = data.Location;
          }
          if (data.EmployeeType) {
            this.employeeType = data.EmployeeType;
          }
          if (data.Position) {
            this.position = data.Position;
          }
          if (data.CostCentre) {
            this.costCentre = data.CostCentre;
          }
          if (data.Grade) {
            this.grade = data.Grade;
          }
          if (data.Department) {
            this.department = data.Department;
          }
          if (data.Entity) {
            this.entity = data.Entity;
          }
          if (data.OrgUnit) {
            this.orgunit = data.OrgUnit
          }
          if (data.SeparationType) {
            this.separationtypename = data.SeparationType
          }
          if (data.TaskOwner) {
            this.taskownername = data.TaskOwner
          }
          if (data.EscalateLevel) {
            this.escaltelevel = data.EscalateLevel
          }
          if (data.EmployeeGroup) {
            this.employeegroup = data.EmployeeGroup
          }
          if (data.Designation) {
            this.designation = data.Designation
          }
          if (data.DayType) {
            this.daytype = data.DayType
          }
          if (data.InterviewType) {
            this.interviewtype = data.InterviewType
          }
          let _orgData = new Array<UiMultiSelectOptions>();
          let _entity = this.LoadAccordionDropdown(data.Entity, "entity", "Entity Group");
          let _department = this.LoadAccordionDropdown(data.Department, "department", "Department");
          let _orgUnit = this.LoadAccordionDropdown(data.OrgUnit, "orgUnit", "Org Unit");
          _orgData.push(_entity);
          _orgData.push(_orgUnit);
          _orgData.push(_department);
          this.orgEntityDepartment = _orgData;
        console.log(this.orgEntityDepartment,"logdata");
          
        }
        console.log(data);
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      }
    );
  }

  LoadAccordionDropdown(data: Array<UiMultiselectData>, controlId: string, text: string) {
    let dropdownData = new UiMultiSelectOptions();
    dropdownData.ControlId = controlId;
    dropdownData.OptionType = text;
    if (data.length > 0) {
      dropdownData.Data = data;
    }
    return dropdownData;
  }
  onItemSelect1(item: Array<UiMultiSelectOptions>) {
    console.log(item);
    this.orgEntityDepartment = item;
    console.log(this.orgEntityDepartment,"onItemSelect1");

  }

  onItemSelect(item: UiMultiSelectOutPut, data: string) {
    this.updateText(data, item.selectedValues);
    console.log(item.selectedValues);
  }

  ShowFormData(event) {
    this.showForm = event.checked;
    this.separationtypeId = 0;
  }

  updateText(data: string, item: string) {
    if (data == "centre") {
      this.centreTypeSelectedValues = item;
    }
    else if (data == "region") {
      this.regionSelectedValues = item;
    }
    else if (data == "location") {
      this.locationSelectedValues = item;
    }
    else if (data == "employeeType") {
      this.employeeTypeSelectedValues = item;
    }
    else if (data == "position") {
      this.positionSelectedValues = item;
    }
    else if (data == "costCentre") {
      this.costCentreSelectedValues = item;
    }
    else if (data == "grade") {
      this.gradeSelectedValues = item;
    }
    else if (data == "employeegroup") {
      this.employeegroupSelectedValues = item;
    }
    else if (data == "entity") {
      this.entitySelectedValues = item;
    }
    else if (data == "designation") {
      this.designationSelectedValues = item;
    }
    else if (data == "orgunit") {
      this.orgunitSelectedValues = item;
    }
    else if (data == "department") {
      this.departmentSelectedValues = item;
    }
  }

  onSelectAll(items: Array<UiMultiselectData>, data: string) {
    //this.updateText(data,"");
  }

  private NoticePeriodNameDetailList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      Start: startRow,
      StartRow: startRow,
      EndRow: endRow,
      NoofData: 10,
      ActionId: 1,
      NoticePeriodDetailId: 5,
      SearchText: searchText || ''
    }
    this.ShowLoader();
    return this.LifeCycleService.GetNoticePeriodDetails(paginationModel);
  }

  private NoticePeriodDetailsResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.NoticePeriodDetailArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.NoticePeriodDetailArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }

  private SeperationReasonResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.SeperationReasonArrayList = [];
      this.uiDataTableComponent.toArray()[1].ConstructRow([], 0);
    }
    else {
      this.SeperationReasonArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[1].ConstructRow(data.Data, data.Total);
    }
  }

  private GetSeperationReasonList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    // debugger;
    let paginationModel: any = {
      UserId: 1,
      SeparationReasonId: 1,
      SeparationTypeId: 1,
      ActionId: 1,
      StartRow: startRow,
      EndRow: endRow
    }
    this.ShowLoader();
    return this.LifeCycleService.GetSeparationReasons(paginationModel);
  }

  private SeperationTaskResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.SeperationTaskArrayList = [];
      this.uiDataTableComponent.toArray()[2].ConstructRow([], 0);
    }
    else {
      this.SeperationTaskArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[2].ConstructRow(data.Data, data.Total);
    }
  }

  private GetSeperationTaskList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      SeparationTaskId: 0,
      Start: startRow,
      NoofData: 10,
      StartRow: startRow,
      EndRow: endRow,
      ActionId: 1
    }
    this.ShowLoader();
    return this.LifeCycleService.GetSeparationTask(paginationModel);
  }

  //Edit Notice Period Details

  public OnNoticePeriodDetailsOp() {
    this.ShowLoader();
    let noticeperiodtype: Observable<any>;
    this.NoticePeriodDetailForm.value.ActionId = this.isAddMode == true ? 1 : 2;
    this.NoticePeriodDetailForm.value.CentreType = this.centreTypeSelectedValues;
    this.NoticePeriodDetailForm.value.Region = this.regionSelectedValues;
    this.NoticePeriodDetailForm.value.Location = this.locationSelectedValues;
    this.NoticePeriodDetailForm.value.EmployeeType = this.employeeTypeSelectedValues;
    this.NoticePeriodDetailForm.value.Position = this.positionSelectedValues;
    this.NoticePeriodDetailForm.value.CostCentre = this.costCentreSelectedValues;
    this.NoticePeriodDetailForm.value.Grade = this.gradeSelectedValues;
    this.NoticePeriodDetailForm.value.OrgUnit = this.getSelectedValues("Org Unit");
    this.NoticePeriodDetailForm.value.Entity = this.getSelectedValues("Entity Group");
    this.NoticePeriodDetailForm.value.Department = this.getSelectedValues("Department");

    noticeperiodtype = this.LifeCycleService.AddNoticePeriodDetails(this.NoticePeriodDetailForm.value);

    noticeperiodtype.subscribe(
      (data: any) => {
        this.HideLoader();
        this.LoadGrid();

        this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
        this.CloseModelPopup('#addNoticePeriod');
      },
      (error) => {
        this.HideLoader();
      }
    )
  }

  public OnSeparationReasonOp() {
    // debugger;
    this.ShowLoader();
    let separationreasontype: Observable<any>;
    this.SeperationReasonForm.value.ActionId = this.isAddMode == true ? 1 : 2;

    separationreasontype = this.LifeCycleService.AddModifySeparationReasonDetails(this.SeperationReasonForm.value);

    separationreasontype.subscribe(
      (data: any) => {
        this.CloseModelPopup('#addSeparationReason');
        this.HideLoader();
        this.LoadGrid();
        this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
      },
      (error) => {
        this.HideLoader();
      }
    )

  }
  public OnSeparationTaskOp() {
    this.ShowLoader();
    let separationtasktype: Observable<any>;
    this.SeperationTaskForm.value.ActionId = this.isAddMode == true ? 1 : 2;
    this.SeperationTaskForm.value.EmployeeGroupId = this.employeegroupSelectedValues;
    this.SeperationTaskForm.value.Entity = this.entitySelectedValues;
    this.SeperationTaskForm.value.OrgUnit = this.orgunitSelectedValues;
    this.SeperationTaskForm.value.Department = this.departmentSelectedValues;
    this.SeperationTaskForm.value.DesignationTypeId = this.designationSelectedValues;
    this.SeperationTaskForm.value.Location = this.locationSelectedValues;
    this.SeperationTaskForm.value.EmployeeType = this.employeeTypeSelectedValues;
    this.SeperationTaskForm.value.Grade = this.gradeSelectedValues;

    separationtasktype = this.LifeCycleService.AddModifySeparationTaskDetails(this.SeperationTaskForm.value);

    separationtasktype.subscribe(
      (data: any) => {
        this.CloseModelPopup('#addSeparationTasks');
        this.HideLoader();
        this.LoadGrid();
        this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
      },
      (error) => {
        this.HideLoader();
      }
    )
  }

  private NoticePeriodDetail(isAddModel: boolean, index) {
    // debugger;
    this.CreateNoticePeriodDetail();
    if (isAddModel) {
      this.NoticePeriodDetailForm.reset();
      this.popupHeading = "Add Notice Period Details";
    }
    else {
      this.popupHeading = "Edit Notice Period Details";
      let item = this.NoticePeriodDetailArrayList[index];

      if (item && item.CentreType) {
        this.centreTypeSelectedValues = item.CentreType;
      }
      if (item && item.Region) {
        this.regionSelectedValues = item.Region;
      }
      if (item && item.Location) {
        this.locationSelectedValues = item.Location;
      }
      if (item && item.Grade) {
        this.gradeSelectedValues = item.Grade;
      }
      if (item && item.EmployeeType) {
        this.employeeTypeSelectedValues = item.EmployeeType;
      }
      if (item && item.CostCentre) {
        this.costCentreSelectedValues = item.CostCentre;
      }
      if (item && item.Position) {
        this.positionSelectedValues = item.Position;
      }
      let entityData = item.Entity;
      let departmentData = item.Department;
      let orgData = item.OrgUnit;
      if (!entityData) {
        entityData = '';
      }
      if (!departmentData) {
        departmentData = '';
      }
      if (!orgData) {
        orgData = '';
      }
      this.donotUse = item.IsDoNotUse;
      this.setSelectedValues(entityData, 'Entity Group');
      this.setSelectedValues(orgData, 'Org Unit');
      this.setSelectedValues(departmentData, 'Department');
      this.selectedOrgEntityDepartment = this.orgEntityDepartment;
      console.log(this.orgEntityDepartment,"this.orgEntityDepartment");
      
      this.NoticePeriodDetailForm.patchValue(this.NoticePeriodDetailArrayList[index]);

    }
  }

  private LoadDropdownData(dropdownData: Array<UiMultiselectData>, data: any) {
    let dropdownValues = data.replace('[', '').replace(']', '').split(',');
    dropdownData.forEach(item => {
      item.isCheckBoxSelected = false;
    });
    dropdownValues.forEach(item => {
      dropdownData.forEach(items => {
        let id = parseInt(item);
        if (items.Value === id) {
          items.isCheckBoxSelected = true;
        }
      });
    });
    return dropdownData;
  }
  private getSelectedValues(option: string) {
    console.log(this.orgEntityDepartment,"getselected");
    let selectedOrgValues = this.orgEntityDepartment.filter(item => item.OptionType == option);
    let selectedArray = [];
    selectedOrgValues.forEach(items => {
      items.Data.forEach(item => {
        if (item.isCheckBoxSelected) {
          selectedArray.push(item.Value);
        }
      });
    });
    return selectedArray && selectedArray.length > 0 ? selectedArray.join(",") : "";
  }
  private setSelectedValues(data: string, option: string) {
    if (data && data != '') {
      let arrayData = data.split(',');
    console.log(this.orgEntityDepartment,"setselect");
      this.orgEntityDepartment.forEach(item => {
        if (item.OptionType == option) {
          item.Data.forEach(items => {
            arrayData.forEach(a => {
              let intValue = parseInt(a);
              if (intValue == items.Value) {
                items.isCheckBoxSelected = true;
              }
              else {
                items.isCheckBoxSelected = false;
              }
            })
          });
        }
      });
    }
    else {
      this.orgEntityDepartment.forEach(item => {
    console.log(this.orgEntityDepartment,"dropdown1");
        if (item.OptionType == option) {
          item.Data.forEach(items => {
            items.isCheckBoxSelected = false;
          });
        }
      });
    }

  }

  handleChange(e) {
    if (e == 'Y') {
      this.showDiv = true;
    }
    else {
      this.showDiv = false;
    }
  }

  private SeparationReasonDetail(isAddModel: boolean, index) {

    this.CreateSeparationReasonDetail();
    if (isAddModel) {
      this.SeperationReasonForm.reset();
      this.popupHeading = "Add Separation Reason Details";
    }
    else {
      this.popupHeading = "Edit Separation Reason Details";
      let item = this.SeperationReasonArrayList[index];
      this.SeperationReasonForm.patchValue(this.SeperationReasonArrayList[index]);

    }
  }
  private SeparationTasksDetail(isAddModel: boolean, index) {
    this.CreateSeparationTasksDetail();
    if (isAddModel) {
      this.SeperationTaskForm.reset();
      this.popupHeading = "Add Separation Tasks";
    }
    else {
      this.popupHeading = "Edit Separation Tasks";

      let item = this.SeperationTaskArrayList[index];
      if (item && item.EmployeeGroup) {
        this.employeegroupSelectedValues = item.EmployeeGroup
      }
      if (item && item.Entity) {
        this.entitySelectedValues = item.Entity
      }
      if (item && item.OrgUnit) {
        this.orgunitSelectedValues = item.OrgUnit
      }
      if (item && item.Department) {
        this.departmentSelectedValues = item.Department
      }
      if (item && item.Designation) {
        this.designationSelectedValues = item.Designation
      }
      if (item && item.Location) {
        this.locationSelectedValues = item.Location;
      }
      if (item && item.EmployeeType) {
        this.employeeTypeSelectedValues = item.EmployeeType
      }
      if (item && item.Grade) {
        this.gradeSelectedValues = item.Grade
      }
      this.SeperationTaskForm.patchValue(this.SeperationTaskArrayList[index]);
    }
  }

  private CreateNoticePeriodDetail() {
    if (isNullOrUndefined(this.NoticePeriodDetailForm)) {
      this.NoticePeriodDetailForm = this.fb.group({
        NoticePeriodName: ['', [Validators.required]],
        NoticePeriodDays: ['', [Validators.required]],
        ConfirmationDays: [''],
        NoticePeriodDetailId: [''],
        IsDoNotUse: [false],
        Id: []
      });
    }
  }
  private CreateSeparationReasonDetail() {
    if (isNullOrUndefined(this.SeperationReasonForm)) {
      this.SeperationReasonForm = this.fb.group({
        SeparationReason: ['', [Validators.required]],
        SeparationTypeId: 0,
        SeparationReasonId: 0,
        SeparationTypeName: [''],
        IsDoNotUse: [false],
        Id: []
      });
    }
  }
  private CreateSeparationTasksDetail() {
    if (isNullOrUndefined(this.SeperationTaskForm)) {
      this.SeperationTaskForm = this.fb.group({
        SeparationTask: ['', [Validators.required]],
        InitiateTaskDays: ['', [Validators.required]],
        EscalatePostDay: ['', [Validators.required]],
        IsDoNotUse: [false],
        SeparationTaskId: 0,
        DayTypeId: 0,
        SeparationTypeId: 0,
        SeparationTypeName: '',
        TaskOwnerName: '',
        EscalateLevel: '',
        DayTypeName: '',
        InterviewTypeName: '',
        TaskOwnerId: 0,
        EscalateLevelId: 0,
        InterviewTypeId: 0,
        EscalateDayTypeId: 0,
        EscalateTypeId: 0,
        EmployeeGroup: '',
        DesignationType: '',
        Id: []
      });
    }
  }



  private SetupPopupData(popupId: string, index: any, isAddMode: boolean) {
    switch (popupId) {
      case "#addNoticePeriod":
        this.NoticePeriodDetail(isAddMode, index);
        break;
      case "#addSeparationReason":
        this.SeparationReasonDetail(isAddMode, index);
        break;
      case "#addSeparationTasks":
        this.SeparationTasksDetail(isAddMode, index);
        break;
      default:
        break;
    }
  }

  private clearNoticePeriodDetails() {
    // console.log(this.orgEntityDepartment,"dropdown");
    this.orgEntityDepartment.forEach(item => {
      item.Data.forEach(items => {
        items.isCheckBoxSelected = false;
      });
    });
    this.centreTypeSelectedValues = "";
  }

  public OnOpenPopup(index: any, popupType: string) {
    if (popupType == "Add") {
      this.NoticePeriodDetailForm.reset();
      this.isAddMode = true;
      this.popupHeading = "Add Notice Period Details";
    }
    else if (popupType == "Edit") {
      this.clearNoticePeriodDetails()
      this.isAddMode = false;
      this.SetupPopupData("#addNoticePeriod", index, false);
    }
    this.OpenModelPopup("#addNoticePeriod");
    console.log(index);
  }

  public OnOpenSeparationReasonPopup(index: any, popupType: string) {
    if (popupType == "Add") {
      this.SeperationReasonForm.reset();
      this.isAddMode = true;
      this.SeperationReasonForm.get('SeparationTypeId').setValue(0);
      this.popupHeading = "Add Separation Reason";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      this.SetupPopupData("#addSeparationReason", index, false);
    }
    this.OpenModelPopup("#addSeparationReason");
  }

  public OnOpenSeparationTaskPopup(index: any, popupType: string) {
    if (popupType == "Add") {
      this.SeperationReasonForm.reset();
      this.isAddMode = true;
      this.popupHeading = "Add Separation Tasks";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      this.SetupPopupData("#addSeparationTasks", index, false);
    }
    this.OpenModelPopup("#addSeparationTasks");
  }

  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }
  onOptionsSelected(event) {
    const value = event.target.value;
    if (value == 0) {
      this.isSeparationtypenameRequiredError = true;
    }
    else {
      this.isSeparationtypenameRequiredError = false;
    }
  }

  public AllowUptChange(event) {
    if (event.checked) {
      //this.SeparationForm.get('AllowDaysTo').setValue('true');
      this.SeparationForm.get('SeparationDaysUpto').setValue(0);
    }
    else {
      //this.SeparationForm.get('AllowDaysTo').setValue('false');
      this.SeparationForm.get('SeparationDaysUpto').setValue(null);
    }
  }

  public onSeparationSave() {
    this.ShowLoader();
    let separationtype: Observable<any>;
    let rowData = [];
    //  fieldname: null,
    //   fieldtype: 0,
    //   fieldvalueid: 0,
    //   fieldvalue: null,
    //   mandatory: false,
    //   enable: false,
    //   fieldvalues: new Array<UiMultiselectData>(),
    //   isTextBox: false,
    //   isDropDown: true,
    //   isDisabledText: false,
    //   disabledContent: ''
    for (let i = 0; i < this.rows.value.length; i++) {
      let data = this.rows.value[i];
      let item = {
        "FieldName": data.fieldname,
        "FieldType": data.fieldtype,
        "FieldValueId": data.fieldvalueid,
        "IsMandatory": data.mandatory,
        "IsActive": data.enable,
        "FieldValue": null
      };
      if(data.isDisabledText){
        item.FieldValue = data.disabledContent;
      }
      if(data.isTextBox){
        item.FieldValue  = data.fieldvalue
      }
      rowData.push(item);
    }
    this.SeparationForm.value.UserDefinedField = JSON.stringify(rowData);
    separationtype = this.LifeCycleService.SaveSeparationDetail(this.SeparationForm.value);
    separationtype.subscribe(
      (data: any) => {
        this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
        this.HideLoader();
      },
      (error) => {
        this.HideLoader();
      }
    );
  }

}
