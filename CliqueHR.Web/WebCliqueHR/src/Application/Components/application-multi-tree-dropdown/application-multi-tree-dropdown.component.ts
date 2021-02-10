import { Component, OnInit, ViewChild, ViewContainerRef, TemplateRef, Input, Output, EventEmitter, ChangeDetectorRef, ComponentFactoryResolver } from '@angular/core';
import { WebAppService, WebInterface } from 'src/Application/Types/types.api';
import { UiDropdownTemplateComponent } from 'projects/clique-hrui/src/lib/ui-dropdown-template/ui-dropdown-template.component';
import { MParentNodeComponent } from './mparent-node/mparent-node.component';
import { MChildNodeComponent } from './mchild-node/mchild-node.component';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-multi-tree-dropdown',
  templateUrl: './application-multi-tree-dropdown.component.html',
  styleUrls: ['./application-multi-tree-dropdown.component.css']
})
export class ApplicationMultiTreeDropdownComponent extends WebAppService.BaseTreeDropdown implements OnInit {

  protected isSingleSelect: boolean = false;
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
  @Output("OnUnSelect")
  public onItemUnSelect: EventEmitter<any> = new EventEmitter<any>();
  @Input("SelectionCallback")
  public selectCallback: Function;
  @ViewChild(UiDropdownTemplateComponent, { static: true, read: UiDropdownTemplateComponent })
  dropdownTemplate: UiDropdownTemplateComponent;
  private selectedItems: Array<string> = new Array<string>();

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
    return context.AddNode<MParentNodeComponent>(MParentNodeComponent);
  }
  protected CreateChildNode(context: this) {
    return context.AddNode<MChildNodeComponent>(MChildNodeComponent);
  }
  protected CheckSelectedValue(data: any) {
    if (!isNullOrUndefined(this.selectCallback) && this.selectCallback(data)) {
      this.selectedItems.push(data[this.displayProperty]);
      this.dropdownTemplate.UpdateSelectedItem(this.selectedItems);
      return true;
    }
    else {
      return false;
    }
  }
  protected SelectedNode(isSelected: boolean, data: any) {
    if (isSelected) {
      this.selectedItems.push(data[this.displayProperty]);
      this.dropdownTemplate.UpdateSelectedItem(this.selectedItems);
      this.onItemSelect.next(data);
    }
    else {
      let i = this.selectedItems.findIndex(x => x == data[this.displayProperty]);
      if (i != -1) {
        this.selectedItems.splice(i, 1);
        this.dropdownTemplate.UpdateSelectedItem(this.selectedItems);
        this.onItemUnSelect.emit(data);
      }
    }
  }

  public UpdateTree(data: any[]) {
    this.data = data;
    if (this.data != null && this.data.length != 0) {
      this.cur_id = 0;
      this.selectedValue = {};
      this.selectedItems = [];
      this.container.clear();
      this.GenarateTree(this.data);
    }
    this.dropdownTemplate.UpdateSelectedItem(this.selectedItems);
  }

  public get SelectedValue() {
    return this.selectedItems;
  }

}
