import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { AdminScreenComponent } from './admin-screen/admin-screen.component';
import { MaterialModule } from '../shared/modules/material.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ManagementComponent } from './management.component';
import { ManagementRoutingModule } from './management-routing.module';
import { LogsScreenComponent } from './logs-screen/logs-screen.component';
import { AdminScreenDialogComponent } from '../shared/components/admin-screen-dialog/admin-screen-dialog.component';
import { PermissionPipe } from '../shared/pipes/permission-pipe';


@NgModule({
  declarations: [
    MainScreenComponent,
    AdminScreenComponent,
    ManagementComponent,
    LogsScreenComponent,
    AdminScreenDialogComponent,
    PermissionPipe
  ],
  imports: [
    ManagementRoutingModule,
    CommonModule,
    MaterialModule,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [DatePipe],
  entryComponents: [AdminScreenDialogComponent]
})
export class ManagementModule { }
