import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AutoNumberingComponent } from './auto-numbering.component';

describe('AutoNumberingComponent', () => {
  let component: AutoNumberingComponent;
  let fixture: ComponentFixture<AutoNumberingComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AutoNumberingComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AutoNumberingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
