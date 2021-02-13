import { ChangeDetectorRef, Component, Inject, OnInit, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { ViewFormsComponent } from './view-forms/view-forms.component';
import { BuilderComponent } from './builder/builder.component';
import { FormsService } from '../../../admin-panel-services/forms.service';
import { RenderFormsComponent } from './render-forms/render-forms.component';

const tabData:WebAppModels.TabData= {
  Hearder: [
    {
      Name: 'Form',
      Component: ViewFormsComponent,
      Selected: true
    },
    {
      Name: 'Rendering',
      Component: RenderFormsComponent,
      Selected: false
    }
  ]
}

@Component({
  selector: 'app-forms',
  templateUrl: './forms.component.html',
  styleUrls: ['./forms.component.css']
})
export class FormsComponent extends WebComponents.ApplicationComponent implements OnInit {

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private formService: FormsService
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
