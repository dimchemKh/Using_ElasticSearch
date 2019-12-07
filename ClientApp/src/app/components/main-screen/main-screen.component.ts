
import { Component, OnInit } from '@angular/core';
import { WebAppData } from 'src/app/shared/models/web-app-data';
import { RequestFilterParametersMainScreen } from 'src/app/shared/models/post/request-filter-parameters-main-screen';
import { ElasticsearchService } from 'src/app/core/services/elasticsearch.service';
import { GetSearchDataView } from 'src/app/shared/models/get/get-search-data-view';
import { PageEvent } from '@angular/material';

@Component({
  selector: 'app-main-screen',
  templateUrl: './main-screen.component.html',
  styleUrls: ['./main-screen.component.scss']
})
export class MainScreenComponent implements OnInit {

  public cloumnNames: string[];
  public dataSource: GetSearchDataView;
  public filter: RequestFilterParametersMainScreen;

  constructor(
    private elasticService: ElasticsearchService
  ) {
    this.cloumnNames =
      [
        'ParkName',
        'AccommName',
        'AccommBeds',
        'AccommTypeName',
        'UnitGradeName',
        'AccommPax',
        'ArrivalDateRevised'
      ];
    this.dataSource = new GetSearchDataView();
    this.filter = new RequestFilterParametersMainScreen();
    this.filter.from = 0;
    this.filter.columnName = 'friCurrentFitPrice';
  }

  filterRange(event: PageEvent = null): void {
    if (event) {
      this.filter.from = event.pageIndex * event.pageSize;
      this.filter.size = event.pageSize;
    }
    this.elasticService.getRange(this.filter).subscribe((data: GetSearchDataView) => {
      this.dataSource = data;
    });
  }

  ngOnInit() {
    // this.filter.columnName = 'holidayYear';
    // this.filter.size = 10;
    // this.filter.values = ['2019'];
    // this.elasticService.getSearch(this.filter).subscribe((data: WebAppData[]) => {
    //   this.dataSource = data;
    // });
  }

}
