import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { env } from 'process';
import { WebInterface } from 'src/Application/Types/types.api';

@Injectable()
export class EngagementService {
  constructor(private httpClient: HttpClient) { }

  public AddUpdateMarketPlaceDetails(model: any) {
    return this.httpClient.post(environment.api_baseurl + "api/Engagement/AddUpdateMarketPlace", model);
  }
  public AddUpdateEngagementSurveyDetails(model: any) {
    return this.httpClient.post(environment.api_baseurl + "api/Engagement/AddUpdateEngagementSurvey", model);
  }
  public AddModifyDailyContentDetails(model: any) {
    return this.httpClient.post(environment.api_baseurl + "api/Engagement/AddModifyDailyContent", model);
  }
  public GetEngagementMasterList(model: any) {
    return this.httpClient.post(environment.api_baseurl + "api/Engagement/EngagementMaster", model);
  }
  public GetEngagementGroups(model: any) {
    return this.httpClient.post(environment.api_baseurl + "api/Engagement/EngagementMarketPlace", model);
  }
}
