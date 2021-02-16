import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { throwError } from "rxjs";
import { Observable } from "rxjs/internal/Observable";
import { catchError, tap } from "rxjs/internal/operators";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) {}

    intercept(request: HttpRequest<any>,
              next: HttpHandler): Observable<HttpEvent<any>> {

        const idToken = localStorage.getItem("id_token");

        if (idToken) {
            request = request.clone({
                setHeaders: {
                    Authorization: 'Bearer ' + idToken
                }
            });
            
            return next.handle(request).pipe(catchError(err => {
                //Mivel ide csak akkor jut, hogyha be van jelentkezve, ezért Unathozired (403) kell ellenőrizni
                //Ekkor forbidden error pagere irányítjuk át.
                if (err.status == 403) {
                    this.router.navigate(['forbidden']);
                    return throwError(err);
                }
            }));
        }
        else {
            return next.handle(request).pipe(catchError(err => {
                //Mivel ide akkor fut, hogyha nincs még bejelentkezve. Ekkor legtisztább, ha a login page-re irányítjuk át
                if (err.status == 401) {
                    this.router.navigate(['login']);
                    return throwError(err);
                }
            }));
        }
    }
}