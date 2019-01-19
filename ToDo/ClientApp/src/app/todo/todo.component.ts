import { Component, OnInit } from '@angular/core';
import { Todo } from '../_model/todo';
import { TodoService } from '../_services/todo.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.scss']
})
export class TodoComponent implements OnInit {
  todo: Todo;
  id: number;
  constructor(private todoService: TodoService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.todo = new Todo();
    this.route.params.subscribe(params => {
      this.id = +params['id']; // (+) converts string 'id' to a number

      this.todoService.getTodo(this.id).subscribe(t => this.todo = t);
    });
  }
}
