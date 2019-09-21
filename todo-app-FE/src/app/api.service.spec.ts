import { TestBed, inject  } from '@angular/core/testing';

import { ApiService } from './api.service';
import { HttpTestingController, HttpClientTestingModule } from '@angular/common/http/testing';
import { HttpRequest, HttpClient, HttpXhrBackend } from '@angular/common/http';

// new
// import { BaseRequestOptions, Http, XHRBackend } from '@angular/http';
// import { MockBackend } from '@angular/http/testing';

describe('ApiService', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      // original
      // providers: [ ApiService ],
      providers: [
        // {
        //   provide: HttpClientTestingModule,
        //   useFactory: (backend, options) => {
        //     return new Http(backend, options);
        //   },
        //   deps: [MockBackend, BaseRequestOptions]
        // },
        // MockBackend,
        // BaseRequestOptions,
        ApiService
      ],
      imports: [ HttpClientTestingModule ]
      });
  });

  it('should ...', inject([ApiService], (service: ApiService) => {
    expect(service).toBeTruthy();
  }));

});
