import { ECalendarValue } from 'ng2-date-picker';

export interface UiConfig {
    isEditable: boolean;
}
export interface UiTextbox extends UiConfig {
    isNumeric?: boolean;
    isDecimal?: boolean;
    Type?: string;
    PlaceHolder?: string;
}
export interface UiTextArea extends UiConfig {
    PlaceHolder?: string;
    Rows: number;
}
export interface UiDropDown extends UiConfig {
}
export interface UiDatepicker extends UiConfig {
    max?: any;
    min?: any;
    disabled?: boolean;
    plcaeholder?: string;
    ReturnDateFormat?: DateFormat;
}
export interface UiMultiselect extends UiConfig {
    Placeholder?: string;
}
export interface UiDropdownTemplate extends UiConfig {
    Placeholder?: string;
    HideSearch: boolean;
}
export class UiMultiSelectOutPut {
    selectedItem: UiMultiselectData;
    selectedValues: string;
    placeHolderText: string;
}

export class UiMultiselectData {
    Text:string;
    Value:number;
    isDisabled:boolean;
    isCheckBoxSelected:boolean;
}
export interface UiLazySingleSelect extends UiSingleselect {
    pageNo: number;
}
export interface UiLazyLoadingData{
    Total: number;
    Data:Array<any>;
}
export interface UiLazyLoadingRequest{
    StartIndex: number;
    EndIndex: number;
    SerchText?: string;
}
export class UiMultiSelectOptions{
    OptionType:string;
    Data: Array<UiMultiselectData>;
    ControlId:string;
    SelectedValues:string;
}

export interface UiSingleselect extends UiConfig {
    Placeholder?: string;
    DisplayProperty:string;
    ValueProperty?:string;
}
export interface UiDataTableConfig extends UiConfig {
    Pagination?: boolean;
    PaginationPageSize?: number;
    ShowSearch?: boolean;
    DefaultSort?: { fieldId: string, direction: string };
    Columns: columnType[];
    UniqueRowCol: string;
}
export interface columnType {
    fieldId: string;
    fieldName: string;
    sort?: { enabled: boolean, direction?: string };
    columnClass?: string;
}
export interface RequestParam {
    StartRow: number;
    EndRow: number;
    Sort: SortType;
    searchText: string;
}
export interface SortType {
    fieldId: string;
    direction: string;
}
export enum DateFormat {
    STRING = 0,
    DATE_OBJECT
}