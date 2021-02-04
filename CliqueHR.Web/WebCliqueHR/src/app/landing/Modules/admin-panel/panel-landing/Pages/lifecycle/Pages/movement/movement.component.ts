import { Component, OnInit, ViewChild, Inject, AfterViewInit, ViewContainerRef, ChangeDetectorRef, ViewChildren, QueryList } from '@angular/core';
import { UiDataTableConfig, SortType, UiMultiselectData } from 'projects/clique-hrui/src/lib/ui-models';
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


@Component({
  selector: 'app-movement',
  templateUrl: './movement.component.html',
  styleUrls: ['./movement.component.css']
})
export class MovementComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public MovementDetailArrayList = new Array<any>();

  public MovementForm: FormGroup;
  public isAddMode: boolean = true;
  public popupHeading: string = '';
  public donotUse: boolean = false;

  public MovementConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'MovementId', fieldName: '', columnClass: 'width100' },
      { fieldId: 'MovementReason', fieldName: 'Movement Reason', columnClass: 'text-nowrap width100' },
      { fieldId: 'MovementField', fieldName: 'Editable Fields', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'MovementId', direction: 'asc' },
    UniqueRowCol: 'MovementId',
    isEditable: true
  };

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private commonService: CommonService,
    private LifeCycleService: LifeCycleService
  ) {
    super(Components.MovementComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateMovementDetail();
  }

  ngAfterViewInit(): void {

    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
        this.MovementDetailList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.MovementDetailsResp(data);
          },
          (error) => {
            console.log(error);
          });;
      })
    );
    forkJoin([
      this.MovementDetailList(1, this.MovementConfig.PaginationPageSize, { fieldId: 'MovementId', direction: 'asc' }),
    ]).subscribe(
      (data: any) => {
        this.HideLoader();
        this.MovementDetailsResp(data[0]);
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      });
  }
  private MovementDetailsResp(data: any) {
    //debugger;
    if (isNullOrUndefined(data)) {
      this, this.MovementDetailArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.MovementDetailArrayList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }

  private MovementDetailList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      Start: startRow,
      StartRow: startRow,
      EndRow: endRow,
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction },
      SearchText: searchText || '',
      ActionId: 1,
      MovementId: 1,
      NoofData: 10
    }
    return this.LifeCycleService.GetMovementReason(paginationModel);
  }

  public OnMovementsOp() {
    this.ShowLoader();
    let movementtype: Observable<any>;

    this.MovementForm.value.ActionId = this.isAddMode == true ? 1 : 2;

    movementtype = this.LifeCycleService.AddMovementDetails(this.MovementForm.value);

    movementtype.subscribe(
      (data: any) => {
        
        this.MovementDetailList(1, this.MovementConfig.PaginationPageSize, { fieldId: 'MovementId', direction: 'asc' }).subscribe(
          (data: any) => {
            // this.HideLoader();
            // this.HandleCourseTypeResp(data);
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
    this.CloseModelPopup('#addReason');
  }

  private MovementDetail(isAddModel: boolean, index) {
    debugger;
    this.CreateMovementDetail();
    if (isAddModel) {
      this.MovementForm.reset();
      this.popupHeading = "Add Reason";
    }
    else {
      let item = this.MovementDetailArrayList[index];
      this.popupHeading = "Edit Reason";
      this.donotUse = item.IsDoNotUse;
      this.MovementForm.patchValue(this.MovementDetailArrayList[index]);
    }
  }
  private CreateMovementDetail() {
    if (isNullOrUndefined(this.MovementForm)) {
      this.MovementForm = this.fb.group({
        MovementReason: ['', [Validators.required]],
        EditableFieldsId: [''],
        MovementId: [''],
        Id: []
      });
    }
  }

  private SetupPopupData(popupId: string, index: any, isAddMode: boolean) {
    // debugger;
    switch (popupId) {
      case "#addReason":
        this.MovementDetail(isAddMode, index);
        break;
      default:
        break;
    }
  }
  public OnOpenPopup(index: any, popupType: string) {
    debugger;
    this.CreateMovementDetail();
    if (popupType == "Add") {
      this.MovementForm.reset();
      this.isAddMode = true;
      this.popupHeading = "Add Reason";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      //this.popupHeading = "Edit Reason";
      this.SetupPopupData("#addReason", index, false);
    }
    this.OpenModelPopup("#addReason");
    console.log(index);
  }
  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }
}
