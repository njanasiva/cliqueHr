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
  selector: 'app-dailycontent',
  templateUrl: './dailycontent.component.html',
  styleUrls: ['./dailycontent.component.css']
})
export class DailycontentComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public popupHeading: string = '';

  public DailyContentArrayList = new Array<any>();
  public orgEntityDepartment = new Array<UiMultiSelectOptions>();
  public DailyContentForm: FormGroup;
  public isAddMode: boolean = true;
  public selectedOrgEntityDepartment = new Array<UiMultiSelectOptions>();
  public filter = new UiMultiselectData();
  public Shuffle: boolean = false;
  public IsDate: boolean = false;
  public StartDate: string = GetDateInString(new Date());
  public EndDate: string = GetDateInString(new Date());
  public Content: string = "";

  public DailyContentConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'DailyContentId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'Content', fieldName: 'Content', columnClass: 'text-nowrap width100' },
      { fieldId: 'Recurring', fieldName: 'Recurring', columnClass: 'text-nowrap width100' },
      { fieldId: 'StartDate', fieldName: 'Start Date', columnClass: 'text-nowrap width100' },
      { fieldId: 'EndDate', fieldName: 'End Date', columnClass: 'text-nowrap width100' },
      { fieldId: 'WrkGrps', fieldName: 'Work Groups', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 3,
    DefaultSort: { direction: 'asc', fieldId: 'DailyContentId' },
    UniqueRowCol: 'DailyContentId',
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
    this.CreateDailyContentDetail();
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
          this.DailyContentList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
            (data: any) => {
              this.DailyContentResp(data);
              this.HideLoader();
            },
            (error) => {
              this.HideLoader();
              console.log(error);
            });
        })
      );
      forkJoin([
        this.DailyContentList(1, this.DailyContentConfig.PaginationPageSize, { fieldId: 'DailyContentId', direction: 'asc' }),
      ]).subscribe(
        (data: any) => {
          this.HideLoader();
          this.DailyContentResp(data[0]);
        },
        (error) => {
          this.HideLoader();
          console.log(error);
        });

    } catch (error) {
      this.HideLoader();
    }
  }

  private CreateDailyContentDetail() {
    debugger;
    this.clearDropdowns();
    if (isNullOrUndefined(this.DailyContentForm)) {
      this.DailyContentForm = this.fb.group({
        UserId: 1,
        DailyContentId: 1,
        ActionId: 3,
        Start: 0,
        NoofData: 10,
        IsShuffle: 1,
        IsDate: 1,
        Recurring: 1,
        Content: ['', Validators.required],
        StartDate: [GetDateInString(new Date()), [Validators.required]],
        EndDate: [GetDateInString(new Date()), [Validators.required]],
      });
    }
  }

  private DailyContentList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      Start: startRow,
      StartRow: startRow,
      EndRow: endRow,
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction },
      SearchText: searchText || '',
      ActionId: 3,
      DailyContentId: 1,
      NoofData: 10
    }
    this.ShowLoader();
    return this.EngagementService.AddModifyDailyContentDetails(paginationModel);
  }

  private DailyContentResp(data: any) {
    debugger;
    if (isNullOrUndefined(data)) {
      this.DailyContentArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.DailyContentArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }

  private DailyContentDetail(isAddModel: boolean, index) {
    this.CreateDailyContentDetail();
    if (isAddModel) {
      this.DailyContentForm.reset();
      this.popupHeading = "Add Daily Content";
    }
    else {
      this.popupHeading = "Edit Daily Content";
      let item = this.DailyContentArrayList[index];      

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
    }
  }

  clearDropdowns() {
    this.orgEntityDepartment.forEach(item => {
      item.Data.forEach(items => {
        items.isCheckBoxSelected = false;
      })
    })
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
  private LoadMultiSelectDropdownList() {
    debugger;
    let paginationModel: any = {
      UserId: 1
    }
    this.LifeCycleService.GetConfirmationMasterList(paginationModel).subscribe(
      (data: any) => {
        if (data && data != undefined) {
          let _orgData = new Array<UiMultiSelectOptions>();
          let _entity = this.LoadAccordionDropdown(data.Entity, "entity", "Entity Group");
          let _department = this.LoadAccordionDropdown(data.Department, "department", "Department");
          let _orgUnit = this.LoadAccordionDropdown(data.OrgUnit, "orgUnit", "Org Unit");
          _orgData.push(_entity);
          _orgData.push(_orgUnit);
          _orgData.push(_department);
          this.orgEntityDepartment = _orgData;
        }
      },
      (error) => {
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
    console.log(item.selectedValues);
  }

  public OnOpenPopup(index: any, popupType: string) {
    this.CreateDailyContentDetail();
    if (popupType == "Add") {
      this.DailyContentForm.reset();
      this.isAddMode = true;
      this.popupHeading = "Add Daily Content";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      this.SetupPopupData("#addDailyContent", index, false);
    }
    this.OpenModelPopup("#addDailyContent");
    console.log(index);
  }

  private SetupPopupData(popupId: string, index: any, isAddMode: boolean) {
    switch (popupId) {
      case "#addDailyContent":
        this.DailyContentDetail(isAddMode, index);
        break;
      default:
        break;
    }
  }

  public OnDailyContent() {
    try {
      //if (this.validateData()) {
      debugger;
        this.ShowLoader();
      let dailycontenttype: Observable<any>;
      this.DailyContentForm.value.ActionId = this.isAddMode == true ? 1 : 2;
      this.DailyContentForm.value.IsShuffle = this.Shuffle
      this.DailyContentForm.value.Recurring = this.Shuffle;
      this.DailyContentForm.value.IsDate = this.IsDate;
      this.DailyContentForm.value.OrgUnit = this.getSelectedValues("Org Unit");
      this.DailyContentForm.value.Entity = this.getSelectedValues("Entity Group");
      this.DailyContentForm.value.Department = this.getSelectedValues("Department");
      this.DailyContentForm.value.Content = this.Content;

      dailycontenttype = this.EngagementService.AddModifyDailyContentDetails(this.DailyContentForm.value);

      dailycontenttype.subscribe(
          (data: any) => {
            this.OnClosePopup("#addDailyContent");
            this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
            this.LoadGrid();
          },
          (error) => {
            if (error.message != "") {
              this.uiSweetAlertService.ShowAlert(error.message);
            }
            this.HideLoader();
          })

      //}
      //else {
      //  this.HideLoader();
      //}
    }
    catch (error) {
      this.HideLoader();
      console.log(error);
    }
  }
  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }
}
