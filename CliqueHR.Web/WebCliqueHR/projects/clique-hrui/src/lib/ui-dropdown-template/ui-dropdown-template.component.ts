import { Component, OnInit, Input, ContentChild, TemplateRef } from '@angular/core';
import { FormControl } from '@angular/forms';
import { UiDropdownTemplate } from '../ui-models';
import { isNullOrUndefined } from 'util';
import { UiBaseComponent } from '../Common/ui-base-component';

@Component({
  selector: 'lib-ui-dropdown-template',
  templateUrl: './ui-dropdown-template.component.html',
  styleUrls: ['./ui-dropdown-template.component.css']
})
export class UiDropdownTemplateComponent extends UiBaseComponent implements OnInit {

  private selectedItems: string[];
  @Input('SearchInput')
  public formControl: FormControl = new FormControl('');
  @Input('Configuration')
  public config: UiDropdownTemplate;
  @ContentChild(TemplateRef, { static: true, read: false })
  public dropdownContaint: TemplateRef<any>;
  public multiDdOpen: boolean = false;
  public displayText: string;

  constructor() {
    super();
  }

  ngOnInit() {
    this.displayText = this.DisplayText;
  }

  private get DisplayText() {
    if (isNullOrUndefined(this.selectedItems) || this.selectedItems.length == 0) {
      return this.Placeholder;
    }
    else if (this.selectedItems.length > 3) {
      return "Selected Item(" + this.selectedItems.length + ")";
    }
    else {
      return this.selectedItems.join(',');
    }
  }
  public get isDisplay(): boolean {
    return true;
  }
  public UpdateSelectedItem(selectedItem:string[]) {
    this.selectedItems = selectedItem;
    this.displayText = this.DisplayText;
  }
  public get Placeholder() {
    return this.isConfigAvailable && !isNullOrUndefined(this.config.Placeholder) ?
      this.config.Placeholder : "";
  }
  public get isSearchHidden() {
    return this.isConfigAvailable ? this.config.HideSearch : false;
  }
  public toggleDdl(event) {
    if (!isNullOrUndefined(event)) {
      event.stopPropagation();
    }
    if (this.isEditable) {
      this.multiDdOpen = !this.multiDdOpen;
    }
  }

}
