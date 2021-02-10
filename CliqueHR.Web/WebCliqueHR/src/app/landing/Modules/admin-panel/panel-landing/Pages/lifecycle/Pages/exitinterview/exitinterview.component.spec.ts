import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExitinterviewComponent } from './exitinterview.component';

describe('ExitinterviewComponent', () => {
  let component: ExitinterviewComponent;
  let fixture: ComponentFixture<ExitinterviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExitinterviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExitinterviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
