import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiRoutes } from 'src/environments/api-routes';
import { RequestGetFiltersMainScreenView } from 'src/app/shared/models/request/request-get-filters-main-screen-view';
import { ResponseFiltersMainScreenView } from 'src/app/shared/models/response/response-filter-main-screen-view';
import { Observable } from 'rxjs';
import { ResponseSearchMainScreenView } from 'src/app/shared/models/response/response-search-main-screen-view';
import { RequestSearchMainScreenParameters } from 'src/app/shared/models/request/request-search-main-screen-parameters';

@Injectable({providedIn: 'root'})

export class MainScreenService {

  constructor(
    private http: HttpClient,
    private apiRoutes: ApiRoutes
    ) {
  }

  public getFilters(model: RequestGetFiltersMainScreenView): Observable<ResponseFiltersMainScreenView> {
    return this.http.post<ResponseFiltersMainScreenView>(this.apiRoutes.mainScreenRoute + 'getFilters', model);
  }

  public search(model: RequestSearchMainScreenParameters): Observable<ResponseSearchMainScreenView> {
    return this.http.post<ResponseSearchMainScreenView>(this.apiRoutes.mainScreenRoute + 'search', model);
  }
}
