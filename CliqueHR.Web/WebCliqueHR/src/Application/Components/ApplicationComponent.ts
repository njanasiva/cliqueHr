import * as CliqueHRType from '../Types/types.api'
<<<<<<< HEAD
import { OnInit, OnDestroy, ChangeDetectorRef, Renderer2, Component, ComponentFactoryResolver, ViewContainerRef, AfterViewInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { WebAppModels } from '../Models/models.api';
import { Components, RequestTypes, ValidationType } from '../Types/Constants';
import { componentFactoryService, WebInterface } from '../Types/types.api';
=======
import { OnInit, OnDestroy, ChangeDetectorRef, Renderer2, Component, ComponentFactoryResolver, ViewContainerRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { WebAppModels } from '../Models/models.api';
import { Components, RequestTypes } from '../Types/Constants';
import { componentFactoryService } from '../Types/types.api';
>>>>>>> change
import { ApplicationLoaderComponent } from './application-loader/application-loader.component';
import { isNullOrUndefined } from 'util';
declare var $: any;

export namespace WebComponents {
<<<<<<< HEAD
    export abstract class ApplicationComponent implements OnInit, AfterViewInit, OnDestroy {
=======
    export abstract class ApplicationComponent implements OnInit, OnDestroy {
>>>>>>> change
        private m_componentName: string;
        private m_applicationService: CliqueHRType.WebInterface.IApplicationService;
        protected SubjectDestroy: Array<Subscription> = new Array<Subscription>();
        private m_changeDetector: ChangeDetectorRef;
        private m_viewContainerRef: ViewContainerRef;
        private m_applicationLoader: ApplicationLoaderComponent;

        public validationMessage: Array<string> = new Array<string>();

        constructor(
<<<<<<< HEAD
            ComponentName: string = null,
=======
            ComponentName: string,
>>>>>>> change
            applicationService: CliqueHRType.WebInterface.IApplicationService,
            changeDetection: ChangeDetectorRef,
            viewContainerRef: ViewContainerRef
        ) {
            this.m_applicationService = applicationService;
            this.m_changeDetector = changeDetection;
<<<<<<< HEAD
            this.m_viewContainerRef = viewContainerRef;
            if (!isNullOrUndefined(ComponentName)) {
                this.m_componentName = ComponentName;
                this.m_applicationService.RegisterComponent(ComponentName);
                this.SubjectDestroy.push(
                    this.m_applicationService.GetComponentSubscriber(ComponentName).subscribe(
                        request => {
                            this.TriggerCommand(request);
                        }
                    )
                );
            }
        }
        ngAfterViewInit(): void {
            $('[data-toggle="tooltip"]').tooltip();
            $('[data-tooltip="tooltip"]').tooltip()
            $('[data-toggle="popover"]').popover({
                trigger: 'focus'
            });
=======
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
>>>>>>> change
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
<<<<<<< HEAD
        protected ValidateFile(validationType: WebInterface.FileValidationConfig, file: any): string {
            if (file) {
                if (!isNullOrUndefined(validationType.MaxSizeInMb)) {
                    let fileSize = (Math.round(file.size * 100 / (1024 * 1024)) / 100);
                    if (fileSize > validationType.MaxSizeInMb) {
                        return ValidationType.InvalidFileSize;
                    }
                }
                if (!isNullOrUndefined(validationType.AllowedExtentions) && validationType.AllowedExtentions.length != 0) {
                    if (isNullOrUndefined(validationType.AllowedExtentions.find(x => x == file.type))) {
                        return ValidationType.InvalidFile;
                    }
                }
            }
            return ValidationType.required;
        }
=======
>>>>>>> change
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