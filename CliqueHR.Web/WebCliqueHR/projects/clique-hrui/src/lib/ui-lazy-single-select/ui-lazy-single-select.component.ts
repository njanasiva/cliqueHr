import { Component, OnInit, Output, Input, ContentChild, TemplateRef, EventEmitter, ViewChild, ViewContainerRef, ChangeDetectorRef } from '@angular/core';
import { UiBaseComponent } from '../Common/ui-base-component';
import { FormControl } from '@angular/forms';
<<<<<<< HEAD
import { UiLazySingleSelect, UiLazyLoadingData, UiLazyLoadingRequest } from '../ui-models';
import { isNullOrUndefined } from 'util';
import { filter, debounceTime, switchMap } from 'rxjs/operators';
=======
import { UiSingleselect, UiLazySingleSelect, UiLazyLoadingData, UiLazyLoadingRequest } from '../ui-models';
import { isNullOrUndefined } from 'util';
>>>>>>> change


@Component({
  selector: 'lib-ui-lazy-single-select',
  templateUrl: './ui-lazy-single-select.component.html',
  styleUrls: ['./ui-lazy-single-select.component.css']
})
export class UiLazySingleSelectComponent extends UiBaseComponent implements OnInit {

  private displayText: string;
  public formControl: FormControl = new FormControl('');
<<<<<<< HEAD
  @Output("FetchRequest")
  private fetchRequest: EventEmitter<UiLazyLoadingRequest> = new EventEmitter<UiLazyLoadingRequest>();
  @Output("OnChange")
  private onOptionSelect: EventEmitter<any> = new EventEmitter<any>();
  public selectedValue: any;
=======
  @Output("OnSelect")
  private onSelect: EventEmitter<any> = new EventEmitter<any>();
  @Output("FetchRequest")
  private fetchRequest: EventEmitter<UiLazyLoadingRequest> = new EventEmitter<UiLazyLoadingRequest>();
  @Input("Selected")
  private selectedValue: any;
>>>>>>> change
  @Input('Configuration')
  public config: UiLazySingleSelect;
  public multiDdOpen: boolean = false;
  @ViewChild('optioncnt', { static: true, read: ViewContainerRef })
  private optionContainer: ViewContainerRef;
  private ActualOptions: any[] = [];
  private m_startIndex: number = 0;
<<<<<<< HEAD
  private m_isLastRecord: boolean = false;
  private m_isLoading: boolean = false;
=======
  private m_isLastRecord: boolean = true;
  private m_isLoading: boolean = true;
>>>>>>> change

  @ViewChild('options', { read: TemplateRef, static: true })
  optionsContent: TemplateRef<any>;
  constructor(
    private changeDetection: ChangeDetectorRef
  ) {
    super();
<<<<<<< HEAD
    this.formControl.valueChanges.
    pipe(
      switchMap(x => x == '' ? ' ': x),
      debounceTime(500),
    ).
    subscribe((value:any) => {
      this.ResetDropdown();
      this.LoadOptions();
    });
  }

  ngOnInit() {
    this.LoadOptions();
    if (!isNullOrUndefined(this.selectedValue)) {
      this.OnSelect(this.selectedValue);
    }
=======
  }

  ngOnInit() {
>>>>>>> change
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
<<<<<<< HEAD
=======
    this.optionContainer.clear();
>>>>>>> change
    this.changeDetection.detach();
    if (!isNullOrUndefined(options)) {
      let i = 0;
      for (let opt of options) {
<<<<<<< HEAD
=======
        if (!isNullOrUndefined(this.selectedValue)) {
          if (opt[this.ValueProperty] == this.selectedValue) {
            this.OnSelect(opt);
          }
        }
>>>>>>> change
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
<<<<<<< HEAD
      this.LoadOptions();
    }
  }
  private LoadOptions() {
    this.m_isLoading = true;
    let prevStartIndex = this.m_startIndex;
    this.m_startIndex = this.m_startIndex + this.config.pageNo;
    this.fetchRequest.emit({
      StartIndex: (prevStartIndex + 1),
      EndIndex: this.m_startIndex,
      SerchText: this.formControl.value
    });
  }
=======
      this.m_isLoading = true;
      let prevStartIndex = this.m_startIndex;
      this.m_startIndex = this.m_startIndex + this.config.pageNo;
      this.fetchRequest.emit({
        StartIndex: (prevStartIndex + 1),
        EndIndex: this.m_startIndex
      });
    }
  }
>>>>>>> change
  public OnScroll(event: any) {
    this.FetchRecord(event);
  }
  public GenerateDropDown(value: UiLazyLoadingData) {
    if (!isNullOrUndefined(value) && !isNullOrUndefined(value.Data)) {
<<<<<<< HEAD
      this.ActualOptions = this.ActualOptions.concat(value.Data);
=======
      this.ActualOptions.concat(value.Data);
>>>>>>> change
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
<<<<<<< HEAD
  public HideLoading() {
    this.m_isLoading = false;
  }
  public ResetDropdown() {
    this.m_isLastRecord = false;
    this.ActualOptions = [];
    this.m_isLoading = false;
    this.m_startIndex = 0;
    this.optionContainer.clear();
  }
  public setSelectValue(data: any) {
    this.OnSelect(data)
  }
  public OnSelect(data: any) {
    this.displayText = data[this.displayProperty];
    this.selectedValue = data;
    this.multiDdOpen = false;
  }
  public OnSelectEvent(data: any) {
    this.OnSelect(data);
    this.onOptionSelect.emit(data);
=======
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
>>>>>>> change
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
