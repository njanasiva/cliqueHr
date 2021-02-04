import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { GroupsComponent } from './Pages/groups/groups.component';
import { DailycontentComponent } from './Pages/dailycontent/dailycontent.component';
import { MarketplaceComponent } from './Pages/marketplace/marketplace.component';
import { SurveyComponent } from './Pages/survey/survey.component';

const tabData: WebAppModels.TabData = {
  Hearder: [
    {
      Name: 'Groups',
      Component: GroupsComponent,
      Selected: true
    },
    {
      Name: 'Daily Content',
      Component: DailycontentComponent,
      Selected: false
    },
    {
      Name: 'Marketplace',
      Component: MarketplaceComponent,
      Selected: false
    },
    {
      Name: 'Survey',
      Component: SurveyComponent,
      Selected: false
    }
  ]
}

@Component({
  selector: 'app-engagement',
  templateUrl: './engagement.component.html',
  styleUrls: ['./engagement.component.css']
})
export class EngagementComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }
  public get tabData() {
    return tabData;
  }
}
