import { Injectable } from '@angular/core';
import { BehaviorSubject, throwError, Observable } from 'rxjs';
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
    private userData: ResponseGenerateAuthentificationView;

    constructor(
        private router: Router,
        private storage: LocalDatabase,
        private apiRoutes: ApiRoutes
    ) {
        this.authSubject = new BehaviorSubject(false);
        this.responseToken().then(x => {
            this.userData = x;
        });
    }

    private async responseToken(): Promise<ResponseGenerateAuthentificationView> {
        return this.storage.get<ResponseGenerateAuthentificationView>(this._userDataKey).toPromise();
    }

    login(response: ResponseGenerateAuthentificationView): void {
        this.saveTokens(response);
        this.router.navigate(['/management/main-screen']);
    }

    saveTokens(response: ResponseGenerateAuthentificationView): void {
        this.storage.set(this._userDataKey, response).subscribe();
        this.userData = response;
    }

    isAuth(): Promise<boolean> {
        return new Promise((resolve) => {
            if (!this.userData) {
                this.storage.get<ResponseGenerateAuthentificationView>(this._userDataKey).subscribe((data) => {
                    resolve(!!data);
                });
            }
            else {
                resolve(!!this.userData);
            }
        });
    }

    public async getRoleFromToken(): Promise<string> {
        let role = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

        let accessToken = await this.getAccessToken();

        let res = this.getDecodedAccessToken(accessToken);

        if (!res) {
            return undefined;
        }

        return res[role];
    }

    getAccessToken(): string {
        if (!this.userData) {
            this.responseToken().then(token => {
                return token;
            });
        }
        if (this.userData) {
            return this.userData.accessToken;
        }
        return undefined;
    }

    getRefreshToken(): string {
        // if (!this.userData) {
        //     (this.storage.get<ResponseGenerateAuthentificationView>(this._userDataKey).toPromise()).then(x => {
        //         return x.refreshToken;
        //     });
        // }
        if (this.userData) {
            return this.userData.refreshToken;
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

    logOut(): void {
        this.userData = null;
        this.storage.clear().subscribe();
    }
}