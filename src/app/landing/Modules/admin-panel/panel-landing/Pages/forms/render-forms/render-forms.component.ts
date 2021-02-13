import { ChangeDetectorRef, Component, ElementRef, EventEmitter, Inject, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { FormioRefreshValue } from '@formio/angular';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { Observable } from 'rxjs';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { Components } from 'src/Application/Types/Constants';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { FormsService } from '../../../../admin-panel-services/forms.service';

@Component({
  selector: 'app-render-forms',
  templateUrl: './render-forms.component.html',
  styleUrls: ['./render-forms.component.css']
})
export class RenderFormsComponent extends WebComponents.ApplicationComponent {
  protected TriggerCommand(request: WebInterface.CommandRequest) {
    throw new Error('Method not implemented.');
  }

  @ViewChild('json', { static: true }) jsonElement?: ElementRef;
  @ViewChild('code', { static: true }) codeElement?: ElementRef;
  public form: Object;
  visible: boolean = false;
  visibleData: boolean = false;
  public submission: any;
  public refreshForm: EventEmitter<FormioRefreshValue> = new EventEmitter();
  constructor(private formService: FormsService,
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private uiSweetAlertService: UiSweetAlertService,) {
    super(Components.RenderFormsComponent, applicationService, changeDetection, viewContainerRef);
    this.form = { components: [] };
    this.ApplicationList();
  }

  onChange(event) {
    this.jsonElement.nativeElement.innerHTML = '';
    this.jsonElement.nativeElement.appendChild(document.createTextNode(JSON.stringify(event.form, null, 4)));
    this.refreshForm.emit({
      property: 'form',
      value: event.form
    });
  }

  /**list api call */
  private ApplicationList() {
     let listModal: any = {
      UserId: 1,
      StartRow: 1,
      EndRow: 10,
      SearchText: '',
      ActionId: 3,
      FormId: 20,
      NoofData: 10
    }
    /**api call to fetch form design */
    this.formService.SaveData(listModal).subscribe((res: any) => {
      var data = res.Data[0];
      var model = {
        UserId: 1,
        StartRow: 1,
        EndRow: 10,
        SearchText: '',
        ActionId: 5,
        FormId: 20,
        FormValue: '',
        FormUserId: data.FormUserId
      }
      /**api call to fetch form values */
      this.formService.SaveData(model).subscribe((res: any) => {
        /**uncomment this when implementing dynamic data */
        var data = res.Data[0];
        this.submission = { data: JSON.parse(data.FormValue)}
        this.visibleData = true;
        /** static value for testing*/ 
        //this.submission = { data: JSON.parse('{"name":"dadds","phoneNumber":"(888) 888-8888","description":"kjljhl","howOftenDoYouConductSurvey":"data1","gender":"male","selectBoxes1":{"sad":true,"das":true,"adsd":false},"select1":"sas","submit":true,"visa":true,"mastercard":true,"checkbox":false,"checkbox1":false,"currency":666666}') };
  
      });
      this.refreshForm.emit({
        property: 'form',
        value: this.form
      });
      this.visible = true;
    });
    let paginationModel: any = {
      UserId: 1,
      StartRow: 1,
      EndRow: 10,
      SearchText: '',
      ActionId: 5,
      FormId: 20,
      NoofData: 10
    }
    /**api call to fetch form design */
    this.formService.FormDetails(paginationModel).subscribe((res: any) => {
      var data = res.Data[0];
      this.form = JSON.parse(data.Data);
      this.refreshForm.emit({
        property: 'form',
        value: this.form
      });
      this.visible = true;
    });
    
  }

  onSubmit(event) {
    this.ShowLoader();
    let formType: Observable<any>;
    var model = {
      UserId: 1,
      StartRow: 1,
      EndRow: 10,
      SearchText: '',
      ActionId: 1,
      FormId: 4,
      FormValue: JSON.stringify(event.data),
      FormUserId: 0
    }
    formType = this.formService.SaveData(model);
    formType.subscribe(
      (data: any) => {
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert('Form Data Saved Successfully');
      },
      (error) => {
        if (error.message != "") {
          this.uiSweetAlertService.ShowAlert(error.message);
        }
        this.HideLoader();
      })
  }
}
