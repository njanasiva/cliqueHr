import { Subject } from "rxjs";
<<<<<<< HEAD
import { UiMultiselect, UiDropdownTemplate } from 'projects/clique-hrui/src/lib/ui-models';
=======
>>>>>>> change

export namespace WebInterface {
    export interface IApplicationService {
        RegisterComponent(ComponentName: string): void;
        UnRegisterComponent(ComponentName: string): void;
        SendCommand(ComponentName: string, Type: string, Data: any): boolean;
        GetComponentSubscriber(ComponentName: string): Subject<CommandRequest>;
        ServiceFactory(serviceName:string): any;
    }
    export interface CommandRequest {
        ComponentName: string;
        Type: string;
        Data: any;
    }
    export interface PaginationModel {
        StartRow: number;
        EndRow: number;
        Sort?: { PropertyName: string, Direction: string };
        SearchText?: string;
    }
<<<<<<< HEAD
    export interface SinglePaginationModel extends PaginationModel{

    }
    export interface FileValidationConfig{
        MaxSizeInMb?:number;
        AllowedExtentions?:string[];
    }

    export interface Multiselect extends UiMultiselect {
        DisplayProperty:any;
    }
    export interface TreeDropdown extends UiDropdownTemplate {
        DisplayProperty:string;
        ChildProperty:string;
        ValueProperty: string;
    }

    export interface ITreeNode {
        data: any;
        displayProperty: string;
        isSelected: boolean;
        Id:number;
        MarkForChange();
        parentComponent: WebInterface.ITreeNode;
        OpenParent(): void;
    }

    export type selectCalback = (data: any) => boolean;
=======
>>>>>>> change
}