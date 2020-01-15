import { Routes, RouterModule } from '@angular/router';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { AdminScreenComponent } from './admin-screen/admin-screen.component';
import { ManagementComponent } from './management.component';
import { ManagementGuard } from '../shared/guards/management.guard';
import { UserRole } from '../shared/enums/user-roles';
import { LogsScreenComponent } from './logs-screen/logs-screen.component';
import { NgModule } from '@angular/core';
import { UserPermission } from '../shared/enums/user-permissons';

const routes: Routes = [
    {
        path: '', component: ManagementComponent,
        children: [
            { path: '', redirectTo: 'main-screen', pathMatch: 'full' },
            {
                path: 'main-screen',
                canActivate: [ManagementGuard],
                component: MainScreenComponent,
                data:
                {
                    // roles: [UserRole.SysAdmin, UserRole.Admin, UserRole.User]
                }
            },
            {
                path: 'admin-screen',
                canActivate: [ManagementGuard],
                component: AdminScreenComponent,
                data:
                {
                    // roles: [UserRole.SysAdmin, UserRole.Admin]
                }
            },
            {
                path: 'logs-screen',
                canActivate: [ManagementGuard],
                component: LogsScreenComponent,
                data:
                {
                    // roles: [UserRole.SysAdmin, UserRole.Admin]
                }
            }
        ]
    }
];

export const ManagementRoutingModule = RouterModule.forChild(routes);