import { Component, OnInit, ViewChild, Inject, AfterViewInit, ViewContainerRef, ChangeDetectorRef, ViewChildren, QueryList, ElementRef, EventEmitter } from '@angular/core';
import { UiDataTableConfig, SortType, UiMultiselectData } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { LifeCycleService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/lifecycle.service';
import { Components } from 'src/Application/Types/Constants';
import { isNullOrUndefined } from 'util';
import { forkJoin, Observable, of } from 'rxjs';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { FormsService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/forms.service';
import { FormioRefreshValue } from '@formio/angular';
declare var $: any;
@Component({
  selector: 'app-exitinterview',
  templateUrl: './exitinterview.component.html',
  styleUrls: ['./exitinterview.component.css']
})
export class ExitinterviewComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public orgunitPlaceHolderText: string = "None Selected";
  public employeegroupPlaceHolderText: string = "None Selected";
  public locationPlaceHolderText: string = "None Selected";
  public designationPlaceHolderText: string = "None Selected";
  public employeeTypePlaceHolderText: string = "None Selected";
  public gradePlaceHolderText: string = "None Selected";

  public ExitInterviewArrayList = new Array<any>();
  public employeegroup = new Array<UiMultiselectData>();
  public formList: any;
  public orgunit = new Array<UiMultiselectData>();
  public designation = new Array<UiMultiselectData>();
  public location = new Array<UiMultiselectData>();
  public employeetype = new Array<UiMultiselectData>();
  public grade = new Array<UiMultiselectData>();

  public form: Object;
  public refreshForm: EventEmitter<FormioRefreshValue> = new EventEmitter();
  @ViewChild('json', {static: true}) jsonElement?: ElementRef;


  public exitInterviewPostData: any = {
    "PolicyName": "",

  };

  public ExitInterviewForm: FormGroup;
  public isAddMode: boolean = true;
  public popupHeading: string = '';
  formId: any;

  public ExtinterviewConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'ExitInterviewId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'PolicyName', fieldName: 'Exit Interview Policy Name', columnClass: 'text-nowrap width100' },
      { fieldId: 'Department', fieldName: 'Form Name', columnClass: 'text-nowrap width100' },
      { fieldId: 'OrgUnit', fieldName: 'Applicable To', columnClass: 'text-nowrap width100' }
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
    private formService: FormsService,
    private commonService: CommonService,
    private LifeCycleService: LifeCycleService
  ) {
    super(Components.ExitinterviewComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateExitInterviewDetail();
    this.getFormList();
  }

  ngAfterViewInit(): void {
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

  getFormList() {
    let paginationModel: any = {
      UserId: 1,
      StartRow: 1,
      EndRow: 100,
      SearchText: '',
      ActionId: 3,
      FormId: 0,
      NoofData: 100
    }
    this.formService.FormDetails(paginationModel).subscribe(
      (data: any) => {
        this.formList = data.Data;
        console.log('checkthelist', this.formList)
      });
  }

  openForm(value) {
    this.formId = value;
  }

  openFormView() {
    if(typeof this.formId != 'undefined'){
      const i = parseInt(this.formId);
      this.form = JSON.parse(this.formList[i].Data);
      this.OpenModelPopup("#formView");
      console.log(this.form,"test")

    }
  }

  onChange(event) {
    this.jsonElement.nativeElement.innerHTML = '';
    this.jsonElement.nativeElement.appendChild(document.createTextNode(JSON.stringify(event.form, null, 4)));
    this.refreshForm.emit({
      property: 'form',
      value: event.form
    });
  }

  private LoadMultiSelectDropdownList() {
    let paginationModel: any = {
      UserId: 1
    }
    this.ShowLoader();
    this.LifeCycleService.GetExitInterviewList(paginationModel).subscribe(
      (data: any) => {
        if (data && data != undefined) {
          if (data.EmployeeGroup) {
            this.employeegroup = data.EmployeeGroup;
          }
          if (data.orgunit) {
            this.orgunit = data.designation;
          }
          if (data.Designation) {
            this.designation = data.Designation;
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

  onItemSelect(item: UiMultiselectData, data: string) {
    this.updateText(data);
  }

  updateText(data: string) {
    if (data == "employeegroup") {
      this.employeegroupPlaceHolderText = this.updatePlaceHolder(this.employeegroup);
    }
    else if (data == "orgunit") {
      this.orgunitPlaceHolderText = this.updatePlaceHolder(this.orgunit);
    }
    else if (data == "designation") {
      this.designationPlaceHolderText = this.updatePlaceHolder(this.designation);
    }
    else if (data == "location") {
      this.locationPlaceHolderText = this.updatePlaceHolder(this.location);
    }
    else if (data == "employeetype") {
      this.employeeTypePlaceHolderText = this.updatePlaceHolder(this.employeetype);
    }   
    else if (data == "grade") {
      this.gradePlaceHolderText = this.updatePlaceHolder(this.grade);
    }
  }

  onSelectAll(items: Array<UiMultiselectData>, data: string) {
    this.updateText(data);
  }

  private updatePlaceHolder(data: Array<UiMultiselectData>) {
    let arrayData = [];
    let selectedemployeegroup = data.filter(item => item.isCheckBoxSelected == true);
    if (selectedemployeegroup.length > 0) {
      if (selectedemployeegroup.length <= 3) {
        selectedemployeegroup.forEach(item => {
          arrayData.push(item.Text);
        });
        return arrayData.join(',');
      }
      else if (selectedemployeegroup.length > 3) {
        return "Selected Item(" + selectedemployeegroup.length + ")";
      }
      else {
        return "None Selected";
      }
    }
    else {
      return "None Selected";
    }
  }

  private ExitInterviewResp(data: any) {
    if (isNullOrUndefined(data)) {
      this, this.ExitInterviewArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.ExitInterviewList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }
  private ExitInterviewList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      ExitInterviewId: 1,
      ActionTypeId: 2,
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
      let item = this.ExitInterviewList[index];

      if (item && item.EmployeeGroup) {
        this.employeegroup = this.LoadDropdownData(this.employeegroup, item.EmployeeGroup);
        this.updateText('employeegroup');
      }
      if (item && item.OrgUnit) {
        this.orgunit = this.LoadDropdownData(this.orgunit, item.OrgUnit);
        this.updateText('orgunit');
      }
      if (item && item.Designation) {
        this.designation = this.LoadDropdownData(this.designation, item.Designation);
        this.updateText('designation');
      }
      if (item && item.Location) {
        this.location = this.LoadDropdownData(this.location, item.Location);
        this.updateText('location');
      }
      if (item && item.EmployeeType) {
        this.employeetype = this.LoadDropdownData(this.employeetype, item.EmployeeType);
        this.updateText('EmployeeType');
      }
      if (item && item.Grade) {
        this.grade = this.LoadDropdownData(this.grade, item.Grade);
        this.updateText('grade');
      }    
     
      this.ExitInterviewForm.patchValue(this.ExitInterviewList[index]);
    }
  }
  private CreateExitInterviewDetail() {
    if (isNullOrUndefined(this.ExitInterviewForm)) {
      this.ExitInterviewForm = this.fb.group({
        ProbationName: ['', [Validators.required]],
        IsDoNotUse: [false],
        Id: []
      });
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
  public OnOpenPopup(index: any) {
    this.SetupPopupData("#addPolicy", index, false);
    this.OpenModelPopup("#addPolicy");
    console.log(index);
  }

  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }
}
