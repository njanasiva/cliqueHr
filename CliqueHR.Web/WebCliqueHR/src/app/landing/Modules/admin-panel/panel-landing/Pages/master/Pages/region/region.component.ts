import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { RegionModel } from '../../master-model';
import { ApplicationEmpSingleDDComponent } from 'src/Application/Components/application-emp-single-dd/application-emp-single-dd.component';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { ValidationMessage } from '../../validation-message';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observer, forkJoin, Observable } from 'rxjs';
import { MasterService } from '../../master.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { isNullOrUndefined } from 'util';
import { LocationDropdown, State, Country, City, LocationDropdownWithoutCountry } from 'src/app/landing/Modules/common-model';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { CustomValidator } from 'src/Application/Utilitis/CustomValidator';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';

@Component({
  selector: 'app-region',
  templateUrl: './region.component.html',
  styleUrls: ['./region.component.css']
})
export class RegionComponent  extends WebComponents.ApplicationComponent implements OnInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  private m_editData: RegionModel;
  @ViewChild(ApplicationEmpSingleDDComponent, { static: true, read: ApplicationEmpSingleDDComponent })
  private empDropdown: ApplicationEmpSingleDDComponent;
  private RegionObs: Observer<any>;

  public RegionConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '' },
      { fieldId: 'Name', fieldName: 'Region Name', columnClass: 'text-nowrap' },
      { fieldId: 'StateName', fieldName: 'State Mapping' },
      { fieldId: 'CityName', fieldName: 'City Mapping' },
      { fieldId: 'RegionHeadName', fieldName: 'Regional Head' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };
  public RegionList: Array<RegionModel>;
  public isAddMode: boolean;
  public validationMassages = ValidationMessage.data;
  public RegionForm: FormGroup;
  public LocationData: LocationDropdownWithoutCountry;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private masterService: MasterService,
    private uiSweetAlertService: UiSweetAlertService,
    private fb: FormBuilder,
    private commonService: CommonService,
  ) {
    super(null, applicationService, changeDetection, viewContainerRef);
    this.RegionObs = {
      next: (data: any) => {
        this.HandleRegionResp(data);
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  public ngOnInit() {
    super.ngOnInit();
    this.CreateRegionForm();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    forkJoin([
        this.GetAllRegion(1, this.RegionConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }),
        this.commonService.GetAllState(1)
      ]).subscribe(
        (data:any[]) => {
          this.HandleRegionResp(data[0]);
          this.LocationData.StateList = data[1];
        },
        (error) => {
          this.HideLoader();
        }
      )
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllRegion(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.RegionObs
      );
    });
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }
  private HandleRegionResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.uiDataTableComponent.ConstructRow([], 0);
    }
    else {
      this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
    }
  }
  private CreateRegionForm() {
    this.RegionForm = this.fb.group({
      Name: ['', [Validators.required]],
      IsDoNotUse: [false],
      Id: [],
      StateId: ['', [CustomValidator.DropdownValidation]],
      CityId: ['', [CustomValidator.DropdownValidation]],
    });
    this.LocationData = new LocationDropdownWithoutCountry(this.RegionForm, this.commonService);
  }

  private GetAllRegion(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllRegionData(paginationModel);
  }

  public OpenRegionPopup(isAddMode: boolean, data: RegionModel) {
    this.isAddMode = isAddMode;
    if (isAddMode) {
      this.RegionForm.reset();
      this.RegionForm.patchValue(new RegionModel());
      this.RegionForm.get("StateId").patchValue(this.LocationData.StateList[0].StateId); // needs to be change
    }
    else {
      this.m_editData = data;
      this.RegionForm.reset();
      this.LocationData.StateValue = data.StateId;
      this.LocationData.CityValue = data.CityId;
      this.RegionForm.patchValue(data);
      this.empDropdown.SetSelectedValue({ Text: this.m_editData.RegionHeadName, Value: this.m_editData.RegionHead });
      this.changeDetection.markForCheck();
    }
    this.OpenModelPopup("#addRegion");
  }

  public RegionOp() {
    this.ShowLoader();
    if (this.RegionForm.invalid) {
      this.triggerValidation(this.RegionForm);
      this.HideLoader();
      return;
    }
    let model: RegionModel = this.RegionForm.value as RegionModel;
    let selectedUnitHead = this.empDropdown.GetSelectedValue();
    if (!isNullOrUndefined(selectedUnitHead)) {
      model.RegionHead = Number(selectedUnitHead.Value);
    }
    let apiObs: Observable<any>;
    if (this.isAddMode) {
      apiObs = this.masterService.AddRegion(model);
    }
    else {
      apiObs = this.masterService.UpdateRegion(model);
    }
    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addRegion");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Region data saved successfully");
        this.GetAllRegion(1, this.RegionConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.RegionObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

  public OnCitySelect(city: City) {
    if (this.RegionForm.get("CityId").value != city.CityId) {
      this.RegionForm.get("CityId").patchValue(city.CityId);
    }
  }

  public OnStateSelect(state: State) {
    if (this.RegionForm.get("StateId").value != state.StateId) {
      this.RegionForm.get("StateId").patchValue(state.StateId);
    }
  }

}
