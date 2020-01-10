import { Component, OnInit } from '@angular/core';
import { ResponseGetLogsAdminScreenModel } from 'src/app/shared/models/logs-screen/response/response-get-logs-admin-screen-model';
import { RequestGetLogsAdminScreenModel } from 'src/app/shared/models/logs-screen/request/request-get-logs-admin-screen-model';
import { TableModel } from 'src/app/shared/models/table-model';
import { TABLE_LOGS_COLUMNS_NAMES } from 'src/app/shared/constants/table-columns';
import { LogsScreenService } from 'src/app/core/services/logs-screen.service';
import { PageEvent } from '@angular/material';
import { Observable, of } from 'rxjs';
import { LogException } from 'src/app/shared/models/log-exception';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-logs-screen',
  templateUrl: './logs-screen.component.html',
  styleUrls: ['./logs-screen.component.scss']
})
export class LogsScreenComponent implements OnInit {

  responseGetLogs: ResponseGetLogsAdminScreenModel;
  requestGetLogs: RequestGetLogsAdminScreenModel;
  exceptions: Observable<LogException>;

  constructor(
    private logsScreenService: LogsScreenService,
    private datePipe: DatePipe
  ) {
    this.responseGetLogs = new ResponseGetLogsAdminScreenModel();
    this.requestGetLogs = new RequestGetLogsAdminScreenModel();
    this.requestGetLogs.from = 0;
    this.requestGetLogs.size = 10;
  }

  get logColumnsNames(): Array<TableModel> {
    return TABLE_LOGS_COLUMNS_NAMES.map<TableModel>(x => x);
  }

  get logDisplayColumnsNames(): string[] {
    return TABLE_LOGS_COLUMNS_NAMES.map(x => x.name);
  }

  pipe(name: string): string {
    let date = new Date(name);
    
    return this.datePipe.transform(date, 'yyyy/MM/dd');
  }

  getLogs(): void {
    this.logsScreenService.getLogs(this.requestGetLogs).subscribe(data => {
      this.responseGetLogs = data;
    });
  }

  changePage(event: PageEvent): void {
    this.requestGetLogs.from = event.pageIndex * event.pageSize;
    this.requestGetLogs.size = event.pageSize;

    this.getLogs();
  }

  ngOnInit() {
    this.getLogs();
  }

}
