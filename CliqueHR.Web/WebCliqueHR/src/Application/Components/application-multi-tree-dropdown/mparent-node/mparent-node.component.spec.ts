import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MParentNodeComponent } from './mparent-node.component';

describe('MParentNodeComponent', () => {
  let component: MParentNodeComponent;
  let fixture: ComponentFixture<MParentNodeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MParentNodeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MParentNodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
