import { Component, OnInit } from '@angular/core';
import { AdminScreenService } from 'src/app/core/services/admin-screen.service';
import { TABLE_USERS_COLUMNS_NAMES } from 'src/app/shared/constants/table-columns';
import { ResponseGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/response/response-get-users-admin-screen-model';
import { RequestGetUsersAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-get-users-admin-screen-model';
import { ApplicationUser } from 'src/app/shared/models/application-user';
import { UserRole } from 'src/app/shared/enums/user-roles';
import { TableModel } from 'src/app/shared/models/table-model';
import { Patterns } from 'src/app/shared/constants/patterns';
import { PageEvent, MatIconRegistry, MatDialog, MatDialogRef } from '@angular/material';
import { ToastrService } from 'ngx-toastr';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthHelper } from 'src/app/shared/helpers/auth.helper';
import { AdminScreenDialogComponent } from '../../shared/components/admin-screen-dialog/admin-screen-dialog.component';

export const CONTROLS_ERRORS = [
  { name: 'firstName', message: 'Incorrect First name', default: 'Empty name' },
  { name: 'lastName', message: 'Incorrect Last name', default: 'Empty name' },
  { name: 'email', message: 'Incorrect email', default: 'Empty email' },
  { name: 'phoneNumber', message: 'Incorrect phone', default: 'Empty field' },
  { name: 'password', message: 'Incorrect password', default: 'Empty password' },
  { name: 'role', message: '', default: 'Choose role' },
];

@Component({
  selector: 'app-admin-screen',
  templateUrl: './admin-screen.component.html',
  styleUrls: ['./admin-screen.component.scss']
})

export class AdminScreenComponent implements OnInit {

  responseGetUsers: ResponseGetUsersAdminScreenModel;
  requestGetUsers: RequestGetUsersAdminScreenModel;

  expandedUser: ApplicationUser | null;
  identityErrors: Array<string>;
  isSysAdmin: boolean;

  UserRoles = UserRole;

  constructor(
    private adminScreenService: AdminScreenService,
    private dialog: MatDialog,
    private authHelper: AuthHelper,
    private matIconRegistry: MatIconRegistry,
    private domSanitizer: DomSanitizer
  ) {

    this.responseGetUsers = new ResponseGetUsersAdminScreenModel();
    this.requestGetUsers = new RequestGetUsersAdminScreenModel();

    this.identityErrors = new Array<string>();

    this.initIcons();
    this.requestGetUsers.from = 0;
    this.requestGetUsers.size = 5;
  }


  openDialog(actionName: string = null, element: ApplicationUser): void {
    let data: string;
    let dialogRef: MatDialogRef<AdminScreenDialogComponent, any>;

    if (!actionName) {
      data = 'Create user';
      dialogRef = this.dialog.open(AdminScreenDialogComponent, {
        data: { 
          name: data,
          action: 'create'
        }
      });
    }
    if (actionName) {
      data = actionName + ' user';
      dialogRef = this.dialog.open(AdminScreenDialogComponent, {
        data: {
          name: data,
          action: 'update',
          model: {
            userId: element.id,
            firstName: element.firstName,
            lastName: element.lastName,
            role: element.role,
            email: element.email,
            phoneNumber: element.phoneNumber,
          }
        }
      });
    }

    dialogRef.afterClosed().subscribe((result) => {
      if (result) {

      }
    });
  }

  private initIcons(): void {
    this.registryIcon('createIcon', 'add-24px.svg');
    this.registryIcon('changeIcon', 'create-24px.svg');
    this.registryIcon('removeIcon', 'clear-24px.svg');
  }

  private registryIcon(name: string, path: string): void {
    this.matIconRegistry.addSvgIcon(
      name,
      this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/icons/' + path)
    );
  }

  get usersColumnsNames(): Array<TableModel> {
    return TABLE_USERS_COLUMNS_NAMES.map<TableModel>(x => x);
  }

  get usersDisplayColumnsNames(): string[] {
    let colums = TABLE_USERS_COLUMNS_NAMES.map(x => x.name);
    colums.push('icons');
    return colums;
  }



  async getRole(): Promise<void> {
    let role = await this.authHelper.getRoleFromToken().then(x => x);

    if (role === UserRole[UserRole.SysAdmin]) {
      this.isSysAdmin = true;
    }
  }

  getUsers(): void {
    this.adminScreenService.getUsers(this.requestGetUsers).subscribe(data => {
      this.responseGetUsers = data;
    });
  }


  changePage(event: PageEvent): void {
    this.requestGetUsers.from = event.pageIndex * event.pageSize;
    this.requestGetUsers.size = event.pageSize;

    this.getUsers();
  }



  ngOnInit(): void {
    this.getRole();
    this.getUsers();
  }

}
