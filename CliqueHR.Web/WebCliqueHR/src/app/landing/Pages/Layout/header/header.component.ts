import { Component, OnInit, Inject, ComponentFactoryResolver, ViewChild, TemplateRef, ViewContainerRef, QueryList, ViewChildren, ElementRef, asNativeElements, AfterViewInit, ChangeDetectorRef } from '@angular/core';
import { Components, Dashboard, RequestTypes, ApplicationVeriable } from 'src/Application/Types/Constants';
import { WebComponents } from 'src/Application/Components/ApplicationComponent';
import { WebInterface, WebTokens } from 'src/Application/Types/types.api';
import { AppCodeDirective } from 'src/Application/Directives/app-code.directive';
import { fromEvent } from 'rxjs';
import { CommonService } from 'src/app/landing/Modules/common.service';
import { Router } from '@angular/router';

@Component({
  selector: 'header',
  templateUrl: './header.component.html',
  host: { 'class': 'header-section fixed-top' }
})
export class HeaderComponent extends WebComponents.ApplicationComponent implements OnInit, AfterViewInit {

  @ViewChildren(AppCodeDirective, { read: AppCodeDirective })
  private myAppList: QueryList<AppCodeDirective>;
  @ViewChild('myappcontainer', { read: ViewContainerRef, static: false })
  private MyAppContainer: ViewContainerRef;
  @ViewChild('fullScreen', { static: true, read: ElementRef })
  private fullScreen: ElementRef;


  constructor(
    @Inject(WebTokens.APPLICATION_SERVICE)
    protected applicationService: WebInterface.IApplicationService,
    protected changeDetection: ChangeDetectorRef,
    protected viewContainerRef: ViewContainerRef,
    private commonService:CommonService,
    public router:Router
  ) {
    super(Components.HeaderComponent, applicationService, changeDetection, viewContainerRef);
  }

  private CreateMyApps() {
    for (let app of this.MyApps) {
      let elementApp = this.myAppList.find(x => x.AppCode == app.Code);
      let template = elementApp.templateRef;
      this.MyAppContainer.createEmbeddedView(template, { $implicit: app });
    }
    this.changeDetection.detectChanges();
  }
  private get MyApps() {
    return Dashboard.MyAppData;
  }
  private SetupFullScreen() {
    if (document.fullscreenEnabled) {
      let fullScreenEvent = fromEvent(this.fullScreen.nativeElement, 'click');
      let fullScreenSubject = fullScreenEvent.subscribe(x => {
        if (!document.fullscreenElement) {
          document.documentElement.requestFullscreen();
        } else {
          document.exitFullscreen();
        }
      });
      this.SubjectDestroy.push(
        fullScreenSubject
      );
    }

  }
  public ngOnInit() {
    this.SetupFullScreen();
  }
  public ngAfterViewInit(): void {
    this.CreateMyApps();
  }
  public TriggerCommand(request: WebInterface.CommandRequest) {
  }
  public OnChangeTheme() {
    this.SendCommand(Components.ChangeTheamComponent, RequestTypes.ChangeTheme, 'show');
  }
  public OnLogout() {
    this.commonService.Logout().subscribe(
      (data:any) => {
        localStorage.removeItem(ApplicationVeriable.AuthTokenKey);
        localStorage.removeItem(ApplicationVeriable.AuthTokenKey);
        localStorage.removeItem(ApplicationVeriable.AccessFactorKey);
        this.router.navigate(['/secure/login']);
      },
      (error:any) => {
        localStorage.removeItem(ApplicationVeriable.AuthTokenKey);
        localStorage.removeItem(ApplicationVeriable.AuthTokenKey);
        localStorage.removeItem(ApplicationVeriable.AccessFactorKey);
        this.router.navigate(['/secure/login']);
      }
    )
  }
}
