import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RequestGetLogsAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-logs-admin-screen-model';
import { ResponseGetLogsAdminScreenModel } from 'src/app/shared/models/admin-screen/response/response-get-logs-admin-screen-model';
import { Observable } from 'rxjs';
import { ApiRoutes } from 'src/environments/api-routes';
import { flatMap } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})

export class LogsScreenService {
    constructor(
        private http: HttpClient,
        private apiRoutes: ApiRoutes
    ) {        
    }
    
    getLogs(model: RequestGetLogsAdminScreenModel): Observable<ResponseGetLogsAdminScreenModel> {
        return this.http.post<ResponseGetLogsAdminScreenModel>(this.apiRoutes.logsScreenRoute + 'getLogs', model);
    }
}