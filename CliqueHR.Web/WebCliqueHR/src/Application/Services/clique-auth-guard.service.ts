import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { ApplicationVeriable } from '../Types/Constants';
import { isNullOrUndefined } from 'util';
import { Observable } from 'rxjs';

@Injectable()
export class CliqueAuthGuardService implements CanActivate {

  constructor(private router: Router) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    let token = localStorage.getItem(ApplicationVeriable.AuthTokenKey);
        if (!isNullOrUndefined(token) && token.trim() != '') {
            return true;
        }
        this.router.navigate(["/secure/login"]);
        return false;
  }
}
