import { TestBed, async, inject } from '@angular/core/testing';
import { TodoDataService } from './todo-data.service';
import { ApiService } from './api.service';
import { ApiMockService } from './api-mock.service';

// tell the injector to provide the ApiMockService whenever the ApiService is requested

describe('TodoDataService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      TodoDataService,
      {
        provide: ApiService,
        useClass: ApiMockService
      }
    ]
  }));

  it('should be created', () => {
    const service: TodoDataService = TestBed.get(TodoDataService);
    expect(service).toBeTruthy();
  });

  it('should ...', inject([TodoDataService], (service: TodoDataService) => {
    expect(service).toBeTruthy();
  }));

});
