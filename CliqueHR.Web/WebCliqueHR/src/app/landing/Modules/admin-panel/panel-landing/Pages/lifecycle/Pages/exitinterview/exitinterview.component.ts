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
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';


@Component({
  selector: 'app-exitinterview',
  templateUrl: './exitinterview.component.html',
  styleUrls: ['./exitinterview.component.css']
})
export class ExitinterviewComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public employeegroupSelectedValues: string = "";
  public designationSelectedValues: string = "";
  public locationSelectedValues: string = "";
  public employeeTypeSelectedValues: string = "";
  public gradeSelectedValues: string = "";

  public ExitInterviewArrayList = new Array<any>();
  public employeegroup = new Array<UiMultiselectData>();
  public assessmentForm = new Array<UiMultiselectData>();
  public orgunit = new Array<UiMultiselectData>();
  public designation = new Array<UiMultiselectData>();
  public location = new Array<UiMultiselectData>();
  public employeetype = new Array<UiMultiselectData>();
  public grade = new Array<UiMultiselectData>();
  public orgEntityDepartment = new Array<UiMultiSelectOptions>();
  public selectedOrgEntityDepartment = new Array<UiMultiSelectOptions>();
  public isAssessmentRequiredError: boolean = false;
  public orgPlaceHolder: string = 'Entity/Org Unit/Department';
  public filter = new UiMultiselectData();
  public separationtypename = new Array<UiMultiselectData>();
  public daytype = new Array<UiMultiselectData>();
  public isSeparationtypenameRequiredError: boolean = false;
  public isdaytypeRequiredError: boolean = false;
  public separationtypeId: number = 0;
  public dropDown1 = new Array<UiMultiselectData>();
  public validationMassages = ValidationMessage.data;
  public popupHeading: string = '';

  public ExitInterviewForm: FormGroup;
  public isAddMode: boolean = true;

  public ExtinterviewConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'ExitInterviewId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'PolicyName', fieldName: 'Exit Interview Policy Name', columnClass: 'text-nowrap width100' },
      { fieldId: 'Form', fieldName: 'Form Name', columnClass: 'text-nowrap width100' },
      { fieldId: 'WorkGroup', fieldName: 'Applicable To', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { direction: 'asc', fieldId: 'ExitInterviewId' },
    UniqueRowCol: 'ExitInterviewId',
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
    super(Components.ExitinterviewComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.filter.Text = '';
    this.CreateExitInterviewDetail();
  }
  keyPress(event){
    console.log(event, "click");
    const key = event.keyCode;
    console.log(key);
    if (key >= 15 && key <= 64) {
      event.preventDefault();
    }
  }
  LoadDropDown(value) {
    this.dropDown1 = [];
    let values = new UiMultiselectData();
    if (value == 1) {
      values.Text = "Relieving Date";
      values.Value = 1;
      this.dropDown1.push(values);
      this.ExitInterviewForm.get('ExitInterviewTypeId').setValue(1);
    }
    else if (value == 2) {
      values = new UiMultiselectData();
      values.Text = "Select...";
      values.Value = 0;
      this.dropDown1.push(values);
      values = new UiMultiselectData();
      values.Text = "Resignation date";
      values.Value = 1;
      this.dropDown1.push(values);
      values = new UiMultiselectData();
      values.Text = "Resignation approval date";
      values.Value = 2;
      this.dropDown1.push(values);
      values = new UiMultiselectData();
      values.Text = "Relieving date";
      values.Value = 3;
      this.dropDown1.push(values);
      this.ExitInterviewForm.get('ExitInterviewTypeId').setValue(0);
    }
    else if(value == 3){
      values = new UiMultiselectData();
      values.Text = "Select...";
      values.Value = 0;
      this.dropDown1.push(values);
      values = new UiMultiselectData();
      values.Text = "Resignation date";
      values.Value = 1;
      this.dropDown1.push(values);
      values = new UiMultiselectData();
      values.Text = "Resignation approval date";
      values.Value = 2;
      this.dropDown1.push(values);
      this.ExitInterviewForm.get('ExitInterviewTypeId').setValue(0);
    }
  }

  LoadGrid() {
    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
        this.ExitInterviewList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.ExitInterviewResp(data);
            this.HideLoader();
          },
          (error) => {
            console.log(error);
            this.HideLoader();
          });;
      })
    );

    this.LoadMultiSelectDropdownList();

    forkJoin([
      this.ExitInterviewList(1, this.ExtinterviewConfig.PaginationPageSize, { fieldId: 'ExitInterviewId', direction: 'asc' }),
    ]).subscribe(
      (data: any) => {
        this.HideLoader();
        this.ExitInterviewResp(data[0]);
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      });
  }

  ngAfterViewInit(): void {
    this.LoadGrid();
  }

  private LoadMultiSelectDropdownList() {
    let paginationModel: any = {
      UserId: 1
    }
    this.LifeCycleService.GetConfirmationMasterList(paginationModel).subscribe(
      (data: any) => {
        if (data && data != undefined) {
          if (data.EmployeeGroup) {
            this.employeegroup = data.EmployeeGroup;
          }
          if (data.Location) {
            this.location = data.Location;
          }
          if (data.EmployeeType) {
            this.employeetype = data.EmployeeType;
          }
          if (data.Grade) {
            this.grade = data.Grade;
          }
          if (data.Designation) {
            this.designation = data.Designation;
          }
          if (data.AssessmentForm) {
            this.assessmentForm = data.AssessmentForm;
          }
          if (data.SeparationType) {
            this.separationtypename = data.SeparationType
          }
          if (data.DayType) {
            this.daytype = data.DayType
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

  updateText(data: string, item: string) {
    if (data == "employeegroup") {
      this.employeegroupSelectedValues = item;
    }
    else if (data == "designation") {
      this.designationSelectedValues = item;
    }
    else if (data == "location") {
      this.locationSelectedValues = item;
    }
    else if (data == "employeeType") {
      this.employeeTypeSelectedValues = item;
    }
    else if (data == "grade") {
      this.gradeSelectedValues = item;
    }
  }

  onSelectAll(items: Array<UiMultiselectData>, data: string) {
    //this.updateText(data,"");
  }

  private ExitInterviewResp(data: any) {
    //debugger;
    if (isNullOrUndefined(data)) {
      this, this.ExitInterviewArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.ExitInterviewArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }

  private ExitInterviewList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      ExitInterviewId: 1,
      ActionTypeId: 1,
      SeparationTypeId: 1,
      Start: startRow,
      NoofData: 10,
      StartRow: startRow,
      EndRow: endRow
    }
    return this.LifeCycleService.GetExitInterviewList(paginationModel);
  }

  private exitinterviewdetail(isAddModel: boolean, index) {
    this.CreateExitInterviewDetail();
    if (isAddModel) {
      this.ExitInterviewForm.reset();
      this.popupHeading = "Add Policy Details";
    }
    else {
      this.popupHeading = "Edit Policy Details";
      let item = this.ExitInterviewArrayList[index];

      if (item && item.EmployeeGroup) {
        this.employeegroupSelectedValues = item.EmployeeGroup;
      }
      if (item && item.Designation) {
        this.designationSelectedValues = item.Designation;
      }
      if (item && item.Location) {
        this.locationSelectedValues = item.Location;
      }
      if (item && item.EmployeeType) {
        this.employeeTypeSelectedValues = item.EmployeeType;
      }
      if (item && item.Grade) {
        this.gradeSelectedValues = item.Grade;
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
      this.LoadDropDown(item.ExitInterviewDayTypeId);
      this.orgEntityDepartment.forEach(item => {
        item.Data.forEach(items => {
          items.isCheckBoxSelected = false;
        });
      });
      this.selectedOrgEntityDepartment = Object.assign([], this.orgEntityDepartment);
      this.setSelectedValues(entityData, 'Entity Group');
      this.setSelectedValues(orgData, 'Org Unit');
      this.setSelectedValues(departmentData, 'Department');
      this.orgEntityDepartment = Object.assign([], this.selectedOrgEntityDepartment);
      this.ExitInterviewForm.patchValue(this.ExitInterviewArrayList[index]);
    }
  }

  private setSelectedValues(data: string, option: string) {
    let arrayData = data.split(',');
    this.selectedOrgEntityDepartment.forEach(items => {
      if (items.OptionType == option) {
        items.Data.forEach(item => {
          arrayData.forEach(a => {
            let intValue = parseInt(a);
            if (intValue == item.Value) {
              item.isCheckBoxSelected = true;
            }
          });
        });
      }
    });
  }

  onOptionsSelected(event) {
    const value = event.target.value;
    if (value == 0) {
      this.isAssessmentRequiredError = true;
      this.isSeparationtypenameRequiredError = true;
      this.isdaytypeRequiredError = true;
    }
    else {
      this.isAssessmentRequiredError = false;
      this.isSeparationtypenameRequiredError = false;
      this.isdaytypeRequiredError = false;
    }
    if (value == 0) {
      this.dropDown1 = [];
    }
    else {
      this.LoadDropDown(value);
    }
  }

  private CreateExitInterviewDetail() {
    this.clearDropdowns();
    if (isNullOrUndefined(this.ExitInterviewForm)) {
      this.ExitInterviewForm = this.fb.group({
        PolicyName: ['', [Validators.required]],
        AssessmentFormId: 0,
        IsSendMail: false,
        IsExitInterviewMandatory: false,
        ExitInterviewDayTypeId: 0,
        ExitInterviewTypeId:0,
        FormId: 0,
        SeparationTypeId: 0,
        ExitInterviewId: 0,
        ExitInterviewDay: 0,
        IsDoNotUse: [false],
        Id: []
      });
    }
  }

  clearDropdowns() {
    this.designationSelectedValues = '';
    this.employeegroupSelectedValues = '';
    this.locationSelectedValues = '';
    this.employeeTypeSelectedValues = '';
    this.gradeSelectedValues = '';
    this.orgEntityDepartment.forEach(items=>{
      items.Data.forEach(item=>{
        item.isCheckBoxSelected = false;
      })
    })
  }

  private SetupPopupData(popupId: string, index: any, isAddMode: boolean) {
    //this.isAddMode = isAddModel;
    switch (popupId) {
      case "#addPolicy":
        this.exitinterviewdetail(isAddMode, index);
        break;
      default:
        break;
    }
  }

  public OnOpenPopup(index: any, popupType: string) {
    this.CreateExitInterviewDetail();
    if (popupType == "Add") {
      this.ExitInterviewForm.reset();
      this.isAddMode = true;
      this.ExitInterviewForm.get('ExitInterviewDayTypeId').setValue(0);
      this.ExitInterviewForm.get('FormId').setValue(0);
      this.ExitInterviewForm.get('SeparationTypeId').setValue(0);
      this.ExitInterviewForm.get('ExitInterviewDay').setValue(0);
      this.popupHeading = "Add Policy Details";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      this.SetupPopupData("#addPolicy", index, false);
    }
    this.OpenModelPopup("#addPolicy");
    console.log(index);
  }

  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }

  public OnExitInterviewDetailsOp() {
    this.ShowLoader();
    try {
      //if (this.validateData()) {
      let exitinterviewtype: Observable<any>;
      this.ExitInterviewForm.value.ActionTypeId = this.isAddMode == true ? 1 : 2;
      this.ExitInterviewForm.value.EmployeeGroup = this.employeegroupSelectedValues;
      this.ExitInterviewForm.value.Designation = this.designationSelectedValues;
      this.ExitInterviewForm.value.EmployeeType = this.employeeTypeSelectedValues;
      this.ExitInterviewForm.value.Location = this.locationSelectedValues;
      this.ExitInterviewForm.value.Grade = this.gradeSelectedValues;
      this.ExitInterviewForm.value.OrgUnit = this.getSelectedValues("Org Unit");
      this.ExitInterviewForm.value.Entity = this.getSelectedValues("Entity Group");
      this.ExitInterviewForm.value.Department = this.getSelectedValues("Department");

      exitinterviewtype = this.LifeCycleService.AddModifyExitInterviewList(this.ExitInterviewForm.value);

      exitinterviewtype.subscribe(
        (data: any) => {
          this.HideLoader();
          this.LoadGrid();
          this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
          this.OnClosePopup("#addPolicy");
        },
        (error) => {
          if (error.message != "") {
            this.uiSweetAlertService.ShowAlert(error.message);
          }
          this.HideLoader();
        })
    }
    catch (error) {
      this.HideLoader();
      console.log(error);
    }
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
}
