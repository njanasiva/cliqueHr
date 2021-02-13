import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { QualificationComponent } from './Pages/qualification/qualification.component';
import { CurrencyComponent } from './Pages/currency/currency.component';

const tabData:WebAppModels.TabData= {
  Hearder: [
    {
      Name: 'Currency',
      Component: CurrencyComponent,
      Selected:true
    },
    {
      Name: 'Qualification',
      Component: QualificationComponent,
      Selected:false
    }
  ]
}

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.css']
})
export class MasterComponent extends WebComponents.ApplicationComponent implements OnInit {

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
  public get tabData(){
    return tabData;
  }
}
