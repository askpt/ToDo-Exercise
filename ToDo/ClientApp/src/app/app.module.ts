import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { AuthGuardService } from './_guards/auth-guard.service';

export function getToken() {
  return localStorage.getItem('token');
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    TodoListComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: 'login', component: HomeComponent, pathMatch: 'full' },
      { path: 'todo-list', component: TodoListComponent, pathMatch: 'full', canActivate: [AuthGuardService] },
      { path: '**', redirectTo: 'login'},
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: getToken
      }
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
