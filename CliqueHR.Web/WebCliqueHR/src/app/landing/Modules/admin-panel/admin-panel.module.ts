import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PanelLandingComponent } from './panel-landing/panel-landing.component';
import { AdminPanelRouteModule } from './admin-panel-route/admin-panel-route.module';
import { CommonImportsModule } from 'src/Application/Modules/common-imports/common-imports.module';
import { LeftPanelComponent } from './panel-landing/Pages/left-panel/left-panel.component';
import { CompanyComponent } from './panel-landing/Pages/company/company.component';
import { ApplicationTabComponent } from 'src/Application/Components/application-tab/application-tab.component';
import { GroupCompanyComponent } from './panel-landing/Pages/company/Pages/group-company/group-company.component';
import { ApplicationBreadCrumbComponent } from 'src/Application/Components/application-bread-crumb/application-bread-crumb.component';
import { EntityComponent } from './panel-landing/Pages/company/Pages/entity/entity.component';
import { MasterComponent } from './panel-landing/Pages/master/master.component';
import { MasterService } from './panel-landing/Pages/master/master.service';
import { SecuritySettingsComponent } from './panel-landing/Pages/company/Pages/security-settings/security-settings.component';
import { CompanyService } from './panel-landing/Pages/company/company.service';
import { CommonService } from '../common.service';
import { LifeCycleService } from '../admin-panel/admin-panel-services/lifecycle.service';
import { EngagementService } from '../admin-panel/admin-panel-services/encagement.services';
import { QualificationComponent } from './panel-landing/Pages/master/Pages/qualification/qualification.component';
import { EmployeeTypeComponent } from './panel-landing/Pages/master/Pages/employee-type/employee-type.component';
import { CurrencyComponent } from './panel-landing/Pages/master/Pages/currency/currency.component';
import { LifecycleComponent } from './panel-landing/Pages/lifecycle/lifecycle.component';
import { ConfirmationComponent } from './panel-landing/Pages/lifecycle/Pages/confirmation/confirmation.component';
import { SeperationComponent } from './panel-landing/Pages/lifecycle/Pages/seperation/seperation.component';
import { MovementComponent } from './panel-landing/Pages/lifecycle/Pages/movement/movement.component';
import { ExitinterviewComponent } from './panel-landing/Pages/lifecycle/Pages/exitinterview/exitinterview.component';
import { SettingsComponent } from './panel-landing/Pages/lifecycle/Pages/settings/settings.component';
import { WorkflowComponent } from './panel-landing/Pages/lifecycle/Pages/workflow/workflow.component';
import { EngagementComponent } from './panel-landing/Pages/engagement/engagement.component';
import { GroupsComponent } from './panel-landing/Pages/engagement/Pages/groups/groups.component';
import { DailycontentComponent } from './panel-landing/Pages/engagement/Pages/dailycontent/dailycontent.component';
import { MarketplaceComponent } from './panel-landing/Pages/engagement/Pages/marketplace/marketplace.component';
import { SurveyComponent } from './panel-landing/Pages/engagement/Pages/survey/survey.component';




@NgModule({
  declarations: [
    PanelLandingComponent, 
    LeftPanelComponent, 
    CompanyComponent, 
    GroupCompanyComponent,
    ApplicationTabComponent,
    ApplicationBreadCrumbComponent,
    EntityComponent,
    MasterComponent,
    QualificationComponent,
    EmployeeTypeComponent,
    SecuritySettingsComponent,
    CurrencyComponent,
    LifecycleComponent,
    ConfirmationComponent,
    SeperationComponent,
    MovementComponent,
    ExitinterviewComponent,
    SettingsComponent,
    WorkflowComponent,
    EngagementComponent,
    GroupsComponent,
    DailycontentComponent,
    MarketplaceComponent,
    SurveyComponent],
  imports: [
    AdminPanelRouteModule,
    CommonImportsModule.forChildWithInterceptor()
  ],
  entryComponents:[
    CompanyComponent,
    GroupCompanyComponent,
    EntityComponent,
    MasterComponent,
    LifecycleComponent,
    QualificationComponent,
    EmployeeTypeComponent,
    SecuritySettingsComponent,
    CurrencyComponent,
    ConfirmationComponent,
    SeperationComponent,
    MovementComponent,
    ExitinterviewComponent,
    SettingsComponent,
    WorkflowComponent,
    EngagementComponent,
    GroupsComponent,
    DailycontentComponent,
    MarketplaceComponent,
    SurveyComponent
  ],
  providers:[
    MasterService,
    CompanyService,
    CommonService,
    LifeCycleService,
    EngagementService
  ]
})
export class AdminPanelModule { }
