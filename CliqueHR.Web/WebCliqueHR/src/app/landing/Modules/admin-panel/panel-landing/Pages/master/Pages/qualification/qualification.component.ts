import { Component, OnInit, Inject, ChangeDetectorRef, AfterViewInit, ViewChild, QueryList, ViewChildren, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { CourceTypeDTConfig, CourceTitleDTConfig, MajorDTConfig, UniversityDTConfig, InstituteDTConfig, Dereference } from './qualification-model';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { isNullOrUndefined } from 'util';
import { CustomValidator } from 'src/Application/Utilitis/CustomValidator';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { forkJoin, Observable, of } from 'rxjs';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { Country, State, City, LocationDropdown } from 'src/app/landing/Modules/common-model';
import { ValidationMessage } from '../../validation-message';
import { MasterService } from '../../master.service';

@Component({
  selector: 'app-qualification',
  templateUrl: './qualification.component.html',
  styleUrls: ['./qualification.component.css']
})
export class QualificationComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public validationMassages = ValidationMessage.data;
  public popupHeading: string = '';

  public CourseTypeList = new Array<any>();
  public CourseTitleList = new Array<any>();
  public MajorList = new Array<any>();
  public UniversityList = new Array<any>();
  public InstituteList = new Array<any>();
  public LocationData: LocationDropdown;

  public courseTypeForm: FormGroup;
  public courseTitleForm: FormGroup;
  public courseMajorForm: FormGroup;
  public courseUniversityForm: FormGroup;
  public courseInstituteForm: FormGroup;
  public locationForm: FormGroup;
  public isAddMode: boolean = true;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private masterService: MasterService,
    private uiSweetAlertService:UiSweetAlertService,
    private commonService:CommonService
  ) {
    super(Components.QualificationComponent, applicationService, changeDetection, viewContainerRef);
  }

  public ngOnInit() {
    this.CreateCourseType();
    this.CreateCourseTitle();
    this.CreateMajor();
    this.CreateUniversity();
    this.CreateInstitute();
    this.CreateLoaction();
  }
  public ngAfterViewInit(): void {
    super.ngAfterViewInit();
    //CourceType
    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
        this.GetCourseTypeList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.HandleCourseTypeResp(data);
          },
          (error) => {
            console.log(error);
          });;
      })
    );
    //CourceTitle
    this.uiDataTableComponent.toArray()[1].fetchObs.subscribe(x => {
      this.GetCourseTitleList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        (data: any) => {
          this.HandleCourseTitleResp(data);
        },
        (error) => {
          console.log(error);
        });;
    });
    //Major
    this.uiDataTableComponent.toArray()[2].fetchObs.subscribe(x => {
      this.GetMajorList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        (data: any) => {
          this.HandleMajorResp(data);
        },
        (error) => {
          console.log(error);
        });;
    });
    //University
    this.uiDataTableComponent.toArray()[3].fetchObs.subscribe(x => {
      this.GetUniversityList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        (data: any) => {
          this.HandleUniversityResp(data);
        },
        (error) => {
          console.log(error);
        });;
    });
    //Institute
    this.uiDataTableComponent.toArray()[4].fetchObs.subscribe(x => {
      this.GetUniversityList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        (data: any) => {
          this.HandleUniversityResp(data);
        },
        (error) => {
          console.log(error);
        });;
    });
    this.ShowLoader();
    forkJoin([
      this.GetCourseTypeList(1, this.CourseTypeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }),
      this.GetCourseTitleList(1, this.CourseTitleConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }),
      this.GetMajorList(1, this.MajorConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }),
      this.commonService.GetAllCountry(),
      this.GetUniversityList(1, this.UniversityConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }),
      this.GetInstituteList(1, this.InstituteConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }),
    ]).subscribe(
      (data: any) => {
        this.HideLoader();
        this.HandleCourseTypeResp(data[0]);
        this.HandleCourseTitleResp(data[1]);
        this.HandleMajorResp(data[2]);
        this.HandleCountryResp(data[3]);
        this.HandleUniversityResp(data[4]);
        this.HandleInstituteResp(data[5]);
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      });
  }
  private HandleCountryResp(data:Array<Country>){
    this.LocationData.CountryList = data;
      this.locationForm.get("CountryId").patchValue(this.LocationData.CountryList[0].CountryId);
  }
  private SetupPopupData(popupId: string, isAddModel: boolean, index: any) {
    this.isAddMode = isAddModel;
    switch (popupId) {
      case "#CourseTypePopup":
        // Specific Code
        this.CourseType(isAddModel, index);
        break;
      case "#CourseTitlePopup":
        // Specific Code
        this.CourseTitle(isAddModel, index);
        break;
      case "#MajorPopup":
        // Specific Code
        this.Major(isAddModel, index);
        break;
      case "#UniversityPopup":
        // Specific Code
        this.University(isAddModel, index);
        break;
      case "#InstitutePopup":
        // Specific Code
        this.Institute(isAddModel, index);
        break;
      default:
        break;
    }
  }
  private GetCourseTypeList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllCourseType(paginationModel);
  }
  private HandleCourseTypeResp(data: any) {
    if (isNullOrUndefined(data)) {
      this.CourseTypeList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.CourseTypeList = data.Data || [];
      this.uiDataTableComponent.toArray()[0].ConstructRow(data.Data, data.Total);
    }
  }
  private CourseType(isAddModel: boolean, index) {
    this.CreateCourseType();
    if (isAddModel) {
      this.courseTypeForm.reset();
      this.popupHeading = "Add Course Type";
    }
    else {
      this.popupHeading = "Edit Course Type";
      this.courseTypeForm.patchValue(this.CourseTypeList[index]);
    }
  }
  private CreateCourseType() {
    if (isNullOrUndefined(this.courseTypeForm)) {
      this.courseTypeForm = this.fb.group({
        TypeName: ['', [Validators.required]],
        IsDoNotUse: [false],
        Id: []
      });
    }
  }

  private GetCourseTitleList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllCourseTitle(paginationModel);
  }
  private HandleCourseTitleResp(data: any) {
    if (isNullOrUndefined(data)) {
      this.CourseTitleList = [];
      this.uiDataTableComponent.toArray()[1].ConstructRow([], 0);
    }
    else {
      this.CourseTitleList = data.Data || [];
      this.uiDataTableComponent.toArray()[1].ConstructRow(data.Data, data.Total);
    }
  }
  private CourseTitle(isAddModel: boolean, index) {
    this.CreateCourseTitle();
    let type = this.CourseTypeList[0] || {};
    if (isAddModel) {
      this.courseTitleForm.reset();
      this.courseTitleForm.get('CourseTypeId').patchValue(type.Id || '');
      this.popupHeading = "Add Course Title";
    }
    else {
      this.popupHeading = "Edit Course Title";
      this.courseTitleForm.patchValue(this.CourseTitleList[index]);
    }
  }
  private CreateCourseTitle() {
    if (isNullOrUndefined(this.courseTitleForm)) {
      this.courseTitleForm = this.fb.group({
        TitleName: ['', [Validators.required]],
        CourseTypeId: ['', [CustomValidator.DropdownValidation]],
        IsDoNotUse: [false],
        Id: []
      });
    }
  }
  private Major(isAddModel: boolean, index) {
    this.CreateMajor();
    let title = this.CourseTitleList[0] || {};
    if (isAddModel) {
      this.courseMajorForm.reset();
      this.courseMajorForm.get('TitleId').patchValue(title.Id || '');
      this.popupHeading = "Add Major";
    }
    else {
      this.popupHeading = "Edit Major";
      this.courseMajorForm.patchValue(this.MajorList[index]);
    }
  }
  private GetMajorList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllMajor(paginationModel);
  }
  private HandleMajorResp(data: any) {
    if (isNullOrUndefined(data)) {
      this.MajorList = [];
      this.uiDataTableComponent.toArray()[2].ConstructRow([], 0);
    }
    else {
      this.MajorList = data.Data || [];
      this.uiDataTableComponent.toArray()[2].ConstructRow(data.Data, data.Total);
    }
  }
  private CreateMajor() {
    if (isNullOrUndefined(this.courseMajorForm)) {
      this.courseMajorForm = this.fb.group({
        MajorName: ['', [Validators.required]],
        TitleId: ['', [CustomValidator.DropdownValidation]],
        IsDoNotUse: [false],
        Id:[]
      });
    }
  }
  private University(isAddModel: boolean, index) {
    this.CreateUniversity();
    if (isAddModel) {
      this.courseUniversityForm.reset();
      this.LocationData.CountryList= Dereference<Array<Country>>(this.LocationData.CountryList);
      this.locationForm.get("CountryId").patchValue(this.LocationData.CountryList[0].CountryId);
      this.LocationData.CountryValue = this.LocationData.CountryList[0].CountryId;
      this.popupHeading = "Add University";
    }
    else {
      this.popupHeading = "Edit University";
      this.LocationData.CountryValue = this.UniversityList[index].CountryId;
      this.LocationData.StateValue = this.UniversityList[index].StateId;
      this.LocationData.CityValue = this.UniversityList[index].CityId;
      this.locationForm.get("CountryId").patchValue(this.UniversityList[index].CountryId);
      this.locationForm.get("StateId").patchValue(this.UniversityList[index].StateId);
      this.locationForm.get("CityId").patchValue(this.UniversityList[index].CityId);
      this.courseUniversityForm.patchValue(this.UniversityList[index]);
    }
  }
  private GetUniversityList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllUniversity(paginationModel);
  }
  private HandleUniversityResp(data: any) {
    if (isNullOrUndefined(data)) {
      this.UniversityList = [];
      this.uiDataTableComponent.toArray()[3].ConstructRow([], 0);
    }
    else {
      this.UniversityList = data.Data || [];
      this.uiDataTableComponent.toArray()[3].ConstructRow(data.Data, data.Total);
    }
  }
  private CreateUniversity() {
    if (isNullOrUndefined(this.courseUniversityForm)) {
      this.courseUniversityForm = this.fb.group({
        UniversityId:[],
        UniversityName: ['', [Validators.required]],
        CountryId: ['', [CustomValidator.DropdownValidation]],
        StateId: ['', [CustomValidator.DropdownValidation]],
        CityId: ['', [CustomValidator.DropdownValidation]],
        IsBlacklist:[false],
        IsDoNotUse: [false],
        Id:[]
      });
    }
  }
  private Institute(isAddModel: boolean, index) {
    this.CreateInstitute();
    let title = this.UniversityList[0] || {};
    if (isAddModel) {
      this.courseInstituteForm.reset();
      this.courseInstituteForm.get('UniversityId').patchValue(title.Id || '');
      this.LocationData.CountryList= Dereference<Array<Country>>(this.LocationData.CountryList);
      this.locationForm.get("CountryId").patchValue(this.LocationData.CountryList[0].CountryId);
      this.LocationData.CountryValue = this.LocationData.CountryList[0].CountryId;
      this.popupHeading = "Add Institute";
    }
    else {
      this.popupHeading = "Edit Institute";
      this.LocationData.CountryValue = this.InstituteList[index].CountryId;
      this.LocationData.StateValue = this.InstituteList[index].StateId;
      this.LocationData.CityValue = this.InstituteList[index].CityId;
      this.locationForm.get("CountryId").patchValue(this.InstituteList[index].CountryId);
      this.locationForm.get("StateId").patchValue(this.InstituteList[index].StateId);
      this.locationForm.get("CityId").patchValue(this.InstituteList[index].CityId);
      this.courseInstituteForm.patchValue(this.InstituteList[index]);
    }
  }
  private GetInstituteList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllInstitute(paginationModel);
  }
  private HandleInstituteResp(data: any) {
    if (isNullOrUndefined(data)) {
      this.InstituteList = [];
      this.uiDataTableComponent.toArray()[4].ConstructRow([], 0);
    }
    else {
      this.InstituteList = data.Data || [];
      this.uiDataTableComponent.toArray()[4].ConstructRow(data.Data, data.Total);
    }
  }
  private CreateInstitute() {
    if (isNullOrUndefined(this.courseInstituteForm)) {
      this.courseInstituteForm = this.fb.group({
        InstituteName: ['', [Validators.required]],
        UniversityId: ['', [CustomValidator.DropdownValidation]],
        CountryId: ['', [CustomValidator.DropdownValidation]],
        StateId: ['', [CustomValidator.DropdownValidation]],
        CityId: ['', [CustomValidator.DropdownValidation]],
        IsBlacklist:[false],
        IsDoNotUse: [false],
        Id:[]
      });
    }
  }


  private CreateLoaction() {
    if (isNullOrUndefined(this.locationForm)) {
      this.locationForm = this.fb.group({
        CountryId: ['', [CustomValidator.DropdownValidation]],
        StateId: ['', [CustomValidator.DropdownValidation]],
        CityId: ['', [CustomValidator.DropdownValidation]]
      });
      this.LocationData = new LocationDropdown(this.locationForm, this.commonService);
    }
  }


  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }
  public OnCourseTypeOp() {
    this.ShowLoader();
    if (this.courseTypeForm.invalid) {
      this.triggerValidation(this.courseTypeForm);
      this.HideLoader();
      return;
    }
    let courseType: Observable<any>;
    if (this.isAddMode) {
      courseType = this.masterService.AddCourseType(this.courseTypeForm.value);
    }
    else {
      courseType = this.masterService.UpdateCourseType(this.courseTypeForm.value);
    }
    courseType.subscribe(
      (data: any) => {
        this.CloseModelPopup('#CourseTypePopup');
        this.GetCourseTypeList(1, this.CourseTypeConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          (data: any) => {
            this.HideLoader();
            this.HandleCourseTypeResp(data);
          },
          (error) => {
            this.HideLoader();
          }
        )
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }
  public OnCourseTitleOp() {
    this.ShowLoader();
    if (this.courseTitleForm.invalid) {
      this.triggerValidation(this.courseTitleForm);
      this.HideLoader();
      return;
    }
    let courseTitle: Observable<any>;
    if (this.isAddMode) {
      courseTitle = this.masterService.AddCourseTitle(this.courseTitleForm.value);
    }
    else {
      courseTitle = this.masterService.UpdateCourseTitle(this.courseTitleForm.value);
    }
    courseTitle.subscribe(
      (data: any) => {
        this.CloseModelPopup('#CourseTitlePopup');
        this.GetCourseTitleList(1, this.CourseTitleConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          (data: any) => {
            this.HideLoader();
            this.HandleCourseTitleResp(data);
          },
          (error) => {
            this.HideLoader();
          }
        )
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }
  public OnMajorOp() {
    this.ShowLoader();
    if (this.courseMajorForm.invalid) {
      this.triggerValidation(this.courseMajorForm);
      this.HideLoader();
      return;
    }
    let courseMajor: Observable<any>;
    if (this.isAddMode) {
      courseMajor = this.masterService.AddMajor(this.courseMajorForm.value);
    }
    else {
      courseMajor = this.masterService.UpdateMajor(this.courseMajorForm.value);
    }
    courseMajor.subscribe(
      (data: any) => {
        this.CloseModelPopup('#MajorPopup');
        this.GetMajorList(1, this.MajorConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          (data: any) => {
            this.HideLoader();
            this.HandleMajorResp(data);
          },
          (error) => {
            this.HideLoader();
          }
        )
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  public OnUniversityOp() {
    this.ShowLoader();
    this.courseUniversityForm.get("CountryId").patchValue(this.locationForm.get("CountryId").value);
    this.courseUniversityForm.get("StateId").patchValue(this.locationForm.get("StateId").value);
    this.courseUniversityForm.get("CityId").patchValue(this.locationForm.get("CityId").value);
    if (this.courseUniversityForm.invalid) {
      this.triggerValidation(this.courseUniversityForm);
      this.HideLoader();
      return;
    }
    let courseUniversity: Observable<any>;
    if (this.isAddMode) {
      courseUniversity = this.masterService.AddUniversity(this.courseUniversityForm.value);
    }
    else {
      courseUniversity = this.masterService.UpdateUniversity(this.courseUniversityForm.value);
    }
    courseUniversity.subscribe(
      (data: any) => {
        this.CloseModelPopup('#UniversityPopup');
        this.GetUniversityList(1, this.UniversityConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          (data: any) => {
            this.HideLoader();
            this.HandleUniversityResp(data);
          },
          (error) => {
            this.HideLoader();
          }
        )
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }
  public OnInstituteOp() {
    this.ShowLoader();
    this.courseInstituteForm.get("CountryId").patchValue(this.locationForm.get("CountryId").value);
    this.courseInstituteForm.get("StateId").patchValue(this.locationForm.get("StateId").value);
    this.courseInstituteForm.get("CityId").patchValue(this.locationForm.get("CityId").value);
    if (this.courseInstituteForm.invalid) {
      this.triggerValidation(this.courseInstituteForm);
      this.HideLoader();
      return;
    }
    let courseInstitute: Observable<any>;
    if (this.isAddMode) {
      courseInstitute = this.masterService.AddInstitute(this.courseInstituteForm.value);
    }
    else {
      courseInstitute = this.masterService.UpdateInstitute(this.courseInstituteForm.value);
    }
    courseInstitute.subscribe(
      (data: any) => {
        this.CloseModelPopup('#InstitutePopup');
        this.GetInstituteList(1, this.InstituteConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          (data: any) => {
            this.HideLoader();
            this.HandleInstituteResp(data);
          },
          (error) => {
            this.HideLoader();
          }
        )
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  public get CourseTypeConfig() {
    return CourceTypeDTConfig;
  }
  public get CourseTitleConfig() {
    return CourceTitleDTConfig;
  }
  public get MajorConfig() {
    return MajorDTConfig;
  }
  public get UniversityConfig() {
    return UniversityDTConfig;
  }
  public get InstituteConfig() {
    return InstituteDTConfig;
  }
  public OnOpenPopup(PopUpID: string, isAddModel: boolean, index: number) {
    this.SetupPopupData(PopUpID, isAddModel, index);
    this.OpenModelPopup(PopUpID);
    console.log(index);
  }
  public OnClosePopup(PopUpID: string) {
    this.CloseModelPopup(PopUpID);
  }

  public OnCitySelect(city:City){
    this.locationForm.get("CityId").patchValue(city.CityId);
  }
  public OnStateSelect(state:State){
    this.locationForm.get("StateId").patchValue(state.StateId);
  }
  public OnCountrySelect(country:Country){
    this.locationForm.get("CountryId").patchValue(country.CountryId);
  }
}
