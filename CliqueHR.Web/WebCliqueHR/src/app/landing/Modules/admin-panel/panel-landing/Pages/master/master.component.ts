import { Component, OnInit, Inject, ChangeDetectorRef, ViewContainerRef } from '@angular/core';
import { WebComponents } from 'src/Application/Components/componenets.api';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { Components } from 'src/Application/Types/Constants';
import { WebAppModels } from 'src/Application/Models/models.api';
import { QualificationComponent } from './Pages/qualification/qualification.component';
import { CurrencyComponent } from './Pages/currency/currency.component';
<<<<<<< HEAD
import { CenterTypeComponent } from './Pages/center-type/center-type.component';
import { EmployeeTypeComponent } from './Pages/employee-type/employee-type.component';
import { GradeComponent } from './Pages/grade/grade.component';
import { BandComponent } from './Pages/band/band.component';
import { FunctionalRoleComponent } from './Pages/functional-role/functional-role.component';
import { DesignationComponent } from './Pages/designation/designation.component';
import { LocationComponent } from './Pages/location/location.component';
import { RegionComponent } from './Pages/region/region.component';
=======
>>>>>>> change

const tabData:WebAppModels.TabData= {
  Hearder: [
    {
<<<<<<< HEAD
      Name: 'Employee Type',
      Component: EmployeeTypeComponent,
      Selected:true
    },
    {
      Name: 'Grade',
      Component: GradeComponent,
      Selected:false
    },
    {
      Name: 'Band',
      Component: BandComponent,
      Selected:false
    },
    {
      Name: 'Centre Type',
      Component: CenterTypeComponent,
      Selected:false
    },
    {
      Name: 'Currency',
      Component: CurrencyComponent,
      Selected:false
    },
    {
      Name: 'Designation',
      Component: DesignationComponent,
      Selected:false
    },
    {
      Name: 'Location',
      Component: LocationComponent,
      Selected:false
    },
    {
      Name: 'Region',
      Component: RegionComponent,
      Selected:false
    },
    {
      Name: 'Functional Role',
      Component: FunctionalRoleComponent,
      Selected:false
=======
      Name: 'Currency',
      Component: CurrencyComponent,
      Selected:true
>>>>>>> change
    },
    {
      Name: 'Qualification',
      Component: QualificationComponent,
      Selected:false
    }
  ]
}

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.css']
})
export class MasterComponent extends WebComponents.ApplicationComponent implements OnInit {

  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef
  ) {
    super(Components.MasterComponent, applicationService, changeDetection, viewContainerRef);
  }

  public ngOnInit() {
  }

  protected TriggerCommand(request: WebInterface.CommandRequest) {
    
  }
  public get tabData(){
    return tabData;
  }
}
