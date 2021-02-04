import { Component, OnInit, ViewChild } from '@angular/core';
import { UiDataTableConfig } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';

@Component({
  selector: 'app-dailycontent',
  templateUrl: './dailycontent.component.html',
  styleUrls: ['./dailycontent.component.css']
})
export class DailycontentComponent implements OnInit {

  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;

  public DailyContentConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'ConID', fieldName: '', columnClass: 'width100' },
      { fieldId: 'Content', fieldName: 'Content', columnClass: 'text-nowrap width100' },
      { fieldId: 'Recurring', fieldName: 'Recurring', columnClass: 'text-nowrap width100' },
      { fieldId: 'StartDate', fieldName: 'Start Date', columnClass: 'text-nowrap width100' },
      { fieldId: 'EndDate', fieldName: 'End Date', columnClass: 'text-nowrap width100' },
      { fieldId: 'WrkGrps', fieldName: 'Work Groups', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 3,
    DefaultSort: { direction: 'asc', fieldId: 'ConID' },
    UniqueRowCol: 'ConID',
    isEditable: true

  };
  constructor() { }

  ngOnInit() {
  }

}
