import { Component, OnInit, ViewChild, Inject, AfterViewInit, ViewContainerRef, ChangeDetectorRef, ViewChildren, QueryList } from '@angular/core';
import { UiDataTableConfig, SortType, UiMultiselectData, UiMultiSelectOptions, UiMultiSelectOutPut } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebAppService, WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { LifeCycleService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/lifecycle.service';
import { Components } from 'src/Application/Types/Constants';
import { isNullOrUndefined } from 'util';
import { forkJoin, Observable, of } from 'rxjs';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { FormGroup, FormBuilder, Validators, FormControl, FormArray } from '@angular/forms';


@Component({
  selector: 'app-workflow',
  templateUrl: './workflow.component.html',
  styleUrls: ['./workflow.component.css']
})
export class WorkflowComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;
  addForm: FormGroup;
  rows: FormArray;
  public separationTypeSelectedValues: string = "";
  public userDefinedData = new Array<any>();
  public WorkFlowArrayList = new Array<any>();
  public separationType = new Array<UiMultiselectData>();
  public designation = new Array<UiMultiselectData>();
  public WorkFlowForm: FormGroup;
  public isAddMode: boolean;
  public popupHeading: string;
  public WorkflowConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'LifeCycleWorkFlowId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'ApprovalPathType', fieldName: 'Approval Path Type', columnClass: 'text-nowrap width100' },
      { fieldId: 'ApprovalPathName', fieldName: 'Approval path', columnClass: 'text-nowrap width100' },
      { fieldId: 'WorkGrps', fieldName: 'Work Groups', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 3,
    DefaultSort: { direction: 'asc', fieldId: 'LifeCycleWorkFlowId' },
    UniqueRowCol: 'LifeCycleWorkFlowId',
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
    super(Components.ExitinterviewComponent, applicationService, changeDetection, viewContainerRef);
    this.addForm = this.fb.group({});
    this.rows = this.fb.array([]);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }

  LoadWorkFlowGrid() {
    try {
      this.SubjectDestroy.push(
        this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
          this.ShowLoader();
          this.WorkFlowList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
            (data: any) => {
              this.WorkFlowListResp(data);
              this.HideLoader();
            },
            (error) => {
              this.HideLoader();
              console.log(error);
            });
        })
      );
      forkJoin([
        this.WorkFlowList(1, this.WorkflowConfig.PaginationPageSize, { fieldId: 'LifeCycleWorkFlowId', direction: 'asc' }),
      ]).subscribe(
        (data: any) => {
          this.HideLoader();
          this.WorkFlowListResp(data[0]);
        },
        (error) => {
          this.HideLoader();
          console.log(error);
        });

    } catch (error) {
      this.HideLoader();
    }
  }

  private WorkFlowListResp(data: any) {
    if (isNullOrUndefined(data)) {
      this.WorkFlowArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.WorkFlowArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }

  private CreateWorkFlow() {
    this.WorkFlowForm = this.fb.group({
      ApprovalPathName: ['', [Validators.required]],
      NumberOfLevels: 3,
      ApprovalPathTypeId: 0,
      IsDoNotUse: [false],
      LifeCycleWorkFlowId: 0
    });
  }

  private WorkFlowList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      Start: startRow,
      StartRow: startRow,
      EndRow: endRow,
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction },
      SearchText: searchText || '',
      NoofData: 10
    }
    this.ShowLoader();
    return this.LifeCycleService.GetWorkFlowList(paginationModel);
  }

  ngOnInit() {
    this.CreateWorkFlow()
    this.addForm.addControl('rows', this.rows);
  }

  ngAfterViewInit(): void {
    this.LoadDropDown();
    this.LoadWorkFlowGrid();
  }

  createItemFormGroup(seqNo, approver, escaleTo, approverId, escalateId): FormGroup {
    let formData = this.fb.group({
      seqNo: seqNo,
      approverId: approverId,
      approverSelectedValues: '',
      approver: [],
      followUp: 0,
      escalateId: escalateId,
      escalateTo: [],
      escalateToSelectedValues: 0,
      escalateAfter: 0,
    });
    formData.get('approver').setValue(approver);
    formData.get('escalateTo').setValue(escaleTo);
    return formData;
  }

  gridMultiOnItemSelect(e, index) {
    for(let i=0; i< this.userDefinedData.length;i++){
      if (i == index) {
        this.userDefinedData[i].ApproverId = e.selectedValues;
      }
    }
  }

  AssignValue(data: string) {
    let values = Object.assign(Array<UiMultiselectData>(), this.designation);
    console.log(values,"values");
    values.forEach(items => {
      items.isCheckBoxSelected = false;
    });

    if (data && data != '') {
      let _data = data.split(',');
      values.forEach(items => {
        _data.forEach(item => {
          let selectedValue = parseInt(item);
          if (items.Value === selectedValue) {
            items.isCheckBoxSelected = true;
          }
        });
      });
    }
    return values;
  }

  LoadGrid(noOfRows: number) {
    if (noOfRows) {
      this.userDefinedData = new Array<any>();
      this.rows.clear();
      for (let i = 0; i < noOfRows; i++) {
        let seq = i + 1;
        let approverId = 'approver' + this.getRandomNumber(i);
        let escalateId = 'escalateId' + this.getRandomNumber(i);
        let approver = Object.assign(new Array<UiMultiselectData>(), this.designation);
        let escateTo = Object.assign(new Array<UiMultiselectData>(), this.designation);
        this.rows.push(this.createItemFormGroup(seq, approver, escateTo, approverId, escalateId));
        let item = {
          "ApproverId": '',
          "id": approverId,
          "Followup": 0,
          "Escalateto": 0,
          "EscalateAfter": 0
        };
        this.userDefinedData.push(item);
      }
    }
  }

  LoadGridByData(noOfRows: number, data: any) {
    if (noOfRows) {
      this.rows.clear();
      this.userDefinedData = new Array<any>();
      for (let i = 0; i < noOfRows; i++) {
        let seq = i + 1;
        let approverId = 'approver' + this.getRandomNumber(i);
        let escalateId = 'escalateId' + this.getRandomNumber(i);
        let approver = Object.assign(new Array<UiMultiselectData>(), this.designation);
        let escateTo = Object.assign(new Array<UiMultiselectData>(), this.designation);
        this.rows.push(this.createItemFormGroupByData(seq, approver, escateTo, approverId, escalateId, data));
        let item = {
          "ApproverId": data.approverSelectedValues,
          "id": approverId,
          "Followup": data.followUp,
          "Escalateto": data.escalateToSelectedValues,
          "EscalateAfter": data.escalateAfter
        };
        this.userDefinedData.push(item);
      }
    }
  }

  createItemFormGroupByData(seqNo, approver: Array<UiMultiselectData>, escaleTo, approverId, escalateId, data): FormGroup {
    let formData = this.fb.group({
      seqNo: seqNo,
      approverId: approverId,
      approverSelectedValues: data.ApproverId,
      approver: [],
      followUp: data.Followup,
      escalateId: escalateId,
      escalateTo: [],
      escalateToSelectedValues: data.Escalateto,
      escalateAfter: data.EscalateAfter,
    });
    let appData = data.ApproverId.split(',');
    approver.forEach(items => {
      appData.forEach(item => {
        let val = parseInt(item);
        if (items.Value == val) {
          items.isCheckBoxSelected = true;
        }
      });
    });
    formData.get('approver').setValue(approver);
    formData.get('escalateTo').setValue(escaleTo);
    formData.get('escalateToSelectedValues').setValue(data.Escalateto);
    return formData;
  }

  getRandomNumber(i: number) {
    let random = Math.floor(Math.random() * (999999 - 100000)) + 100000;
    return random;
  }

  onItemSelect(item: UiMultiSelectOutPut, data: string) {
    this.updateText(data, item.selectedValues);
    console.log(item.selectedValues);
  }

  onSelectAll(items: Array<UiMultiselectData>, data: string) {
  }

  updateText(data: string, item: string) {
    if (data == "separationType") {
      this.separationTypeSelectedValues = item;
    }
  }

  public OnOpenPopup(index: any, popupType: string) {
    this.WorkFlowForm.reset();
    if (popupType == "Add") {
      this.CreateWorkFlow();
      this.isAddMode = true;
      this.LoadGrid(3);

      this.popupHeading = "Add Approval Path";
      this.OpenModelPopup("#addlifeCycleWorkflow");
    }
    else if (popupType == "Edit") {
      this.GetSingleData(index);
    }
    console.log(index);
  }

  GetSingleData(index: number) {
    let item = this.WorkFlowArrayList[index];
    this.ShowLoader();
    if (item) {
      let id = item.LifeCycleWorkFlowId;
      this.LifeCycleService.GetWorkFlowData(id).subscribe(
        (data) => {
          if (data && data["Table"]) {
            let workFlowData = data["Table"];
            this.OpenModelPopup("#addlifeCycleWorkflow");
            this.isAddMode = false;
            this.popupHeading = "Edit Approval Path";
            if (workFlowData) {
              this.WorkFlowForm.patchValue(item);
              this.separationTypeSelectedValues = item.ApprovalReasonsId;
              let noOfRows = item.NumberOfLevels;
              this.LoadGridByData(noOfRows, workFlowData[0]);
              console.log(workFlowData);
            }
          }
          this.HideLoader();
        },
        (error) => {
          this.HideLoader();
        }
      );
    }
  }

  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }

  SaveWorkFlow() {
    let data = [];
    this.userDefinedData.forEach(element => {
      let value = this.rows.value.find(a=>a.approverId == element.id);
      let item = {
        "ApproverId": element.ApproverId,
        "Followup": value.followUp,
        "Escalateto": value.escalateToSelectedValues,
        "EscalateAfter": value.escalateAfter
      };
      data.push(item);
    });
    let approverList = JSON.stringify(data);
    this.WorkFlowForm.value.ApprovalReasonsId = this.separationTypeSelectedValues;
    this.WorkFlowForm.value.ApproverList = approverList;
    this.ShowLoader();
    this.LifeCycleService.SaveWorkFlow(this.WorkFlowForm.value).subscribe(
      (data) => {
        this.HideLoader();
        this.LoadWorkFlowGrid();
        this.OnClosePopup("#addlifeCycleWorkflow");
        this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
      },
      (error) => {
        this.HideLoader();
      }
    );
  }

  LoadDropDown() {
    let paginationModel: any = {
      UserId: 1
    }
    this.LifeCycleService.GetConfirmationMasterList(paginationModel).subscribe(
      (data: any) => {
        if (data && data != undefined) {
          console.log(data,"data");
          if (data.SeparationType) {
            let separationTypeData = data.SeparationType.filter(a => a.Value != 0);
            this.separationType = separationTypeData;
          }
          if (data.Designation) {
            this.designation = data.Designation;
          }
        }
      });
  }
}
