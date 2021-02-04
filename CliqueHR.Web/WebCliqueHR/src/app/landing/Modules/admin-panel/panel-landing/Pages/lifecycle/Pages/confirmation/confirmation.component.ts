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
  selector: 'app-confirmation',
  templateUrl: './confirmation.component.html',
  styleUrls: ['./confirmation.component.css']
})
export class ConfirmationComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public validationMassages = ValidationMessage.data;
  public popupHeading: string = '';

  public ProbationDetailArrayList = new Array<any>();
  public centerType = new Array<UiMultiselectData>();
  public region = new Array<UiMultiselectData>();
  public location = new Array<UiMultiselectData>();
  public employeeType = new Array<UiMultiselectData>();
  public position = new Array<UiMultiselectData>();
  public costCentre = new Array<UiMultiselectData>();
  public grade = new Array<UiMultiselectData>();
  public assessmentForm = new Array<UiMultiselectData>();
  public orgEntityDepartment = new Array<UiMultiSelectOptions>();
  public selectedOrgEntityDepartment = new Array<UiMultiSelectOptions>();

  public ProbationDetailForm: FormGroup;
  public isAddMode: boolean = true;
  public showForm: boolean = false;
  public showDiv: boolean = false;
  public centreTypeSelectedValues: string = '';
  public regionSelectedValues: string = '';
  public locationSelectedValues: string = '';
  public employeeTypeSelectedValues: string = '';
  public positionSelectedValues: string = '';
  public costCentreSelectedValues: string = '';
  public gradeSelectedValues: string = '';
  public assessmentformid: number = 0;
  public assessmentRequired: boolean = false;
  public donotUse: boolean = false;
  public orgPlaceHolder: string = 'Entity/Org Unit/Department';
  public filter = new UiMultiselectData();

  alphaRegex = "/[a-zA-Z0-9]/";
  public ConfirmationConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'ProbationId', fieldName: '', columnClass: 'width40' },
      { fieldId: 'ProbationName', fieldName: 'Probation Name', columnClass: 'text-nowrap width100'  },
      { fieldId: 'Probationperiod', fieldName: 'Probation Period', columnClass: 'text-nowrap width60' },
      { fieldId: ' Confirmation days', fieldName: 'Confirmation days', columnClass: 'text-nowrap width100' },
      { fieldId: ' AssessmentRequired', fieldName: 'AssessmentRequired', columnClass: 'text-nowrap width100' },
      { fieldId: 'WorkGroup', fieldName: 'WorkGroup', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'ProbationId', direction: 'asc' },
    UniqueRowCol: 'ProbationId',
    isEditable: true
  };

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private LifeCycleService: LifeCycleService,
    private uiSweetAlertService: UiSweetAlertService
  ) {
    super(Components.ConfirmationComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.filter.Text = '';
    this.CreateProbationDetail();
  }

  ngAfterViewInit(): void {
    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
        this.ProbationDetailList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.ProbationDetailsResp(data);
            this.HideLoader();
          },
          (error) => {
            this.HideLoader();
            console.log(error);
          });
      })
    );
    this.LoadMultiSelectDropdownList();

    forkJoin([
      this.ProbationDetailList(1, this.ConfirmationConfig.PaginationPageSize, { fieldId: 'ProbationId', direction: 'asc' }),
    ]).subscribe(
      (data: any) => {
        this.HideLoader();
        this.ProbationDetailsResp(data[0]);
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      });
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
          if (data.AssessmentForm) {
            this.assessmentForm = data.AssessmentForm;
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


  private ProbationDetailList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      Start: startRow,
      StartRow: startRow,
      EndRow: endRow,
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction },
      SearchText: searchText || '',
      Action: 1,
      ProbationDetailId: 5,
      NoofData: 10
    }
    this.ShowLoader();
    return this.LifeCycleService.GetProbationDetails(paginationModel);
  }

  private ProbationDetailsResp(data: any) {
    if (isNullOrUndefined(data)) {
      this.ProbationDetailArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.ProbationDetailArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }

  private validateData(){
    let isValid = true;
    if(this.ProbationDetailForm.invalid){
       isValid = false;
       this.triggerValidation(this.ProbationDetailForm);
    }
    return isValid;
  }

  public OnProbationDetailsOp() {
    this.ShowLoader();
    try
    {
      if (this.validateData()) {
        let probationtype: Observable<any>;
        this.ProbationDetailForm.value.Action = this.isAddMode == true ? 1 : 2;
        this.ProbationDetailForm.value.CentreType = this.centreTypeSelectedValues;
        this.ProbationDetailForm.value.Region = this.regionSelectedValues;
        this.ProbationDetailForm.value.Location = this.locationSelectedValues;
        this.ProbationDetailForm.value.EmployeeType = this.employeeTypeSelectedValues;
        this.ProbationDetailForm.value.Position = this.positionSelectedValues;
        this.ProbationDetailForm.value.CostCentre = this.costCentreSelectedValues;
        this.ProbationDetailForm.value.Grade = this.gradeSelectedValues;
        this.ProbationDetailForm.value.AssessmentFormId = this.assessmentformid;
        this.ProbationDetailForm.value.OrgUnit = this.getSelectedValues("Org Unit");
        this.ProbationDetailForm.value.Entity = this.getSelectedValues("Entity Group");
        this.ProbationDetailForm.value.Department = this.getSelectedValues("Department");
        // this.ProbationDetailForm.value.IsDoNotUse = this.donotUse;
        if (this.assessmentformid == 0) {
          console.log(this.assessmentformid,"this.assessmentformid ==0 " )
          this.ProbationDetailForm.value.AssessmentRequired = false;
        }
        else {
          console.log(this.assessmentformid,"this.assessmentformid ==else " )
          this.ProbationDetailForm.value.AssessmentRequired = true;
        }
        probationtype = this.LifeCycleService.AddModifyProbation(this.ProbationDetailForm.value);

        probationtype.subscribe(
          (data: any) => {
            this.ProbationDetailList(1, this.ConfirmationConfig.PaginationPageSize, { fieldId: 'ProbationDetailId', direction: 'asc' }).subscribe(
              (data: any) => {
                this.HideLoader();
                this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
              },
              (error) => {
                this.HideLoader();
              }
            )
          },
          (error) => {
            if (error.message != "") {
              this.uiSweetAlertService.ShowAlert(error.message);
            }
            this.HideLoader();
          })
        this.OnClosePopup("#addProbationPopUp");
      }
      else {
        this.HideLoader();
      }
    }
    catch(error){
      this.HideLoader();
      this.uiSweetAlertService.ShowAlert(error.message);
      console.log(error);
    }
  }

  private ProbationDetail(isAddModel: boolean, index) {
    this.CreateProbationDetail();
    if (isAddModel) {
      this.ProbationDetailForm.reset();
      this.popupHeading = "Add Probation Details";
    }
    else {
      this.popupHeading = "Edit Probation Details";
      let item = this.ProbationDetailArrayList[index];
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
      this.showForm = item.AssessmentRequired;
      this.donotUse = item.IsDoNotUse;
      this.assessmentformid = item.AssessmentFormId;
      this.assessmentRequired = item.AssessmentRequired;
      this.setSelectedValues(entityData, 'Entity Group');
      this.setSelectedValues(orgData, 'Org Unit');
      this.setSelectedValues(departmentData, 'Department');
      this.selectedOrgEntityDepartment = this.orgEntityDepartment;
      this.ProbationDetailForm.patchValue(this.ProbationDetailArrayList[index]);
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

  handleChange(e){
   if(e == 'Y'){
     this.showDiv = true;
   }
   else{
     this.showDiv = false;
   }
  }

  private CreateProbationDetail() {

    this.clearDropdowns();
    if (isNullOrUndefined(this.ProbationDetailForm)) {
      this.ProbationDetailForm = this.fb.group({
        ProbationName: ['', [Validators.required,Validators.max]],
        ProbationPeriod: ['', [Validators.required]],
        AllowExtension: ['', [Validators.required]],
        ConfirmationDay: ['', [Validators.required]],
        AssessmentFormId: [''],
        ProbationDetailId: ['']
      });
    }    
  }

  private SetupPopupData(popupId: string, index: any, isAddMode: boolean) {
    //this.isAddMode = isAddModel;
    switch (popupId) {
      case "#addProbationPopUp":
        this.ProbationDetail(isAddMode, index);
        break;
      default:
        break;
    }
  }

  keyup(event){
    console.log(event, "");
    const key = event.keyCode;



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
  clearDropdowns() {
    this.centreTypeSelectedValues = '';
    this.regionSelectedValues = '';
    this.locationSelectedValues = '';
    this.employeeTypeSelectedValues = '';
    this.positionSelectedValues = '';
    this.costCentreSelectedValues = '';
    this.gradeSelectedValues = '';
  }

  public OnOpenPopup(index: any, popupType: string) {
    this.CreateProbationDetail();
    if (popupType == "Add") {
      this.ProbationDetailForm.reset();
      this.isAddMode = true;
      this.popupHeading = "Add Probation Details";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      this.SetupPopupData("#addProbationPopUp", index, false);
    }
    this.OpenModelPopup("#addProbationPopUp");
    console.log(index);
  }

  private ClearDropdownValues(data: Array<UiMultiselectData>) {
    data.forEach(item => {
      item.isCheckBoxSelected = false;
    });
    return data;
  }

  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }

}
