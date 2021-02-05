import { Component, OnInit, ViewChild, Inject, AfterViewInit, ViewContainerRef, ChangeDetectorRef, ViewChildren, QueryList } from '@angular/core';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { EngagementService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/encagement.services';
import { Components } from 'src/Application/Types/Constants';
import { isNullOrUndefined } from 'util';
import { forkJoin, Observable, of } from 'rxjs';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';

@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})
export class SurveyComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {


  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public SettingsArrayList = new Array<any>();
  public SurveyForm: FormGroup;
  public CreateSurvey: boolean = false;
  public PostSurvey: boolean = false;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private commonService: CommonService,
    private uiSweetAlertService: UiSweetAlertService,
    private EngagementService: EngagementService
  ) {
    super(Components.SettingsComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateSurveyDetail();
  }

  ngAfterViewInit(): void {
    this.SurveyDetailList();
  }

  private CreateSurveyDetail() {
    if (isNullOrUndefined(this.SurveyForm)) {
      this.SurveyForm = this.fb.group({
        IsCreateSurvey: false,
        IsPostSurvey: false,
        IsDoNotUse: false,
        ActionId: 2,
        UserId: 1,
        EngagementSurveyId: 1
      });
    }
  }
  private SurveyDetailList() {
    let paginationModel: any = {
      UserId: 1,
      ActionId: 2
    }
    this.ShowLoader();
    this.EngagementService.AddUpdateEngagementSurveyDetails(paginationModel).subscribe(
      (data: any) => {
        this.SurveyDetailsResp(data);
        this.HideLoader();
        return data;
      },
      (error) => {
        this.HideLoader();
        return null;
        console.log(error);
      });
  }
  private SurveyDetailsResp(data: any) {
    debugger;
    if (data && data != undefined && data.Table && data.Table.length > 0) {
      this.CreateSurvey = data.Table[0].IsCreateSurvey;
      this.PostSurvey = data.Table[0].IsPostSurvey;
    }
    else {
      this.CreateSurvey = false;
      this.PostSurvey = false;
    }
  }
  public OnSurveySave() {
    this.ShowLoader();
    let surveytype: Observable<any>;
    this.SurveyForm.value.IsCreateSurvey = this.CreateSurvey;
    this.SurveyForm.value.IsPostSurvey = this.PostSurvey;
    this.SurveyForm.value.ActionId = 1;

    surveytype = this.EngagementService.AddUpdateEngagementSurveyDetails(this.SurveyForm.value);

    surveytype.subscribe(
      (data: any) => {
        this.SurveyDetailList();
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
      },
      (error) => {
        if (error.message != "") {
          this.uiSweetAlertService.ShowAlert(error.message);
        }
        this.HideLoader();
      })
    this.HideLoader();
  }
}
