import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
declare var $:any;
@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html'
})
export class LandingComponent extends WebComponents.ApplicationComponent implements OnInit {

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef
  ) {
    super(Components.LandingComponent, applicationService, changeDetection, viewContainerRef);
  }

  public ngOnInit() {
      $('body').css({ "background-image": "url(assets/images/bg-image-1.jpg)"});
  }
  
  public TriggerCommand(request: WebInterface.CommandRequest) {
  }
}
