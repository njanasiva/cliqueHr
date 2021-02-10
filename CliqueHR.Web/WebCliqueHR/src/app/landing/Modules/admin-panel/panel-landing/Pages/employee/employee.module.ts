import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommonImportsModule } from 'src/Application/Modules/common-imports/common-imports.module';
import { EmployeeService } from './employee.service';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { ApplicationComponentService } from 'src/Application/Services/application-component.service';
import { EmployeeComponent } from './employee.component';
import { UserdefinedfieldComponent } from './Pages/userdefinedfield/userdefinedfield.component';
import { GlobalprofileComponent } from './Pages/globalprofile/globalprofile.component';

const Employee: any[] = [
  EmployeeComponent,
  UserdefinedfieldComponent,
  GlobalprofileComponent
];

@NgModule({
  declarations: [
    ...Employee,
  ],
  imports: [
    CommonImportsModule.forChildWithInterceptor()
  ],
  entryComponents:[
    ...Employee,
  ],
  providers:[
    EmployeeService,
    CommonService,
    ApplicationComponentService
  ],
  bootstrap:[
    EmployeeComponent
  ]
})
export class EmployeeModule { }
