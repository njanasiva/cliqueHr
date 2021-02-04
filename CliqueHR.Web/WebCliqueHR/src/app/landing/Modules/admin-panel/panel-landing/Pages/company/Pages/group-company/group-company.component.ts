import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens, MultiSelectUtil } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { isNullOrUndefined } from 'util';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { CompanyService } from '../../company.service';
import { forkJoin } from 'rxjs';
import { GetDateInString, ValidationBuilder, GetUTCDateFromString } from 'src/Application/Utilitis/ApplicationUtility';
import { CustomValidator } from 'src/Application/Utilitis/CustomValidator';
import { State, City, LocationDropdown, Country } from 'src/app/landing/Modules/common-model';
import { ValidationMessage } from '../../validation-message';
import { GroupCompanyBase, AddGroupCompany, ViewGroupCompany, EditGroupCompany, GroupCompany } from '../../company-model';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';

@Component({
  selector: 'app-group-company',
  templateUrl: './group-company.component.html',
  styleUrls: ['./group-company.component.css']
})
export class GroupCompanyComponent extends WebComponents.ApplicationComponent implements OnInit {

  private GroupCompanyForm: FormGroup;
  public LocationData: LocationDropdown;
  public CompanyType = new Array<any>();
  public validationMassages = ValidationMessage.data;
  public groupCompanyVM: GroupCompanyBase;
  public DateValidation: string = '';

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private uiSweetAlertService:UiSweetAlertService,
    private commonService: CommonService,
    private companyService: CompanyService,
    private fb: FormBuilder
  ) {
    super(Components.GroupCompanyComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateGroupCompanyForm();
    this.groupCompanyVM = new ViewGroupCompany(this.GroupCompanyForm);
    this.LocationData.SetEditable(!this.groupCompanyVM.isDiabled);
    this.ShowLoader();
    forkJoin([
      this.commonService.GetAllCountry(),
      this.commonService.GetAllCompanyType(),
      this.companyService.GetGroupCompany()
    ]).subscribe(
      (data: any) => {
        this.HideLoader();
        this.LocationData.CountryList = data[0];
        this.GroupCompanyForm.get("CountryId").patchValue(this.LocationData.CountryList[0].CountryId);
        this.CompanyType = data[1];
        this.GroupCompanyForm.get("TypeId").patchValue(this.CompanyType[0].TypeId);
        if (isNullOrUndefined(data[2])) {
          this.groupCompanyVM = new AddGroupCompany(this.GroupCompanyForm);
          this.LocationData.SetEditable(!this.groupCompanyVM.isDiabled);
        }
        else{
          let model:GroupCompany = data[2];
          model.IncorporationDate = GetDateInString(new Date(model.IncorporationDate));
          this.GroupCompanyForm.patchValue(model);
          this.LocationData.CountryValue = model.CountryId;
          this.LocationData.StateValue = model.StateId;
          this.LocationData.CityValue = model.CityId;
        }
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      }
    )
  }

  private CreateGroupCompanyForm() {
    this.GroupCompanyForm = this.fb.group({
      Id: [],
      Name: ['', [Validators.required]],
      Code: ['', [Validators.required]],
      TypeId: ['', [CustomValidator.DropdownValidation]],
      IncorporationDate: [GetDateInString(new Date()), [Validators.required]],
      Address: ['', [Validators.required]],
      CountryId: ['', [CustomValidator.DropdownValidation]],
      StateId: ['', [CustomValidator.DropdownValidation]],
      CityId: ['', [CustomValidator.DropdownValidation]],
      PinCode: ['', [Validators.required]],
      ContcatNo: ['', [Validators.required]],
      WebSite: ['', [Validators.required]],
      PAN: [''],
      TAN: [''],
      GSTIN: [''],
      PF: [''],
      ESIC: [''],
    });
    this.LocationData = new LocationDropdown(this.GroupCompanyForm, this.commonService);
  }

  public OnCitySelect(city: City) {
    this.GroupCompanyForm.get("CityId").patchValue(city.CityId);
  }
  public OnStateSelect(state: State) {
    this.GroupCompanyForm.get("StateId").patchValue(state.StateId);
  }
  public OnCountrySelect(country: Country) {
    this.GroupCompanyForm.get("CountryId").patchValue(country.CountryId);
  }

  public EditData() {
    if (this.groupCompanyVM instanceof EditGroupCompany) {
      this.groupCompanyVM = new ViewGroupCompany(this.GroupCompanyForm);
      this.groupCompanyVM.ActivateSave();
    }
    else {
      this.groupCompanyVM = new EditGroupCompany(this.GroupCompanyForm);
    }
    this.LocationData.SetEditable(!this.groupCompanyVM.isDiabled);
  }

  public SaveData(){
    this.ShowLoader();
    this.DateValidation = '';
    console.log(this.GroupCompanyForm.value);
    if (this.GroupCompanyForm.invalid) {
      this.triggerValidation(this.GroupCompanyForm);
      this.DateValidation = this.GroupCompanyForm.get("IncorporationDate").invalid ? 'Incorporation Date Required' : '';
      this.HideLoader();
      return;
    }
    let model:GroupCompany = this.GroupCompanyForm.value;
    model.IncorporationDate = GetUTCDateFromString(this.GroupCompanyForm.get("IncorporationDate").value);
    console.log(model);
    this.companyService.AddUpdateGroupCompany(model).subscribe(
      (data:any) => {
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Data saved successfully");
        this.groupCompanyVM = new ViewGroupCompany(this.GroupCompanyForm);
        this.LocationData.SetEditable(!this.groupCompanyVM.isDiabled);
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }
}
