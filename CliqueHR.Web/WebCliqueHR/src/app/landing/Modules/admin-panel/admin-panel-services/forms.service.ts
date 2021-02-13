import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { WebInterface } from 'src/Application/Types/types.api';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FormsService {

  private subject = new Subject<any>();
  constructor(private httpClient: HttpClient) { }

  /**Add/edit/View/Delete api call */
  public FormDetails(model: WebInterface.PaginationModel) {
    // console.log("data")
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/CreateForm", model);
  }

   /**Save formData api call */
   public SaveData(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Qualification/AssignFormvalue", model);
  }

  sendDetail(value: any) {
    this.subject.next({ content: value });
  }

  getDetail(): Observable<any> {
    return this.subject.asObservable();
  }
}
