import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components, FileType, ValidationType } from 'src/Application/Types/Constants';
import { FormGroup, FormBuilder } from '@angular/forms';
import { CompanyService } from '../../company.service';
import { forkJoin } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { PageSettingImages, PageSettings } from '../../../master/master-model';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { environment } from 'src/environments/environment';


enum ImageType {
  Login,
  Dashboard
}

@Component({
  selector: 'app-page-settings',
  templateUrl: './page-settings.component.html',
  styleUrls: ['./page-settings.component.css']
})
export class PageSettingsComponent extends WebComponents.ApplicationComponent implements OnInit {
  public imageUrl:string = environment.api_baseurl+"api/Storage/GetPublic?path=";
  public SettingForm: FormGroup; 
  public LoginImages: Array<PageSettingImages> = [];
  public DashboardImages: Array<PageSettingImages> = [];
  public isLoginUploadEnabled: boolean = false;
  public isDashboardUploadEnabled: boolean = false;
  public fileValidations: string = '';
  public loginType = ImageType.Login.toString();
  public dashboardType = ImageType.Dashboard.toString();

  private readonly MaxLoginImage: number = 3;
  private readonly MaxDashboardImage: number = 5;
  private fileValidation: WebInterface.FileValidationConfig = {
    AllowedExtentions: [FileType.JPEG, FileType.JPG, FileType.PNG]
  };
  private fileValidationMessage = {
    [ValidationType.InvalidFile]: "Invalid File."
  };
  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private companyService: CompanyService,
    private uiSweetAlertService: UiSweetAlertService,
    private fb: FormBuilder
  ) {
    super(Components.PageSettingsComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.ShowLoader();
    this.CreateSettingForm();
    forkJoin([
      this.companyService.GetPageSettings(),
      this.companyService.GetPageSettingImages()
    ]).subscribe(
      (data) => {
        this.HideLoader();
        if(!isNullOrUndefined(data[0])) {
          this.SettingForm.patchValue(data[0]);
        }
        this.HandleImageResp(data[1]);
      },
      (error) => {
        this.HideLoader();
      }
    )
  }

  private HandleImageResp(data: Array<PageSettingImages>) {
    if(! isNullOrUndefined(data)) {
      this.LoginImages = data.filter(img => img.ImageType == ImageType[0]);
      this.DashboardImages = data.filter(img => img.ImageType == ImageType[1]);
    }
    this.SetUploadParameter();
  }

  private SetUploadParameter() {
    this.isLoginUploadEnabled = false;
    this.isDashboardUploadEnabled = false;
    if (this.LoginImages.length < (this.MaxLoginImage))
    {
      this.isLoginUploadEnabled = true;
    }
    if (this.DashboardImages.length < (this.MaxDashboardImage))
    {
      this.isDashboardUploadEnabled = true;
    }

  }

  private CreateSettingForm(): void {
    this.SettingForm = this.fb.group({
      IsBirthdayVisible: [false],
      IsWorkAnniversaryVisible: [false],
      IsMarriageAnniversaryVisible: [false],
      IsNewJoineeVisible: [false],
      IsExitVisible: [false],
    });
  }

  private AddImage(type:string, imageFile: any) {
    let pageSetting: PageSettingImages = {
      ImageType: type
    } as PageSettingImages;
    this.companyService.AddUpdatePageSettingImages(pageSetting, imageFile)
    .subscribe(
      (data: PageSettingImages) => {
        if (type == ImageType[0]) {
          this.LoginImages.push(data);
        } else if (type == ImageType[1]) {
          this.DashboardImages.push(data);
        }
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    );
  }

  public SelectImage(event, type: string) {
    this.fileValidations = undefined;
    let _selectedImage = undefined;
    if (event.target.files && event.target.files[0]) {
      this.fileValidations = this.fileValidationMessage[this.ValidateFile(this.fileValidation, event.target.files[0])];
      if (!isNullOrUndefined(this.fileValidations)) {
        this.uiSweetAlertService.ShowAlert(this.fileValidations);
        return;
      }
      _selectedImage = event.target.files[0];
      this.AddImage(ImageType[type], _selectedImage);
    }
  }

  public DeleteImage(model: PageSettingImages, index: number) {
    this.ShowLoader();
    this.companyService.DeletePageSettingImages(model).subscribe(
      (data:any) => {
        this.HideLoader();
        if (model.ImageType == ImageType[0]) {
          this.LoginImages.splice(index, 1);
        } else if (model.ImageType == ImageType[1]) {
          this.DashboardImages.splice(index, 1);
        }
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }

  public SavePageSetting() {
    this.ShowLoader();
    let model: PageSettings = this.SettingForm.value as PageSettings;
    this.companyService.AddUpdatePageSettings(model).subscribe(
      (data:any) => {
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Data saved successfully");
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    );
  }

}
