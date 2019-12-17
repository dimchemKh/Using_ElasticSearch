import { LoginComponent } from './login/login.component';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
    {
      path: "", component: LoginComponent,
      children: [
        { path: "", redirectTo: "login", pathMatch: "full" },
        { path: 'login', component: LoginComponent }
      ]
    }
  ];
  
  export const AuthenticationRoutingModule  = RouterModule.forChild(routes)
