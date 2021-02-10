import { Component, OnInit, Output, EventEmitter, ChangeDetectorRef, ComponentFactoryResolver, ViewContainerRef, ViewChild } from '@angular/core';
import { WebAppService, WebInterface } from 'src/Application/Types/types.api';

@Component({
  selector: 'app-mparent-node',
  templateUrl: './mparent-node.component.html',
  styleUrls: ['./mparent-node.component.css']
})
export class MParentNodeComponent extends WebAppService.BaseTreeNode implements OnInit {
  public parentComponent: WebInterface.ITreeNode;
  @ViewChild('parent', { static: true, read: ViewContainerRef })
  container: ViewContainerRef;
  @Output('OnNodeSelection')
  public onNodeSelection : EventEmitter<{isSelected: boolean, id:number, data: any}>;
  
  constructor(
    protected componentFactoryResolver: ComponentFactoryResolver,
    protected cd: ChangeDetectorRef
  ) { 
    super(componentFactoryResolver, cd);
    this.onNodeSelection = new EventEmitter<{isSelected: boolean, id:number, data: any}>();
  }

  ngOnInit() {
  }

}
