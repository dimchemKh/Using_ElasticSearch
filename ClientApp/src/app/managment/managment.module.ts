import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { AdminScreenComponent } from './admin-screen/admin-screen.component';
import { MaterialModule } from '../shared/modules/material.module';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { routes } from 'src/app/managment/managment-routing.module'
import { ManagmentComponent } from './managment.component';


@NgModule({
  declarations: [
    MainScreenComponent,
    AdminScreenComponent,
    ManagmentComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    MaterialModule,
    NgSelectModule,
    FormsModule,
    ReactiveFormsModule
  ]
})
export class ManagmentModule { }
