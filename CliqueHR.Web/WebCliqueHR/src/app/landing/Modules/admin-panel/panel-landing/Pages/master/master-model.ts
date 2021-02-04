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
}