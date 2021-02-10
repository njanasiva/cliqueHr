<<<<<<< HEAD
import { Subject, Subscription } from 'rxjs';
import { isNullOrUndefined } from 'util';
import * as CliqueHRInterface from './AppInterface';
import { Renderer2, ComponentFactoryResolver, TemplateRef, ViewContainerRef, Type, ChangeDetectorRef, OnDestroy, EventEmitter } from '@angular/core';
import { WebInterface } from './AppInterface';
import { ParentNodeComponent } from '../Components/application-single-tree-dropdown/parent-node/parent-node.component';
import { ChildNodeComponent } from '../Components/application-single-tree-dropdown/child-node/child-node.component';
=======
import { Subject } from 'rxjs';
import { isNullOrUndefined } from 'util';
import * as CliqueHRInterface from './AppInterface';
import { Renderer2, ComponentFactoryResolver } from '@angular/core';
>>>>>>> change

export const rendererService = 'renderer';
export const componentFactoryService = 'componentFactory';

export namespace WebAppService {
    export abstract class AbstractApplicationService implements CliqueHRInterface.WebInterface.IApplicationService {
        private m_componentSubjectMap: { [key: string]: Subject<CliqueHRInterface.WebInterface.CommandRequest> } = {};
        private m_services: { [key: string]: any } = {};


        constructor(protected renderer: Renderer2,
            protected componentFactory: ComponentFactoryResolver) {
            this.m_services[rendererService] = renderer;
            this.m_services[componentFactoryService] = componentFactory;
        }
        public ServiceFactory(serviceName: string): void {
            return this.m_services[serviceName];
        }
        public RegisterComponent(ComponentName: string): void {
            this.m_componentSubjectMap[ComponentName] = new Subject<CliqueHRInterface.WebInterface.CommandRequest>();
        }
        public UnRegisterComponent(ComponentName: string): void {
            if (!isNullOrUndefined(this.m_componentSubjectMap[ComponentName])) {
                this.m_componentSubjectMap[ComponentName].unsubscribe();
            }
        }
        public GetComponentSubscriber(ComponentName: string): Subject<CliqueHRInterface.WebInterface.CommandRequest> {
            if (!isNullOrUndefined(this.m_componentSubjectMap[ComponentName])) {
                return this.m_componentSubjectMap[ComponentName];
            }
            return null;
        }
        public SendCommand(ComponentName: string, Type: string, Data: any): boolean {
            if (!isNullOrUndefined(this.m_componentSubjectMap[ComponentName])) {
                let request: CliqueHRInterface.WebInterface.CommandRequest = {
                    ComponentName: ComponentName,
                    Type: Type,
                    Data: Data
                };
                this.m_componentSubjectMap[ComponentName].next(request);
                return true;
            }
            else {
                return false;
            }
        }
    }
<<<<<<< HEAD

    export abstract class BaseTreeDropdown implements OnDestroy {
        protected abstract data: any;
        protected abstract configuration: WebInterface.TreeDropdown;
        protected abstract container: ViewContainerRef;
        protected abstract childTemplate: TemplateRef<any>;
        protected abstract isSingleSelect: boolean;
        protected cur_id: number = 0;
        protected selectedValue: { [key: number]: WebInterface.ITreeNode } = {};
        private m_subscriptions: Array<Subscription> = new Array<Subscription>();
        constructor(
            protected componentFactoryResolver: ComponentFactoryResolver,
            protected cd: ChangeDetectorRef
        ) {

        }
        ngOnDestroy(): void {
            for (let sub of this.m_subscriptions) {
                sub.unsubscribe();
            }
        }

        protected GenarateTree(data: any[], context?: any) {
            var _me = this;
            if (!isNullOrUndefined(context)) {
                _me = context;
            }
            for (let obj of data) {
                let isChildExists = !isNullOrUndefined(obj[this.childProperty]) && obj[this.childProperty].length != 0 ?
                    true : false;
                if (!isChildExists) {
                    let node = this.CreateChildNode(_me);
                    this.SetupNode(node, obj, context);
                } else {
                    let node = this.CreateParentNode(_me);
                    this.SetupNode(node, obj, context);
                    this.GenarateTree(obj[this.childProperty], node);
                }
            }
        }
        private SetupNode(node: any, obj: any, context: any) {
            node.data = obj;
            node.Id = ++this.cur_id;
            node.isSelected = this.CheckSelectedValue(obj);
            node.displayProperty = this.displayProperty;
            if (!isNullOrUndefined(context)) {
                node.parentComponent = context;
                if (node.isSelected) {
                    context.OpenParent();
                }
            }
            this.selectedValue[node.Id] = node;
            this.SubscribeNodeEvent(node);
        }
        protected abstract CheckSelectedValue(data: any);
        protected abstract SelectedNode(isSelected: boolean, data: any);
        protected abstract CreateParentNode(context: any): any;
        protected abstract CreateChildNode(context: any): any;
        private get childProperty() {
            return this.configuration.ChildProperty;
        }
        protected get valueProperty() {
            return this.configuration.ValueProperty;
        }
        protected get displayProperty() {
            return this.configuration.DisplayProperty;
        }
        public ResetSelection() {
            if (!isNullOrUndefined(this.selectedValue)) {
                const componentIds = Object.keys(this.selectedValue);
                for (let id of componentIds) {
                    this.selectedValue[id].isSelected = false;
                    this.selectedValue[id].MarkForChange();
                }
            }
        }
        protected AddNode<T>(type: Type<any>) {
            let resolvedComponent = this.componentFactoryResolver.resolveComponentFactory(type);
            return this.container.createComponent(resolvedComponent).instance as T;
        }
        private SubscribeNodeEvent(node: any) {
            this.m_subscriptions.push(
                node.onNodeSelection.subscribe(
                    (data: { isSelected: boolean, id: number, data: any }) => {
                        if (this.isSingleSelect) {
                            this.ResetSelection();
                        }
                        this.SelectedNode(data.isSelected, data.data);
                        this.selectedValue[data.id].isSelected = data.isSelected;
                        this.selectedValue[data.id].MarkForChange();
                    }
                )
            );
        }
    }

    export abstract class BaseTreeNode implements WebInterface.ITreeNode {
        private m_isShowChild: boolean = false;
        public data: any;
        public displayProperty: string;
        public isSelected: boolean = false;
        public Id: number;
        protected container: ViewContainerRef;
        public onNodeSelection: EventEmitter<{ isSelected: boolean, id: number, data: any }> = new EventEmitter<{ isSelected: boolean, id: number, data: any }>();
        public parentComponent: WebInterface.ITreeNode;

        constructor(
            protected componentFactoryResolver: ComponentFactoryResolver,
            protected cd: ChangeDetectorRef
        ) { }

        public get isShowChild(): boolean {
            return this.m_isShowChild;
        }

        public get GetDisplayText() {
            return this.data[this.displayProperty];
        }

        public AddNode<T>(component: Type<any>): T {
            let resolvedComponent = this.componentFactoryResolver.resolveComponentFactory(component);
            return this.container.createComponent(resolvedComponent).instance as T;
        }

        public toggleChild() {
            this.m_isShowChild = !this.isShowChild;
        }

        public OnNodeSelection() {
            this.isSelected = !this.isSelected;
            this.onNodeSelection.emit({ isSelected: this.isSelected, id: this.Id, data: this.data });
        }

        public OpenParent() {
            if (this.m_isShowChild) {
                return;
            }
            this.m_isShowChild = true;
            if (!isNullOrUndefined(this.parentComponent)) {
                this.parentComponent.OpenParent();
            }
        }

        public MarkForChange() {
            this.cd.markForCheck();
        }
    }
=======
>>>>>>> change
}