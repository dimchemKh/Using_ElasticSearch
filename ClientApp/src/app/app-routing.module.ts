import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './shared/guards/auth.guard';


const routes: Routes = [
  { path: '', redirectTo: 'auth', pathMatch: "full" },
  { path: 'managment', canActivate: [AuthGuard], loadChildren: () => import('src/app/managment/managment.module').then(x => x.ManagmentModule)  },
  { path: 'auth', loadChildren: () => import('src/app/authentification/authentification.module').then(x => x.AuthentificationModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
