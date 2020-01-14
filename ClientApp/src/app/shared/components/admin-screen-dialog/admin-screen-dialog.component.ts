import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatIconRegistry } from '@angular/material';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Patterns } from 'src/app/shared/constants/patterns';
import { RequestCreateUserAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-user-admin-screen-model';
import { UserRole } from 'src/app/shared/enums/user-roles';
import { CONTROLS_ERRORS } from '../../../management/admin-screen/admin-screen.component';
import { ToastrService } from 'ngx-toastr';
import { AdminScreenService } from 'src/app/core/services/admin-screen.service';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthHelper } from '../../helpers/auth.helper';
import { UserPermission } from '../../enums/user-permissons';
import { PagePermission } from '../../enums/page-permission';
import { PermissionModel } from '../../models/common/PermissionModel';
import { RequestGetPermissionAdminScreenModel } from '../../models/admin-screen/request/request-get-permissions-screen-model';
import { ResponseGetPermissionsAdminScreenModel } from '../../models/admin-screen/response/resposne-get-permissions-admin-screen-model';

@Component({
    selector: 'app-admin-screen-dialog',
    templateUrl: 'admin-screen-dialog.component.html',
    styleUrls: ['./admin-screen-dialog.component.scss']
})

export class AdminScreenDialogComponent implements OnInit {

    nameView: string;
    action: string;
    formGroup: FormGroup;
    requestUser: RequestCreateUserAdminScreenModel;
    isExistedData: boolean;
    userRole: string;
    // UserPermission = UserPermission;
    PagePermission = PagePermission;
    mainScreen = new Array<string>();
    adminScreen = new Array<string>();
    logsScreen = new Array<string>();


    constructor(
        private dialogRef: MatDialogRef<AdminScreenDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private data: any,
        private authHelper: AuthHelper,
        private patterns: Patterns,
        private fb: FormBuilder,
        private toastr: ToastrService,
        private adminScreenService: AdminScreenService,
        private matIconRegistry: MatIconRegistry,
        private domSanitizer: DomSanitizer
    ) {
        this.requestUser = new RequestCreateUserAdminScreenModel();
        this.nameView = data.name;
        this.action = data.action;
        this.initFormGroup();
        this.initIcons();
    }

    eventSelection(event: Array<string>, page: PagePermission) {
        this.requestUser.permissions[page] = new PermissionModel();
        this.requestUser.permissions[page].page = page;

        event.forEach((data) => {
            this.requestUser.permissions[page][data] = !this.requestUser.permissions[page][data];
            this.requestUser.permissions[page].userId = this.requestUser.userId;
        });
    }

    private initFormGroup(): void {
        this.formGroup = this.fb.group({
            firstName: new FormControl(null, Validators.required),
            lastName: new FormControl(null, Validators.required),
            email: new FormControl(null, [
                Validators.required,
                Validators.pattern(this.patterns.email)
            ]),
            phoneNumber: new FormControl(null, [
                Validators.required,
                Validators.pattern('^((\\+91-?)|0)?[0-9]{12}$')
            ]),
            password: new FormControl(null, Validators.required),
            role: new FormControl(null, Validators.required)
        });
    }

    get roles(): string[] {
        return this.reflectEnum(UserRole);
    }

    get permissions(): string[] {
        return this.reflectEnum(UserPermission);
    }

    get pagePermission(): string[] {
        return this.reflectEnum(PagePermission);
    }

    private reflectEnum(enumName: object) {
        let roles = Object.keys(enumName);

        return roles.slice(roles.length / 2);
    }

    get formControls() {
        return this.formGroup.controls;
    }

    getUserRole(): void {
        this.authHelper.getRoleFromToken().then(x => {
            this.userRole = x;
        });
    }

    isControlInvalid(controlName: string): boolean {
        let control = this.formControls[controlName];

        return control.invalid;
    }

    isPermissionAccess(item: UserRole): boolean {
        if (this.requestUser.role === item) {
            return true;
        }
        return false;
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

    invokeAction(actionName: string): void {
        this[actionName + 'User']();
    }

    private createUser(): void {
        debugger
        // if (!this.formGroup.invalid) {
        this.adminScreenService.createUser(this.requestUser).subscribe((errors) => {
            if (errors.length > 0) {
                this.showError(errors);
            }
        });
        // }
        this.formGroup.markAllAsTouched();
    }

    updateUser(): void {
        debugger
        this.adminScreenService.updateUser(this.requestUser).subscribe();
    }

    removeUser(): void {
        // TODO userId

    }

    resetUser(): void {
        this.formGroup.reset();
        this.requestUser.reset();
    }

    showError(error: string[]): void {
        let message = error.join('\n');
        this.toastr.warning('Error', message);
    }

    close(): void {
        this.dialogRef.close(this.isExistedData);
    }
    private initIcons(): void {
        this.registryIcon('removeIcon', 'clear-24px.svg');
    }

    private registryIcon(name: string, path: string): void {
        this.matIconRegistry.addSvgIcon(
            name,
            this.domSanitizer.bypassSecurityTrustResourceUrl('../assets/icons/' + path)
        );
    }

    private initPermissions(): void {
        this.requestUser.permissions = new Array<PermissionModel>();
    }

    // pemrissionsForUser: PermissionModel[] = [];

    private getUserPermissions(): void {
        let request: RequestGetPermissionAdminScreenModel = { userId: this.data.model.userId };

        this.adminScreenService.getPermissions(request).subscribe((data: ResponseGetPermissionsAdminScreenModel) => {
            data.items.forEach(element => {
                this[PagePermission[element.page]] = this.getCorectPermissionView(element);

                this.eventSelection(this[PagePermission[element.page]], element.page);
            });
        });
    }


    private getCorectPermissionView(data: PermissionModel): Array<string> {
        let tempArray = new Array<string>();

        this.permissions.forEach(elem => {
            if (data[elem]) {
                tempArray.push(elem);
            }
        });

        return tempArray;
    }

    ngOnInit(): void {
        if (this.data.model) {
            this.requestUser = this.data.model;
            this.initPermissions();
            this.getUserPermissions();
        }
        this.initPermissions();
        this.getUserRole();
    }
}