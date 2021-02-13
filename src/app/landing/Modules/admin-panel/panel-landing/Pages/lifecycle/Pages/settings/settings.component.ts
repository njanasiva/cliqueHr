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
  public isAddMode: boolean = true;
  public popupHeading: string = '';

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private commonService: CommonService,
    private LifeCycleService: LifeCycleService
  ) {
    super(Components.SettingsComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
  }

  ngAfterViewInit(): void {
    
  }
 

}
