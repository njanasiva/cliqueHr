import { Component, OnInit, ViewChild, Inject, AfterViewInit, ViewContainerRef, ChangeDetectorRef, ViewChildren, QueryList } from '@angular/core';
import { UiDataTableConfig, SortType, UiMultiselectData, UiMultiSelectOptions, UiMultiSelectOutPut, DateFormat } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { EngagementService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/encagement.services';
import { LifeCycleService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/lifecycle.service';
import { isNullOrUndefined } from 'util';
import { forkJoin, Observable, of } from 'rxjs';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { GetDateInString, ValidationBuilder, GetUTCDateFromString } from 'src/Application/Utilitis/ApplicationUtility';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public popupHeading: string = '';
  public GroupsForm: FormGroup;
  public isAddMode: boolean = true;
  public filter = new UiMultiselectData();
  public donotUse: boolean = false;

  public EngagementGroupArrayList = new Array<any>();
  public groupModerators = new Array<UiMultiselectData>();
  public employeegroup = new Array<UiMultiselectData>();
  public entity = new Array<UiMultiselectData>();
  public orgunit = new Array<UiMultiselectData>();
  public department = new Array<UiMultiselectData>();
  public location = new Array<UiMultiselectData>();
  public employeesearch = new Array<UiMultiselectData>();

  public groupModeratorsSelectedValues: string = '';
  public employeeGroupSelectedValues: string = '';
  public entitySelectedValues: string = '';
  public orgunitSelectedValues: string = '';
  public departmentSelectedValues: string = '';
  public locationSelectedValues: string = '';
  public employeesearchSelectedValues: string = '';

  public PostStatus: boolean = false;
  public PostPoll: boolean = false;
  public PostEvent: boolean = false;
  public ApprovalReqforStatus: boolean = false;
  public ApprovalReqforPoll: boolean = false;
  public ApprovalReqforEvent: boolean = false;
  public fileToUpload: string = '';

  public GroupsConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'GroupId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'GroupName', fieldName: 'Group Name', columnClass: 'text-nowrap width100' },
      { fieldId: 'GroupModerators', fieldName: 'Group Moderators', columnClass: 'text-nowrap width100' },
      { fieldId: 'WorkGroup', fieldName: 'WorkGroup', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { direction: 'asc', fieldId: 'GroupId' },
    UniqueRowCol: 'GroupId',
    isEditable: true

  };

  constructor(@Inject(WebTokens.APPLICATION_SERVICE)
  protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private EngagementService: EngagementService,
    private uiSweetAlertService: UiSweetAlertService,
    private LifeCycleService: LifeCycleService

  ) {
    super(Components.EntityComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.filter.Text = '';
    this.CreateEngagementGroupDetail();
  }

  ngAfterViewInit(): void {
    this.LoadGrid();
  }

  LoadGrid() {
    try {
      this.LoadMultiSelectDropdownList();
      this.SubjectDestroy.push(
        this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
          this.ShowLoader();
          this.EngagementGroupList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
            (data: any) => {
              this.EngagementGroupResp(data);
              this.HideLoader();
            },
            (error) => {
              this.HideLoader();
              console.log(error);
            });
        })
      );
      forkJoin([
        this.EngagementGroupList(1, this.GroupsConfig.PaginationPageSize, { fieldId: 'GroupId', direction: 'asc' }),
      ]).subscribe(
        (data: any) => {
          this.HideLoader();
          this.EngagementGroupResp(data[0]);
        },
        (error) => {
          this.HideLoader();
          console.log(error);
        });

    } catch (error) {
      this.HideLoader();
    }
  }
  private EngagementGroupList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      Start: startRow,
      StartRow: startRow,
      EndRow: endRow,
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction },
      SearchText: searchText || '',
      ActionId: 5,
      NoofData: 10
    }
    this.ShowLoader();
    return this.EngagementService.GetEngagementGroups(paginationModel);
  }

  private EngagementGroupResp(data: any) {
    
    if (isNullOrUndefined(data)) {
      this.EngagementGroupArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.EngagementGroupArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }
  private CreateEngagementGroupDetail() {
    this.clearDropdowns();
    if (isNullOrUndefined(this.GroupsForm)) {
      this.GroupsForm = this.fb.group({
        UserId: 1,
        GroupId: 1,
        ActionId: 0,
        Start: 0,
        NoofData: 10,
        GroupName: ['', Validators.required],
        GroupModerators: ['', Validators.required],
        EmployeeGroup: ['', Validators.required],
        PostStatus: false,
        PostPoll: false,
        PostEvent: false,
        IsApprovalStatus: false,
        IsApprovalPostPoll: false,
        IsApprovalPostEvent: false,
        IsDoNotUse: false
      });
    }
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
          if (data.Moderators) {
            this.groupModerators = data.Moderators;
          }
          if (data.Location) {
            this.location = data.Location;
          }
          if (data.Entity) {
            this.entity = data.Entity;
          }
          if (data.OrgUnit) {
            this.orgunit = data.OrgUnit;
          }
          if (data.Department) {
            this.department = data.Department;
          }
          if (data.Users) {
            this.employeesearch = data.Users;
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

  clearDropdowns() {
    this.employeeGroupSelectedValues = '';
    this.groupModeratorsSelectedValues = '';
    this.entitySelectedValues = '';
    this.orgunitSelectedValues = '';
    this.departmentSelectedValues = '';
    this.locationSelectedValues = '';
    this.employeesearchSelectedValues = '';
  }

  public OnOpenPopup(index: any, popupType: string) {
    this.CreateEngagementGroupDetail();
    if (popupType == "Add") {
      this.GroupsForm.reset();
      this.isAddMode = true;
      this.popupHeading = "Add Groups";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      this.SetupPopupData("#addGroup", index, false);
    }
    this.OpenModelPopup("#addGroup");
    console.log(index);
  }

  private SetupPopupData(popupId: string, index: any, isAddMode: boolean) {
     
    switch (popupId) {
      case "#addGroup":
        this.GroupDetail(isAddMode, index);
        break;
      default:
        break;
    }
  }

  private GroupDetail(isAddModel: boolean, index) {
    
    this.CreateEngagementGroupDetail();
    if (isAddModel) {
      this.GroupsForm.reset();
      this.popupHeading = "Add Groups";
    }
    else {
      // this.ClearValues();
      let item = this.EngagementGroupArrayList[index];
      this.popupHeading = "Edit Groups";

      if (item && item.GroupModerators) {
        this.groupModeratorsSelectedValues = item.GroupModerators;
      }
      if (item && item.EmployeeGroup) {
        this.employeeGroupSelectedValues = item.EmployeeGroup;
      }
      if (item && item.Entity) {
        this.entitySelectedValues = item.Entity;
      }
      if (item && item.OrgUnit) {
        this.orgunitSelectedValues = item.OrgUnit;
      }
      if (item && item.Department) {
        this.departmentSelectedValues = item.Department;
      }
      if (item && item.Location) {
        this.locationSelectedValues = item.Location;
      }
      if (item && item.EmployeeSearch) {
        this.employeesearchSelectedValues = item.EmployeeSearch;
      }
      this.donotUse = item.IsDoNotUse;
      this.PostPoll = item.PostPoll;
      this.PostStatus = item.PostStatus;
      this.PostEvent = item.PostEvent;
      this.ApprovalReqforStatus = item.IsApprovalStatus;
      this.ApprovalReqforPoll = item.IsApprovalPostPoll;
      this.ApprovalReqforEvent = item.IsApprovalPostEvent;
     // this.fileToUpload = item.Icon; 
      this.GroupsForm.patchValue(this.EngagementGroupArrayList[index]);
    }
  }

  onSelectAll(items: Array<UiMultiselectData>, data: string) {
  }


  public OnGroupSave() {   
    debugger;
    this.ShowLoader();   
    let grouptype: Observable<any>;
    this.GroupsForm.value.ActionId = this.isAddMode == true ? 1 : 2;
    this.GroupsForm.value.PostPoll = this.PostPoll;
    this.GroupsForm.value.PostStatus = this.PostStatus;
    this.GroupsForm.value.PostEvent = this.PostEvent;
    this.GroupsForm.value.IsApprovalStatus = this.ApprovalReqforStatus;
    this.GroupsForm.value.IsApprovalPostPoll = this.ApprovalReqforPoll;
    this.GroupsForm.value.IsApprovalPostEvent = this.ApprovalReqforEvent;

    this.GroupsForm.value.Entity = this.entitySelectedValues;
    this.GroupsForm.value.OrgUnit = this.orgunitSelectedValues;
    this.GroupsForm.value.Department = this.departmentSelectedValues;
    this.GroupsForm.value.Location = this.locationSelectedValues;
    this.GroupsForm.value.GroupModerators = this.groupModeratorsSelectedValues;
    this.GroupsForm.value.EmployeeGroup = this.employeeGroupSelectedValues;
    this.GroupsForm.value.EmployeeSearch = this.employeesearchSelectedValues;
    this.GroupsForm.value.Icon = this.fileToUpload;

    this.GroupsForm.value.IsDoNotUse = this.donotUse;
    grouptype = this.EngagementService.GetEngagementGroups(this.GroupsForm.value);
    grouptype.subscribe(
      (data: any) => {
        this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
        this.LoadGrid();
        this.HideLoader();
        this.CloseModelPopup('#addGroup'); 0
      },
      (error) => {
        this.HideLoader();
      }
    )
  }
  onItemSelect(item: UiMultiSelectOutPut, data: string) {
    this.updateText(data, item.selectedValues);
    console.log(item.selectedValues);
  }

  updateText(data: string, item: string) {
    if (data == "groupModerators") {
      this.groupModeratorsSelectedValues = item;
    }
    else if (data == "employeegroup") {
      this.employeeGroupSelectedValues = item;
    }
    else if (data == "entity") {
      this.entitySelectedValues = item;
    }
    else if (data == "orgunit") {
      this.orgunitSelectedValues = item;
    }
    else if (data == "department") {
      this.departmentSelectedValues = item;
    }
    else if (data == "location") {
      this.locationSelectedValues = item;
    }
    else if (data == "employeesearch") {
      this.employeesearchSelectedValues = item;
    }
  }  
  handleFileInput(files: FileList) {   
    this.fileToUpload = files.item(0).name;
  }
}
