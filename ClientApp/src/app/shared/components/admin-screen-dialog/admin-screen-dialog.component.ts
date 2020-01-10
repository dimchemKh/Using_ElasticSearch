import { Component, OnInit, Injectable, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatIconRegistry } from '@angular/material';
import { FormControl, Validators, FormGroup, FormBuilder } from '@angular/forms';
import { Patterns } from 'src/app/shared/constants/patterns';
import { RequestUserAdminScreenModel } from 'src/app/shared/models/admin-screen/request/request-user-admin-screen-model';
import { UserRole } from 'src/app/shared/enums/user-roles';
import { CONTROLS_ERRORS } from '../../../management/admin-screen/admin-screen.component';
import { ToastrService } from 'ngx-toastr';
import { AdminScreenService } from 'src/app/core/services/admin-screen.service';
import { DomSanitizer } from '@angular/platform-browser';
import { IfStmt } from '@angular/compiler';

@Component({
    selector: 'app-admin-screen-dialog',
    templateUrl: 'admin-screen-dialog.component.html',
    styleUrls: ['./admin-screen-dialog.component.scss']
})

export class AdminScreenDialogComponent implements OnInit {

    nameView: string;
    formGroup: FormGroup;
    requestUser: RequestUserAdminScreenModel;
    isExistedData: boolean;

    constructor(
        private dialogRef: MatDialogRef<AdminScreenDialogComponent>,
        @Inject(MAT_DIALOG_DATA) private data: any,
        private patterns: Patterns,
        private fb: FormBuilder,
        private toastr: ToastrService,
        private adminScreenService: AdminScreenService,
        private matIconRegistry: MatIconRegistry,
        private domSanitizer: DomSanitizer
    ) {
        this.requestUser = new RequestUserAdminScreenModel();
        this.nameView = data.name;
        this.initFormGroup();
        this.initIcons();
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
        let roles = Object.keys(UserRole);

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

    createUser(): void {
        if (!this.formGroup.invalid) {
            this.adminScreenService.createUser(this.requestUser).subscribe((errors) => {
                if (errors.length > 0) {
                    this.showError(errors);
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

    }

    resetUser(): void {
        this.formGroup.reset();
        this.requestUser.reset();
    }


    showError(error: string[]) {
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
    ngOnInit() {
        if (this.data.model) {
            this.requestUser = this.data.model;
        }
     }
}