import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { WebInterface } from '../Types/types.api';
import { UiLazyLoadingData } from 'projects/clique-hrui/src/lib/ui-models';

@Injectable()
export class ApplicationComponentService {

  constructor(private httpClient: HttpClient) { }

  public GetEmployeeForSingleDD(model: WebInterface.SinglePaginationModel) {
    return this.httpClient.post<UiLazyLoadingData>(environment.api_baseurl + "api/EmployeeCreation/GetAllEmployees", model);
  }
}
