import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DailycontentComponent } from './dailycontent.component';

describe('DailycontentComponent', () => {
  let component: DailycontentComponent;
  let fixture: ComponentFixture<DailycontentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DailycontentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DailycontentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
