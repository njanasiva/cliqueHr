import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CommonImportsModule } from 'src/Application/Modules/common-imports/common-imports.module';
import { LoginComponent } from './Pages/login/login.component';
import { AuthRouteModule } from './auth-route/auth-route.module';
import { WebTokens } from 'src/Application/Types/types.api';
import { ApplicationService } from 'src/Application/Services/application.service';
import { AuthService } from './auth.service';



@NgModule({
  declarations: [LoginComponent],
  imports: [
    CommonImportsModule.forChildWithoutInterceptor(),
    AuthRouteModule
  ],
  providers:[
    {
      provide: WebTokens.APPLICATION_SERVICE,
      useClass: ApplicationService
    },
    AuthService
  ]
})
export class AuthModule { }
