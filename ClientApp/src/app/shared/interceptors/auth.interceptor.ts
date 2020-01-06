import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthHelper } from '../helpers/auth.helper';

@Injectable({
    providedIn: 'root'
})

export class AuthInterceptor implements HttpInterceptor {
    constructor(
        private authHelper: AuthHelper
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
        let accessToken = this.authHelper.getAccessToken();

        request = request.clone({
            setHeaders: {
                Authorization: `Bearer ${accessToken}`
            }
        });

        return next.handle(request);
    }
}