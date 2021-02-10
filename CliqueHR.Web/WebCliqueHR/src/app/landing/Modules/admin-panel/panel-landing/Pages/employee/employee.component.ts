import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';

import { GlobalprofileComponent } from './Pages/globalprofile/globalprofile.component';
import { UserdefinedfieldComponent } from './Pages/userdefinedfield/userdefinedfield.component';

const tabData: WebAppModels.TabData = {
  Hearder: [
    {
      Name: 'User Defined Fields',
      Component: UserdefinedfieldComponent,
      Selected: true
    },
    {
      Name: 'Global Profile',
      Component: GlobalprofileComponent,
      Selected: false
    }

  ]
}

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent extends WebComponents.ApplicationComponent implements OnInit {



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

  public get tabData() {
    return tabData;
  }

  ngOnInit() {
  }

}
