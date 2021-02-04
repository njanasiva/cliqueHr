import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { GroupCompanyComponent } from './Pages/group-company/group-company.component';
import { WebAppModels } from 'src/Application/Models/models.api';
import { EntityComponent } from './Pages/entity/entity.component';
import { SecuritySettingsComponent } from './Pages/security-settings/security-settings.component';

const tabData:WebAppModels.TabData= {
  Hearder: [
    {
      Name: 'Group Company',
      Component: GroupCompanyComponent,
      Selected:true
    },
    {
      Name: 'Entity',
      Component: EntityComponent,
      Selected:false
    },
    {
      Name: 'Security Settings',
      Component: SecuritySettingsComponent,
      Selected:false
    }
  ]
}

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent extends WebComponents.ApplicationComponent implements OnInit {

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef
  ) {
    super(Components.CompanyComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
  }

  public get tabData(){
    return tabData;
  }

}
