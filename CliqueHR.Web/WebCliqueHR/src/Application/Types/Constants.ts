export class ApplicationVeriable {
    public static AuthTokenKey: string = '**clhrtkn**';
    public static RefreshTokenKey: string = '**clhrrtkn**';
    public static AccessFactorKey: string = '**clhraf**';
    public static InvalidRefreshTokenError: string = 'invalid_grant';
}
export class ValidationType {
    public static required: string = "required";
    public static minlength: string = "minlength";
    public static maxlength: string = "maxlength";
    public static min: string = "min";
    public static max: string = "max";
    public static pattern: string = "pattern";
    public static valueMismatch: string = "valueMismatch";
    public static email: string = "email";
    public static InvalidFileSize = "invalid-file-size";
    public static InvalidFile = "invalid-file";
    public static valueMinMax: string = "valueMinMax";
}
export class FileType{
    public static JPEG = "image/jpeg";
    public static JPG = "image/jpg";
    public static PNG = "image/png";
    public static GIF = "image/gif";
    public static PDF = "application/pdf";
}
export class Components {
    public static LandingComponent = "LandingComponent";
    public static HeaderComponent = "HeaderComponent";
    public static ChangeTheamComponent = "ChangeTheamComponent";
    public static PanelLandingComponent = "PanelLandingComponent";
    public static CompanyComponent = "CompanyComponent";
    public static GroupCompanyComponent = "GroupCompanyComponent";
    public static EntityComponent = "EntityComponent";
    public static MasterComponent = "MasterComponent";
    public static ApplicationLoaderComponent = "ApplicationLoaderComponent";
    public static SecuritySettingsComponent = "SecuritySettingsComponent";
    public static QualificationComponent = "QualificationComponent";
    public static CurrencyComponent = "CurrencyComponent";
    public static CenterTypeComponent = "CenterTypeComponent";
    public static SeperationComponent = "SeperationComponent";
    public static ConfirmationComponent = "ConfirmationComponent";
    public static SettingsComponent = "SettingsComponent";
    public static MovementComponent = "MovementComponent";
    public static ExitinterviewComponent = "ExitinterviewComponent";
    public static LifecycleComponent = "LifecycleComponent";
    public static EmployeeTypeComponent = "EmployeeTypeComponent";
    public static AutoNumberingComponent = "AutoNumberingComponent";
    public static GradeComponent = "GradeComponent";
    public static BandComponent = "BandComponent";
    public static FunctionalRoleComponent = "FunctionalRoleComponent";
    public static PageSettingsComponent = "PageSettingsComponent";
    public static LoginComponent = "LoginComponent";
    public static OrgUnitsComponent = "OrgUnitsComponent";
    public static DepartmentsComponent = "DepartmentsComponent";
    public static DesignationComponent = "DesignationComponent";
}

export class Dashboard {
    public static MyAppData = [
        {
            Name: 'Home',
            RouteLink: '',
            Code: 'home'
        },
        {
            Name: 'Admin Panel',
            RouteLink: 'admin-panel',
            Code: 'admin-panel'
        },
        {
            Name: 'Reminders',
            RouteLink: '',
            Code: 'reminders'
        },
        {
            Name: 'Approvals',
            RouteLink: '',
            Code: 'approvals'
        },
        {
            Name: 'HR Policy',
            RouteLink: '',
            Code: 'hr-policy'
        },
        {
            Name: 'Employee Directory',
            RouteLink: '',
            Code: 'employee-directory'
        },
        {
            Name: 'Attendance',
            RouteLink: '',
            Code: 'attendance'
        },
        {
            Name: 'Leave',
            RouteLink: '',
            Code: 'leave'
        }
    ]
}

export class RequestTypes {
    public static ChangeTheme = 'ChangeTheme';
    public static ShowLoading = 'ShowLoading';
    public static HideLoading = 'HideLoading';
}