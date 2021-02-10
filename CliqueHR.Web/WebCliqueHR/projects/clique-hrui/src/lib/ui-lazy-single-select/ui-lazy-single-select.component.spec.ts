import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UiLazySingleSelectComponent } from './ui-lazy-single-select.component';

describe('UiLazySingleSelectComponent', () => {
  let component: UiLazySingleSelectComponent;
  let fixture: ComponentFixture<UiLazySingleSelectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UiLazySingleSelectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UiLazySingleSelectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
