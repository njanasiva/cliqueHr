import { Component, OnInit, Output, Input, ContentChild, TemplateRef, EventEmitter, ViewChild, ViewContainerRef, ChangeDetectorRef } from '@angular/core';
import { UiBaseComponent } from '../Common/ui-base-component';
import { FormControl } from '@angular/forms';
import { UiSingleselect } from '../ui-models';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'lib-ui-single-select',
  templateUrl: './ui-single-select.component.html',
  styleUrls: ['./ui-single-select.component.css']
})
export class UiSingleSelectComponent extends UiBaseComponent implements OnInit {

  private displayText: string;
  public formControl: FormControl = new FormControl('');
  @Output("OnSelect")
  private onSelect: EventEmitter<any> = new EventEmitter<any>();
  @Input("Selected")
  private selectedValue:any;
  @Input('Configuration')
  public config: UiSingleselect;
  @Input('Options')
  public set options(value) {
    this.ActualOptions = value;
    this.ConstructOptions(value);
<<<<<<< HEAD
    if (this.DisplayText == '') {
=======
    if (isNullOrUndefined(this.displayText)) {
>>>>>>> change
      this.SetDefaultOption();
    }
  }
  public multiDdOpen: boolean = false;
  @ViewChild('optioncnt', { static: true, read: ViewContainerRef })
  private optionContainer: ViewContainerRef;
  private ActualOptions: any[];

  @ViewChild('options', { read: TemplateRef, static: true })
  optionsContent: TemplateRef<any>;
  constructor(
    private changeDetection:ChangeDetectorRef
  ) {
    super();
    this.formControl.valueChanges.subscribe(
      (value: string) => {
        if (!isNullOrUndefined(value)) {
          let filterOption = this.ActualOptions.filter((data: any) =>
            data[this.displayProperty].toLowerCase().startsWith(value.toLowerCase())
            || data[this.displayProperty].toLowerCase().includes(value.toLowerCase())
            || data[this.displayProperty].toLowerCase().endsWith(value.toLowerCase()));
          this.ConstructOptions(filterOption);
        }
        else {
          this.ConstructOptions(this.ActualOptions);
        }
      }
    )
  }

  ngOnInit() {
  }
  public toggleDdl(event) {
    event.stopPropagation();
    if (this.isEditable) {
      this.multiDdOpen = !this.multiDdOpen;
    }
  }
  private SetDefaultOption() {
    if (!isNullOrUndefined(this.ActualOptions) && this.ActualOptions.length != 0) {
      this.OnSelect(this.ActualOptions[0]);
    }
  }
  private ConstructOptions(options: Array<any>) {
    this.optionContainer.clear();
    this.changeDetection.detach();
    if (!isNullOrUndefined(options)) {
      let i = 0;
      for (let opt of options) {
        if(!isNullOrUndefined(this.selectedValue))
        {
          if(opt[this.ValueProperty] == this.selectedValue)
          {
            this.OnSelect(opt);
          }
        }
        this.optionContainer.createEmbeddedView(this.optionsContent,
          { $implicit: { index: i, data: opt } });
        i++;
      }
<<<<<<< HEAD
      if(isNullOrUndefined(this.ActualOptions) || 
      this.ActualOptions.length == 0){
=======
      if(i == 0){
>>>>>>> change
        this.displayText = undefined;
      }
    }
    this.changeDetection.reattach();
  }
<<<<<<< HEAD
  public OnScroll(event: any) {
    console.log(event.srcElement.scrollHeight, event.srcElement.offsetHeight, event.srcElement.scrollTop);
    console.log(((event.srcElement.scrollHeight - event.srcElement.offsetHeight) - 26) < event.srcElement.scrollTop )
  }
=======
>>>>>>> change
  public OnSelect(data: any) {
    this.displayText = data[this.displayProperty];
    this.selectedValue = data[this.ValueProperty];
    this.onSelect.emit(data);
  }
  public get DisplayText() {
    if (!isNullOrUndefined(this.displayText)) {
      return this.displayText;
    }
    if (this.isConfigAvailable && !isNullOrUndefined(this.config.Placeholder)) {
      return this.config.Placeholder || '';
    }
    return '';
  }
  public get isDisplay(): boolean {
    return true;
  }
  public get displayProperty() {
    return this.isConfigAvailable ? this.config.DisplayProperty : 'Text';
  }
  public get ValueProperty() {
    return this.isConfigAvailable ? this.config.ValueProperty : 'Id';
  }

}
