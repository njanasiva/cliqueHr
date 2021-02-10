import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { FunctionalRoleComponent } from './functional-role.component';

describe('FunctionalRoleComponent', () => {
  let component: FunctionalRoleComponent;
  let fixture: ComponentFixture<FunctionalRoleComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ FunctionalRoleComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FunctionalRoleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
