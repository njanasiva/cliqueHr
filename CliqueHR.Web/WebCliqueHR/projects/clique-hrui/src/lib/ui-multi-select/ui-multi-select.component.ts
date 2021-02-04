import { Component, OnInit, Input, Output, ContentChild, TemplateRef, ChangeDetectionStrategy, EventEmitter } from '@angular/core';
import { UiBaseComponent } from '../Common/ui-base-component';
import { FormControl } from '@angular/forms';
import { UiMultiselectData, UiMultiSelectOptions, UiMultiSelectOutPut } from '../ui-models';
import { isNullOrUndefined } from 'util';

const noop = () => {};
@Component({
  selector: 'lib-ui-multi-select',
  templateUrl: './ui-multi-select.component.html',
  styleUrls: ['./ui-multi-select.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UiMultiSelectComponent extends UiBaseComponent implements OnInit {

  private masterSelected:boolean;
  private singleSelection: boolean;
  public _dropdownContainter: Array<UiMultiselectData> = [];
  public multiDdOpen: boolean = false;
  public displayText: string;
  public selectedValue: string;
  public filter = new UiMultiselectData();
  constructor() {
    super();
  }

  ngOnInit() {
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
  public set data(_data: Array<any>){
    this._dropdownContainter = _data;
  }

  @Input()
  PlaceHolderText:string; 

  // @Input('FormInput')
  // public formControl: FormControl;

  @Input()
  public set isSingleSelection(_isSingleSelection: boolean){
    this.singleSelection = _isSingleSelection;
  }

  @Input()
  public set selectedValues(data: any){
    this._dropdownContainter.forEach(item => {
      item.isCheckBoxSelected = false;
    });
    if (data != '') {
      let dropdownValues = data.split(',');
      dropdownValues.forEach(item => {
        this._dropdownContainter.forEach(items => {
          let id = parseInt(item);
          if (items.Value === id) {
            items.isCheckBoxSelected = true;
          }
        });
      });
    }
    this.PlaceHolderText = this.updatePlaceHolder(this._dropdownContainter);
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

  checkUncheckAll(){
    this._dropdownContainter.forEach(item => {
      item.isCheckBoxSelected = this.masterSelected;
    });
    this.PlaceHolderText = this.updatePlaceHolder(this._dropdownContainter);
    this.onSelectAll.emit(this.emittedValue(this._dropdownContainter));
  }

  isSelected(clickedItem: UiMultiselectData) {
    let found = false;
    this._dropdownContainter.forEach(item => {
      if (clickedItem.Value === item.Value) {
        found = true;
      }
    });
    return found;
  }

  modelChanged(item: UiMultiselectData){
    console.log(item);
    let selectedData = [];
    let selectedDropDownValues = new Array<UiMultiselectData>();
    this._dropdownContainter.forEach(item=> {
      if(item.isCheckBoxSelected){
        selectedData.push(item.Value);
        selectedDropDownValues.push(item);
      }
    });
    this.PlaceHolderText = this.updatePlaceHolder(selectedDropDownValues);
    let SelectedUiMultiSelectOutPut = new UiMultiSelectOutPut();
    SelectedUiMultiSelectOutPut.selectedValues = selectedData.join(',');
    SelectedUiMultiSelectOutPut.placeHolderText = this.PlaceHolderText;
    SelectedUiMultiSelectOutPut.selectedItem = item;
    this.onSelect.emit(this.emittedValue(SelectedUiMultiSelectOutPut));
  }

  private updatePlaceHolder(data: Array<UiMultiselectData>){
    let arrayData = [];
    let selectedCentreType = data.filter(item => item.isCheckBoxSelected == true);
      if (selectedCentreType.length > 0) {
        if (selectedCentreType.length <= 3) {
          selectedCentreType.forEach(item => {
            arrayData.push(item.Text);
          });
          return arrayData.join(',');
        }
        else if (selectedCentreType.length > 3) {
          return "Selected Item(" + selectedCentreType.length + ")"; 
        }
        else{
          return "None Selected";
        }
      }
      else{
        return "None Selected";
      }
  }


  public toggleDdl(event) {
    event.stopPropagation();
    if(this.isEditable){
      this.multiDdOpen = !this.multiDdOpen;
    }
  }
}