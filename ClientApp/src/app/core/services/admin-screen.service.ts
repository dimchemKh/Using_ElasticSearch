import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestGetLogsAdminScreenView } from 'src/app/shared/models/admin-screen/request/request-get-logs-admin-screen-view';
import { Observable } from 'rxjs';
import { ResponseGetLogsAdminScreen } from 'src/app/shared/models/admin-screen/response/response-get-logs-admin-screen-view';
import { ApiRoutes } from 'src/environments/api-routes';

@Injectable({ providedIn: 'root' })

export class AdminScreenService {

    constructor(
        public http: HttpClient,
        public apiRoutes: ApiRoutes
    ) {
    }

    public getLogs(model: RequestGetLogsAdminScreenView): Observable<ResponseGetLogsAdminScreen> {
        return this.http.post<ResponseGetLogsAdminScreen>(this.apiRoutes.adminScreenRoute + 'getLogs', model);
    }
}