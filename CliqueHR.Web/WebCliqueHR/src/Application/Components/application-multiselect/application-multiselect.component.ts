import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { UiMultiselect } from 'projects/clique-hrui/src/lib/ui-models';
import { WebInterface } from 'src/Application/Types/types.api';
import { FormControl } from '@angular/forms';
import { isNullOrUndefined } from 'util';

@Component({
  selector: 'app-application-multiselect',
  templateUrl: './application-multiselect.component.html',
  styleUrls: ['./application-multiselect.component.css']
})
export class ApplicationMultiselectComponent implements OnInit {

  @Input("Configuration")
  public Configuration: WebInterface.Multiselect;
  @Input("Data")
  public ActualData: any[] = [];
  @Output("OnSelect")
  public onItemSelect: EventEmitter<any> = new EventEmitter<any>();
  @Output("OnUnSelect")
  public onItemUnSelect: EventEmitter<any> = new EventEmitter<any>();
  @Output("OnSelectAll")
  public onItemSelectAll: EventEmitter<any> = new EventEmitter<any>();
  @Output("OnUnSelectAll")
  public onItemUnSelectAll: EventEmitter<any> = new EventEmitter<any>();
  public formControl: FormControl = new FormControl('');
  public SelectedItem: string[] = [];
  public Data: any[] = [];

  constructor(
    private cd: ChangeDetectorRef
  ) {
    this.formControl.valueChanges.subscribe(
      (value: string) => {
        if (!isNullOrUndefined(value)) {
          let filterOption = this.ActualData.filter((data: any) =>
            data[this.Configuration.DisplayProperty].toLowerCase().startsWith(value.toLowerCase())
            || data[this.Configuration.DisplayProperty].toLowerCase().includes(value.toLowerCase())
            || data[this.Configuration.DisplayProperty].toLowerCase().endsWith(value.toLowerCase()));
          this.Data = filterOption;
        }
        else {
          this.Data = this.ActualData;
        }
      }
    )
  }

  ngOnInit() {
    if (!isNullOrUndefined(this.ActualData)) {
      this.Refresh(this.ActualData);
    }
  }

  public DisplayText(data: any) {
    return data[this.Configuration.DisplayProperty];
  }

  public OnSelect(event, i: number) {
    this.cd.detach();
    this.Data[i].Selected = event.currentTarget.checked;
    let items = this.SelectedItem.splice(0);
    if (this.Data[i].Selected) {
      this.onItemSelect.emit(this.Data[i]);
      items.push(this.Data[i][this.Configuration.DisplayProperty]);
    }
    else {
      let j = items.findIndex(it => it == this.Data[i][this.Configuration.DisplayProperty]);
      if (j != -1) {
        items.splice(j, 1);
      }
      this.onItemUnSelect.emit(this.Data[i]);
    }
    this.SelectedItem = items;
    this.cd.detectChanges();
    this.cd.reattach();
  }

  public OnSelectUnSelectAll(event) {
    this.cd.detach();
    let items = [];
    if (event.currentTarget.checked) {
      this.ActualData.forEach((obj: any) => {
        obj.Selected = true;
        items.push(obj[this.Configuration.DisplayProperty]);
      });
      this.onItemSelectAll.emit(this.ActualData);
    }
    else {
      this.ActualData.forEach((obj: any) => {
        obj.Selected = false;
      });
      this.onItemUnSelectAll.emit(this.ActualData);
    }
    this.SelectedItem = items;
    this.cd.detectChanges();
    this.cd.reattach();
  }

  public get isSelectedAll() {
    return this.ActualData.findIndex(data => isNullOrUndefined(data.Selected) || data.Selected == false) == -1;
  }

  public isChecked(value: any) {
    return (!isNullOrUndefined(value.Selected)) && value.Selected;
  }

  public Refresh(data:any[]) {
    this.Data = data;
    this.SelectedItem = [];
    this.Data.forEach(
      obj => {
        if (obj.Selected == true) {
          this.SelectedItem.push(obj[this.Configuration.DisplayProperty]);
        }
      }
    );
  }
}
