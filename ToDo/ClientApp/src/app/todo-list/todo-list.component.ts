import { Component, OnInit } from '@angular/core';
import { TodoService } from '../_services/todo.service';
import { Todo } from '../_model/todo';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit {
  todos: Todo[];
  addTodoForm: FormGroup;

  constructor(private todoService: TodoService) { }

  ngOnInit() {
    this.todos = new Array<Todo>();
    this.addTodoForm = new FormGroup({
      description: new FormControl(null, Validators.required)
    });

    this.todoService.getTodos().subscribe(t => this.todos = t);
  }

  addTodo(): void {
    const description = this.addTodoForm.value.description;

    this.todoService.addTodo(description).subscribe(id => {
      this.todos.push({
        id: id,
        description: description,
        userId: null,
        checked: false,
        lastUpdated: null
      });

      this.addTodoForm.reset();
    });
  }

  deleteTodo(todo: Todo): void {
    this.todoService.deleteTodo(todo.id).subscribe(() => {
      this.todos = this.todos.filter(t => t.id !== todo.id);
    });
  }

  updateTodo(todo: Todo): void {
    this.todoService.updateTodo(todo.id, todo.description, todo.checked).subscribe();
  }
}
