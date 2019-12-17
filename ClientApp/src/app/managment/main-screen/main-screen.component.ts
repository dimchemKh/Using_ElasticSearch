import { Component, OnInit } from '@angular/core';
import { MainScreenService } from 'src/app/core/services/main-screen.service';
import { ResponseFiltersMainScreenView } from 'src/app/shared/models/main-screen/response/response-filter-main-screen-view';
import { RequestGetFiltersMainScreenView } from 'src/app/shared/models/main-screen/request/request-get-filters-main-screen-view';
import { MatSelectChange, PageEvent } from '@angular/material';
import { NgModel, FormControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ResponseSearchMainScreenView } from 'src/app/shared/models/main-screen/response/response-search-main-screen-view';
import { STICKY_COLUMNS, REPETED_COLUMNS, DAY_PREFIX, GROUP_HEADERS } from 'src/app/managment/main-screen/shared/constants/column-names';
import { FilterName } from 'src/app/shared/enums/filter-name.enum';
import * as _ from 'lodash';
import { RequestSearchMainScreenParameters } from 'src/app/shared/models/main-screen/request/request-search-main-screen-parameters';
import { FILTERS_NAMES } from 'src/app/managment/main-screen/shared/constants/filters-names';
import { TableView } from './shared/views/table-view';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.scss']
})

export class MainScreenComponent implements OnInit {

  public responseFilters: ResponseFiltersMainScreenView;
  public requestGetFilters: RequestGetFiltersMainScreenView;
  public responseSearch: ResponseSearchMainScreenView;
  public requestSearch: RequestSearchMainScreenParameters;
  public readonly filterNames = FILTERS_NAMES;
  public numberFrom: number;
  public numberTo: number;

  constructor(
    private mainScreenService: MainScreenService
  ) {
    this.responseFilters = new ResponseFiltersMainScreenView();
    this.requestGetFilters = new RequestGetFiltersMainScreenView();
    this.responseSearch = new ResponseSearchMainScreenView();
    this.requestSearch = new RequestSearchMainScreenParameters();
  }

  get displayedColumns(): string[] {
    const stickyColumns = STICKY_COLUMNS.map(c => c.name);

    return stickyColumns.concat(this.repetedColumns.map(z => z.name));
  }

  get groupHeadersNames(): string[] {
    return GROUP_HEADERS.map(h => h.name);
  }

  get groupHeaders(): any {
    const headers = GROUP_HEADERS;

    return headers;
  }

  fromPercent(num: number): number | null {
    return num !== null ? num / 100 : null;
  }

  toPercent(num: number): number | null {
    return num !== null ? num * 100 : null;
  }

  get repetedColumns(): Array<TableView> {
    const names = Array<TableView>();

    DAY_PREFIX.forEach(day => {
      REPETED_COLUMNS.forEach(column => {
        const tab: TableView = { name: day.prefix + column.name, viewName: column.viewName };
        names.push(tab);
      });
    });

    return names;
  }

  drawBorderCells(columnName: string): boolean {
    const column = columnName.substring(3);

    return column === REPETED_COLUMNS[REPETED_COLUMNS.length - 1].name;
  }

  drawBorderHeaders(columnName: string): boolean {
    const column = columnName.substring(4);

    return column === 'header';
  }

  getFilters(currentFilter: number): void {
    this.requestGetFilters.size = 100;
    this.requestGetFilters.currentFilter = currentFilter;

    this.mainScreenService.getFilters(this.requestGetFilters).subscribe((data) => {
      this.responseFilters = data;
      this.requestSearch.filters[FilterName[currentFilter]] = data.items;
    });
  }

  clearSelect(event: MatSelectChange): void {
    const model = (event.source.ngControl as NgModel).model as Array<string>;

    if (model[0] !== null) {
      this.requestGetFilters.filters.responsibleRevenueManager.forEach((name: string) => {
        if (name !== model[0]) {
          this.requestGetFilters.filters.responsibleRevenueManager = [name];
        }
      });
    }
  }

  search(): void {
    this.requestSearch.filters = _.cloneDeep(this.requestGetFilters.filters);

    this.mainScreenService.search(this.requestSearch).subscribe((data) => {
      this.responseSearch = data;
    });
  }

  reset() {
    this.requestGetFilters.reset();
    this.search();
  }

  filterRange(event: PageEvent): void {
    this.requestSearch.from = event.pageIndex * event.pageSize;
    this.requestSearch.size = event.pageSize;

    this.search();
  }

  ngOnInit() {
    this.requestSearch.size = 5;
    this.search();
  }

}
