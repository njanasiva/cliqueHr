import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { ConfirmationComponent } from './Pages/confirmation/confirmation.component';
import { SeperationComponent } from './Pages/seperation/seperation.component';
import { MovementComponent } from './Pages/movement/movement.component';
import { ExitinterviewComponent } from './Pages/exitinterview/exitinterview.component';
import { SettingsComponent } from './Pages/settings/settings.component';
import { WorkflowComponent } from './Pages/workflow/workflow.component';

const tabData: WebAppModels.TabData = {
  Hearder: [
    {
      Name: 'Confirmation',
      Component: ConfirmationComponent,
      Selected: true
    },
    {
      Name: 'Seperation',
      Component: SeperationComponent,
      Selected: false
    },
    {
      Name: 'Movement',
      Component: MovementComponent,
      Selected: false
    },
    {
      Name: 'Exit Interview',
      Component: ExitinterviewComponent,
      Selected: false
    },
    {
      Name: 'Settings',
      Component: SettingsComponent,
      Selected: false
    },
    {
      Name: 'Workflow',
      Component: WorkflowComponent,
      Selected: false
    }
  ]
}

@Component({
  selector: 'app-lifecycle',
  templateUrl: './lifecycle.component.html',
  styleUrls: ['./lifecycle.component.css']
})
export class LifecycleComponent extends WebComponents.ApplicationComponent implements OnInit {

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef
  ) {
    super(Components.LifecycleComponent, applicationService, changeDetection, viewContainerRef);
  }

  public ngOnInit() {
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {

  }
  public get tabData() {
    return tabData;
  }
}
