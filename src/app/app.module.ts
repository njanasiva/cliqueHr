import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CliqueHRInterceptorService } from 'src/Application/Services/clique-hrinterceptor.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { DpDatePickerModule } from 'ng2-date-picker';
import { ɵb,ɵi,ɵf,ɵg,ɵa,ɵe,ɵd,ɵh,ɵc } from 'ng2-date-picker';
import { ModalModule } from 'ngx-bootstrap/modal';
import { BuilderComponent } from './landing/Modules/admin-panel/panel-landing/Pages/forms/builder/builder.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DpDatePickerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CliqueHRInterceptorService,
      multi: true
    },
    ɵb,ɵi,ɵf,ɵg,ɵa,ɵe,ɵd,ɵh,ɵc
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
