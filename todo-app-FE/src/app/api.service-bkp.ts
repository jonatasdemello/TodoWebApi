import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

// import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Todo } from './todo';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  constructor(private http: HttpClient) { }

  // API: GET /todos
  public getAllTodos(): Observable<Todo[]> {
    return this.http
      .get<Todo[]>(API_URL + '/todos')
      .pipe(
        map(response => {
          const todos = response as any[];
          return todos.map((todo) => new Todo(todo));
        })
      );
      // .pipe(map((response: any) => {
      //   const todos = response.json();
      //   return todos.map((todo) => new Todo(todo));
      // }))
      // .pipe(catchError(this.handleError));
  }

  // API: POST /todos
  public createTodo(todo: Todo): Observable<Todo> {
    return this.http
      .post<Todo>(API_URL + '/todos', todo);
      // .pipe(map((response: any) => {
      //   return new Todo(response.json());
      // }))
      // .pipe(catchError(this.handleError));
  }

  // API: GET /todos/:id
  public getTodoById(todoId: number): Observable<Todo> {
    return this.http
      .get<Todo>(API_URL + '/todos/' + todoId);
      // .subscribe(response => return new Todo(response));
      // .pipe(map((response: HttpResponseBase) => {
      //   return new Todo(response.json());
      // }))
      // .pipe(catchError(this.handleError));
  }

  // API: PUT /todos/:id
  public updateTodo(todo: Todo): Observable<Todo> {
    return this.http
      .put<Todo>(API_URL + '/todos/' + todo.id, todo);
      // .pipe(map((response: any) => {
      //   return new Todo(response.json());
      // }))
      // .pipe(catchError(this.handleError));
  }

  // DELETE /todos/:id
  public deleteTodoById(todoId: number): Observable<null> {
    return this.http
      .delete<null>(API_URL + '/todos/' + todoId);
      // .pipe(map((response: any) => null))
      // .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse | any) {
    console.error('ApiService::handleError', error);
    return Observable.throw(error);
  }
}
