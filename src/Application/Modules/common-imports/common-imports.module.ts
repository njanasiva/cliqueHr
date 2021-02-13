import { NgModule, ModuleWithProviders, Renderer2 } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CliqueHRInterceptorService } from 'src/Application/Services/clique-hrinterceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NumericDirective } from 'src/Application/Directives/numeric.directive';
import { UsecimageDirective } from 'src/Application/Directives/usecimage.directive';
import { AppCodeDirective } from 'src/Application/Directives/app-code.directive';
import { CliqueHRUiModule, UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { DpDatePickerModule } from "ng2-date-picker";
import { ApplicationLoaderComponent } from 'src/Application/Components/application-loader/application-loader.component';


@NgModule({
  declarations: [
    UsecimageDirective,
    UsecimageDirective,
    AppCodeDirective,
    ApplicationLoaderComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    CliqueHRUiModule,
    DpDatePickerModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    UsecimageDirective,
    UsecimageDirective,
    AppCodeDirective,
    ApplicationLoaderComponent,
    CliqueHRUiModule,
    DpDatePickerModule
  ],
  entryComponents:[
    ApplicationLoaderComponent
  ],
  providers:[
    UiSweetAlertService
  ]
})
export class CommonImportsModule {
  public static forChildWithInterceptor(): ModuleWithProviders[] {
    return [{
      ngModule: CommonImportsModule,
      providers: [
        {
          provide: HTTP_INTERCEPTORS,
          useClass: CliqueHRInterceptorService,
          multi: true
        }]
    }]
  }
  public static forChildWithoutInterceptor(): ModuleWithProviders[] {
    return [{
      ngModule: CommonImportsModule,
      providers: []
    }]
  }
}
