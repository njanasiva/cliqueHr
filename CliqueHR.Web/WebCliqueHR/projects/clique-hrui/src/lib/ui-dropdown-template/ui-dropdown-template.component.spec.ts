import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UiDropdownTemplateComponent } from './ui-dropdown-template.component';

describe('UiDropdownTemplateComponent', () => {
  let component: UiDropdownTemplateComponent;
  let fixture: ComponentFixture<UiDropdownTemplateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UiDropdownTemplateComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UiDropdownTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
