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
<<<<<<< HEAD
import { SecureimageDirective } from 'src/Application/Directives/secureimage.directive';
import { ApplicationMultiselectComponent } from 'src/Application/Components/application-multiselect/application-multiselect.component';
import { ApplicationMultiTreeDropdownComponent } from 'src/Application/Components/application-multi-tree-dropdown/application-multi-tree-dropdown.component';
import { ApplicationSingleTreeDropdownComponent } from 'src/Application/Components/application-single-tree-dropdown/application-single-tree-dropdown.component';
import { ParentNodeComponent } from 'src/Application/Components/application-single-tree-dropdown/parent-node/parent-node.component';
import { ChildNodeComponent } from 'src/Application/Components/application-single-tree-dropdown/child-node/child-node.component';
import { MParentNodeComponent } from 'src/Application/Components/application-multi-tree-dropdown/mparent-node/mparent-node.component';
import { MChildNodeComponent } from 'src/Application/Components/application-multi-tree-dropdown/mchild-node/mchild-node.component';
import { ApplicationEmpSingleDDComponent } from 'src/Application/Components/application-emp-single-dd/application-emp-single-dd.component';
import { ApplicationBreadCrumbComponent } from 'src/Application/Components/application-bread-crumb/application-bread-crumb.component';
import { ApplicationTabComponent } from 'src/Application/Components/application-tab/application-tab.component';
=======
>>>>>>> change


@NgModule({
  declarations: [
    UsecimageDirective,
<<<<<<< HEAD
    SecureimageDirective,
    AppCodeDirective,
    ApplicationLoaderComponent,
    ApplicationMultiselectComponent,
    ApplicationSingleTreeDropdownComponent,
    ApplicationMultiTreeDropdownComponent,
    ParentNodeComponent,
    ChildNodeComponent,
    MParentNodeComponent,
    MChildNodeComponent,
    ApplicationEmpSingleDDComponent,
    ApplicationTabComponent,
=======
    UsecimageDirective,
    AppCodeDirective,
    ApplicationLoaderComponent
>>>>>>> change
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
<<<<<<< HEAD
    SecureimageDirective,
    AppCodeDirective,
    ApplicationLoaderComponent,
    CliqueHRUiModule,
    DpDatePickerModule,
    ApplicationMultiselectComponent,
    ApplicationSingleTreeDropdownComponent,
    ApplicationMultiTreeDropdownComponent,
    ParentNodeComponent,
    ChildNodeComponent,
    MParentNodeComponent,
    MChildNodeComponent,
    ApplicationEmpSingleDDComponent,
    ApplicationTabComponent,
  ],
  entryComponents:[
    ApplicationLoaderComponent,
    ParentNodeComponent,
    ChildNodeComponent,
    MParentNodeComponent,
    MChildNodeComponent
=======
    UsecimageDirective,
    AppCodeDirective,
    ApplicationLoaderComponent,
    CliqueHRUiModule,
    DpDatePickerModule
  ],
  entryComponents:[
    ApplicationLoaderComponent
>>>>>>> change
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
