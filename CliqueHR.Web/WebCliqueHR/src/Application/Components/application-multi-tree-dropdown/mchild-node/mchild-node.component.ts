import { Component, OnInit, Output, EventEmitter, ComponentFactoryResolver, ChangeDetectorRef } from '@angular/core';
import { WebAppService, WebInterface } from 'src/Application/Types/types.api';

@Component({
  selector: 'app-mchild-node',
  templateUrl: './mchild-node.component.html',
  styleUrls: ['./mchild-node.component.css']
})
export class MChildNodeComponent extends WebAppService.BaseTreeNode implements OnInit {
  public parentComponent: WebInterface.ITreeNode;
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
