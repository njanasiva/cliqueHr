import * as CliqueHRType from '../Types/types.api'
import { OnInit, OnDestroy, ChangeDetectorRef, Renderer2, Component, ComponentFactoryResolver, ViewContainerRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { WebAppModels } from '../Models/models.api';
import { Components, RequestTypes } from '../Types/Constants';
import { componentFactoryService } from '../Types/types.api';
import { ApplicationLoaderComponent } from './application-loader/application-loader.component';
import { isNullOrUndefined } from 'util';
declare var $: any;

export namespace WebComponents {
    export abstract class ApplicationComponent implements OnInit, OnDestroy {
        private m_componentName: string;
        private m_applicationService: CliqueHRType.WebInterface.IApplicationService;
        protected SubjectDestroy: Array<Subscription> = new Array<Subscription>();
        private m_changeDetector: ChangeDetectorRef;
        private m_viewContainerRef: ViewContainerRef;
        private m_applicationLoader: ApplicationLoaderComponent;

        public validationMessage: Array<string> = new Array<string>();

        constructor(
            ComponentName: string,
            applicationService: CliqueHRType.WebInterface.IApplicationService,
            changeDetection: ChangeDetectorRef,
            viewContainerRef: ViewContainerRef
        ) {
            this.m_applicationService = applicationService;
            this.m_changeDetector = changeDetection;
            this.m_componentName = ComponentName;
            this.m_viewContainerRef = viewContainerRef;
            this.m_applicationService.RegisterComponent(ComponentName);
            this.SubjectDestroy.push(
                this.m_applicationService.GetComponentSubscriber(ComponentName).subscribe(
                    request => {
                        this.TriggerCommand(request);
                    }
                )
            );
        }
        protected abstract TriggerCommand(request: CliqueHRType.WebInterface.CommandRequest);

        protected SendCommand(ComponentName: string, Type: string, Data: any): boolean {
            return this.m_applicationService.SendCommand(ComponentName, Type, Data);
        }
        protected OpenModelPopup(id: string) {
            $(id).modal('show');
        }
        protected CloseModelPopup(id: string) {
            $(id).modal('hide');
        }
        protected triggerValidation(formGrp: FormGroup) {
            formGrp.markAllAsTouched();
            formGrp.markAsDirty();
        }
        protected ShowLoader() {
            if (isNullOrUndefined(this.m_applicationLoader)) {
                let componentFactory = this.m_applicationService.ServiceFactory(componentFactoryService) as ComponentFactoryResolver;
                let comp = componentFactory.resolveComponentFactory(ApplicationLoaderComponent);
                let appLoader = this.m_viewContainerRef.createComponent<ApplicationLoaderComponent>(comp);
                this.m_applicationLoader = appLoader.instance;
            }
            else {
                this.m_applicationLoader.ShowAppLoader();
            }
        }
        protected HideLoader() {
            if (!isNullOrUndefined(this.m_applicationLoader)) {
                this.m_applicationLoader.CloseLoader();
            }
        }
        public ngOnInit(): void {

        }
        public ngOnDestroy(): void {
            this.m_applicationService.UnRegisterComponent(this.m_componentName);
            if (this.SubjectDestroy != null) {
                for (let sub of this.SubjectDestroy) {
                    sub.unsubscribe();
                }
            }
        }
    }
}