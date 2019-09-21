import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Todo } from './todo';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  // API: GET /item
  public getAllTodos(): Observable<Todo[]> {
    return this.http
      .get<Todo[]>(API_URL + '/item')
      .pipe(
        tap((c) => console.log(c)),
        map((response: any) => {
          const todos = response as any[];
          return todos.map((todo) => new Todo(todo));
      }));
  }

  // API: POST /item
  public createTodo(todo: Todo): Observable<Todo> {
    return this.http
      .post<Todo>(API_URL + '/item', todo);
  }

  // API: GET /item/:id
  public getTodoById(todoId: number): Observable<Todo> {
    return this.http
      .get<Todo>(API_URL + '/item/' + todoId);
  }

  // API: PUT /item/:id
  public updateTodo(todo: Todo): Observable<Todo> {
    return this.http
      .put<Todo>(API_URL + '/item/' + todo.id, todo);
  }

  // DELETE /item/:id
  public deleteTodoById(todoId: number): Observable<null> {
    return this.http
      .delete<null>(API_URL + '/item/' + todoId);
  }
}
