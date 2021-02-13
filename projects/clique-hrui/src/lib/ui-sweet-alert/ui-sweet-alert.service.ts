import { Injectable } from '@angular/core';
declare var swal: any;
@Injectable()
export class UiSweetAlertService {

  constructor() { }

  public ShowAlert(message: string) {
    swal({
      // title: "Have Good day!",
      text: message,
      // icon: "success",
      buttons: false,
      timer: 2000,
    });
  }

  public ShowMultipleMessageAlert(messages: Array<string>) {
    if (messages != null && messages.length != 0) {
      let message = messages.join('\n');
      swal({
        // title: "Have Good day!",
        text: message,
        // icon: "success",
        buttons: false,
        timer: 2000,
      });
    }
  }
}
