import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './shared/guards/auth.guard';
import { NgModule } from '@angular/core';

const routes: Routes = [
  { path: '', redirectTo: 'auth', pathMatch: 'full' },
  { path: 'management', canActivate: [AuthGuard], loadChildren: 'src/app/management/management.module#ManagementModule' },
  { path: 'auth', loadChildren: 'src/app/authentification/authentification.module#AuthentificationModule' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { initialNavigation: 'enabled' })],
  exports: [RouterModule]
})


export class AppRoutingModule {}
