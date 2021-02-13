import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CourseType, CourseTitle, Major, University, Institute, CurrancyMapping } from './master-model';
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
   * GetAllCurrencyMapping
   */
  public GetAllCurrencyMapping(model:WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/GetAllCurrencyMapping", model);
  }
  /**
   * AddCurrencyMapping
   */
  public AddCurrencyMapping(model:CurrancyMapping) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/AddCurrencyMapping", model)
  }
  /**
   * UpdateCurrencyMapping
   */
  public UpdateCurrencyMapping(model:CurrancyMapping) {
    return this.httpClient.post(environment.api_baseurl + "api/Master/UpdateCurrencyMapping", model)
  }
}
