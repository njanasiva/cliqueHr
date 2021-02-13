import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GroupCompany, SecuritySettings } from './company-model';

@Injectable()
export class CompanyService {

  constructor(private httpClient: HttpClient) { }

  public AddUpdateSecuritySettings(model: SecuritySettings) {
    return this.httpClient.post(environment.api_baseurl + "api/SecuritySettings/AddUpdateSecuritySettings", model);
  }
  public GetSecuritySettings() {
    return this.httpClient.get<SecuritySettings>(environment.api_baseurl + "api/SecuritySettings/GetSecuritySettings");
  }

  public GetGroupCompany() {
    return this.httpClient.get<GroupCompany>(environment.api_baseurl + "api/GroupCompany/GetGroupCompany");
  }
  public AddUpdateGroupCompany(model:GroupCompany) {
    return this.httpClient.post(environment.api_baseurl + "api/GroupCompany/AddUpdateGroupCompany", model);
  }
}
