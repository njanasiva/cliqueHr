import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ApplicationVeriable } from 'src/Application/Types/Constants';
import { EntityOrgunitTreeVM } from './common-model';

@Injectable()
export class CommonService {

  constructor(private httpClient: HttpClient) { }

  /**
   * GetAllCompanyType
   */
  public GetAllCompanyType() {
    return this.httpClient.get(environment.api_baseurl + "api/Common/GetAllCompanyType");
  }
  /**
   * GetAllCountry
   */
  public GetAllCountry() {
    return this.httpClient.get(environment.api_baseurl + "api/Common/GetAllCountry");
  }

  /**
   * GetAllState
   */
  public GetAllState(CountryId: number) {
    return this.httpClient.get(environment.api_baseurl + "api/Common/GetAllState", {
      params:{
        CountryId: CountryId.toString()
      }
    });
  }

  /**
   * GetAllCity
   */
  public GetAllCity(StateId: number) {
    return this.httpClient.get(environment.api_baseurl + "api/Common/GetAllCity", {
      params:{
        StateId: StateId.toString()
      }
    });
  }

  /**
   * Logout
   */
  public Logout() {
    return this.httpClient.get(environment.api_baseurl + "api/Auth/Logout", {
      params:{
        token: localStorage.getItem(ApplicationVeriable.RefreshTokenKey)
      }
    });
  }

  /**
   * GetEntityOrgunitDeptTree
   */
  public GetEntityOrgunitDeptTree(DepartmentId?: number) {
    let value = DepartmentId || 0;
    return this.httpClient.get<Array<EntityOrgunitTreeVM>>(environment.api_baseurl + "api/Department/GetEntityOrgunitDeptTree",
      {
        params: {
          "DepartmentId": value.toString()
        }
      });
  }
  
}
