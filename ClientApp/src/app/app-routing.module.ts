import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './shared/guards/auth.guard';

const routes: Routes = [
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  { path: 'management', canActivate: [AuthGuard], loadChildren: 'src/app/management/management.module#ManagementModule' },
  { path: 'auth', loadChildren: 'src/app/authentification/authentification.module#AuthentificationModule' }
];

export const AppRoutingModule = RouterModule.forRoot(routes);
