import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_model/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  authenticate(username: string, password: string): Observable<any> {
    const user = new User();
    user.username = username;
    user.password = password;

    return this.http.post('/api/auth/login', user, {responseType: 'text'});
  }
}
