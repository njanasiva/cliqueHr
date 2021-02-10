import { Component, OnInit, Inject, ViewContainerRef, ChangeDetectorRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebTokens } from 'src/Application/Types/Tokens';
import { WebInterface } from 'src/Application/Types/types.api';
import { Components, ApplicationVeriable } from 'src/Application/Types/Constants';
import { AuthService } from '../../auth.service';
import { LoginPageModel, LoginModel, LoginResponse, LoginErrorModel } from '../../AuthModels';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { environment } from 'src/environments/environment';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ValidationMessage } from '../../validation-message';
import { Router } from '@angular/router';
declare var $:any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent  extends WebComponents.ApplicationComponent implements OnInit {

  private companyCode:string = "devDB";
  public imageUrl:string = environment.api_baseurl+"api/Storage/GetPublic?path=";
  public loginPageData: LoginPageModel;
  public loginForm: FormGroup;
  public validationMassages = ValidationMessage.data;
  
  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private authService: AuthService,
    private uiSweetAlertService: UiSweetAlertService,
    private fb:FormBuilder,
    private router:Router
  ) {
    super(Components.LoginComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateLoginForm();
    this.ShowLoader();
    $('body').css({ "background-image": ""});
      $('.nav-link.active').click(function(e){
          $('.hidden-login').toggleClass("show");
          $('.login-btn').toggleClass("btn-active");
          e.stopPropagation(); 
      });
    this.authService.GetLoginPageDetails(this.companyCode).subscribe(
      (data:LoginPageModel) => {
        this.HideLoader();
        this.loginPageData = data;
        $(".carousel-item img").height(window.innerHeight);
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  private DisplayLoginValidationMessage(error:any) {
    if(error.status == 400) {
      let resp = error.error as LoginErrorModel;
      if (resp != null && resp.error == "Validation") {
        this.validationMessage = [resp.error_description];
      }
      else {
        this.validationMessage = ValidationBuilder.build(error);
      }
      this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
    }
  }
  public CreateLoginForm() {
    this.loginForm = this.fb.group(
      {
        EmployeeCode: ['',[Validators.required]],
        Password:['',[Validators.required]]
      }
    );
  }

  public OnLogin() {
    this.ShowLoader();
    if (this.loginForm.invalid) {
      this.triggerValidation(this.loginForm);
      this.HideLoader();
      return;
    }
    let loginModel:LoginModel = this.loginForm.value;
    loginModel.companyCode = this.companyCode;
    this.authService.login(loginModel).subscribe(
      (data:LoginResponse) => {
        console.log(data);
        this.HideLoader();
        localStorage.setItem(ApplicationVeriable.AuthTokenKey,data.access_token);
        localStorage.setItem(ApplicationVeriable.RefreshTokenKey,data.refresh_token);
        localStorage.setItem(ApplicationVeriable.AccessFactorKey,data.AccessFactor);
        this.router.navigate(['/main']);
      },
      (error) => {
        this.HideLoader();
        this.DisplayLoginValidationMessage(error);
      }
    )
  }

}
