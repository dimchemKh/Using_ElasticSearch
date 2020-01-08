import { Component, OnInit } from '@angular/core';
import { AdminScreenService } from 'src/app/core/services/admin-screen.service';
import { TABLE_USERS_COLUMNS_NAMES } from 'src/app/shared/constants/table-columns';
import { trigger, state, transition, style, animate } from '@angular/animations';
import { ResponseGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/response/response-get-users-admin-screen-model';
import { RequestGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-users-admin-screen-model';
import { RequestUserAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-user-admin-screen-model';
import { ApplicationUser } from 'src/app/shared/models/application-user';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { UserRoles } from 'src/app/shared/enums/user-roles';
import { TableModel } from 'src/app/shared/models/table-model';
import { Patterns } from 'src/app/shared/constants/patterns';
import { PageEvent } from '@angular/material';
import { ToastrService } from 'ngx-toastr';

export const CONTROLS_ERRORS = [
  { name: 'firstName', message: 'Incorrect First name', default: 'Empty name' },
  { name: 'lastName', message: 'Incorrect Last name', default: 'Empty name' },
  { name: 'email', message: 'Incorrect email', default: 'Empty email' },
  { name: 'password', message: 'Incorrect password', default: 'Empty password' },
  { name: 'role', message: '', default: 'Choose role' },
];

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

  responseGetUsers: ResponseGetUsersAdminScreenModel;
  requestGetUsers: RequestGetUsersAdminScreenModel;
  requestUser: RequestUserAdminScreenModel;
  expandedUser: ApplicationUser | null;
  formGroup: FormGroup;
  identityErrors: Array<string>;

  UserRoles = UserRoles;

  constructor(
    private adminScreenService: AdminScreenService,
    private fb: FormBuilder,
    private patterns: Patterns,
    private toastr: ToastrService
  ) {

    this.responseGetUsers = new ResponseGetUsersAdminScreenModel();
    this.requestGetUsers = new RequestGetUsersAdminScreenModel();
    this.requestUser = new RequestUserAdminScreenModel();
    this.identityErrors = new Array<string>();
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
    this.requestGetUsers.from = 0;
    this.requestGetUsers.size = 5;
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

    let control = this.formControls[controlName];

    return control.invalid;
  }

  getErrorMessage(control: string): string {
    let error = CONTROLS_ERRORS.find(elem => elem.name === control);

    if (this.formControls[control].hasError('pattern')) {
      return error.message;
    }
    if (this.formControls[control].hasError('required')) {
      return error.default;
    }
  }

  getUsers(): void {
    this.adminScreenService.getUsers(this.requestGetUsers).subscribe(data => {
      this.responseGetUsers = data;
    });
  }

  createUser(): void {
    if (!this.formGroup.invalid) {
      this.adminScreenService.createUser(this.requestUser).subscribe((errors) => {
        if (errors.length > 0) {
          this.showSuccess();
        }
      });
    }
    this.formGroup.markAllAsTouched();
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

  changePage(event: PageEvent): void {
    this.requestGetUsers.from = event.pageIndex * event.pageSize;
    this.requestGetUsers.size = event.pageSize;

    this.getUsers();
  }

  showSuccess() {
    this.toastr.success('Hello world!', 'Toastr fun!');
  }

  ngOnInit(): void {
    this.getUsers();
  }

}
