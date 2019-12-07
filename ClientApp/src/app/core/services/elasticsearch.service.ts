import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiRoutes } from 'src/environments/api-routes';
import { RequestFilterParametersMainScreen } from 'src/app/shared/models/post/request-filter-parameters-main-screen';
import { GetSearchDataView } from 'src/app/shared/models/get/get-search-data-view';

@Injectable({providedIn: 'root'})

export class ElasticsearchService {

  constructor(
    private http: HttpClient,
    private apiRoutes: ApiRoutes
    ) {

  }

  public getSearch(model: RequestFilterParametersMainScreen): Observable<GetSearchDataView> {
    return this.http.post<GetSearchDataView>(this.apiRoutes.mainScreenRoute + 'search', model, {
      withCredentials: true
    });
  }
  public getRange(model: RequestFilterParametersMainScreen): Observable<GetSearchDataView> {
    return this.http.post<GetSearchDataView>(this.apiRoutes.mainScreenRoute + 'range', model);
  }
}
