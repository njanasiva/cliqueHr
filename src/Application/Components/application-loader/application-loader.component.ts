import { Component, OnInit, Inject, ChangeDetectorRef, AfterViewInit, ViewChild, QueryList, ViewChildren, Renderer2, ElementRef } from '@angular/core';
import { WebInterface } from 'src/Application/Types/types.api';

declare var $: any;
@Component({
  selector: 'app-application-loader',
  templateUrl: './application-loader.component.html',
  styleUrls: ['./application-loader.component.css']
})
export class ApplicationLoaderComponent implements OnInit {
  title = 'WebCliqueHR';
  @ViewChild('loader', { static: true, read: ElementRef })
  private element: ElementRef;

  constructor(
    private renderer: Renderer2
  ) {
    
  }
  public ngOnInit(): void {
  }
  
  public ShowAppLoader() {
    this.renderer.setStyle(this.element.nativeElement, 'display', 'block');
  }
  public CloseLoader() {
    this.renderer.removeStyle(this.element.nativeElement, 'display');
    $('.birthday-wish').fadeOut('slow');
    $(".body").removeClass("overflow-hidden");
  }

}
