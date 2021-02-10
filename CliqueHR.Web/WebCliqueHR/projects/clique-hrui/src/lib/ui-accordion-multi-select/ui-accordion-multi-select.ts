import { Component, OnInit, Input, Output, ContentChild, TemplateRef, ChangeDetectionStrategy, EventEmitter } from '@angular/core';
import { UiBaseComponent } from '../Common/ui-base-component';
import { FormControl } from '@angular/forms';
import { UiMultiselectData, UiMultiSelectOptions, UiMultiSelectOutPut } from '../ui-models';
import { isNullOrUndefined } from 'util';

const noop = () => {};
@Component({
  selector: 'lib-ui-accordion-multi-select',
  templateUrl: './ui-accordion-multi-select.html',
  styleUrls: ['./ui-accordion-multi-select.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UiAccordionMultiSelectComponent extends UiBaseComponent implements OnInit {

  @Input('SearchInput')
  public formControl: FormControl = new FormControl('');
  public _dropdownContainter: Array<UiMultiSelectOptions> = [];
  public multiDdOpen: boolean = false;
  public displayText: string;
  public selectedValue: string;
  public filter = new UiMultiselectData();
  areaExpand: any[];
  selectedData: string;
  constructor() {
    super();
    this.areaExpand = ['', '', '', '', '', '', '', ''];
  }

  ngOnInit() {
    this.filter.Text = '';
  }

  public get isDisplay(): boolean {
    return true;
  }

  @Input()
  PlaceHolderText:string; 
 
  @Output("onSelect")
  onSelect: EventEmitter<UiMultiselectData> = new EventEmitter<UiMultiselectData>();

  @Input()
  public set data(_data: Array<UiMultiSelectOptions>){
    this._dropdownContainter = _data;
  }

  @Input()
  public set selectedValues(data: Array<UiMultiSelectOptions>){
    this._dropdownContainter = data;
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

  trackByIndex(index: number, obj: any): any {
    return index;
}

  // modelChanged(item: UiMultiselectData, text: string){
  //   console.log(item);
  //   this._dropdownContainter.forEach(items=>{
  //       items.Data.forEach(a=> {
  //           if(a.Value == item.Value && a.Text == text){
  //               a.isCheckBoxSelected = item.isCheckBoxSelected;
  //           }
  //       })
  //   });
  //   this.onSelect.emit(this.emittedValue(this._dropdownContainter));
  // }
  modelChanged(item: any, text: string){
    const selectedData = [];
    // console.log(this._dropdownContainter);
    const selectedValues = new Array<any>();
    this._dropdownContainter.forEach(item=> {
      item.Data.forEach(item1 => {
        if (item1.isCheckBoxSelected) {
          selectedData.push(item1);
          // console.log(selectedData);
        }
      });
    });
    this.selectedData = this.updatePlaceHolder(selectedData);
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
  getValue(event, oIndex) {
    // console.log(oIndex);
    this.areaExpand[oIndex] = document.getElementById(oIndex).getAttribute('aria-expanded');
    // console.log(this.areaExpand);
  }
}