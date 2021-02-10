import { Component, OnInit, ViewChild, Input, AfterViewInit } from '@angular/core';
import { WebInterface } from 'src/Application/Types/types.api';
import { UiLazySingleSelect, UiLazyLoadingRequest } from 'projects/clique-hrui/src/lib/ui-models';
import { UiLazySingleSelectComponent } from 'projects/clique-hrui/src/lib/ui-lazy-single-select/ui-lazy-single-select.component';
import { ApplicationComponentService } from 'src/Application/Services/application-component.service';

@Component({
  selector: 'app-application-emp-single-dd',
  templateUrl: './application-emp-single-dd.component.html',
  styleUrls: ['./application-emp-single-dd.component.css']
})
export class ApplicationEmpSingleDDComponent implements OnInit, AfterViewInit {

  public config: UiLazySingleSelect = {
    DisplayProperty: 'Text',
    ValueProperty: 'Value',
    pageNo: 10,
    isEditable: true
  };
  @ViewChild(UiLazySingleSelectComponent, { static: true, read: UiLazySingleSelectComponent })
  private dropdown: UiLazySingleSelectComponent;

  constructor(
    private applicationComponentService: ApplicationComponentService) {

  }
  ngAfterViewInit(): void {
   
  }

  ngOnInit() {
  }
  public SetSelectedValue(data: { Text: string, Value: number }) {
    this.dropdown.setSelectValue(data);
  }
  public GetSelectedValue() {
    return this.dropdown.selectedValue;
  }
  public OnLoadRequest(request: UiLazyLoadingRequest) {
    let requestData: WebInterface.SinglePaginationModel = {
      StartRow: request.StartIndex,
      EndRow: request.EndIndex,
      SearchText: request.SerchText
    };
    this.applicationComponentService.GetEmployeeForSingleDD(requestData).subscribe(
      (data) => {
        this.dropdown.GenerateDropDown(data);
      },
      (error) => {
        this.dropdown.HideLoading();
      }
    )
  }

}
