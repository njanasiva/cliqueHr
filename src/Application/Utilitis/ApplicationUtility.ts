import { HttpErrorResponse } from '@angular/common/http';
import { WebAppModels } from '../Models/models.api';
import { isNullOrUndefined } from 'util';

export class ValidationBuilder {
    public static build(error: HttpErrorResponse): string[] {
        if (error.status == 400) {
            let resp = error.error as WebAppModels.ValidationResponse;
            if (resp != null) {
                return resp.Messages.map(m => m.Message);
            }
        }
        return null;
    }
}

export function GetDateInString(date: Date) {
    if (!isNullOrUndefined(date)) {
      return (date.getDate() < 10 ? '0' + date.getDate() : date.getDate())
        + '-' + ((date.getMonth()+1) < 10 ? '0' + (date.getMonth()+1) : date.getMonth()+1)
        + '-' + date.getFullYear();
    }
    return null;
  }

  export function GetDateFromString(date: string) {
    if (!isNullOrUndefined(date)) {
      let dateArr = date.split('-');
      if (dateArr.length == 3) {
        let dateObj = new Date();
        dateObj.setDate(parseInt(dateArr[0]));
        dateObj.setMonth(parseInt(dateArr[1])-1);
        dateObj.setFullYear(parseInt(dateArr[2]));
        dateObj.setHours(0);
        dateObj.setMinutes(0);
        dateObj.setMilliseconds(0);
        return dateObj;
      }
    }
    return null;
  }

  export function GetUTCDateFromString(date: string) {
    if (!isNullOrUndefined(date)) {
      let dateArr = date.split('-');
      if (dateArr.length == 3) {
        let dateObj = new Date(Date.UTC(parseInt(dateArr[2]),parseInt(dateArr[1])-1, parseInt(dateArr[0]),0,0,0,0));
        return dateObj;
      }
    }
    return null;
  }