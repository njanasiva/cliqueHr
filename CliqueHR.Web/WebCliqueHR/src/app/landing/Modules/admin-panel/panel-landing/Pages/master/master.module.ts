import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommonImportsModule } from 'src/Application/Modules/common-imports/common-imports.module';
import { MasterService } from './master.service';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { ApplicationComponentService } from 'src/Application/Services/application-component.service';
import { MasterComponent } from './master.component';
import { QualificationComponent } from './Pages/qualification/qualification.component';
import { EmployeeTypeComponent } from './Pages/employee-type/employee-type.component';
import { CurrencyComponent } from './Pages/currency/currency.component';
import { CenterTypeComponent } from './Pages/center-type/center-type.component';
import { GradeComponent } from './Pages/grade/grade.component';
import { BandComponent } from './Pages/band/band.component';
import { FunctionalRoleComponent } from './Pages/functional-role/functional-role.component';
import { DesignationComponent } from './Pages/designation/designation.component';
import { LocationComponent } from './Pages/location/location.component';
import { RegionComponent } from './Pages/region/region.component';
import { AutoNumberingComponent } from './Pages/auto-numbering/auto-numbering.component';

const Master: any[] = [
  MasterComponent,
  QualificationComponent,
  EmployeeTypeComponent,
  CurrencyComponent,
  CenterTypeComponent,
  GradeComponent,
  BandComponent,
  FunctionalRoleComponent,
  DesignationComponent,
  LocationComponent,
  RegionComponent,
  AutoNumberingComponent
];

@NgModule({
  declarations: [
    ...Master,
  ],
  imports: [
    CommonImportsModule.forChildWithInterceptor()
  ],
  entryComponents:[
    ...Master,
  ],
  providers:[
    MasterService,
    CommonService,
    ApplicationComponentService
  ],
  bootstrap:[
    MasterComponent
  ]
})
export class MasterModule { }
