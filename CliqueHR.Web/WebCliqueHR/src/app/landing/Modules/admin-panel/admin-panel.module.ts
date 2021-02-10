import { NgModule, Injector } from '@angular/core';
import { PanelLandingComponent } from './panel-landing/panel-landing.component';
import { AdminPanelRouteModule } from './admin-panel-route/admin-panel-route.module';
import { ApplicationBreadCrumbComponent } from 'src/Application/Components/application-bread-crumb/application-bread-crumb.component';
import { AppSetings } from 'src/Application/Loader/runtime-function';
import { CommonImportsModule } from 'src/Application/Modules/common-imports/common-imports.module';

@NgModule({
  declarations: [
    PanelLandingComponent,
    ApplicationBreadCrumbComponent
  ],
  imports: [
    AdminPanelRouteModule,
    CommonImportsModule.forChildWithInterceptor()
  ],
  providers: [
  ]
})
export class AdminPanelModule {
  constructor(private injector: Injector) {
    AppSetings.setInjector = injector; 
   }
 }
