import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { env } from 'process';
import { WebInterface } from 'src/Application/Types/types.api';

@Injectable()
export class LifeCycleService {

  constructor(private httpClient: HttpClient) { }

  public GetProbationDetails(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/GetProbationDetailList", model);
  }

  public AddModifyProbation(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/AddModifyProbationDetail", model);
  }

  public GetNoticePeriodDetails(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/GetNoticePeriodDetail", model);
  }
  public AddNoticePeriodDetails(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/AddNoticePeriodDetail", model);
  }
  public GetSeparationReasons(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/GetSeparationReason", model);
  }
  public AddModifySeparationReasonDetails(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/AddModifySeparationReason", model);
  }
  public GetSeparationTask(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/GetSeparationTask", model);
  }  
  public AddModifySeparationTaskDetails(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/AddModifySeparationTask", model);
  }
  public GetMovementReason(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/GetMovementList", model);
  }
  public AddMovementDetails(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/AddMovement", model);
  }
  public GetExitInterviewList(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/ExitInterviewList", model);
  }
  public GetLifeCycleSetting(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/LifeCycleSetting", model);
  }
  public GetConfirmationMasterList(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/GetMastersList", model);
  }
  public GETMasterForSeparationTaskList(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/LifeCycle/GETMasterForSeparationTask", model);
  }
  

}
