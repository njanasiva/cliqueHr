import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild, AfterViewInit, ViewChildren, QueryList } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { CompanyService } from '../../company.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { UiDataTableConfig } from 'projects/clique-hrui/src/lib/ui-models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ValidationMessage } from '../../validation-message';
import { Observer } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { AutoNumberingBase, ViewAutoNumbering, EditAutoNumbering, AddAutoNumbering, AutoNumbering } from '../../company-model';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';

@Component({
  selector: 'app-auto-numbering',
  templateUrl: './auto-numbering.component.html',
  styleUrls: ['./auto-numbering.component.css']
})
export class AutoNumberingComponent extends WebComponents.ApplicationComponent implements OnInit {

  public AutoNoFormDept: FormGroup;
  public validationMassages = ValidationMessage.data;
  public autoNoVM:AutoNumberingBase;

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private componyService: CompanyService,
    private uiSweetAlertService: UiSweetAlertService,
    private fb : FormBuilder
  ) {
    super(Components.AutoNumberingComponent, applicationService, changeDetection, viewContainerRef);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
  }

  ngOnInit() {
    this.CreateAutoNoForm();
    this.autoNoVM = new ViewAutoNumbering(this.AutoNoFormDept);
    this.autoNoVM.DeActivateSave();
    this.ShowLoader();
    this.componyService.GetAutoNumbering().subscribe(
      (data:any) => {
        this.HideLoader();
        if(!isNullOrUndefined(data)){
          this.AutoNoFormDept.patchValue(data);
        }
        else {
          this.autoNoVM = new AddAutoNumbering(this.AutoNoFormDept);
        }
      },
      (error) => {
        this.HideLoader();
      }
    );
  }

  private CreateAutoNoForm() {
    this.AutoNoFormDept = this.fb.group({
      Prefix:['',[Validators.required]],
      AppendNumber:['',[Validators.required]]
    });
  }

  public EditClick() {
    if(this.autoNoVM instanceof EditAutoNumbering){
      this.autoNoVM = new ViewAutoNumbering(this.AutoNoFormDept);
    }
    else {
      this.autoNoVM = new EditAutoNumbering(this.AutoNoFormDept);
    }
  }

  public AddUpdateAutoNo() {
    this.ShowLoader();
    if (this.AutoNoFormDept.invalid) {
      this.triggerValidation(this.AutoNoFormDept);
      this.HideLoader();
      return;
    }
    let model:AutoNumbering = this.AutoNoFormDept.value as AutoNumbering;
    this.componyService.AddUpdateAutoNumbering(model).subscribe(
      (data:any) => {
        this.HideLoader();
        this.autoNoVM = new ViewAutoNumbering(this.AutoNoFormDept);
        this.autoNoVM.DeActivateSave();
        this.uiSweetAlertService.ShowAlert("data saved successfully");
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      }
    )
  }
}
