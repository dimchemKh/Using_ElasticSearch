import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { AdminScreenComponent } from './admin-screen/admin-screen.component';
import { MaterialModule } from '../shared/modules/material.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ManagementComponent } from './management.component';
import { ManagementRoutingModule } from './management-routing.module';
import { LogsScreenComponent } from './logs-screen/logs-screen.component';


@NgModule({
  declarations: [
    MainScreenComponent,
    AdminScreenComponent,
    ManagementComponent,
    LogsScreenComponent
  ],
  imports: [
    ManagementRoutingModule,
    CommonModule,
    MaterialModule,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class ManagementModule { }
