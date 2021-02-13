import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeperationComponent } from './seperation.component';

describe('SeperationComponent', () => {
  let component: SeperationComponent;
  let fixture: ComponentFixture<SeperationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeperationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
