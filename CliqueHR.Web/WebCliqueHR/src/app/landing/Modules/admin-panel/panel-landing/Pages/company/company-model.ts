import { FormGroup } from '@angular/forms';
import { isNullOrUndefined } from 'util';
import { UiDatepicker, UiSingleselect } from 'projects/clique-hrui/src/lib/ui-models';
import { GetDateInString } from 'src/Application/Utilitis/ApplicationUtility';

export class SecuritySettings {
    PasswordExpiryIndays: number;
    SessionTimeOutInMins: number;
    HideMobileNumberFromEd: string;
}
export class GroupCompany {
    Id: number;
    Name: string;
    Code: string;
    TypeId: number;
    IncorporationDate: any;
    Address: string;
    CountryId: number;
    StateId: number;
    CityId: number;
    PinCode: number;
    ContcatNo: string;
    WebSite: string;
    PAN: string;
    TAN: string;
    GSTIN: string;
    PF: string;
    ESIC: string;
    Logo: string;
}

export abstract class GroupCompanyBase {
    protected m_isEditable: boolean;
    protected m_isSaveActivated: boolean;
    protected m_isDiabled: boolean = false;
    protected formGroup: FormGroup;
    protected m_incorporationDateConfig: UiDatepicker = {
        isEditable: true,
        disabled: !this.isEditable,
        max: GetDateInString(new Date())
    }

    constructor(formGroup: FormGroup) {
        this.formGroup = formGroup;
    }
    public ActivateSave() {

    }
    public DeActivateSave() {

    }
    public get isSaveActivated() {
        return this.m_isSaveActivated;
    }
    public get isEditable() {
        return this.m_isEditable;
    }
    public get isDiabled() {
        return this.m_isDiabled;
    }
    public get incorporationDateConfig() {
        return this.m_incorporationDateConfig;
    }

    protected ExecuteReadonlyStrategy() {
        var keys = Object.keys(this.formGroup.controls);
        for (let prop of keys) {
            let control = this.formGroup.get(prop);
            if (!isNullOrUndefined(control)) {
                if (this.isDiabled) {
                    control.disable();
                }
                else {
                    control.enable();
                }
            }
        }
    }
}

export class ViewGroupCompany extends GroupCompanyBase {
    constructor(formGroup: FormGroup) {
        super(formGroup);
        this.m_isEditable = true;
        this.m_isSaveActivated = false;
        this.m_isDiabled = true;
        this.ExecuteReadonlyStrategy();
    }

    public ActivateSave() {
        this.m_isSaveActivated = true;
    }
    public DeActivateSave() {
        this.m_isSaveActivated = false;
    }
}
export class AddGroupCompany extends GroupCompanyBase {
    constructor(formGroup: FormGroup) {
        super(formGroup);
        this.m_isEditable = false;
        this.m_isSaveActivated = true;
        this.ExecuteReadonlyStrategy();
    }
}
export class EditGroupCompany extends GroupCompanyBase {
    constructor(formGroup: FormGroup) {
        super(formGroup);
        this.m_isEditable = true;
        this.m_isSaveActivated = true;
        this.ExecuteReadonlyStrategy();
    }
}

export class Entity {
    Id: number;
    Name: string;
    Code: string;
    TypeId: number;
    TypeName: string;
    IncorporationDate: Date;
    Address: string;
    CountryId: number;
    StateId: number;
    CityId: number;
    PinCode: number;
    ContcatNo: string;
    WebSite: string;
    PAN: string;
    TAN: string;
    GSTIN: string;
    PF: string;
    ESIC: string;
    Logo: string;
    IsDoNotUse: boolean;
    constructor() {
        this.Id = 0;
        this.IsDoNotUse = false;
    }
}

export class AutoNumbering {
    Prefix: string;
    AppendNumber: number;
}

export abstract class AutoNumberingBase {
    protected m_isEditable: boolean;
    protected m_isSaveActivated: boolean;
    protected m_isDiabled: boolean = false;
    protected formGroup: FormGroup;

    constructor(formGroup: FormGroup) {
        this.formGroup = formGroup;
    }
    public ActivateSave() {

    }
    public DeActivateSave() {

    }
    public get isSaveActivated() {
        return this.m_isSaveActivated;
    }
    public get isEditable() {
        return this.m_isEditable;
    }
    public get isDiabled() {
        return this.m_isDiabled;
    }

    protected ExecuteReadonlyStrategy() {
        var keys = Object.keys(this.formGroup.controls);
        for (let prop of keys) {
            let control = this.formGroup.get(prop);
            if (!isNullOrUndefined(control)) {
                if (this.isDiabled) {
                    control.disable();
                }
                else {
                    control.enable();
                }
            }
        }
    }
}

export class ViewAutoNumbering extends AutoNumberingBase {
    constructor(formGroup: FormGroup) {
        super(formGroup);
        this.m_isEditable = true;
        this.m_isSaveActivated = false;
        this.m_isDiabled = true;
        this.ExecuteReadonlyStrategy();
    }

    public ActivateSave() {
        this.m_isSaveActivated = true;
    }
    public DeActivateSave() {
        this.m_isSaveActivated = false;
    }
}

export class AddAutoNumbering extends AutoNumberingBase {
    constructor(formGroup: FormGroup) {
        super(formGroup);
        this.m_isEditable = false;
        this.m_isSaveActivated = true;
        this.ExecuteReadonlyStrategy();
    }
}

export class EditAutoNumbering extends AutoNumberingBase {
    constructor(formGroup: FormGroup) {
        super(formGroup);
        this.m_isEditable = true;
        this.m_isSaveActivated = true;
        this.ExecuteReadonlyStrategy();
    }
}

export class OrgUnits {
    Id: number;
    Name: string;
    Code: string;
    UnitHeadId: number;
    UnitHead: string;
    ParentEntityId: number;
    ParentOrgUnitId: number;
    ParentUnit: string;
    IsDoNotUse: boolean;
    constructor() {
        this.IsDoNotUse = false;
    }
}

export class Department {
    public Id: number;
    public Name: string;
    public Code: string;
    public ParentEntityId: number;
    public ParentOrgUnitId: number;
    public ParentDepartmentId: number;
    public HOD: string;
    public ParentUnit: string;
    public HODId: number;
    public IsDoNotUse: boolean;
}