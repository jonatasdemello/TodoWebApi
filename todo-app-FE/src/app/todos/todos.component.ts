import { Component, OnInit  } from '@angular/core';
import { Todo } from '../todo';
import { TodoDataService } from '../todo-data.service';
import { ActivatedRoute } from '@angular/router';

import { map } from 'rxjs/operators';

@Component({
  selector: 'app-todos',
  templateUrl: './todos.component.html',
  styleUrls: ['./todos.component.css'],
  providers: [TodoDataService]
})
export class TodosComponent implements OnInit {
  // Define a public property and set its initial value to an empty array.
  todos: Todo[] = [];

  // use Angular dependency injection to get a handle of the activated route
  constructor(
    private todoDataService: TodoDataService,
    private route: ActivatedRoute ) {
  }

  // then use the ngOnInit() method to subscribe to this.todoDataService.getAllTodos(),
  // and when a value comes in, assign it to this.todos, overwriting its initial value.
  public ngOnInit() {
    // this.todoDataService
    //   .getAllTodos()
    //   .subscribe((todos) => { this.todos = todos; });

      // this.route.data
      // .map((data) => data['todos'])
      // .subscribe( (todos) => { this.todos = todos; } );

    // Now that Angular Router fetches the todos using TodosResolver,
    // we want to fetch the todos in TodosComponent from the route data instead of the API.
    this.route.data
     .pipe(map( (data) => data['todos']) )
     .subscribe((todos) => { this.todos = todos; });
  }


  // Add new method to handle event emitted by TodoListHeaderComponent
  onAddTodo(todo: Todo) {
    this.todoDataService
    .addTodo(todo)
    .subscribe((newTodo) => { this.todos = this.todos.concat(newTodo); });
  }

  onToggleTodoComplete(todo: Todo) {
    this.todoDataService
      .toggleTodoComplete(todo)
      .subscribe((updatedTodo) => { todo = updatedTodo; });
  }

  onRemoveTodo(todo: Todo) {
    this.todoDataService
      .deleteTodoById(todo.id)
      .subscribe((_) => { this.todos = this.todos.filter((t) => t.id !== todo.id); });
  }
}
