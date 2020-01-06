import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestLoginAuthentificationView } from 'src/app/shared/models/authentification/request/request-login-authentification.view';
import { Observable } from 'rxjs';
import { ResponseGenerateAuthentificationView } from 'src/app/shared/models/authentification/response/response-generation-authentification-view';
import { ApiRoutes } from 'src/environments/api-routes';
import { RequestRefreshAuthentificationView } from 'src/app/shared/models/authentification/request/request-refresh-authentification.view';

@Injectable({ providedIn: 'root' })

export class AuthentificationService {

    constructor(
        public http: HttpClient,
        public apiRoutes: ApiRoutes
    ) {        
    }

    login(model: RequestLoginAuthentificationView): Observable<ResponseGenerateAuthentificationView> {         
        return this.http.post<ResponseGenerateAuthentificationView>(this.apiRoutes.authentificationRoute + 'login', model);
    }

    refresh(model: RequestRefreshAuthentificationView): Observable<ResponseGenerateAuthentificationView> {
        return this.http.post<ResponseGenerateAuthentificationView>(this.apiRoutes.authentificationRoute + 'refresh', model);
    }
}