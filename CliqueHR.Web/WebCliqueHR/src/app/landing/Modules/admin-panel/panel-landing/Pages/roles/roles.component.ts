import { Component, OnInit, Inject, ChangeDetectorRef, ViewChild, ViewContainerRef, TemplateRef, ComponentFactoryResolver } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { CreateRoleComponent } from './Pages/create-role/create-role.component';
import { AssignRoleComponent } from './Pages/assign-role/assign-role.component';

const tabData: WebAppModels.TabData = {
  Hearder: [
    {
      Name: 'Create Role',
      Component: CreateRoleComponent,
      Selected: true
    },
    {
      Name: 'Assign Role',
      Component: AssignRoleComponent,
      Selected: false
    }

  ]
}

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html',
  styleUrls: ['./roles.component.css']
})
export class RolesComponent extends WebComponents.ApplicationComponent implements OnInit {



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
