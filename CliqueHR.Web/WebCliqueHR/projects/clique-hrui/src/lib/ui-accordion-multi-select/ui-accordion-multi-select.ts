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
  constructor() {
    super();
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

    this._dropdownContainter.forEach(item=> {
         item.Data.forEach(items=> {
            items.isCheckBoxSelected = false;
         });
    });

    this._dropdownContainter.forEach(item => {
        let modifyValue = data.find(a=> a.OptionType == item.OptionType);
        item.Data.forEach(items=> {
            modifyValue.Data.forEach(b=> {
                if(b.Value == items.Value){
                    items.isCheckBoxSelected = true;
                }
                else{
                    items.isCheckBoxSelected = false;
                }
            });
        });
    });
   
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

  modelChanged(item: UiMultiselectData, text: string){
    console.log(item);
    this._dropdownContainter.forEach(items=>{
        items.Data.forEach(a=> {
            if(a.Value == item.Value && a.Text == text){
                a.isCheckBoxSelected = item.isCheckBoxSelected;
            }
        })
    });
    this.onSelect.emit(this.emittedValue(this._dropdownContainter));
  }

  public toggleDdl(event) {
    event.stopPropagation();
    if(this.isEditable){
      this.multiDdOpen = !this.multiDdOpen;
    }
  }

}