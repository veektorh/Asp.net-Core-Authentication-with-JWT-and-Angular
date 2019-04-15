import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";

import { DatatableComponent } from "../components/datatable/datatable.component";
import { RegisterComponent } from "../components/register/register.component";
import { LoginComponent } from "../components/login/login.component";
import { ProfileComponent } from "../components/profile/profile.component";

import { RouterModule, Routes } from "@angular/router";
import { HTTP_INTERCEPTORS } from "@angular/common/http";
import { AuthInterceptor } from "src/interceptors/authInterceptor";
import { HttpService } from "src/services/http.service";
import { JwtInterceptor } from "src/interceptors/jwtInterceptor";

let appRoutes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent }
]

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    DatatableComponent,
    ProfileComponent,
    LoginComponent
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    RouterModule.forRoot(appRoutes, { enableTracing: false })
  ],
  providers: [
    HttpService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {


}
