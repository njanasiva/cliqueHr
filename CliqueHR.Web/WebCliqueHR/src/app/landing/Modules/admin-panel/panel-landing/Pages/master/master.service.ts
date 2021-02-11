import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {
  CourseType, CourseTitle, Major, University,
  Institute, CurrancyMapping, CenterTypeModel, EmployeeType,
  GradeType, BandType, FunctionalRole, DesignationModel, Location, RegionModel
} from './master-model';
import { environment } from 'src/environments/environment';
import { WebInterface } from 'src/Application/Types/types.api';

@Injectable()
export class MasterService {

  constructor(private httpClient: HttpClient) { }

  /**
   * AddCourseType
   */
  public AddCourseType(courseType: CourseType) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/AddCourseType", courseType);
  }
  /**
   * GetAllCourseType
   */
  public GetAllCourseType(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/GetAllCourseType", model);
  }
  /**
   * UpdateCourseType
   */
  public UpdateCourseType(courseType: CourseType) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/UpdateCourseType", courseType);
  }

  /**
   * AddCourseTitle
   */
  public AddCourseTitle(courseTitle: CourseTitle) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/AddCourseTitle", courseTitle);
  }
  /**
  * GetAllCourseTitle
  */
  public GetAllCourseTitle(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/GetAllCourseTitle", model);
  }
  /**
   * UpdateCourseTitle
   */
  public UpdateCourseTitle(courseTitle: CourseTitle) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/UpdateCourseTitle", courseTitle);
  }

  /**
  * AddMajor
  */
  public AddMajor(major: Major) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/AddMajor", major);
  }
  /**
  * GetAllMajor
  */
  public GetAllMajor(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/GetAllMajor", model);
  }
  /**
   * UpdateMajor
   */
  public UpdateMajor(major: Major) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/UpdateMajor", major);
  }

  /**
   * AddUniversity
   */
  public AddUniversity(university: University) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/AddUniversity", university);
  }
  /**
  * GetAllUniversity
  */
  public GetAllUniversity(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/GetAllUniversity", model);
  }
  /**
   * UpdateUniversity
   */
  public UpdateUniversity(university: University) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/UpdateUniversity", university);
  }



  /**
   * AddInstitute
   */
  public AddInstitute(institute: Institute) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/AddInstitute", institute);
  }
  /**
  * GetAllInstitute
  */
  public GetAllInstitute(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/GetAllInstitute", model);
  }
  /**
   * UpdateInstitute
   */
  public UpdateInstitute(institute: Institute) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/UpdateInstitute", institute);
  }



  /**
   * GetAllCurrency
   */
  public GetAllCurrency() {
    return this.httpClient.get(environment.api_baseurl + "api/Master/GetAllCurrency");
  }
  /**
 * AddCurrencyMapping
 */
  public AddCurrencyMapping(model: CurrancyMapping) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/AddCurrencyMapping", model)
  }
  /**
   * UpdateCurrencyMapping
   */
  public UpdateCurrencyMapping(model: CurrancyMapping) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/UpdateCurrencyMapping", model)
  }
  /**
   * GetAllCurrencyMapping
   */
  public GetAllCurrencyMapping(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/GetAllCurrencyMapping", model);
  }


  /**
   * AddCenterTypeData
   */
  public AddCenterTypeData(model: CenterTypeModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/AddCenterTypeData", model)
  }
  /**
   * UpdateCenterTypeData
   */
  public UpdateCenterTypeData(model: CenterTypeModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/UpdateCenterTypeData", model)
  }

  /**
   * GetAllCenterType
   */
  public GetAllCenterType(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/GetAllCenterType", model);
  }


  /**
   * GetCenterType
   */
  public GetCenterType() {
    return this.httpClient.get(environment.api_baseurl + "api/Employee/GetCenterType");
  }


  /**
   * AddEmployeeType
   */
  public AddEmployeeType(model: EmployeeType) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/AddEmployeeType", model)
  }
  /**
   * UpdateEmployeeType
   */
  public UpdateEmployeeType(model: EmployeeType) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/UpdateEmployeeType", model)
  }

  /**
   * GetAllEmployeeType
   */
  public GetAllEmployeeType(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/GetAllEmployeeType", model);
  }


  /**
   * AddGradeType
   */
  public AddGradeType(model: GradeType) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/AddGradeType", model)
  }
  /**
   * UpdateGradeType
   */
  public UpdateGradeType(model: GradeType) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/UpdateGradeType", model)
  }

  /**
   * GetAllGradeType
   */
  public GetAllGradeType(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/GetAllGradeType", model);
  }

  /**
   * GetGradeList
   */
  public GetGradeList() {
    return this.httpClient.get(environment.api_baseurl + "api/Employee/GetGradeList");
  }


  /**
   * AddBandType
   */
  public AddBandType(model: BandType) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/AddBandType", model)
  }
  /**
   * UpdateBandType
   */
  public UpdateBandType(model: BandType) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/UpdateBandType", model)
  }

  /**
   * GetAllBandType
   */
  public GetAllBandType(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Employee/GetAllBandType", model);
  }


  /**
   * AddFunctionalRole
   */
  public AddFunctionalRole(model: FunctionalRole, selectedFile: File) {
    let formData: FormData = new FormData();
    formData.append("file", selectedFile);
    formData.append("data", JSON.stringify(model));
    return this.httpClient.post(environment.api_baseurl + "api/Master/AddFunctionalRole", formData);
  }
  /**
   * UpdateFunctionalRole
   */
  public UpdateFunctionalRole(model: FunctionalRole, selectedFile: File) {
    let formData: FormData = new FormData();
    formData.append("file", selectedFile);
    formData.append("data", JSON.stringify(model));
    return this.httpClient.post(environment.api_baseurl + "api/Master/UpdateFunctionalRole", formData);
  }
  /**
   * GetAllFunctionalRole
   */
  public GetAllFunctionalRole(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/GetAllFunctionalRole", model);
  }


  /**
   * AddDesignation
   */
  public AddDesignation(model: DesignationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/AddDesignation", model)
  }
  /**
   * UpdateDesignation
   */
  public UpdateDesignation(model: DesignationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/UpdateDesignation", model)
  }

  /**
   * GetAllDesignation
   */
  public GetAllDesignation(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/GetAllDesignation", model);
  }

  /**
   * GetDesignationById
   */
  public GetDesignationById(Id: number) {
    return this.httpClient.get(environment.api_baseurl + "api/Master/GetDesignationById", {
      params: {
        Id: Id.toString()
      }
    });
  }


  /**
   * AddLocation
   */
  public AddLocation(model: Location) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/AddLocation", model)
  }
  /**
   * UpdateLocation
   */
  public UpdateLocation(model: Location) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/UpdateLocation", model)
  }

  /**
   * GetAllLocation
   */
  public GetAllLocation(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/GetAllLocation", model);
  }

  /**
   * AddRegion
   */
  public AddRegion(model: RegionModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/AddRegion", model)
  }
  /**
   * UpdateRegion
   */
  public UpdateRegion(model: RegionModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/UpdateRegion", model)
  }

  /**
   * GetAllRegionData
   */
  public GetAllRegionData(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/GetAllRegionData", model);
  }
}
