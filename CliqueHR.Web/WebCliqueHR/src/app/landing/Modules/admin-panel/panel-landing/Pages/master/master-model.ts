<<<<<<< HEAD
import { EntityOrgunitDepartmentModel } from 'src/app/landing/Modules/common-model';

=======
>>>>>>> change
export class CourseType {
    Id: number;
    TypeName: string;
    IsDoNotUse: boolean;
}

export class CourseTitle {
    Id: number;
    TitleName: string;
    CourseTypeName: string;
    CourseTypeId: number;
    IsDoNotUse: boolean;
}
export class Major {
    Id: number;
    MajorName: string;
    TitleName: string;
    TitleId: number;
    IsDoNotUse: boolean;
}

export class University {
    Id: number;
    UniversityName: string;
    CountryId: number;
    CountryName: string;
    StateId: number;
    StateName: string;
    CityId: number;
    CityName: string;
    IsBlacklist: boolean;
    IsDoNotUse: boolean;
}

export class Institute {
    Id: number;
    InstituteName: string;
    UniversityName: string;
    UniversityId: number;
    CountryId: number;
    CountryName: string;
    StateId: number;
    StateName: string;
    CityId: number;
    CityName: string;
    IsBlacklist: boolean;
    IsDoNotUse: boolean;
}

export class Currancy {
    Id: number;
    CurrencyCode: string;
    CurrencyDesc: string;
}
export class CurrancyMapping {
    Id: number;
    CurrencyId: number;
    CurrencyCode: string;
    IsDefault: boolean;
    IsDoNotUse: boolean;
<<<<<<< HEAD
    constructor() {
        this.IsDoNotUse = false;
        this.IsDefault = false;
    }
}

export class CenterTypeModel {
    Id: number;
    CenterTypeName: string;
    IsDoNotUse: boolean;
}
export class EmployeeType {
    Id: number;
    TypeName: string;
    SelfService: boolean;
    MinAge: number;
    IsDoNotUse: boolean;
}

export class GradeType {
    Id: number;
    TypeName: string;
    MinSalary: number;
    MaxSalary: number;
    IsDoNotUse: boolean;
}

export class BandType {
    Id: number;
    TypeName: string;
    GradeMapping: string;
    GradeMappingText: string;
    IsDoNotUse: boolean;
    constructor() {
        this.IsDoNotUse = false;
    }
}

export class PageSettings {
    IsBirthdayVisible: boolean;
    IsWorkAnniversaryVisible: boolean;
    IsMarriageAnniversaryVisible: boolean;
    IsNewJoineeVisible: boolean;
    IsExitVisible: boolean;
}

export class PageSettingImages {
    Id: number;
    ImageType: string;
    ImagePath: string;
}

export class FunctionalRole {
    Id: number;
    FRoleName: string;
    FRoleCode: string;
    FRoleDesc: string;
    AttachmentFile: string;
    IsDoNotUse: boolean;
    constructor() {
        this.IsDoNotUse = false;
        this.Id = 0;
    }
}
export interface AttributeModel {
    EntityId: number;
    OrgUnitId: number;
    DeptId: number;
}
export class DesignationModel {
    Id: number;
    Name: string;
    Code: string;
    DTAttributesMap: Array<AttributeModel>;
    IsDoNotUse: boolean;
}

export class DesignationResponseModel {
    DesignationID: number;
    Designation: string;
    DesignationCode: string;
    EntityOrgDeptDesc: string;
    IsDoNotUse: boolean;
    entityOrgunitDepartment: Array<EntityOrgunitDepartmentModel>;
}

export class Location {
    Id: number;
    Name: string;
    Code: string;
    CenterType: number;
    CenterTypeName: string;
    IsRegisteredOffice: boolean;
    Address: string;
    CountryId: number;
    StateId: number;
    CityId: number;
    PinCode: number;
    Phone: string;
    LocHeadEmpId: number;
    LocHeadName: string;
    IsDoNotUse: boolean;
    constructor() {
        this.Id = 0;
        this.IsDoNotUse = false;
        this.IsRegisteredOffice = false;
    }
}

export class RegionModel {
    Id: number;
    Name: string;
    StateId: number;
    StateName: string;
    CityName: string;
    CityId: number;
    RegionHead: number;
    RegionHeadName: string;
    IsDoNotUse: boolean;
    constructor() {
        this.Id = 0;
        this.IsDoNotUse = false;
    }
=======
>>>>>>> change
}