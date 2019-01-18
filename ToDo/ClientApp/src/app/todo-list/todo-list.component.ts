import { Component, OnInit } from '@angular/core';
import { TodoService } from '../_services/todo.service';
import { Todo } from '../_model/todo';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {
  todos: Todo[];

  constructor(private todoService: TodoService) { }

  ngOnInit() {
    this.todos = new Array<Todo>();
    this.todoService.getTodos().subscribe(t => this.todos = t);
  }

}
