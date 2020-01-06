import { Routes, RouterModule } from '@angular/router';
import { MainScreenComponent } from './main-screen/main-screen.component';
import { AdminScreenComponent } from './admin-screen/admin-screen.component';
import { ManagementComponent } from './management.component';
import { RoleGuard } from '../shared/guards/role.guard';
import { UserRoles } from '../shared/enums/user-roles';

const routes: Routes = [
    {
        path: '', component: ManagementComponent,
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
                data: { roles: [UserRoles.Admin] }
            }
        ]
    }
];

export const ManagementRoutingModule = RouterModule.forChild(routes);