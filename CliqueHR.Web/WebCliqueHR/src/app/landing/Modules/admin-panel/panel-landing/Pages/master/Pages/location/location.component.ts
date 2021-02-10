import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observer, forkJoin, Observable } from 'rxjs';
import { ValidationMessage } from '../../validation-message';
import { MasterService } from '../../master.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { isNullOrUndefined } from 'util';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { CenterTypeModel, Location } from '../../master-model';
import { LocationDropdown, City, State, Country } from 'src/app/landing/Modules/common-model';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { CustomValidator } from 'src/Application/Utilitis/CustomValidator';
import { ApplicationEmpSingleDDComponent } from 'src/Application/Components/application-emp-single-dd/application-emp-single-dd.component';

@Component({
  selector: 'app-location',
  templateUrl: './location.component.html',
  styleUrls: ['./location.component.css']
})
export class LocationComponent extends WebComponents.ApplicationComponent implements OnInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  @ViewChild(ApplicationEmpSingleDDComponent, { static: true, read: ApplicationEmpSingleDDComponent })
  private empDropdown: ApplicationEmpSingleDDComponent;

  private m_editData: Location;


  public LocationConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '' },
      { fieldId: 'Name', fieldName: 'Location Name', columnClass: 'text-nowrap' },
      { fieldId: 'Code', fieldName: 'Location Code', columnClass: 'text-nowrap' },
      { fieldId: 'CenterTypeName', fieldName: 'Centre Type', columnClass: 'text-nowrap' },
      { fieldId: 'Address', fieldName: 'Address' },
      { fieldId: 'Phone', fieldName: 'Phone', columnClass: 'text-nowrap' },
      { fieldId: 'LocHeadName', fieldName: 'Location Head', columnClass: 'text-nowrap' },
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };
  public LocationList: Array<Location>;
  public isAddMode: boolean;
  public validationMassages = ValidationMessage.data;
  public LocationForm: FormGroup;
  public CenterTypeList: CenterTypeModel[];
  public LocationData: LocationDropdown;

  private LocationObs: Observer<any>;


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
    this.LocationObs = {
      next: (data: any) => {
        this.HandleLocationResp(data);
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  public ngOnInit() {
    this.CreateLocationForm();
  }

  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    forkJoin([
      this.masterService.GetCenterType(),
      this.GetAllLocation(1, this.LocationConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }),
      this.commonService.GetAllCountry(),
    ]).subscribe(
      (data: any[]) => {
        this.CenterTypeList = data[0] || [];
        this.HandleLocationResp(data[1]);
        this.LocationData.CountryList = data[2];
      },
      (error) => {
        this.HideLoader();
      }
    )
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllLocation(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.LocationObs
      );
    });
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }
  private CreateLocationForm() {
    this.LocationForm = this.fb.group({
      Id: [],
      Name: ['', [Validators.required]],
      Code: ['', [Validators.required]],
      CenterType: [''],
      IsRegisteredOffice: [false],
      Address: ['', [Validators.required]],
      PinCode: ['', [Validators.required]],
      Phone: ['', [Validators.required, Validators.maxLength(10)]],
      IsDoNotUse: [false],
      CountryId: ['', [CustomValidator.DropdownValidation]],
      StateId: ['', [CustomValidator.DropdownValidation]],
      CityId: ['', [CustomValidator.DropdownValidation]],
    });
    this.LocationData = new LocationDropdown(this.LocationForm, this.commonService);
  }

  private HandleLocationResp(data: any) {
    this.HideLoader();
    if (isNullOrUndefined(data)) {
      this.uiDataTableComponent.ConstructRow([], 0);
    }
    else {
      this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
    }
  }

  private GetAllLocation(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllLocation(paginationModel);
  }

  public OpenLocationPopup(isAddMode: boolean, data: Location) {
    this.isAddMode = isAddMode;
    if (isAddMode) {
      this.LocationForm.reset();
      this.LocationForm.patchValue(new Location());
      this.LocationForm.get("CountryId").patchValue(this.LocationData.CountryList[0].CountryId);
      if (!isNullOrUndefined(this.CenterTypeList)) {
        this.LocationForm.get("CenterType").patchValue(this.CenterTypeList[0].Id);
      }
    }
    else {
      this.m_editData = data;
      this.LocationForm.reset();
      this.LocationData.CountryValue = data.CountryId;
      this.LocationData.StateValue = data.StateId;
      this.LocationData.CityValue = data.CityId;
      this.LocationForm.patchValue(data);
      this.empDropdown.SetSelectedValue({ Text: this.m_editData.LocHeadName, Value: this.m_editData.LocHeadEmpId });
      this.changeDetection.markForCheck();
    }
    this.OpenModelPopup("#addLocation");
  }

  public LocationOp() {
    this.ShowLoader();
    if (this.LocationForm.invalid) {
      this.triggerValidation(this.LocationForm);
      this.HideLoader();
      return;
    }
    let model: Location = this.LocationForm.value as Location;
    let apiObs: Observable<any>;
    let selectedUnitHead = this.empDropdown.GetSelectedValue();
    if (!isNullOrUndefined(selectedUnitHead)) {
      model.LocHeadEmpId = Number(selectedUnitHead.Value);
    }
    if (this.isAddMode) {
      apiObs = this.masterService.AddLocation(model);
    }
    else {
      apiObs = this.masterService.UpdateLocation(model);
    }
    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addLocation");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Brand data saved successfully");
        this.GetAllLocation(1, this.LocationConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.LocationObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

  public OnCitySelect(city: City) {
    if (this.LocationForm.get("CityId").value != city.CityId) {
      this.LocationForm.get("CityId").patchValue(city.CityId);
    }
  }
  public OnStateSelect(state: State) {
    if (this.LocationForm.get("StateId").value != state.StateId) {
      this.LocationForm.get("StateId").patchValue(state.StateId);
    }
  }
  public OnCountrySelect(country: Country) {
    if (this.LocationForm.get("CountryId").value != country.CountryId) {
      this.LocationForm.get("CountryId").patchValue(country.CountryId);
    }
  }
}
