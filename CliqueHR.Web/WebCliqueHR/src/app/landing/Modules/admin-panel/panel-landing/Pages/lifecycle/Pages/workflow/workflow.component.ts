import { Component, OnInit, ViewChild, Inject, AfterViewInit, ViewContainerRef, ChangeDetectorRef, ViewChildren, QueryList } from '@angular/core';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebAppService, WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { LifeCycleService } from 'src/app/landing/Modules/admin-panel/admin-panel-services/lifecycle.service';
import { Components } from 'src/Application/Types/Constants';
import { isNullOrUndefined } from 'util';
import { forkJoin, Observable, of } from 'rxjs';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';


@Component({
  selector: 'app-workflow',
  templateUrl: './workflow.component.html',
  styleUrls: ['./workflow.component.css']
})
export class WorkflowComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;

  public WorkflowConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'WfID', fieldName: '', columnClass: 'width100' },
      { fieldId: 'AppPathTyp', fieldName: 'Approval Path Type', columnClass: 'text-nowrap width100' },
      { fieldId: 'AppPath', fieldName: 'Approval Path', columnClass: 'text-nowrap width100' },
      { fieldId: 'WrkGrps', fieldName: 'Workgroups', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 3,
    DefaultSort: { direction: 'asc', fieldId: 'WfID' },
    UniqueRowCol: 'WfID',
    isEditable: true

  };

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private fb: FormBuilder,
    private commonService: CommonService,
    private LifeCycleService: LifeCycleService
  ) {
    super(Components.ExitinterviewComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }

  ngOnInit() {
  }

  ngAfterViewInit(): void {

  }

}
