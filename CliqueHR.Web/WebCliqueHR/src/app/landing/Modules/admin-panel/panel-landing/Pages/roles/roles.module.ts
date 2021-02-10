import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RolesComponent } from './roles.component';
import { CreateRoleComponent } from './Pages/create-role/create-role.component';
import { AssignRoleComponent } from './Pages/assign-role/assign-role.component';
import { CommonImportsModule } from 'src/Application/Modules/common-imports/common-imports.module';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { ApplicationComponentService } from 'src/Application/Services/application-component.service';
const Roles: any[] = [
  RolesComponent,
  CreateRoleComponent,
  AssignRoleComponent
];


@NgModule({
  declarations: [
    ...Roles
  ],
  imports: [
    CommonImportsModule.forChildWithInterceptor()
  ],
  entryComponents:[
    ...Roles
  ],
  providers:[
    CommonService,
    ApplicationComponentService
  ],
  bootstrap:[
    RolesComponent
  ]
})
export class RolesModule { }