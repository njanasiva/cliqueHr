import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, AfterViewInit, ViewChild } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { MasterService } from '../../master.service';
import { forkJoin, Observable } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { Currancy, CurrancyMapping } from '../../master-model';
import { FormGroup, FormBuilder } from '@angular/forms';
import { CustomValidator } from 'src/Application/Utilitis/CustomValidator';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';

@Component({
  selector: 'app-currency',
  templateUrl: './currency.component.html',
  styleUrls: ['./currency.component.css']
})
export class CurrencyComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  private m_editData:CurrancyMapping;

  public CurrencyConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '', columnClass: 'width100' },
      { fieldId: 'CurrencyCode', fieldName: 'Currency', columnClass: 'text-nowrap' },
      { fieldId: 'IsDefault', fieldName: 'Default Currency', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };
  public CurrencyList: Array<Currancy>;
  public isAddMode: boolean;
  public CurrencyForm: FormGroup;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private masterService: MasterService,
    private uiSweetAlertService: UiSweetAlertService,
    private fb: FormBuilder
  ) {
    super(Components.CurrencyComponent, applicationService, changeDetection, viewContainerRef);
  }

  public ngOnInit() {
    this.CreateCurrencyForm();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    forkJoin([
      this.GetCurrency(),
      this.GetCurrencyData(1, this.CurrencyConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' })
    ]).subscribe(
      (data: any) => {
        this.HideLoader();
        this.CurrencyList = data[0];
        if (isNullOrUndefined(data[1])) {
          this.uiDataTableComponent.ConstructRow([], 0);
        }
        else {
          this.uiDataTableComponent.ConstructRow(data[1].Data, data[1].Total);
        }
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      }
    );
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetCurrencyData(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        (data: any) => {
          if (isNullOrUndefined(data)) {
            this.uiDataTableComponent.ConstructRow([], 0);
          }
          else {
            this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
          }
        },
        (error) => {
          console.log(error);
        });;
    });
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }

  private GetCurrencyData(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllCurrencyMapping(paginationModel);
  }

  private GetCurrency() {
    return this.masterService.GetAllCurrency();
  }

  private CreateCurrencyForm() {
    this.CurrencyForm = this.fb.group({
      Id: [],
      CurrencyId: ['', CustomValidator.DropdownValidation],
      IsDefault: [false],
      IsDoNotUse: [false]
    });
  }
  private RefreshCurrencyData() {
    this.GetCurrencyData(1, this.CurrencyConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
      (data: any) => {
        if (isNullOrUndefined(data)) {
          this.uiDataTableComponent.ConstructRow([], 0);
        }
        else {
          this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
        }
      }
    );
  }
  public OpenCurrencyPopup(isAddMode: boolean, data: CurrancyMapping) {
    this.isAddMode = isAddMode;
    if (isAddMode) {
      this.CurrencyForm.reset();
      this.CurrencyForm.patchValue(new CurrancyMapping());
      this.CurrencyForm.get("CurrencyId").enable();
      let currency: any = this.CurrencyList[0] || {};
      this.CurrencyForm.get('CurrencyId').patchValue(currency.Id);
    }
    else {
      this.m_editData = data;
      this.CurrencyForm.get("CurrencyId").disable();
      this.CurrencyForm.patchValue(data);
    }
    this.OpenModelPopup("#addCurrency");
  }

  public CurrencyOp() {
    this.ShowLoader();
    if (this.CurrencyForm.invalid) {
      this.triggerValidation(this.CurrencyForm);
      this.HideLoader();
      return;
    }
    let model: CurrancyMapping = this.CurrencyForm.value as CurrancyMapping;
    let apiObs: Observable<any>;
    if (this.isAddMode) {
      apiObs = this.masterService.AddCurrencyMapping(model);
    }
    else {
      model.CurrencyId = this.m_editData.CurrencyId;
      apiObs = this.masterService.UpdateCurrencyMapping(model);
    }
    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addCurrency");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Currency data saved successfully");
        this.RefreshCurrencyData();
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

}
