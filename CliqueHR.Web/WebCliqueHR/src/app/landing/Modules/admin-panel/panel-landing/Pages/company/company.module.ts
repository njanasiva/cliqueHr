import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CompanyComponent } from './company.component';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { ApplicationComponentService } from 'src/Application/Services/application-component.service';
import { CompanyService } from './company.service';
import { CommonImportsModule } from 'src/Application/Modules/common-imports/common-imports.module';
import { GroupCompanyComponent } from './Pages/group-company/group-company.component';
import { EntityComponent } from './Pages/entity/entity.component';
import { SecuritySettingsComponent } from './Pages/security-settings/security-settings.component';
import { AutoNumberingComponent } from './Pages/auto-numbering/auto-numbering.component';
import { PageSettingsComponent } from './Pages/page-settings/page-settings.component';
import { OrgUnitsComponent } from './Pages/org-units/org-units.component';
import { DepartmentsComponent } from './Pages/departments/departments.component';

const Company: any[] = [
  CompanyComponent,
  GroupCompanyComponent,
  EntityComponent,
  SecuritySettingsComponent,
  AutoNumberingComponent,
  PageSettingsComponent,
  OrgUnitsComponent,
  DepartmentsComponent
];

@NgModule({
  declarations: [
    ...Company,
  ],
  imports: [
    CommonImportsModule.forChildWithInterceptor()
  ],
  entryComponents:[
    ...Company,
  ],
  providers:[
    CompanyService,
    CommonService,
    ApplicationComponentService
  ],
  bootstrap:[
    CompanyComponent
  ]
})
export class CompanyModule { }
