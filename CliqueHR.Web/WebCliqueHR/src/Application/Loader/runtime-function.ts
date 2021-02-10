import { Injector, Compiler, ViewContainerRef } from '@angular/core';
import { isUndefined } from 'util';
import { HttpClient } from '@angular/common/http';

declare const SystemJS: any;
import * as angularCore from '@angular/core';
import * as angularCommon from '@angular/common';

SystemJS.set('@angular/core', SystemJS.newModule(angularCore));
SystemJS.set('@angular/common', SystemJS.newModule(angularCommon));

export class AppSetings {
    private static injector: Injector;
    private static compiler: Compiler;

    public static set setInjector(injector: Injector) {
        AppSetings.injector = injector;
        AppSetings.compiler = AppSetings.injector.get(Compiler);
    }

    public static async GetModuleFromImport(moduleObj: any, content: ViewContainerRef) {
        const loadModel = await moduleObj;
        const module = loadModel[Object.keys(loadModel)[0]];
        const moduleFactory = await AppSetings.compiler.compileModuleAsync<any>(module);
        const moduleRef: any = moduleFactory.create(AppSetings.injector);
        const componentFactory = moduleRef.componentFactoryResolver.resolveComponentFactory(moduleRef._bootstrapComponents[0])
        let ref = content.createComponent(componentFactory, null, moduleRef.injector);
        return ref;
    }
}