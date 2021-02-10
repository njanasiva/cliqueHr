import { Component, OnInit, Input, EventEmitter, Output, ComponentFactoryResolver, ViewChild, ViewContainerRef, TemplateRef, ChangeDetectorRef, AfterViewInit } from '@angular/core';
import { WebInterface, WebAppService } from 'src/Application/Types/types.api';
import { ParentNodeComponent } from './parent-node/parent-node.component';
import { UiDropdownTemplateComponent } from 'projects/clique-hrui/src/lib/ui-dropdown-template/ui-dropdown-template.component';
import { isNullOrUndefined } from 'util';
import { ChildNodeComponent } from './child-node/child-node.component';

@Component({
  selector: 'app-single-tree-dropdown',
  templateUrl: './application-single-tree-dropdown.component.html',
  styleUrls: ['./application-single-tree-dropdown.component.css']
})
export class ApplicationSingleTreeDropdownComponent extends WebAppService.BaseTreeDropdown implements OnInit, AfterViewInit {

  protected isSingleSelect: boolean = true;
  @ViewChild('root', { static: true, read: ViewContainerRef })
  protected container: ViewContainerRef;
  @ViewChild('child', { static: true, read: TemplateRef })
  protected childTemplate: TemplateRef<any>;
  @Input("Configuration")
  protected configuration: WebInterface.TreeDropdown;
  @Input("Data")
  protected data: any[] = [];
  @Output("OnSelect")
  public onItemSelect: EventEmitter<any> = new EventEmitter<any>();
  @Input("SelectionCallback")
  public selectCallback: Function;
  @ViewChild(UiDropdownTemplateComponent, { static: true, read: UiDropdownTemplateComponent })
  dropdownTemplate: UiDropdownTemplateComponent;

  constructor(
    protected componentFactoryResolver: ComponentFactoryResolver,
    protected cd: ChangeDetectorRef
  ) {
    super(componentFactoryResolver, cd);
  }
  ngAfterViewInit(): void {
  }

  ngOnInit() {
    if (this.data != null && this.data.length != 0) {
      this.cur_id = 0;
      this.GenarateTree(this.data);
    }
  }
  protected CreateParentNode(context: this) {
    return context.AddNode<ParentNodeComponent>(ParentNodeComponent);
  }
  protected CreateChildNode(context: this) {
    return context.AddNode<ChildNodeComponent>(ChildNodeComponent);
  }
  protected CheckSelectedValue(data: any) {
    if (!isNullOrUndefined(this.selectCallback) && this.selectCallback(data)) {
      this.dropdownTemplate.UpdateSelectedItem([data[this.displayProperty]]);
      return true;
    }
    else {
      return false;
    }
  }
  protected SelectedNode(isSelected: boolean, data: any) {
    if (isSelected) {
      this.dropdownTemplate.UpdateSelectedItem([data[this.displayProperty]]);
      this.onItemSelect.next(data);
    }
    this.dropdownTemplate.toggleDdl(null);
  }

  public UpdateTree(data: any[]) {
    this.data = data;
    if (this.data != null && this.data.length != 0) {
      this.cur_id = 0;
      this.selectedValue = {};
      this.container.clear();
      this.GenarateTree(this.data);
    }
  }

}
