import { Routes } from '@angular/router';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { AdminScreenComponent } from './admin-screen/admin-screen.component';
import { ManagmentComponent } from './managment.component';
import { RoleGuard } from '../shared/guards/role.guard';
import { UserRoles } from '../shared/enums/user-roles';

export const routes: Routes = [
    {
        path: '', component: ManagmentComponent,
        children: [
            { path: '', redirectTo: 'main-screen', pathMatch: 'full' },
            {
                path: 'main-screen',
                canActivate: [RoleGuard],
                component: MainScreenComponent,
                data: { roles: [UserRoles.Admin, UserRoles.User] }
            },
            {
                path: 'admin-screen',
                canActivate: [RoleGuard],
                component: AdminScreenComponent,
                data: { role: [UserRoles.Admin] }
            }
        ]
    }
]

export class ManagementRoutingModule { }