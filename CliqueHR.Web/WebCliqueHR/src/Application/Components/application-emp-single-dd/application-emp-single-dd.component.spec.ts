import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationEmpSingleDDComponent } from './application-emp-single-dd.component';

describe('ApplicationEmpSingleDDComponent', () => {
  let component: ApplicationEmpSingleDDComponent;
  let fixture: ComponentFixture<ApplicationEmpSingleDDComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplicationEmpSingleDDComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationEmpSingleDDComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
