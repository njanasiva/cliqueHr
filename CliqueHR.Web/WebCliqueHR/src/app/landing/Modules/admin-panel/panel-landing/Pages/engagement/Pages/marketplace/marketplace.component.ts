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
  selector: 'app-marketplace',
  templateUrl: './marketplace.component.html',
  styleUrls: ['./marketplace.component.css']
})
export class MarketplaceComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {


  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public SettingsArrayList = new Array<any>();
  public MarketPlaceForm: FormGroup;

  public PostSaleVehicle: boolean = false;
  public PostSaleProperty: boolean = false;
  public PostSaleHouseholdGood: boolean = false;
  public PostRentalProperty: boolean = false;

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
    this.CreateMarketPlaceDetail();
  }

  ngAfterViewInit(): void {
    this.MarketPlaceDetailList();
  }
  CreateMarketPlaceDetail() {   
    if (isNullOrUndefined(this.MarketPlaceForm)) {
      this.MarketPlaceForm = this.fb.group({
        IsPostSaleVehicle: false,
        IsPostSaleProperty: false,
        IsPostSaleHouseholdGood: false,
        IsPostRentalProperty: false,
        IsDoNotUse: false,
        ActionId: 2,
        UserId: 1,
        MarketPlaceId:1
      });
    }
  }
  private MarketPlaceDetailList() {
    let paginationModel: any = {
      UserId: 1,
      ActionId: 2
    }
    this.ShowLoader();
    this.EngagementService.AddUpdateMarketPlaceDetails(paginationModel).subscribe(
      (data: any) => {
        this.MarketPlaceDetailsResp(data);
        this.HideLoader();
        return data;
      },
      (error) => {
        this.HideLoader();
        return null;
        console.log(error);
      });
  }
  private MarketPlaceDetailsResp(data: any) {   
    if (data && data != undefined && data.Table && data.Table.length > 0) {
      this.PostSaleVehicle = data.Table[0].IsPostSaleVehicle;
      this.PostSaleProperty = data.Table[0].IsPostSaleProperty;
      this.PostSaleHouseholdGood = data.Table[0].IsPostSaleHouseholdGood;
      this.PostRentalProperty = data.Table[0].IsPostRentalProperty;
    }
    else {
      this.PostSaleVehicle = false;
      this.PostSaleProperty = false;
      this.PostSaleHouseholdGood = false;
      this.PostRentalProperty = false;
    }
  }

  public OnMarketPlaceSave() {
    this.ShowLoader();
    let marketplacetype: Observable<any>;
    this.MarketPlaceForm.value.IsPostSaleVehicle = this.PostSaleVehicle;
    this.MarketPlaceForm.value.IsPostSaleProperty = this.PostSaleProperty;
    this.MarketPlaceForm.value.IsPostSaleHouseholdGood = this.PostSaleHouseholdGood;
    this.MarketPlaceForm.value.IsPostRentalProperty = this.PostRentalProperty;
    this.MarketPlaceForm.value.ActionId = 1;

    marketplacetype = this.EngagementService.AddUpdateMarketPlaceDetails(this.MarketPlaceForm.value);

    marketplacetype.subscribe(
      (data: any) => {
        this.MarketPlaceDetailList();
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
