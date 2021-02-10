import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { LoginPageModel, LoginModel, LoginResponse } from './AuthModels';

@Injectable()
export class AuthService {

  constructor(private httpClient: HttpClient) { }

  /**
   * GetLoginPageDetails
   */
  public GetLoginPageDetails(companyCode: string) {
    return this.httpClient.get<LoginPageModel>(environment.api_baseurl + "api/Auth/GetLoginPageDetails",
    {
      params: {
        companyCode: companyCode
      }
    });
  }

  public login(loginModel:LoginModel) {
    let body = new URLSearchParams();
    body.set('username', loginModel.EmployeeCode);
    body.set('password', loginModel.Password);
    body.set('CompanyCode', loginModel.companyCode);
    body.set('grant_type', 'password');
    let options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
    return this.httpClient.post<LoginResponse>(environment.api_baseurl + "login", body.toString(), options);
  }
}
