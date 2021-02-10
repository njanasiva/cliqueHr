import { Component, OnInit, TemplateRef, ViewChild, ViewContainerRef, Type, ComponentFactoryResolver, ChangeDetectionStrategy, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { WebAppService } from 'src/Application/Types/types.api';

@Component({
  selector: 'app-parent-node',
  templateUrl: './parent-node.component.html',
  styleUrls: ['./parent-node.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ParentNodeComponent  extends WebAppService.BaseTreeNode  implements  OnInit {
  @ViewChild('parent', { static: true, read: ViewContainerRef })
  container: ViewContainerRef;
  @Output('OnNodeSelection')
  public onNodeSelection : EventEmitter<{isSelected: boolean, id:number, data: any}> = new EventEmitter<{isSelected: boolean, id:number, data: any}>();

  constructor(
    protected componentFactoryResolver: ComponentFactoryResolver,
    protected cd: ChangeDetectorRef
  ) { 
    super(componentFactoryResolver, cd);
  }

  ngOnInit() {
  }

}
