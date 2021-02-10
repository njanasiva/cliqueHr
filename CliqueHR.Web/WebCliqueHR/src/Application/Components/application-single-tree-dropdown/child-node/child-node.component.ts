import { Component, OnInit, Type, Output, EventEmitter, ChangeDetectorRef, ComponentFactoryResolver } from '@angular/core';
import { WebInterface, WebAppService } from 'src/Application/Types/types.api';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-child-node',
  templateUrl: './child-node.component.html',
  styleUrls: ['./child-node.component.css']
})
export class ChildNodeComponent extends WebAppService.BaseTreeNode implements OnInit {
  public parentComponent: WebInterface.ITreeNode;
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
