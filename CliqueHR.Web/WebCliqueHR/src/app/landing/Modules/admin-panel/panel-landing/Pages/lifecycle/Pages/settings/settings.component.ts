import { Component, OnInit, ViewChild, Inject, AfterViewInit, ViewContainerRef, ChangeDetectorRef, ViewChildren, QueryList } from '@angular/core';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { LifeCycleService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/lifecycle.service';
import { Components } from 'src/Application/Types/Constants';
import { isNullOrUndefined } from 'util';
import { forkJoin, Observable, of } from 'rxjs';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public SettingsArrayList = new Array<any>();
  public SettingForm: FormGroup;
  public InitiatePIP: boolean = true;
  public RecommendSalaryHike: boolean = true;
  public EnablePEP: boolean = true;
  public Error: boolean = false;
  public AutoTriggerPEP: boolean = true;
  public AutoTriggerPEPDay: number = 0;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private commonService: CommonService,
    private uiSweetAlertService: UiSweetAlertService,
    private LifeCycleService: LifeCycleService
  ) {
    super(Components.SettingsComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateSettingDetail();
  }

  ngAfterViewInit(): void {
    this.SettingDetailList();
  }

  private SettingDetailsResp(data: any) {
    if (data && data != undefined && data.Table && data.Table.length > 0) {
      this.InitiatePIP = data.Table[0].InitiatePIP;
      this.RecommendSalaryHike = data.Table[0].RecommendSalaryHike;
      this.EnablePEP = data.Table[0].EnablePEP;
      this.AutoTriggerPEP = data.Table[0].AutoTriggerPEP;
      this.AutoTriggerPEPDay = data.Table[0].AutoTriggerPEPDay;
    }
    else {
      this.InitiatePIP = false;
      this.RecommendSalaryHike = false;
      this.EnablePEP = false;
      this.AutoTriggerPEP = false;
      this.AutoTriggerPEPDay = null;
    }
  }

  showDiv(event) {
    if (event.checked) {
      this.AutoTriggerPEPDay = null;
      this.Error = true;
    }
    else{
      this.Error = false;
    }
  }

  private CreateSettingDetail() {
    if (isNullOrUndefined(this.SettingForm)) {
      this.SettingForm = this.fb.group({
        InitiatePIP: [''],
        RecommendSalaryHike: [''],
        EnablePEP: [''],
        AutoTriggerPEP: [''],
        AutoTriggerPEPDay: [''],
        LifeCycleSettingsId: ['']
      });
    }
  }

  public OnSettingSave() {
    if (this.Validate()) {
      debugger;
      this.ShowLoader();
      let settingtype: Observable<any>;
      this.SettingForm.value.InitiatePIP = this.InitiatePIP;
      this.SettingForm.value.RecommendSalaryHike = this.RecommendSalaryHike;
      this.SettingForm.value.EnablePEP = this.EnablePEP;
      this.SettingForm.value.AutoTriggerPEP = this.AutoTriggerPEP;
      this.SettingForm.value.AutoTriggerPEPDay = this.AutoTriggerPEPDay;
      this.SettingForm.value.ActionTypeId = 1;

      settingtype = this.LifeCycleService.AddModifyLifeCycleSetting(this.SettingForm.value);

      settingtype.subscribe(
        (data: any) => {
          this.SettingDetailList();
          this.HideLoader();
          this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
        },
        (error) => {
          if (error.message != "") {
            this.uiSweetAlertService.ShowAlert(error.message);
          }
          this.HideLoader();
        })
    }
  }

  autoTriggerPEPDayChanged(){
    if(this.AutoTriggerPEPDay == null){
      this.Error = true;
    }
    else{
      this.Error = false;
    }
  }

  Validate() {
    if (this.Error) {
      return false;
    }
    return true;
  }
  private SettingDetailList() {
    let paginationModel: any = {
      UserId: 1,
      ActionTypeId: 3
    }
    this.ShowLoader();
    this.LifeCycleService.AddModifyLifeCycleSetting(paginationModel).subscribe(
      (data: any) => {
        this.SettingDetailsResp(data);
        this.HideLoader();
        return data;
      },
      (error) => {
        this.HideLoader();
        return null;
        console.log(error);
      });
  }
}

