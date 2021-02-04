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
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';

@Component({
  selector: 'app-seperation',
  templateUrl: './seperation.component.html',
  styleUrls: ['./seperation.component.css']
})
export class SeperationComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public NoticePeriodDetailArrayList = new Array<any>();
  public SeperationReasonArrayList = new Array<any>();
  public SeperationTaskArrayList = new Array<any>();

  //public CentrePlaceHolderText: string = "None Selected";
  //public regionPlaceHolderText: string = "None Selected";
  //public locationPlaceHolderText: string = "None Selected";
  //public employeeTypePlaceHolderText: string = "None Selected";
  //public positionPlaceHolderText: string = "None Selected";
  //public costCentrePlaceHolderText: string = "None Selected";
  //public gradePlaceHolderText: string = "None Selected";
  //public entityPlaceHolderText: string = "None Selected";
  //public orgunitPlaceHolderText: string = "None Selected";
  //public employeegroupPlaceHolderText: string = "None Selected";
  //public departmentPlaceHolderText: string = "None Selected";
  //public designationPlaceHolderText: string = "None Selected";

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
  public orgEntityDepartment = new Array<UiMultiSelectOptions>();
  public selectedOrgEntityDepartment = new Array<UiMultiSelectOptions>();

  public SeparationFromList = new Array<any>();
  public TaskOwnerList = new Array<any>();
  public EscalateLevelList = new Array<any>();
  public DayTypeNameList = new Array<any>();
  public InterviewTypeNameList = new Array<any>();

  public NoticePeriodDetailForm: FormGroup;
  public SeperationReasonForm: FormGroup;
  public SeperationTaskForm: FormGroup;
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
  public donotUse: boolean = false;
  public orgPlaceHolder: string = 'Entity/Org Unit/Department';

  public SeperationConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'NoticePeriodDetailId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'NoticePeriodName', fieldName: 'Notice Period Name', columnClass: 'text-nowrap width100' },
      { fieldId: 'NoticePeriodDays', fieldName: 'Notice Period Days', columnClass: 'text-nowrap width100' },
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
  }


  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateNoticePeriodDetail();
    this.CreateSeparationReasonDetail();
    this.CreateSeparationTasksDetail();
  }

  ngAfterViewInit(): void {

    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
        this.NoticePeriodNameDetailList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.NoticePeriodDetailsResp(data);
            this.HideLoader();
          },
          (error) => {
            console.log(error);
            this.HideLoader();
          });
      })
    );
    this.LoadSeparationDetails();
    this.LoadMultiSelectDropdownList();
    // Seperation Reason
    this.uiDataTableComponent.toArray()[1].fetchObs.subscribe(x => {
      this.GetSeperationReasonList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        (data: any) => {
          this.SeperationReasonResp(data);
          this.HideLoader();
        },
        (error) => {
          console.log(error);
          this.HideLoader();
        });
    });
    // Seperation Task
    this.uiDataTableComponent.toArray()[2].fetchObs.subscribe(x => {
      this.GetSeperationTaskList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        (data: any) => {
          this.SeperationTaskResp(data);
          this.HideLoader();
        },
        (error) => {
          console.log(error);
          this.HideLoader();
        });
    });

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


  private LoadSeparationDetails() {
    let paginationModel: any = {
      UserId: 1
    }
    this.ShowLoader();
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
        this.HideLoader();
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      }
    );
  }

  private LoadMultiSelectDropdownList() {
    debugger;
    let paginationModel: any = {
      UserId: 1
    }
    this.ShowLoader();
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
          let _orgData = new Array<UiMultiSelectOptions>();
          let _entity = this.LoadAccordionDropdown(data.Entity, "entity", "Entity Group");
          let _department = this.LoadAccordionDropdown(data.Department, "department", "Department");
          let _orgUnit = this.LoadAccordionDropdown(data.OrgUnit, "orgUnit", "Org Unit");
          _orgData.push(_entity);
          _orgData.push(_orgUnit);
          _orgData.push(_department);
          this.orgEntityDepartment = _orgData;
        }
        console.log(data);
        this.HideLoader();
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
  }

  onItemSelect(item: UiMultiSelectOutPut, data: string) {
    this.updateText(data, item.selectedValues);
    console.log(item.selectedValues);
  }

  ShowFormData(event) {
    this.showForm = event.checked;

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
  }

  onSelectAll(items: Array<UiMultiselectData>, data: string) {
    //this.updateText(data,"");
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
    if (isNullOrUndefined(data)) {
      this.NoticePeriodDetailArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.NoticePeriodNameDetailList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }

  private SeperationReasonResp(data: any) {
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
    let paginationModel: any = {
      UserId: 1,
      SeparationReasonId: 1,
      ActionId: 2,
      StartRow: startRow,
      EndRow: endRow
    }
    return this.LifeCycleService.GetSeparationReasons(paginationModel);
  }

  private SeperationTaskResp(data: any) {
    if (isNullOrUndefined(data)) {
      this.SeperationTaskArrayList = [];
      this.uiDataTableComponent.toArray()[2].ConstructRow([], 0);
    }
    else {
      this.GetSeperationTaskList = data.Data || [];
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
        this.NoticePeriodNameDetailList(1, this.SeperationConfig.PaginationPageSize, { fieldId: 'NoticePeriodDetailId', direction: 'asc' }).subscribe(
          (data: any) => {
          },
          (error) => {
            this.HideLoader();
          })
      },
      (error) => {
        this.HideLoader();
      }
    )
    this.CloseModelPopup('#addNoticePeriod');
    this.HideLoader();
  }

  public OnSeparationReasonOp() {
    alert("hi")
    this.ShowLoader();
    let separationreasontype: Observable<any>;
    if (this.isAddMode) {
      separationreasontype = this.LifeCycleService.AddModifySeparationReasonDetails(this.SeperationReasonForm.value);
    }
    else {
      separationreasontype = this.LifeCycleService.AddModifySeparationReasonDetails(this.SeperationReasonForm.value);
    }
    separationreasontype.subscribe(
      (data: any) => {
        this.CloseModelPopup('#addSeparationReason');
        this.GetSeperationReasonList(1, this.SeperationReasonConfig.PaginationPageSize, { fieldId: 'SeparationReasonId', direction: 'asc' }).subscribe(
          (data: any) => {
            // this.HideLoader();
            // this.HandleCourseTypeResp(data);
            this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
        // this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);

          },
          (error) => {
            this.HideLoader();
          })
      },
      (error) => {
        this.HideLoader();
        //this.validationMessage = ValidationBuilder.build(error);
        ///this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }
  public OnSeparationTaskOp() {
    this.ShowLoader();
    let separationtasktype: Observable<any>;
    if (this.isAddMode) {
      separationtasktype = this.LifeCycleService.AddModifySeparationTaskDetails(this.SeperationTaskForm.value);
    }
    else {
      separationtasktype = this.LifeCycleService.AddModifySeparationTaskDetails(this.SeperationTaskForm.value);
    }
    separationtasktype.subscribe(
      (data: any) => {
        debugger;
        this.CloseModelPopup('#addSeparationTasks');
        this.GetSeperationTaskList(1, this.SeperationTasksConfig.PaginationPageSize, { fieldId: 'SeparationTaskId', direction: 'asc' }).subscribe(
          (data: any) => {
            // this.HideLoader();
            // this.HandleCourseTypeResp(data);
            this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
        // this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);

          },
          (error) => {
            this.HideLoader();
          })
      },
      (error) => {
        this.HideLoader();
        //this.validationMessage = ValidationBuilder.build(error);
        ///this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  private NoticePeriodDetail(isAddModel: boolean, index) {
    debugger;
    this.CreateNoticePeriodDetail();
    if (isAddModel) {
      this.NoticePeriodDetailForm.reset();
      this.popupHeading = "Add Notice Period Details";
    }
    else {
      this.popupHeading = "Edit Notice Period Details";
      let item = this.NoticePeriodNameDetailList[index];

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

      this.NoticePeriodDetailForm.patchValue(this.NoticePeriodNameDetailList[index]);

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
      this.SeperationReasonForm.patchValue(this.SeperationReasonArrayList[index]);

    }
  }
  private SeparationTasksDetail(isAddModel: boolean, index) {
    this.CreateSeparationTasksDetail();
    if (isAddModel) {
      this.SeperationTaskForm.reset();
      this.popupHeading = "Add Separation Task Details";
    }
    else {
      this.popupHeading = "Edit Separation Task Details";

      let item = this.GetSeperationTaskList[index];
      //if (item && item.EmployeeGroup) {
      //  this.employeegroup = this.LoadDropdownData(this.employeegroup, item.EmployeeGroup);
      //  this.updateText('employeegroup');
      //}
      //if (item && item.Entity) {
      //  this.entity = this.LoadDropdownData(this.entity, item.Entity);
      //  this.updateText('entity');
      //}
      //if (item && item.OrgUnit) {
      //  this.orgunit = this.LoadDropdownData(this.orgunit, item.OrgUnit);
      //  this.updateText('orgunit');
      //}
      //if (item && item.Department) {
      //  this.department = this.LoadDropdownData(this.department, item.Department);
      //  this.updateText('Department');
      //}
      //if (item && item.Designation) {
      //  this.designation = this.LoadDropdownData(this.designation, item.Designation);
      //  this.updateText('designation');
      //}
      //if (item && item.Location) {
      //  this.location = this.LoadDropdownData(this.location, item.Location);
      //  this.updateText('location');
      //}
      //if (item && item.EmployeeType) {
      //  this.employeeType = this.LoadDropdownData(this.employeeType, item.EmployeeType);
      //  this.updateText('employeeType');
      //}
      //if (item && item.Grade) {
      //  this.grade = this.LoadDropdownData(this.grade, item.Grade);
      //  this.updateText('grade');
      //}
      this.SeperationTaskForm.patchValue(this.GetSeperationTaskList[index]);
    }
  }

  private CreateNoticePeriodDetail() {
   
    if (isNullOrUndefined(this.NoticePeriodDetailForm)) {
      this.NoticePeriodDetailForm = this.fb.group({
        NoticePeriodName: ['', [Validators.required]],
        NoticePeriodDays: ['', [Validators.required]],
        ConfirmationDays: [''],
        NoticePeriodDetailId: [''],
        //IsDoNotUse: [false],
        Id: []
      });
    }
  }
  private CreateSeparationReasonDetail() {
    if (isNullOrUndefined(this.SeperationReasonForm)) {
      this.SeperationReasonForm = this.fb.group({
        SeparationReason: ['', [Validators.required]],
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
  public OnOpenPopup(index: any, popupType: string) {
    //this.SetupPopupData("#addNoticePeriod", index, false);
    //this.OpenModelPopup("#addNoticePeriod");
    //console.log(index);

   // this.CreateProbationDetail();
    if (popupType == "Add") {
      this.NoticePeriodDetailForm.reset();
      this.isAddMode = true;
      this.popupHeading = "Add Notice Period Details";
    }
    else if (popupType == "Edit") {
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
      this.popupHeading = "Add Notice Period Details";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      this.SetupPopupData("#addSeparationReason", index, false);
    }   
    this.OpenModelPopup("#addSeparationReason");
  }

  public OnOpenSeparationTaskPopup(index: any, popupType: string) {
    this.SetupPopupData("#addSeparationTasks", index, false);
    this.OpenModelPopup("#addSeparationTasks");
  }
  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }

  //onItemSelect(item: UiMultiselectData, data: string) {
  //  this.updateText(data);
  //}
  //updateText(data: string) {
  //  if (data == "centre") {
  //    this.CentrePlaceHolderText = this.updatePlaceHolder(this.centerType);
  //  }
  //  else if (data == "region") {
  //    this.regionPlaceHolderText = this.updatePlaceHolder(this.region);
  //  }
  //  else if (data == "location") {
  //    this.locationPlaceHolderText = this.updatePlaceHolder(this.location);
  //  }
  //  else if (data == "employeeType") {
  //    this.employeeTypePlaceHolderText = this.updatePlaceHolder(this.employeeType);
  //  }
  //  else if (data == "position") {
  //    this.positionPlaceHolderText = this.updatePlaceHolder(this.position);
  //  }
  //  else if (data == "costCentre") {
  //    this.costCentrePlaceHolderText = this.updatePlaceHolder(this.costCentre);
  //  }
  //  else if (data == "grade") {
  //    this.gradePlaceHolderText = this.updatePlaceHolder(this.grade);
  //  }
  //  else if (data == "employeegroup") {
  //    this.employeegroupPlaceHolderText = this.updatePlaceHolder(this.employeegroup);
  //  }
  //  else if (data == "entity") {
  //    this.entityPlaceHolderText = this.updatePlaceHolder(this.entity);
  //  }
  //  else if (data == "orgunit") {
  //    this.orgunitPlaceHolderText = this.updatePlaceHolder(this.orgunit);
  //  }
  //  else if (data == "department") {
  //    this.departmentPlaceHolderText = this.updatePlaceHolder(this.department);
  //  }
  //  else if (data == "designation") {
  //    this.designationPlaceHolderText = this.updatePlaceHolder(this.designation);
  //  }
  //}

  //onSelectAll(items: Array<UiMultiselectData>, data: string) {
  //  this.updateText(data);
  //}

  private updatePlaceHolder(data: Array<UiMultiselectData>) {
    let arrayData = [];
    let selectedCentreType = data.filter(item => item.isCheckBoxSelected == true);
    if (selectedCentreType.length > 0) {
      if (selectedCentreType.length <= 3) {
        selectedCentreType.forEach(item => {
          arrayData.push(item.Text);
        });
        return arrayData.join(',');
      }
      else if (selectedCentreType.length > 3) {
        return "Selected Item(" + selectedCentreType.length + ")";
      }
      else {
        return "None Selected";
      }
    }
    else {
      return "None Selected";
    }
  }

}
