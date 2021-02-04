import { Component, OnInit, ViewChild } from '@angular/core';
import { UiDataTableConfig } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';

@Component({
  selector: 'app-groups',
  templateUrl: './groups.component.html',
  styleUrls: ['./groups.component.css']
})
export class GroupsComponent implements OnInit {

  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;

  public GroupsConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'GroupsID', fieldName: '', columnClass: 'width100' },
      { fieldId: 'GroupName', fieldName: 'Group Name', columnClass: 'text-nowrap width100' },
      { fieldId: 'GroupModerators', fieldName: 'Group Moderators', columnClass: 'text-nowrap width100' },
      { fieldId: 'Attributes', fieldName: 'Attributes', columnClass: 'text-nowrap width100' }
    ],
    Pagination: true,
    PaginationPageSize: 3,
    DefaultSort: { direction: 'asc', fieldId: 'GroupsID' },
    UniqueRowCol: 'GroupsID',
    isEditable: true

  };

  constructor() { }

  ngOnInit() {
  }

}
