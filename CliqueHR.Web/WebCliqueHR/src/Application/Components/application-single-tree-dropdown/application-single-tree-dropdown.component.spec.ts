import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationSingleTreeDropdownComponent } from './application-single-tree-dropdown.component';

describe('ApplicationSingleTreeDropdownComponent', () => {
  let component: ApplicationSingleTreeDropdownComponent;
  let fixture: ComponentFixture<ApplicationSingleTreeDropdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApplicationSingleTreeDropdownComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationSingleTreeDropdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
