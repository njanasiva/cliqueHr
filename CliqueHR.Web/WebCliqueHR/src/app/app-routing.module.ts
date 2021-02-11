import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CliqueAuthGuardService } from 'src/Application/Services/clique-auth-guard.service';


const routes: Routes = [
  {
    path: "", redirectTo: 'secure', pathMatch: 'full'
  },
  {
    path: "main", canActivate:[CliqueAuthGuardService] 
    , loadChildren: () => import("./landing/landing.module").then(x => x.LandingModule)
  },
  {
    path: "secure", loadChildren: () => import("./auth/auth.module").then(x => x.AuthModule)
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
