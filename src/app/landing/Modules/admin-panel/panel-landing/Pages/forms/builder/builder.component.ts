import { FormioRefreshValue } from '@formio/angular';
import { Component, AfterViewInit, ViewChild, ElementRef, EventEmitter, OnInit, Input, Output, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidationMessage } from '../../company/validation-message';
import { FormsService } from '../../../../admin-panel-services/forms.service';
import { Observable, Subscription } from 'rxjs';
import { WebTokens } from 'src/Application/Types/Tokens';
import { WebInterface } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { isNullOrUndefined } from 'util';
import { SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { UiSweetAlertService } from 'projects/clique-hrui/src/lib/ui-sweet-alert/ui-sweet-alert.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-builder',
  templateUrl: './builder.component.html',
  styleUrls: ['./builder.component.css']
})
export class BuilderComponent extends WebComponents.ApplicationComponent implements OnInit{

  @Output() hideForm: EventEmitter<any> = new EventEmitter();
  modalRef: BsModalRef;
  editForm: Subscription;
  isEdit: boolean = false;
  public form: Object;
  formData: Object;
  applicationForm: FormGroup;
  public validationMassages = ValidationMessage.data;
  public refreshForm: EventEmitter<FormioRefreshValue> = new EventEmitter();
  Heading: String = 'Add Form';
  constructor(private fb: FormBuilder,
    private formService: FormsService,
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private uiSweetAlertService: UiSweetAlertService,
    private modalService: BsModalService) {
      super(Components.BuilderComponent, applicationService, changeDetection, viewContainerRef);
      this.form = { components: [] };
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateForm();
    this.formService.getDetail().subscribe((data: any) => {
      if(data.content && data.content.isEdit) {
        this.Heading = 'Edit Form';
        this.isEdit = true;
        var res = data.content.data;
        if (res) {
          this.applicationForm.patchValue({
            Data: res.Data,
            Form: res.Form,
            Descripction: res.Descripction,
            FormId: res.FormId
          });
          this.form = JSON.parse(res.Data);
          this.refreshForm.emit({
            property: 'form',
            value: this.form
          });
        }
      }
    });
  }

  /**form initialization */
  CreateForm() {
    this.applicationForm = this.fb.group({
      Form: ['', [Validators.required]],
      Descripction: ['', []],
      Data: [''],
      FormId:['']
    });
  }

  /**triggering event on change in form design when drag and dropped */
  onChange(event) {
    this.formData = event.form;
    this.refreshForm.emit({
      property: 'form',
      value: event.form
    });
  }

  /**save form */
  SaveData() {
    (Object as any).values(this.applicationForm.controls).forEach(control => {
      control.markAsTouched();
    });
    try
    {
      if (this.applicationForm.valid) {
        this.ShowLoader();
        let formType: Observable<any>;
        let paginationModel: any = {
          UserId: 1,
          StartRow: 1,
          EndRow: 10,
          SearchText: 'A',
          ActionId: this.isEdit != true ? 1 : 2,
          FormId: this.applicationForm.value.FormId ?  this.applicationForm.value.FormId : 0,
          // Data: JSON.stringify(this.formData),
          Data: this.formData ? JSON.stringify(this.formData) : this.applicationForm.value.Data,
          Form: this.applicationForm.value.Form,
          Descripction: this.applicationForm.value.Descripction
        }
        formType = this.formService.FormDetails(paginationModel);
        formType.subscribe(
          (data: any) => {
            this.HideLoader();
            this.uiSweetAlertService.ShowAlert('Data Saved Successfully');
            var send = {
              close: true,
              refresh: true
            }
            this.formService.sendDetail(send);
          },
          (error) => {
            if (error.message != "") {
              this.uiSweetAlertService.ShowAlert(error.message);
            }
            this.HideLoader();
            this.clear();
          })
      } else {
        this.HideLoader();
        this.clear();
      }
    }
    catch(error){
      this.HideLoader();
      console.log(error);
    }
  }

  /**hide form dialog*/
  clear() {
    this.form = {};
    this.isEdit = false;
    var send = {
      close: true
    }
    this.formService.sendDetail(send);
  }

  /**unsubscribe subscriptions */
  ngOnDestroy() {
    if(this.editForm) {
      this.editForm.unsubscribe();
    }
  }
}
