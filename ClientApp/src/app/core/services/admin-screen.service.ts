import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiRoutes } from 'src/environments/api-routes';
import { RequestGetLogsAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-logs-admin-screen-model';
import { ResponseGetLogsAdminScreenModel } from 'src/app/shared/models/admin-screen/response/response-get-logs-admin-screen-model';
import { RequestGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-users-admin-screen-model';
import { ResponseGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/response/response-get-users-admin-screen-model';
import { RequestUserAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-user-admin-screen-model';

@Injectable({ providedIn: 'root' })

export class AdminScreenService {

    constructor(
        public http: HttpClient,
        public apiRoutes: ApiRoutes
    ) {
    }
    
    getUsers(model: RequestGetUsersAdminScreenModel): Observable<ResponseGetUsersAdminScreenModel> {
        return this.http.post<ResponseGetUsersAdminScreenModel>(this.apiRoutes.adminScreenRoute + 'getUsers', model);
    }
    createUser(model: RequestUserAdminScreenModel): Observable<Array<string>> {
        return this.http.post<Array<string>>(this.apiRoutes.adminScreenRoute + 'create', model);
    }
    updateUser(model: RequestUserAdminScreenModel): Observable<any> {
        return this.http.post(this.apiRoutes.adminScreenRoute + 'update', model);
    }
    removeUser(userId: string): Observable<any> {
        return this.http.post(this.apiRoutes.adminScreenRoute + 'remove', userId);
    }
}