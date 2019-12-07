import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/modules/shared.module';
import { MainScreenModule } from './components/main-screen.module';
import { MainScreenComponent } from './components/main-screen/main-screen.component';
import { MaterialModule } from './shared/modules/material.module';
import { ApiRoutes } from 'src/environments/api-routes';
import { ElasticsearchService } from './core/services/elasticsearch.service';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

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
    MaterialModule
  ],
  providers: [ApiRoutes, ElasticsearchService],
  bootstrap: [AppComponent]
})
export class AppModule { }
