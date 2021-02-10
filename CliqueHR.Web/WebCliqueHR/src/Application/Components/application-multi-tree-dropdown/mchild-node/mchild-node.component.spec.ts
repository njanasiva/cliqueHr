import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MChildNodeComponent } from './mchild-node.component';

describe('MChildNodeComponent', () => {
  let component: MChildNodeComponent;
  let fixture: ComponentFixture<MChildNodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MChildNodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MChildNodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
