import { Component, OnInit, Inject, ChangeDetectorRef, ViewChild, AfterViewInit, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens, MultiSelectUtil } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService } from '../../company.service';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { isNullOrUndefined } from 'util';
import { ValidationMessage } from '../../validation-message';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { SecuritySettings } from '../../company-model';

@Component({
  selector: 'app-security-settings',
  templateUrl: './security-settings.component.html',
  styleUrls: ['./security-settings.component.css']
})
export class SecuritySettingsComponent extends WebComponents.ApplicationComponent implements OnInit {
  
  public securitySettingForm:FormGroup;
  public validationMessageData = ValidationMessage.data.SecuritySettingForm;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb:FormBuilder,
    private uiSweetAlertService:UiSweetAlertService,
    private companyService: CompanyService
  ) { 
    super(Components.SecuritySettingsComponent, applicationService, changeDetection, viewContainerRef);
  }

  ngOnInit() {
    this.CreateSecuritySettingForm();
    this.GetSecuritySettings();
  }

  private CreateSecuritySettingForm() {
    this.securitySettingForm = this.fb.group(
      {
        PasswordExpiryIndays: ['',[Validators.required]],
        SessionTimeOutInMins: ['',[Validators.required]],
        HideMobileNumberFromEd: ['all']
      }
    )
  }
  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  private GetSecuritySettings() {
    this.ShowLoader();
    this.companyService.GetSecuritySettings().subscribe(
      (data:SecuritySettings) => {
        this.HideLoader();
        if(!isNullOrUndefined(data) && data != null){
          this.securitySettingForm.patchValue(data);
        }
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  public AddUpdateSecuritySettings() {
    this.ShowLoader();
    if(this.securitySettingForm.invalid){
      this.triggerValidation(this.securitySettingForm);
      this.HideLoader();
      return;
    }
    let model = this.securitySettingForm.value as SecuritySettings;
    this.companyService.AddUpdateSecuritySettings(model).subscribe(
      (data:any) => {
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Security settings saved successfully");
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

}
