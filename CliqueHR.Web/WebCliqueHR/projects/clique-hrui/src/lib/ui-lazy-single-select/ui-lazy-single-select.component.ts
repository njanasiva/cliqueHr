import { Component, OnInit, Output, Input, ContentChild, TemplateRef, EventEmitter, ViewChild, ViewContainerRef, ChangeDetectorRef } from '@angular/core';
import { UiBaseComponent } from '../Common/ui-base-component';
import { FormControl } from '@angular/forms';
import { UiSingleselect, UiLazySingleSelect, UiLazyLoadingData, UiLazyLoadingRequest } from '../ui-models';
import { isNullOrUndefined } from 'util';


@Component({
  selector: 'lib-ui-lazy-single-select',
  templateUrl: './ui-lazy-single-select.component.html',
  styleUrls: ['./ui-lazy-single-select.component.css']
})
export class UiLazySingleSelectComponent extends UiBaseComponent implements OnInit {

  private displayText: string;
  public formControl: FormControl = new FormControl('');
  @Output("OnSelect")
  private onSelect: EventEmitter<any> = new EventEmitter<any>();
  @Output("FetchRequest")
  private fetchRequest: EventEmitter<UiLazyLoadingRequest> = new EventEmitter<UiLazyLoadingRequest>();
  @Input("Selected")
  private selectedValue: any;
  @Input('Configuration')
  public config: UiLazySingleSelect;
  public multiDdOpen: boolean = false;
  @ViewChild('optioncnt', { static: true, read: ViewContainerRef })
  private optionContainer: ViewContainerRef;
  private ActualOptions: any[] = [];
  private m_startIndex: number = 0;
  private m_isLastRecord: boolean = true;
  private m_isLoading: boolean = true;

  @ViewChild('options', { read: TemplateRef, static: true })
  optionsContent: TemplateRef<any>;
  constructor(
    private changeDetection: ChangeDetectorRef
  ) {
    super();
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
        if (!isNullOrUndefined(this.selectedValue)) {
          if (opt[this.ValueProperty] == this.selectedValue) {
            this.OnSelect(opt);
          }
        }
        this.optionContainer.createEmbeddedView(this.optionsContent,
          { $implicit: { index: i, data: opt } });
        i++;
      }
      if (i == 0) {
        this.displayText = undefined;
      }
    }
    this.changeDetection.reattach();
  }
  private FetchRecord(event: any) {
    if (!this.m_isLastRecord && !this.m_isLoading && (((event.srcElement.scrollHeight - event.srcElement.offsetHeight) - 26) < event.srcElement.scrollTop))
    {
      this.m_isLoading = true;
      let prevStartIndex = this.m_startIndex;
      this.m_startIndex = this.m_startIndex + this.config.pageNo;
      this.fetchRequest.emit({
        StartIndex: (prevStartIndex + 1),
        EndIndex: this.m_startIndex
      });
    }
  }
  public OnScroll(event: any) {
    this.FetchRecord(event);
  }
  public GenerateDropDown(value: UiLazyLoadingData) {
    if (!isNullOrUndefined(value) && !isNullOrUndefined(value.Data)) {
      this.ActualOptions.concat(value.Data);
      if (this.ActualOptions.length == value.Total) {
        this.m_isLastRecord = true;
      }
      this.ConstructOptions(value.Data);
      if (this.DisplayText == '') {
        this.SetDefaultOption();
      }
      this.m_isLoading = false;
    }
  }
  public ResetDropdown() {
    this.m_isLastRecord = false;
    this.ActualOptions = [];
    this.m_isLoading = true;
    this.m_startIndex = 0;
  }
  public SelectValue(text: string, value: any) {
    this.displayText = text;
    this.selectedValue = value;
  }
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
