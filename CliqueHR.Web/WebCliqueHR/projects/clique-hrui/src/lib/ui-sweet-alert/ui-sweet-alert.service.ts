import { Injectable } from '@angular/core';
<<<<<<< HEAD
declare var swal: any;
@Injectable()
=======
import { Component, OnInit, Output, Input, ContentChild, TemplateRef, EventEmitter, ViewChild, ViewContainerRef, ChangeDetectorRef } from '@angular/core';
declare var swal: any;
@Injectable()
@Component({
  styleUrls: ['./ui-sweet-alert.service.css']
})
>>>>>>> change
export class UiSweetAlertService {

  constructor() { }

  public ShowAlert(message: string) {
<<<<<<< HEAD
    swal({
      // title: "Have Good day!",
      text: message,
      // icon: "success",
=======
    
    swal({
      // title: "Have Good day!",
      text: message,
      icon: "success",
>>>>>>> change
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
