import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef, ViewChild } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components, FileType, ValidationType } from 'src/Application/Types/Constants';
import { MasterService } from '../../master.service';
import { UiSweetAlertService } from 'projects/clique-hrui/src/public-api';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { UiDataTableComponent } from 'projects/clique-hrui/src/lib/ui-data-table/ui-data-table.component';
import { FunctionalRole } from '../../master-model';
import { UiDataTableConfig, SortType } from 'projects/clique-hrui/src/lib/ui-models';
import { ValidationMessage } from '../../validation-message';
import { Observer, Observable } from 'rxjs';
import { isNullOrUndefined } from 'util';
import { ValidationBuilder } from 'src/Application/Utilitis/ApplicationUtility';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-functional-role',
  templateUrl: './functional-role.component.html',
  styleUrls: ['./functional-role.component.css']
})
export class FunctionalRoleComponent  extends WebComponents.ApplicationComponent implements OnInit {
  @ViewChild(UiDataTableComponent, { read: UiDataTableComponent, static: true })
  private uiDataTableComponent: UiDataTableComponent;
  private FunctionalRoleObs: Observer<any>;
  private _selectedImage: any;
  private fileValidationConfig: WebInterface.FileValidationConfig = {
    AllowedExtentions: [FileType.PDF]
  };
  private fileValidationMessages = {
    [ValidationType.InvalidFile]: "Invalid File."
  };
  
  public editData:FunctionalRole;
  public fileValidation:string = '';
  public FunctionalRoleConfig: UiDataTableConfig = {
    Columns: [
      { fieldId: 'Id', fieldName: '' },
      { fieldId: 'FRoleName', fieldName: 'Name'},
      { fieldId: 'FRoleCode', fieldName: 'Code'},
      { fieldId: 'FRoleDesc', fieldName: 'Job Description'},
      { fieldId: 'AttachmentFile', fieldName: 'Uploaded JD'}
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'Id', direction: 'asc' },
    UniqueRowCol: 'Id',
    isEditable: true
  };
  public FunctionRoleList: Array<FunctionalRole>;
  public isAddMode: boolean;
  public validationMassages = ValidationMessage.data;
  public FunctionalRoleForm: FormGroup;
  public FileUploadControl:FormControl = new FormControl();
  public DownloadPath:string = environment.api_baseurl+"api/Storage/GetPublic?path=";
  
  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private masterService: MasterService,
    private uiSweetAlertService: UiSweetAlertService,
    private fb: FormBuilder
  ) {
    super(Components.FunctionalRoleComponent, applicationService, changeDetection, viewContainerRef);
    this.FunctionalRoleObs = {
      next: (data: any) => {
        this.HideLoader();
        if (isNullOrUndefined(data)) {
          this.uiDataTableComponent.ConstructRow([], 0);
        }
        else {
          this.uiDataTableComponent.ConstructRow(data.Data, data.Total);
        }
      },
      error: (error) => {
        this.HideLoader();
        console.log(error);
      },
      complete: () => { }
    };
  }

  public ngOnInit() {
    this.CreateFunctionRoleForm();
  }
  ngAfterViewInit(): void {
    super.ngAfterViewInit();
    this.ShowLoader();
    this.GetAllFunctionalRole(1, this.FunctionalRoleConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
      this.FunctionalRoleObs
    );
    this.uiDataTableComponent.fetchObs.subscribe(x => {
      this.GetAllFunctionalRole(x.StartRow, x.EndRow, x.Sort, x.searchText).subscribe(
        this.FunctionalRoleObs
      );
    });
  }

  private CreateFunctionRoleForm() {
    this.FunctionalRoleForm = this.fb.group({
      FRoleName: ['', [Validators.required]],
      FRoleCode: ['', [Validators.required]],
      FRoleDesc: [''],
      IsDoNotUse: [false],
      Id:[0]
    });
  }
  private GetAllFunctionalRole(startRow: number, endRow: number, sort?: SortType, searchText?: string) {
    let paginationModel: WebInterface.PaginationModel = {
      StartRow: startRow,
      EndRow: endRow,
      SearchText: searchText || '',
      Sort: isNullOrUndefined(sort) ? null : { PropertyName: sort.fieldId, Direction: sort.direction }
    }
    return this.masterService.GetAllFunctionalRole(paginationModel);
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
    
  }
  
  public SelectImage(event) {
    this._selectedImage = undefined;
    this.fileValidation = undefined;
    if (event.target.files && event.target.files[0]) {
      this._selectedImage = event.target.files[0];
      this.fileValidation = this.fileValidationMessages[this.ValidateFile(this.fileValidationConfig, this._selectedImage)];
    }
  }
  public OpenFunctionalRolePopup(isAddMode: boolean, data: FunctionalRole) {
    this.isAddMode = isAddMode;
    this.FileUploadControl.reset();
    if (isAddMode) {
      this.CreateFunctionRoleForm();
    }
    else {
      this.editData = data;
      this.FunctionalRoleForm.patchValue(data);
    }
    this.OpenModelPopup("#addFunctionalRole");
  }

  public FunctionalRoleOp() {
    this.ShowLoader();
    if (this.FunctionalRoleForm.invalid) {
      this.triggerValidation(this.FunctionalRoleForm);
      this.HideLoader();
      return;
    }
    let model: FunctionalRole = this.FunctionalRoleForm.value as FunctionalRole;
    let apiObs: Observable<any>;
    if (this.isAddMode) {
      model.Id = 0;
      apiObs = this.masterService.AddFunctionalRole(model, this._selectedImage);
    }
    else {
      apiObs = this.masterService.UpdateFunctionalRole(model, this._selectedImage);
    }
    apiObs.subscribe(
      (data: any) => {
        this.CloseModelPopup("#addFunctionalRole");
        this.HideLoader();
        this.uiSweetAlertService.ShowAlert("Functional Role data saved successfully");
        this.GetAllFunctionalRole(1, this.FunctionalRoleConfig.PaginationPageSize, { fieldId: 'Id', direction: 'asc' }).subscribe(
          this.FunctionalRoleObs
        );
      },
      (error) => {
        this.HideLoader();
        this.validationMessage = ValidationBuilder.build(error);
        this.uiSweetAlertService.ShowMultipleMessageAlert(this.validationMessage);
      });
  }

}
