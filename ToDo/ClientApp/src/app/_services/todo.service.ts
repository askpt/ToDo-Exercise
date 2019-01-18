import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../_model/todo';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  constructor(private http: HttpClient) { }

  getTodos(): Observable<Array<Todo>> {
    return this.http.get<Array<Todo>>('api/todo');
  }
}
