import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';

@Component({
  selector: 'app-auto-numbering',
  templateUrl: './auto-numbering.component.html',
  styleUrls: ['./auto-numbering.component.css']
})
export class AutoNumberingComponent extends WebComponents.ApplicationComponent implements OnInit {

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef
  ) {
    super(Components.MasterComponent, applicationService, changeDetection, viewContainerRef);
  }

  public ngOnInit() {
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
    
  }

}
