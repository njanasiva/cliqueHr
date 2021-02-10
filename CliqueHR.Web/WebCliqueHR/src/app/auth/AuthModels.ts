export class PageSettingImages {
    public Id: number;
    ImageType: string;
    ImagePath: string;
}

export class LoginPageModel {
    public loginPageImages: Array<PageSettingImages>;
}

export class LoginModel {
    EmployeeCode: string;
    Password: string;
    companyCode: string;
}

export class LoginResponse {
    access_token: string;
    refresh_token: string;
    AccessFactor: string;
}
export class LoginErrorModel {
    error:string;
    error_description: string;
}