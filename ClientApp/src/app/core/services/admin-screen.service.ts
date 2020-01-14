import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiRoutes } from 'src/environments/api-routes';
import { RequestGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-users-admin-screen-model';
import { ResponseGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/response/response-get-users-admin-screen-model';
import { RequestCreateUserAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-user-admin-screen-model';
import { RequestGetPermissionAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-permissions-screen-model';
import { ResponseGetPermissionsAdminScreenModel } from 'src/app/shared/models/admin-screen/response/resposne-get-permissions-admin-screen-model';

@Injectable({ 
    providedIn: 'root' 
})

export class AdminScreenService {

    constructor(
        public http: HttpClient,
        public apiRoutes: ApiRoutes
    ) {
    }
    
    getPermissions(request: RequestGetPermissionAdminScreenModel): Observable<ResponseGetPermissionsAdminScreenModel> {
        return this.http.post<ResponseGetPermissionsAdminScreenModel>(this.apiRoutes.adminScreenRoute + 'getPermissions', request );
    }
    getUsers(model: RequestGetUsersAdminScreenModel): Observable<ResponseGetUsersAdminScreenModel> {
        return this.http.post<ResponseGetUsersAdminScreenModel>(this.apiRoutes.adminScreenRoute + 'getUsers', model);
    }
    createUser(model: RequestCreateUserAdminScreenModel): Observable<Array<string>> {
        return this.http.post<Array<string>>(this.apiRoutes.adminScreenRoute + 'create', model);
    }
    updateUser(model: RequestCreateUserAdminScreenModel): Observable<any> {
        return this.http.post(this.apiRoutes.adminScreenRoute + 'update', model);
    }
    removeUser(userId: string): Observable<any> {
        return this.http.post(this.apiRoutes.adminScreenRoute + 'remove', userId);
    }
}