import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainScreenComponent } from './managment/main-screen/main-screen.component';
import { LoginComponent } from './authentification/login/login.component';


const routes: Routes = [
  { path: '', component: MainScreenComponent  },
  { path: 'auth', loadChildren: () => import('src/app/authentification/authentification.module').then(x => x.AuthentificationModule) },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
