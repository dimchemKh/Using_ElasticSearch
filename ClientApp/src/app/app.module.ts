import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/modules/shared.module';
import { MainScreenModule } from './managment/main-screen/main-screen.module';
import { MainScreenComponent } from './managment/main-screen/main-screen.component';
import { MaterialModule } from 'src/app/shared/modules/material.module';
import { ApiRoutes } from 'src/environments/api-routes';
import { MainScreenService } from 'src/app/core/services/main-screen.service';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select';
import { AuthentificationModule } from './authentification/authentification.module';

@NgModule({
  declarations: [
    AppComponent,
    MainScreenComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    MainScreenModule,
    MaterialModule,
    NgSelectModule,
    AuthentificationModule
  ],
  providers: [ApiRoutes, MainScreenService],
  bootstrap: [AppComponent]
})
export class AppModule { }
