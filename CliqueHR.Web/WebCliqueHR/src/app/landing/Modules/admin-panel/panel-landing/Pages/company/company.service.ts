import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GroupCompany, SecuritySettings, Entity, AutoNumbering, OrgUnits, Department } from './company-model';
import { WebInterface } from 'src/Application/Types/types.api';
import { PageSettings, PageSettingImages } from '../master/master-model';
import { isNullOrUndefined } from 'util';
import { EntityOrgunitTreeVM } from 'src/app/landing/Modules/common-model';

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
  public AddUpdateGroupCompany(model: GroupCompany) {
    return this.httpClient.post(environment.api_baseurl + "api/GroupCompany/AddUpdateGroupCompany", model);
  }

  public GetEntity(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Entity/GetEntity", model);
  }
  public AddEntity(model: Entity, selectedFile: File) {
    let formData: FormData = new FormData();
    formData.append("file", selectedFile);
    formData.append("data", JSON.stringify(model));
    return this.httpClient.post(environment.api_baseurl + "api/Entity/AddEntity", formData);
  }
  public UpdateEntity(model: Entity, selectedFile: File) {
    let formData: FormData = new FormData();
    formData.append("file", selectedFile);
    formData.append("data", JSON.stringify(model));
    return this.httpClient.post(environment.api_baseurl + "api/Entity/UpdateEntity", formData);
  }
  public GetEntityById(Id: number) {
    return this.httpClient.get<Entity>(environment.api_baseurl + "api/Entity/GetEntityById", {
      params: {
        Id: Id.toString()
      }
    });
  }

  /**
   * AddUpdateAutoNumbering
   */
  public AddUpdateAutoNumbering(model: AutoNumbering) {
    return this.httpClient.post(environment.api_baseurl + "api/AutoNumbering/AddUpdateAutoNumbering", model)
  }

  /**
   * GetAutoNumbering
   */
  public GetAutoNumbering() {
    return this.httpClient.get(environment.api_baseurl + "api/AutoNumbering/GetAutoNumbering");
  }

  /**
  * AddUpdatePageSettings
  */
  public AddUpdatePageSettings(model: PageSettings) {
    return this.httpClient.post(environment.api_baseurl + "api/PageSettings/AddUpdatePageSettings", model);
  }

  /**
   * AddUpdatePageSettingImages
   */
  public AddUpdatePageSettingImages(model: PageSettingImages, file: File) {
    let formData: FormData = new FormData();
    formData.append("file", file);
    formData.append("data", JSON.stringify(model));
    return this.httpClient.post(environment.api_baseurl + "api/PageSettings/AddUpdatePageSettingImages", formData);
  }

  /**
   * DeletePageSettingImages
   */
  public DeletePageSettingImages(model: PageSettingImages) {
    return this.httpClient.post(environment.api_baseurl + "api/PageSettings/DeletePageSettingImages", model);
  }
  /**
   * GetPageSettings
   */
  public GetPageSettings() {
    return this.httpClient.get<PageSettings>(environment.api_baseurl + "api/PageSettings/GetPageSettings");
  }

  /**
   * GetPageSettingImages
   */
  public GetPageSettingImages() {
    return this.httpClient.get<Array<PageSettingImages>>(environment.api_baseurl + "api/PageSettings/GetPageSettingImages");
  }

  /**
   * AddUpdateOrgUnits
   */
  public AddUpdateOrgUnits(model: OrgUnits) {
    return this.httpClient.post(environment.api_baseurl + "api/OrgUnits/AddUpdateOrgUnits", model)
  }

  /**
   * GetOrgUnits
   */
  public GetOrgUnits(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/OrgUnits/GetOrgUnits", model);
  }

  /**
   * GetEntityOrgunitTree
   */
  public GetEntityOrgunitTree(OrUnitId?: number) {
    let value = OrUnitId || 0;
    return this.httpClient.get<Array<EntityOrgunitTreeVM>>(environment.api_baseurl + "api/OrgUnits/GetEntityOrgunitTree",
      {
        params: {
          "OrUnitId": value.toString()
        }
      });
  }

  /**
   * AddUpdateDepartment
   */
  public AddUpdateDepartment(model: Department) {
    return this.httpClient.post(environment.api_baseurl + "api/Department/AddUpdateDepartment", model)
  }

  /**
   * GetDepartments
   */
  public GetDepartments(model: WebInterface.PaginationModel) {
    return this.httpClient.post(environment.api_baseurl + "api/Department/GetDepartments", model);
  }

  /**
   * GetDepartmentCode
   */
  public GetDepartmentCode() {
    return this.httpClient.get(environment.api_baseurl + "api/Department/GetDepartmentCode");
  }
}
