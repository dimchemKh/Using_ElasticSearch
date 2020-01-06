import { Component, OnInit } from '@angular/core';
import { AdminScreenService } from 'src/app/core/services/admin-screen.service';
import { TABLE_LOGS_COLUMNS_NAMES, TABLE_USERS_COLUMNS_NAMES } from 'src/app/management/admin-screen/shared/column-names';
import { trigger, state, transition, style, animate } from '@angular/animations';
import { ResponseGetLogsAdminScreenModel } from 'src/app/shared/models/admin-screen/response/response-get-logs-admin-screen-model';
import { RequestGetLogsAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-logs-admin-screen-model';
import { ResponseGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/response/response-get-users-admin-screen-model';
import { RequestGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-users-admin-screen-model';
import { RequestUserAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-user-admin-screen-model';
import { ApplicationUser } from 'src/app/shared/models/application-user';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { UserRoles } from 'src/app/shared/enums/user-roles';
import { TableModel } from 'src/app/shared/models/table-model';
import { Patterns } from 'src/app/shared/constants/patterns';
import { PageEvent } from '@angular/material';

@Component({
  selector: 'app-admin-screen',
  templateUrl: './admin-screen.component.html',
  styleUrls: ['./admin-screen.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class AdminScreenComponent implements OnInit {

  responseGetLogs: ResponseGetLogsAdminScreenModel;
  requestGetLogs: RequestGetLogsAdminScreenModel;
  responseGetUsers: ResponseGetUsersAdminScreenModel;
  requestGetUsers: RequestGetUsersAdminScreenModel;
  requestUser: RequestUserAdminScreenModel;
  expandedUser: ApplicationUser | null;
  formGroup: FormGroup;

  UserRoles = UserRoles;


  constructor(
    private adminScreenService: AdminScreenService,
    private fb: FormBuilder,
    private patterns: Patterns
  ) {
    this.responseGetLogs = new ResponseGetLogsAdminScreenModel();
    this.requestGetLogs = new RequestGetLogsAdminScreenModel();
    this.responseGetUsers = new ResponseGetUsersAdminScreenModel();
    this.requestGetUsers = new RequestGetUsersAdminScreenModel();
    this.requestUser = new RequestUserAdminScreenModel();
    this.formGroup = this.fb.group({
      firstName: new FormControl(null, Validators.required),
      lastName: new FormControl(null, Validators.required),
      email: new FormControl(null, [
        Validators.required,
        Validators.pattern(this.patterns.email)
      ]),
      password: new FormControl(null, Validators.required),
      role: new FormControl(null, Validators.required)
    });
    this.requestGetLogs.from = 0;
    this.requestGetLogs.size = 10;
    this.requestGetUsers.from = 0;
    this.requestGetUsers.size = 5;
  }

  get logColumnsNames(): Array<TableModel> {
    return TABLE_LOGS_COLUMNS_NAMES.map<TableModel>(x => x);
  }

  get logDisplayColumnsNames(): string[] {
    return TABLE_LOGS_COLUMNS_NAMES.map(x => x.name);
  }

  get usersColumnsNames(): Array<TableModel> {
    return TABLE_USERS_COLUMNS_NAMES.map<TableModel>(x => x);
  }

  get usersDisplayColumnsNames(): string[] {
    return TABLE_USERS_COLUMNS_NAMES.map(x => x.name);
  }

  get roles(): string[] {
    let roles = Object.keys(UserRoles);

    return roles.slice(roles.length / 2);
  }

  get formControls() {
    return this.formGroup.controls;
  }

  isControlInvalid(controlName: string): boolean {

    let control = this.formGroup.controls[controlName];

    let result = control.invalid && control.touched;

    return result;
  }

  private checkError(control: string, patternError: string, requiredError: string): string {
    if (this.formControls[control].hasError('pattern')) {
      return patternError;
    }
    if (this.formControls[control].hasError('required')) {
      return requiredError;
    }
  }

  getLastNameErrorMessage(): string {
    return this.checkError('lastName', 'Incorrect name', 'Empty field');
  }

  getFirstNameErrorMessage(): string {

    return this.checkError('firstName', 'Incorrect name', 'Empty field');
  }

  getEmailErrorMessage(): string {
    return this.checkError('email', 'Incorrect email', 'Empty email');
  }

  getPasswordErrorMessage(): string {
    return this.checkError('password', '', 'Empty password');
  }

  getLogs(): void {
    this.adminScreenService.getLogs(this.requestGetLogs).subscribe(data => {
      this.responseGetLogs = data;
    });
  }

  getUsers(): void {
    this.adminScreenService.getUsers(this.requestGetUsers).subscribe(data => {
      this.responseGetUsers = data;
    });
  }

  createUser(): void {
    if (!this.formGroup.invalid) {
      this.adminScreenService.createUser(this.requestUser).subscribe();
    }
  }

  updateUser(): void {
    this.adminScreenService.updateUser(this.requestUser).subscribe();
  }

  removeUser(): void {
    // TODO userId
    // this.adminScreenService.removeUser()
  }

  resetUser(): void {
    this.formGroup.reset();
    this.requestUser.reset();
  }

  changePage(event: PageEvent, requestName: string, requestMethod: string): void {
    this[requestName].from = event.pageIndex * event.pageSize;
    this[requestName].size = event.pageSize;

    this[requestMethod]();
  }

  ngOnInit(): void {
    this.getUsers();

    this.getLogs();
  }

}
