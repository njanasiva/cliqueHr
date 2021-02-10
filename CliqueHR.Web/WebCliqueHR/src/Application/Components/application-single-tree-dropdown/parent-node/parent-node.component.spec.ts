import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ParentNodeComponent } from './parent-node.component';

describe('ParentNodeComponent', () => {
  let component: ParentNodeComponent;
  let fixture: ComponentFixture<ParentNodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ParentNodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ParentNodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
