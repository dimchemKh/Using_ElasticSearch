import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestLoginAuthentificationView } from 'src/app/shared/models/authentification/request/request-login-authentification-view';
import { Observable } from 'rxjs';
import { ResponseGenerateAuthentificationView } from 'src/app/shared/models/authentification/response/response-generation-authentification-view';
import { ApiRoutes } from 'src/environments/api-routes';

@Injectable({ providedIn: 'root' })

export class AuthentificationService {

    constructor(
        public http: HttpClient,
        public apiRoutes: ApiRoutes
    ) {        
    }

    public login(model: RequestLoginAuthentificationView): Observable<ResponseGenerateAuthentificationView> {
        return this.http.post<ResponseGenerateAuthentificationView>(this.apiRoutes.authentificationRoute + 'auth', model);
    }
}