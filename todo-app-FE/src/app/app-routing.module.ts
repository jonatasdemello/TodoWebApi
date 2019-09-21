import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TodosComponent } from './todos/todos.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { TodosResolver } from './todos.resolver';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'todos',
    pathMatch: 'full'
  },
  {
    path: 'todos',
    component: TodosComponent,
    resolve: {
      todos: TodosResolver
    }
  },
  {
    path: '**',
    component: PageNotFoundComponent
  }
];
// the wildcard route must be the last route in our routing configuration
// when Angular Router matches a request URL to the router configuration,
// it stops processing as soon as it finds the first match.

// Angular Router uses Angular dependency injection to access resolvers,
// so we have register [TodosResolver] by adding it to the providers property

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [ TodosResolver ]
})
export class AppRoutingModule {
}
