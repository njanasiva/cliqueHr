import { Component, OnInit, Inject, ChangeDetectorRef, ViewChild, AfterViewInit, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens, MultiSelectUtil } from 'src/Application/Types/types.api';
import { Components, ValidationType, FileType } from 'src/Application/Types/Constants';
import { UiDataTableConfig, SortType, UiDatepicker } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { Observer, forkJoin } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { CompanyService } from '../../company.service';
import { CustomValidator } from 'src/Application/Utilitis/CustomValidator';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { LocationDropdown, Country, State, City } from 'src/app/landing/Modules/common-model';
import { Entity } from '../../company-model';
import { ValidationBuilder, GetDateInString, GetUTCDateFromString } from 'src/Application/Utilitis/ApplicationUtility';
import { ValidationMessage } from '../../validation-message';

@Component({
  selector: 'app-entity',
  templateUrl: './entity.component.html',
  styleUrls: ['./entity.component.css']
})
export class EntityComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChild(UiDataTableComponent, { static: true, read: UiDataTableComponent })
  private uiDataTableComponent: UiDataTableComponent;
  private EntityObs: Observer<any>;
  private _selectedImage: any;
  private fileValidation: WebInterface.FileValidationConfig = {
    AllowedExtentions: [FileType.JPEG, FileType.JPG, FileType.GIF]
  };
  private fileValidationMessage = {
    [ValidationType.InvalidFile]: "Invalid File."
  };
  
  public entityForm: FormGroup;
  public validationMassages = ValidationMessage.data;
  public fileValidations: string;
  public isAddMode: boolean;
  public CompanyType = new Array<any>();
  public LocationData: LocationDropdown;
  public editData:Entity =  new Entity();
  public restoGridConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '' },
      { fieldId: 'Name', fieldName: 'Entity Name', columnClass: 'text-nowrap' },
      { fieldId: 'Code', fieldName: 'Entity Code', columnClass: 'text-nowrap' },
      { fieldId: 'WebSite', fieldName: 'Website', columnClass: 'text-nowrap' },
      { fieldId: 'TypeName', fieldName: 'Company Type', columnClass: 'text-nowrap' },
      { fieldId: 'IncorporationDate', fieldName: 'Incorporation Date', columnClass: 'text-nowrap' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };
  public incorporationDateConfig: UiDatepicker = {
    isEditable: true,
    disabled: false,
    max: GetDateInString(new Date())
  }
  public DateValidation: string;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private componyService: CompanyService,
    private commonService: CommonService,
    private uiSweetAlertService: UiSweetAlertService,
  ) {
    super(Components.EntityComponent, applicationService, changeDetection, viewContainerRef);
    this.EntityObs = {
      next: (data: any) => {
        this.HideLoader();
        if (isNullOrUndefined(data)) {
          this.uiDataTableComponent.ConstructRow([], 0);
        }
        else {
          this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
        }
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }
  ngOnInit() {
    this.CreateEntityForm();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    forkJoin([
      this.commonService.GetAllCountry(),
      this.commonService.GetAllCompanyType(),
      this.GetAllEntity(1, this.restoGridConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' })
    ]).subscribe(
      (data: any) => {
        this.HideLoader();
        this.LocationData.CountryList = data[0];
        this.CompanyType = data[1];

        if (isNullOrUndefined(data[2])) {
          this.uiDataTableComponent.ConstructRow([], 0);
        }
        else {
          this.uiDataTableComponent.ConstructRow(data[2].Data, data[2].Total);
        }
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      }
    )
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllEntity(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.EntityObs
      )
    });
  }
  private CreateEntityForm() {
    this.entityForm = this.fb.group({
      Id: [0],
      Name: ['', [Validators.required]],
      Code: ['', [Validators.required]],
      TypeId: ['', [CustomValidator.DropdownValidation]],
      IncorporationDate: [new Date(), [Validators.required]],
      Address: ['', [Validators.required]],
      CountryId: ['', [CustomValidator.DropdownValidation]],
      StateId: ['', [CustomValidator.DropdownValidation]],
      CityId: ['', [CustomValidator.DropdownValidation]],
      PinCode: ['', [Validators.required]],
      ContcatNo: ['', [Validators.required, Validators.maxLength(10)]],
      WebSite: ['', [Validators.required]],
      PAN: [''],
      TAN: [''],
      GSTIN: [''],
      PF: [''],
      ESIC: [''],
      IsDoNotUse: [false]
    });
    this.LocationData = new LocationDropdown(this.entityForm, this.commonService);
  }
  private GetAllEntity(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.componyService.GetEntity(paginationModel);
  }

  public OnEditClick(Id: number) {
    this.ShowLoader()
    this.editData = new Entity();
    this.componyService.GetEntityById(Id).subscribe(
      (data: Entity) => {
        this.HideLoader();
        this.isAddMode = false;
        this.editData = data;
        this.entityForm.get("Name").disable();
        this.entityForm.get("Code").disable();
        this.entityForm.get("Id").setValidators([Validators.required]);
        this.entityForm.patchValue(data);
        this.entityForm.get("IncorporationDate").setValue(GetDateInString(new Date(data.IncorporationDate)));
        this.OpenModelPopup('#addEntity');
      },
      (error: any) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  public OnAddClick() {
    this.isAddMode = true;
    this.entityForm.reset();
    this.entityForm.patchValue(new Entity());
    this.entityForm.get("Name").enable();
    this.entityForm.get("Code").enable();
    this.entityForm.get("CountryId").patchValue(this.LocationData.CountryList[0].CountryId);
    this.entityForm.get("TypeId").patchValue(this.CompanyType[0].TypeId);
    this.entityForm.get("IncorporationDate").setValue(GetDateInString(new Date()));
    this.OpenModelPopup('#addEntity');
  }

  public OnAddEntity() {
    this.ShowLoader();
    this.DateValidation = '';
    if (this.entityForm.invalid || !isNullOrUndefined(this.fileValidations)) {
      this.triggerValidation(this.entityForm);
      this.DateValidation = this.entityForm.get("IncorporationDate").invalid ? 'Incorporation Date Required' : '';
      this.HideLoader();
      return;
    }
    let model: Entity = this.entityForm.value;
    model.IncorporationDate = GetUTCDateFromString(this.entityForm.get("IncorporationDate").value);
    this.componyService.AddEntity(model, this._selectedImage).subscribe(
      (data: any) => {
        this.GetAllEntity(1, this.restoGridConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.EntityObs
        );
        this.CloseModelPopup('#addEntity');
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  public OnEditEntity() {
    this.ShowLoader();
    this.DateValidation = '';
    if (this.entityForm.invalid || !isNullOrUndefined(this.fileValidations)) {
      this.triggerValidation(this.entityForm);
      this.DateValidation = this.entityForm.get("IncorporationDate").invalid ? 'Incorporation Date Required' : '';
      this.HideLoader();
      return;
    }
    let model: Entity = this.entityForm.value;
    model.IncorporationDate = GetUTCDateFromString(this.entityForm.get("IncorporationDate").value);
    this.componyService.UpdateEntity(model, this._selectedImage).subscribe(
      (data: any) => {
        this.GetAllEntity(1, this.restoGridConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.EntityObs
        );
        this.CloseModelPopup('#addEntity');
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  public OnCitySelect(city: City) {
    this.entityForm.get("CityId").patchValue(city.CityId);
  }
  public OnStateSelect(state: State) {
    this.entityForm.get("StateId").patchValue(state.StateId);
  }
  public OnCountrySelect(country: Country) {
    this.entityForm.get("CountryId").patchValue(country.CountryId);
  }

  public SelectImage(event) {
    this.fileValidations = undefined;
    this._selectedImage = undefined;
    if (event.target.files && event.target.files[0]) {
      this._selectedImage = event.target.files[0];
      this.fileValidations = this.fileValidationMessage[this.ValidateFile(this.fileValidation, this._selectedImage)];
    }
  }
}
