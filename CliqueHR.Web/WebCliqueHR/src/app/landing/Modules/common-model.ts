import { FormGroup } from '@angular/forms';
import { CommonService } from './common.service';
import { isNullOrUndefined } from 'util';
import { UiSingleselect } from 'projects/clique-hrui/src/lib/ui-models';

export class City {
    public CityId: number;
    public StateId: number;
    public Code: string;
    public Description: string;
}

export class State {
    public CountryId: number;
    public StateId: number;
    public Code: string;
    public Description: string;
}
export class Country {
    public CountryId: number;
    public Code: string;
    public Description: string;
}

export class LocationDropdown {
    public CountryList = new Array<any>();
    public StateList = new Array<any>();
    public CityList = new Array<any>();
    public CountryValue:number;
    public StateValue:number;
    public CityValue:number;

    public CountryConfig : UiSingleselect = {
      isEditable: true,
      DisplayProperty: 'Description',
      ValueProperty: 'CountryId'
    }
    public StateConfig : UiSingleselect = {
      isEditable: true,
      DisplayProperty: 'Description',
      ValueProperty: 'StateId'
    }

    public CityConfig : UiSingleselect = {
      isEditable: true,
      DisplayProperty: 'Description',
      ValueProperty: 'CityId'
    }

    constructor(formGroup:FormGroup, commonService:CommonService){
        formGroup.get("CountryId").valueChanges.subscribe(
            (value:string) => {
              if(!isNullOrUndefined(value))
              {
                formGroup.get("StateId").reset();
                formGroup.get("CityId").reset();
                commonService.GetAllState(parseInt(value)).subscribe((data:Array<State>) => {
                  this.StateList = data;
                  let state = this.StateList[0] || {};
                  formGroup.get("StateId").patchValue(state.StateId);
                },
                (error) => {
                  console.log(error);
                });
              }
              else{
                this.StateList = [];
                this.CityList = [];
              }
            }
          );
          formGroup.get("StateId").valueChanges.subscribe(
            (value:string) => {
              if(!isNullOrUndefined(value))
              {
                commonService.GetAllCity(parseInt(value)).subscribe((data:Array<City>) => {
                  this.CityList = data;
                  let city = this.CityList[0] || {};
                  formGroup.get("CityId").patchValue(city.CityId);
                },
                (error) => {
                  console.log(error);
                });
              }
              else{
                this.CityList = [];
              }
            }
          );
    }

    public SetEditable(flag:boolean){
      this.CityConfig.isEditable = flag;
      this.StateConfig.isEditable = flag;
      this.CountryConfig.isEditable = flag;
    }
}