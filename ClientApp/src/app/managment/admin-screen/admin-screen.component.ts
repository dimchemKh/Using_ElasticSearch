import { Component, OnInit } from '@angular/core';
import { AdminScreenService } from 'src/app/core/services/admin-screen.service';
import { ResponseGetLogsAdminScreen } from 'src/app/shared/models/admin-screen/response/response-get-logs-admin-screen-view';
import { RequestGetLogsAdminScreenView } from 'src/app/shared/models/admin-screen/request/request-get-logs-admin-screen-view';
import { COLUMN_NAMES } from 'src/app/managment/admin-screen/shared/column-names';

@Component({
  selector: 'app-admin-screen',
  templateUrl: './admin-screen.component.html',
  styleUrls: ['./admin-screen.component.scss']
})
export class AdminScreenComponent implements OnInit {

  public reponseGetLogs: ResponseGetLogsAdminScreen;
  public requestGetLogs: RequestGetLogsAdminScreenView;
  constructor(
    public adminScreenService: AdminScreenService
  ) { 
    this.reponseGetLogs = new ResponseGetLogsAdminScreen();
    this.requestGetLogs = new RequestGetLogsAdminScreenView();
    this.requestGetLogs.from = 0;
    this.requestGetLogs.size = 10;
  }

  get displayedColumns(): string[] {
    return COLUMN_NAMES.map(c => c.name);
  }

  getLogs(): void {
    this.adminScreenService.getLogs(this.requestGetLogs).subscribe((data) => {
      this.reponseGetLogs = data;
    })
  }

  ngOnInit(): void {
    this.getLogs();
  }

}
