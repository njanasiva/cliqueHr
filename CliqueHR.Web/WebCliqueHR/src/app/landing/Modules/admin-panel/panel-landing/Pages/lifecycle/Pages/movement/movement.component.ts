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
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
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
  public Editablefields = new Array<any>();
  public MovementForm: FormGroup;
  public isAddMode: boolean = true;
  public popupHeading: string = '';
  public donotUse: boolean = false;
  public AllowManagerInitiate: boolean = false;
  public AllowManager: boolean = false;
  public AllowRoleHolder: boolean = false;

  public Entity: boolean = false;
  public OrgUnit: boolean = false;
  public Department: boolean = false;
  public PrimaryLocation: boolean = false;
  public SecondaryLocation: boolean = false;
  public Designation: boolean = false;
  public Position: boolean = false;
  public Grade: boolean = false;
  public ReportingManager: boolean = false;
  public PrimaryCostCentre: boolean = false;
  public SecondaryCostCentre: boolean = false;
  public FunctionalManager: boolean = false;
  public DottedLineManager: boolean = false;

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
    private LifeCycleService: LifeCycleService,
    private uiSweetAlertService: UiSweetAlertService
  ) {
    super(Components.MovementComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateMovementDetail();
  }


  LoadGrid() {
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

  ngAfterViewInit(): void {
    this.LoadGrid();
  }

  private MovementDetailsResp(data: any) {
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
    this.Editablefields = [];
    let movementtype: Observable<any>;
    if (this.Entity == true) {
      this.Editablefields.push(1);
    }
    if (this.OrgUnit == true) {
      this.Editablefields.push(2);
    }
    if (this.Department == true) {
      this.Editablefields.push(3);
    }
    if (this.PrimaryLocation == true) {
      this.Editablefields.push(4);
    }
    if (this.SecondaryLocation == true) {
      this.Editablefields.push(5);
    }
    if (this.Designation == true) {
      this.Editablefields.push(6);
    }
    if (this.Position == true) {
      this.Editablefields.push(7);
    }
    if (this.Grade == true) {
      this.Editablefields.push(8);
    }
    if (this.ReportingManager == true) {
      this.Editablefields.push(9);
    }
    if (this.PrimaryCostCentre == true) {
      this.Editablefields.push(10);
    }
    if (this.SecondaryCostCentre == true) {
      this.Editablefields.push(11);
    }
    if (this.FunctionalManager == true) {
      this.Editablefields.push(12);
    }
    if (this.DottedLineManager == true) {
      this.Editablefields.push(13);
    }

    this.MovementForm.value.ActionId = this.isAddMode == true ? 1 : 2;
    this.MovementForm.value.AllowManagerInitiate = this.AllowManagerInitiate;
    this.MovementForm.value.AllowRoleHolder = this.AllowRoleHolder == true ? 1 : 0;
    this.MovementForm.value.AllowManager = this.AllowManager == true ? 1 : 0;
    this.MovementForm.value.EditableFieldsId = this.Editablefields.toString();
    this.MovementForm.value.IsDoNotUse = this.donotUse;

    movementtype = this.LifeCycleService.AddMovementDetails(this.MovementForm.value);

    movementtype.subscribe(
      (data: any) => {
        this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
        this.LoadGrid();
        this.HideLoader();
        this.CloseModelPopup('#addReason'); 0
      },
      (error) => {
        this.HideLoader();
      }
    )
  }

  private MovementDetail(isAddModel: boolean, index) {
    debugger;
    this.CreateMovementDetail();
    if (isAddModel) {
      this.MovementForm.reset();
      this.popupHeading = "Add Reason";
    }
    else {
      this.ClearValues();
      let item = this.MovementDetailArrayList[index];
      this.popupHeading = "Edit Reason";
      this.donotUse = item.IsDoNotUse;
      this.AllowManager = item.AllowManager == 1 ? true : false;
      this.AllowRoleHolder = item.AllowRoleHolder == 1 ? true : false;
      this.AllowManagerInitiate = item.AllowManagerInitiate;
      if (item.MovementField != null) {
        for (var i = 0; i < item.MovementField.trim().split(',').length; i++) {
          var movefieldvalue = item.MovementField.trim().split(',')[i];
          let trimedValue = movefieldvalue.replace(/\s+/, "");
          switch (trimedValue) {
            case "Entity":
              this.Entity = true;
              break;
            case "Org Unit":
              this.OrgUnit = true;
              break;
            case "Department":
              this.Department = true;
              break;
            case "Primary Location":
              this.PrimaryLocation = true;
              break;
            case "Secondary Location":
              this.SecondaryLocation = true;
              break;
            case "Designation":
              this.Designation = true;
              break;
            case "Position":
              this.Position = true;
              break;
            case "Grade":
              this.Grade = true;
              break;
            case "Reporting Manager":
              this.ReportingManager = true;
              break;
            case "Primary Cost Centre":
              this.PrimaryCostCentre = true;
              break;
            case "Secondary Cost Centre":
              this.SecondaryCostCentre = true;
              break;
            case "Functional Manager":
              this.FunctionalManager = true;
              break;
            case "Dotted Line Manager":
              this.DottedLineManager = true;
              break;
          }
        }
      }
      this.MovementForm.patchValue(this.MovementDetailArrayList[index]);
    }
  }

  ClearValues() {
    this.Entity = false;
    this.Entity = false;
    this.OrgUnit = false;
    this.Department = false;
    this.PrimaryLocation = false;
    this.SecondaryLocation = false;
    this.Designation = false;
    this.Position = false;
    this.Grade = false;
    this.ReportingManager = false;
    this.PrimaryCostCentre = false;
    this.SecondaryCostCentre = false;
    this.FunctionalManager = false;
    this.DottedLineManager = false;
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
    this.ClearValues();
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
    this.CreateMovementDetail();
    if (popupType == "Add") {
      this.MovementForm.reset();
      this.AllowManager = false;
      this.AllowManagerInitiate = false;
      this.AllowRoleHolder = false;
      this.isAddMode = true;
      this.popupHeading = "Add Reason";
    }
    else if (popupType == "Edit") {
      this.isAddMode = false;
      this.SetupPopupData("#addReason", index, false);
    }
    this.OpenModelPopup("#addReason");
    console.log(index);
  }

  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }
}