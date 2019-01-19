import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Todo } from '../_model/todo';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  constructor(private http: HttpClient) { }

  getTodos(): Observable<Array<Todo>> {
    return this.http.get<Array<Todo>>('api/todo');
  }

  addTodo(description: string): Observable<number> {
    const body = {
      description: description
    };

    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.http.post<number>('api/todo', body, httpOptions);
  }

  deleteTodo(id: number): Observable<any> {
    return this.http.delete(`api/todo?id=${id}`);
  }
}
