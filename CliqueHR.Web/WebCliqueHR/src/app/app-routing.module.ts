import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
<<<<<<< HEAD
import { CliqueAuthGuardService } from 'src/Application/Services/clique-auth-guard.service';
=======
>>>>>>> change


const routes: Routes = [
  {
<<<<<<< HEAD
    path: "", redirectTo: 'secure', pathMatch: 'full'
  },
  {
    path: "main", canActivate:[CliqueAuthGuardService] 
    , loadChildren: () => import("./landing/landing.module").then(x => x.LandingModule)
  },
  {
    path: "secure", loadChildren: () => import("./auth/auth.module").then(x => x.AuthModule)
=======
    path: "", redirectTo: 'main', pathMatch: 'full'
  },
  {
    path: "main", loadChildren: () => import("./landing/landing.module").then(x => x.LandingModule)
>>>>>>> change
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
