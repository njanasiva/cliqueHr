import { TestBed } from '@angular/core/testing';

import { UiSweetAlertService } from './ui-sweet-alert.service';

describe('UiSweetAlertService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: UiSweetAlertService = TestBed.get(UiSweetAlertService);
    expect(service).toBeTruthy();
  });
});
