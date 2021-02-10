import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationMultiTreeDropdownComponent } from './application-multi-tree-dropdown.component';

describe('ApplicationMultiTreeDropdownComponent', () => {
  let component: ApplicationMultiTreeDropdownComponent;
  let fixture: ComponentFixture<ApplicationMultiTreeDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplicationMultiTreeDropdownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationMultiTreeDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
