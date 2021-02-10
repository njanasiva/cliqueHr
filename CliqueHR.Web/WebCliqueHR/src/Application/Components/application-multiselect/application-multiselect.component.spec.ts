import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationMultiselectComponent } from './application-multiselect.component';

describe('ApplicationMultiselectComponent', () => {
  let component: ApplicationMultiselectComponent;
  let fixture: ComponentFixture<ApplicationMultiselectComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplicationMultiselectComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationMultiselectComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
