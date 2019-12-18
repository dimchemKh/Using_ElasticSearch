import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/modules/shared.module';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { ApiRoutes } from 'src/environments/api-routes';
import { MainScreenService } from 'src/app/core/services/main-screen.service';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AuthentificationService } from './core/services/authentification.service';
import { AdminScreenService } from './core/services/admin-screen.service';
import { StorageModule } from '@ngx-pwa/local-storage';
import { AuthHelper } from './shared/helpers/auth.helper';
import { AuthGuard } from './shared/guards/auth.guard';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    MaterialModule,
    StorageModule.forRoot({ IDBNoWrap: true })
  ],
  providers: [ApiRoutes, MainScreenService, AuthentificationService, AdminScreenService, AuthHelper, AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
