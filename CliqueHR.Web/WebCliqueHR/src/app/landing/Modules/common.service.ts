import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

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
}
