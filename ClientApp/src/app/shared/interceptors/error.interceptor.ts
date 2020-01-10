import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, Subject, throwError, EMPTY } from 'rxjs';
import { catchError, map, flatMap, switchMap, mergeMap, finalize, observeOn } from 'rxjs/operators';
import { AuthentificationService } from 'src/app/core/services/authentification.service';
import { AuthHelper } from '../helpers/auth.helper';
import { RequestRefreshAuthentificationView } from '../models/authentification/request/request-refresh-authentification.view';
import { ResponseGenerateAuthentificationView } from '../models/authentification/response/response-generation-authentification-view';
import { async } from 'rxjs/internal/scheduler/async';

@Injectable({
    providedIn: 'root'
})

export class ErrorInterceptor implements HttpInterceptor {

    private isRefreshing = false;

    constructor(
        private authService: AuthentificationService,
        private authHelper: AuthHelper
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        return next.handle(request).pipe(

            catchError((error: HttpErrorResponse) => {
                
                if (error.status === 419) {

                    if (!this.isRefreshing) {

                        this.isRefreshing = true;

                        let body: RequestRefreshAuthentificationView = {
                            refreshToken: this.authHelper.getRefreshToken()
                        };

                        return this.authService.refresh(body).pipe(
                            mergeMap(response => {

                                this.authHelper.saveTokens(response);

                                this.isRefreshing = false;

                                const newRequest = request.clone({
                                    setHeaders: {
                                        Authorization: `Bearer ${this.authHelper.getAccessToken()}`
                                    }
                                });

                                return next.handle(newRequest).pipe(
                                    map((evt: HttpEvent<any>) => {
                                        return evt;
                                    })
                                );
                            }),
                            catchError(() => {
                                this.authHelper.logOut();

                                return EMPTY;
                            })
                        );
                    } else {
                        return this.waitNewTokens().pipe(
                            flatMap((tokens) => {

                                const newRequest = request.clone({
                                    setHeaders: {
                                        Authorization: `Bearer ${this.authHelper.getAccessToken()}`
                                    }
                                });
                                return next.handle(newRequest).pipe(
                                    map((evt: HttpEvent<any>) => {
                                        return evt;
                                    })
                                );
                            })
                        );
                    }
                } else if (error.status === 401) {
                    this.authHelper.logOut();
                    
                    return throwError('');
                }
            }));
    }

    private waitNewTokens(): Observable<ResponseGenerateAuthentificationView> {

        let subject = new Subject<ResponseGenerateAuthentificationView>();

        let wait = (callback) => {

            setTimeout(() => {
                if (this.isRefreshing) {
                    wait(callback);
                } else {
                    if (callback !== undefined) {
                        callback();
                    }
                    return;
                }
            }, 200);
        };

        wait(() => {
            subject.next();
        });
        return subject.asObservable();
    }
}