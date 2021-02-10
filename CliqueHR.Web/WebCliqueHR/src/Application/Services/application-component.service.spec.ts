import { TestBed } from '@angular/core/testing';

import { ApplicationComponentService } from './application-component.service';

describe('ApplicationComponentService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ApplicationComponentService = TestBed.get(ApplicationComponentService);
    expect(service).toBeTruthy();
  });
});
