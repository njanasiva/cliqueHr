import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ChildNodeComponent } from './child-node.component';

describe('ChildNodeComponent', () => {
  let component: ChildNodeComponent;
  let fixture: ComponentFixture<ChildNodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ChildNodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ChildNodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
