import { Component, OnInit, Inject, ChangeDetectorRef, AfterViewInit, ViewContainerRef, QueryList, ViewChildren, EventEmitter } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { SortType, UiDataTableConfig, UiMultiselectData } from 'projects/clique-hrui/src/lib/ui-models';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { isNullOrUndefined } from 'util';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { forkJoin, Subscription } from 'rxjs';
import { ValidationMessage } from '../../company/validation-message';
import { FormsService } from '../../../../admin-panel-services/forms.service';
import { FormioRefreshValue } from '@formio/angular';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { BuilderComponent } from '../builder/builder.component';
declare var $: any;
@Component({
  selector: 'app-view-forms',
  templateUrl: './view-forms.component.html',
  styleUrls: ['./view-forms.component.css']
})
export class ViewFormsComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  formData: Object;
  testData: any;
  applicationForm: FormGroup;
  isEdit: boolean = false;
  isVisible: boolean = false;
  modalRef: BsModalRef;
  @ViewChildren(UiDataTableComponent, { read: UiDataTableComponent })
  private uiDataTableComponent: QueryList<UiDataTableComponent>;
  public filter = new UiMultiselectData();
  public FormDetailArrayList = new Array<any>();
  public emitData: EventEmitter<any> = new EventEmitter<any>();
  public form: Object;
  public validationMassages = ValidationMessage.data;
  public refreshForm: EventEmitter<FormioRefreshValue> = new EventEmitter();
  /**grid column configuration */
  public formsConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'FormId', fieldName: '', columnClass: 'width10' },
      { fieldId: 'Form', fieldName: 'Form Name', columnClass: 'text-nowrap' },
      { fieldId: 'Descripction', fieldName: 'Form Description', columnClass: 'text-nowrap' },
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'FormId', direction: 'asc' },
    UniqueRowCol: 'FormId',
    isEditable: true
  };
  
  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private formService: FormsService,
    private modalService: BsModalService
  ) {
    super(Components.ViewFormsComponent, applicationService, changeDetection, viewContainerRef);
    this.form = { components: [] };
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.filter.Text = '';
    this.formService.getDetail().subscribe((data: any) => {
      if(data.content && data.content.close) {
        // close add/edit modal
        this.modalRef.hide();
      }
      if(data.content  && data.content.close && data.content.refresh) {
        /**refresh grid values after save or edit*/
        this.ngAfterViewInit();
      }
    });
  }

  ngAfterViewInit(): void {
    this.SubjectDestroy.push(
      this.uiDataTableComponent.toArray()[0].fetchObs.subscribe(x => {
        this.ApplicationList(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
          (data: any) => {
            this.ApplicationListResponse(data);
            this.HideLoader();
          },
          (error) => {
            this.HideLoader();
            console.log(error);
          });
      })
    );

    forkJoin([
      this.ApplicationList(1, this.formsConfig.PaginationPageSize, { fieldId: 'FormId', direction: 'asc' }),
    ]).subscribe(
      (data: any) => {
        this.ApplicationListResponse(data);
        this.HideLoader();
      },
      (error) => {
        this.HideLoader();
        console.log(error);
      });
  }

  /**unsubscribe subscriptions */
  ngOnDestroy() {
   
  }

  /**send list response to table for display */
  private ApplicationListResponse(data: any) {
    if (isNullOrUndefined(data)) {
      console.log('no data.....');
      this.FormDetailArrayList = [];
      this.uiDataTableComponent.toArray()[0].ConstructRow([], 0);
    }
    else {
      this.FormDetailArrayList = data.length ? data[0].Data : data.Data|| [];
      this.uiDataTableComponent.toArray()[0].ConstructRow( data.length ? data[0].Data : data.Data,  data.length ? data[0].Total : data.Total);
    }
  }

  /**list api call */
  private ApplicationList(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: any = {
      UserId: 1,
      StartRow: startRow,
      EndRow: endRow,
     // Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction },
      SearchText: searchText || '',
      ActionId: 3,
      FormId: 0,
      NoofData: 10
    }
    this.ShowLoader();
    return this.formService.FormDetails(paginationModel);
  }

  /**open add/edit popup */
  public OnOpenPopup(index: any, popupType: string) {
    this.modalRef = this.modalService.show(BuilderComponent, Object.assign({}, { class: 'builder-template modal-dialog-scrollable' }));
    $('modal-container.modal:has(div.builder-template)').addClass('modal-from top animate__animated animate__slideInDown');
    this.OpenModelPopup("#formEditPopup");
    if (popupType == 'Edit') {
      var send = {
        isEdit: true,
        data: this.FormDetailArrayList[index]
      }
      this.formService.sendDetail(send);
    }
  }

  getform(value) {
    console.log('form rendered value.....', value);
  }
}
