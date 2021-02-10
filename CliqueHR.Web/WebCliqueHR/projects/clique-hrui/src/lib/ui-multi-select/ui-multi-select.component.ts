<<<<<<< HEAD
import { Component, OnInit, Input, ContentChild, TemplateRef, ChangeDetectionStrategy } from '@angular/core';
import { UiBaseComponent } from '../Common/ui-base-component';
import { FormControl } from '@angular/forms';
import { UiMultiselect } from '../ui-models';
import { isNullOrUndefined } from 'util';
=======
import { Component, OnInit, HostListener, ContentChild, TemplateRef, forwardRef, Input, Output, EventEmitter, ChangeDetectionStrategy, ChangeDetectorRef } from "@angular/core";
import { UiBaseComponent } from '../Common/ui-base-component';
import { FormControl, NG_VALUE_ACCESSOR, ControlValueAccessor } from "@angular/forms";
import { UiMultiselectData, UiMultiSelectOptions, UiMultiSelectOutPut } from '../ui-models';
import { isNullOrUndefined } from 'util';
import { parse } from "querystring";

const noop = () => { };
export const DROPDOWN_CONTROL_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => UiMultiSelectComponent),
  multi: true
};
>>>>>>> change

@Component({
  selector: 'lib-ui-multi-select',
  templateUrl: './ui-multi-select.component.html',
  styleUrls: ['./ui-multi-select.component.css'],
<<<<<<< HEAD
=======
  providers: [DROPDOWN_CONTROL_VALUE_ACCESSOR],
>>>>>>> change
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UiMultiSelectComponent extends UiBaseComponent implements OnInit {

<<<<<<< HEAD
  @Input('SelectedItem')
  private set SelectedItems(value: string[]) {
    this.selectedItems = value;
    this.displayText = this.DisplayText;
  }
  private selectedItems: string[];
  @Input('SearchInput')
  public formControl: FormControl = new FormControl('');
  @Input('Configuration')
  public config: UiMultiselect;
  @ContentChild(TemplateRef, {static: true, read: false})
  public dropdownContaint: TemplateRef<any>;
  public multiDdOpen: boolean = false;
  public displayText: string;

=======
  private masterSelected: boolean;
  private masterSelectedAll: boolean;
  public _dropdownContainter: Array<UiMultiselectData> = [];
  public multiDdOpen: boolean = false;
  public selectedContainer = new Array<UiMultiselectData>();
  public displayText: string;
  public selectedValue: string;
  public formControl: FormControl = new FormControl('');
  public filter = new UiMultiselectData();
>>>>>>> change
  constructor() {
    super();
  }

  ngOnInit() {
<<<<<<< HEAD
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
  public get Placeholder() {
    return this.isConfigAvailable && !isNullOrUndefined(this.config.Placeholder) ?
      this.config.Placeholder : "";
  }
  public toggleDdl(event) {
    if(!isNullOrUndefined(event))
    {
      event.stopPropagation();
    }
    if(this.isEditable){
=======
    this.filter.Text = '';
    this.masterSelected = false;
  }

  public get isDisplay(): boolean {
    return true;
  }

  @Output("onSelect")
  onSelect: EventEmitter<UiMultiselectData> = new EventEmitter<UiMultiselectData>();

  @Output("onDeSelect")
  onDeSelect: EventEmitter<UiMultiselectData> = new EventEmitter<UiMultiselectData>();

  @Output("onSelectAll")
  onSelectAll: EventEmitter<Array<UiMultiselectData>> = new EventEmitter<Array<UiMultiselectData>>();

  @Input()
  public set data(_data: Array<any>) {
    this._dropdownContainter = _data;
    this.PlaceHolderText = "None Selected";
  }

  @Input()
  PlaceHolderText: string;

  @Input()
  public set selectedValues(_data: any) {
    let selectedDataValues = [];
    this.selectedContainer = new Array<UiMultiselectData>();
    if (this._dropdownContainter && this._dropdownContainter.length > 0) {
      this._dropdownContainter.forEach(item => {
        item.isCheckBoxSelected = false;
      });
      if (_data != undefined && _data != null && _data != '') {
        let dropdownValues = _data.split(',');
        dropdownValues.forEach(item => {
          this._dropdownContainter.forEach(items => {
            let id = parseInt(item);
            if (items.Value === id) {
              selectedDataValues.push(items.Value);
              items.isCheckBoxSelected = true;
              this.selectedContainer.push(items);
            }
          });
        });
      }
      this.data = this._dropdownContainter;
      this.PlaceHolderText = this.updatePlaceHolder(selectedDataValues);
    }
  }

  emittedValue(val: any): any {
    const selected = [];
    if (Array.isArray(val)) {
      val.map(item => {
        selected.push(item);
      });
    } else {
      if (val) {
        return val;
      }
    }
    return selected;
  }

  checkUncheckAll(event) {
    this._dropdownContainter.forEach(item => {
      item.isCheckBoxSelected = event.checked;
    });
    this.masterSelected = event.checked;
    //this.data = this._dropdownContainter;
    this.PlaceHolderText = this.updatePlaceHolder(this._dropdownContainter);
    this.onSelectAll.emit(this.emittedValue(this._dropdownContainter));
  }

  isSelected(clickedItem: UiMultiselectData) {
    let found = false;
    this._dropdownContainter.forEach(item => {
      if (clickedItem.isCheckBoxSelected && clickedItem.Value == item.Value) {
        found = true;
      }
    });
    return found;
  }

  modelChanged(item: UiMultiselectData, event: any) {
    console.log(item);
    let selectedData = [];
    let dropDownData = Object.assign(new Array<UiMultiselectData>(), this._dropdownContainter);
    dropDownData.forEach(items => {
      if (items.Value == item.Value && event.checked) {
        let dataExist = this.selectedContainer.filter(a => a.Value == item.Value);
        if (dataExist.length == 0)
          this.selectedContainer.push(items);
      }
      else if (items.Value == item.Value && !event.checked) {
        if (this.selectedContainer.length > 0) {
          let dataExist = this.selectedContainer.filter(a => a.Value == item.Value);
          if (dataExist.length > 0) {
            let index = this.selectedContainer.indexOf(item);
            this.selectedContainer.splice(index, 1);
          }
        }
      }
    });

    this.selectedContainer.forEach(item => {
      selectedData.push(item.Value);
    });

    if (dropDownData.length == selectedData.length) {
      this.masterSelectedAll = true;
    }
    else {
      this.masterSelectedAll = false;
    }

    this.PlaceHolderText = this.updatePlaceHolder(selectedData);
    let SelectedUiMultiSelectOutPut = new UiMultiSelectOutPut();
    SelectedUiMultiSelectOutPut.selectedValues = selectedData.join(',');
    SelectedUiMultiSelectOutPut.placeHolderText = this.PlaceHolderText;
    SelectedUiMultiSelectOutPut.selectedItem = item;
    //this.selectedValues = selectedData.join(',');
    this.onSelect.emit(this.emittedValue(SelectedUiMultiSelectOutPut));
  }

  private updatePlaceHolder(data: any[]) {
    let arrayData = [];
    if (data.length > 0) {
      data.forEach(item => {
        let val = parseInt(item);
        this._dropdownContainter.forEach(items => {
          if (items.Value == val) {
            arrayData.push(items.Text);
          }
        });
      });
      if (arrayData.length == this._dropdownContainter.length) {
        this.masterSelectedAll = true;
      }
      else {
        this.masterSelectedAll = false;
      }
      if (data.length <= 3) {
        return arrayData.join(',');
      }
      else if (data.length > 3) {
        return "Selected Item(" + data.length + ")";
      }
      else {
        return "None Selected";
      }
    }
    else {
      return "None Selected";
    }
  }


  public toggleDdl(event) {
    event.stopPropagation();
    if (this.isEditable) {
>>>>>>> change
      this.multiDdOpen = !this.multiDdOpen;
    }
  }
}
