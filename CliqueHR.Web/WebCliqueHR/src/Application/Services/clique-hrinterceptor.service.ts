import { Injectable } from '@angular/core';
<<<<<<< HEAD
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpClient, HttpResponse, HttpErrorResponse, HttpEventType, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { ApplicationVeriable } from '../Types/Constants';
import { map, catchError, filter, take, switchMap, finalize } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoginResponse } from 'src/app/auth/AuthModels';
import { Router } from '@angular/router';
=======
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpClient, HttpResponse, HttpErrorResponse, HttpEventType } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { ApplicationVeriable } from '../Types/Constants';
import { catchError } from 'rxjs/operators';
>>>>>>> change
@Injectable({
  providedIn: 'root'
})
export class CliqueHRInterceptorService implements HttpInterceptor {
<<<<<<< HEAD
  private refreshTokenInProgress: boolean = false;
  private refreshTokenObs: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(
    private httpClient: HttpClient,
    private router:Router
    ) { }
=======

  constructor(private httpClient: HttpClient) { }
>>>>>>> change

  private AddAuthToken(req: HttpRequest<any>) {
    let token = localStorage.getItem(ApplicationVeriable.AuthTokenKey);
    if (isNullOrUndefined(token) || token == "") {
      return req;
    }
    return req.clone({
      setHeaders: {
        "Authorization": "bearer " + token
      }
    });
  }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = this.AddAuthToken(req);
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        if (!isNullOrUndefined(error)) {
<<<<<<< HEAD
          let isInvalidRefreshToken = this.isInvalidRefreshTokenError(error);
          if (error.status == 401) {
            // Handle error code
            if (this.refreshTokenInProgress) {
              return this.refreshTokenObs.pipe(
                filter(x => x != null),
                take(1),
                switchMap(() => next.handle(this.AddAuthToken(req)))
              );
            }
            else {
              this.refreshTokenInProgress = true;
              this.refreshTokenObs.next(null);
              return this.GetRefreshToken().pipe(
                switchMap((data: LoginResponse) => {
                  this.refreshTokenInProgress = false;
                  localStorage.setItem(ApplicationVeriable.AuthTokenKey, data.access_token)
                  localStorage.setItem(ApplicationVeriable.RefreshTokenKey, data.refresh_token)
                  this.refreshTokenObs.next(data.access_token);
                  return next.handle(this.AddAuthToken(req));
                }
                ),
                finalize(() => this.refreshTokenInProgress = false)
              );
            }

          }
          else if (error.status == 403 || isInvalidRefreshToken) {
            // Redirect to login
            this.router.navigate(['/secure/login']);
=======
          if (error.status == 401) {
            // Handle error code
          }
          else if (error.status == 403) {
            // Handle error code
>>>>>>> change
          }
        }
        return throwError(error);
      }
      )
    );
  }
<<<<<<< HEAD
  private isInvalidRefreshTokenError(error:any) {
    if(error.status == 400 &&
      !isNullOrUndefined(error.error) && 
      !isNullOrUndefined(error.error.error)) {
      return error.error.error === ApplicationVeriable.InvalidRefreshTokenError;
    }
    return false;
  }
  private GetRefreshToken() {
    let body = new URLSearchParams();
    body.set('refresh_token', localStorage.getItem(ApplicationVeriable.RefreshTokenKey));
    body.set('grant_type', 'refresh_token');
    let options = {
      headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
    };
    return this.httpClient.post(environment.api_baseurl + "login", body.toString(), options);
  }
=======
>>>>>>> change
}
