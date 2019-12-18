import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { LocalDatabase } from '@ngx-pwa/local-storage';
import { ApiRoutes } from 'src/environments/api-routes';
import { ResponseGenerateAuthentificationView } from '../models/authentification/response/response-generation-authentification-view';
import * as jwt_decode from 'jwt-decode';

@Injectable({
    providedIn: 'root'
})

export class AuthHelper {

    private readonly _userDataKey: string = 'USER_KEY';
    private authSubject: BehaviorSubject<boolean>;
    private _userData: ResponseGenerateAuthentificationView;

    constructor(
        private router: Router,
        private storage: LocalDatabase,
        private apiRoutes: ApiRoutes
    ) {
        this.authSubject = new BehaviorSubject(false);
        this.storage.get<ResponseGenerateAuthentificationView>(this._userDataKey).subscribe((data) => {
            this._userData = data;
        })
    }

    login(response: ResponseGenerateAuthentificationView): void {
        this.storage.set(this._userDataKey, response).subscribe();

        this.router.navigate(['/managment/main-screen']);
    }

   
    isAuth(): Promise<boolean> {
        return new Promise((resolve) => {
            if (!this._userData) {
                this.storage.get<ResponseGenerateAuthentificationView>(this._userDataKey).subscribe((data) => {
                    resolve(!!data);
                });
            }
            else {
                resolve(!!this._userData);
            }
        });
    }

    public getRoleFromToken(): string {
        let role = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

        let res = this.getDecodedAccessToken(this.accessToken);
        if (!res) {
            return undefined;
        }

        return res[role];
    }

    private get accessToken(): string {
        if (this._userData) {
            return this._userData.accessToken;
        }
        return undefined;
    }

    private getDecodedAccessToken(token: string) {
        try {
            return jwt_decode(token);
        } catch (error) {
            return null;
        }
    }
}