import { TestBed } from '@angular/core/testing';

import { CliqueAuthGuardService } from './clique-auth-guard.service';

describe('CliqueAuthGuardService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CliqueAuthGuardService = TestBed.get(CliqueAuthGuardService);
    expect(service).toBeTruthy();
  });
});
