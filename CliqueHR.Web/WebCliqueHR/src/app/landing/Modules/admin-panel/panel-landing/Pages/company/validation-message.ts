import { ValidationType } from 'src/Application/Types/Constants';

export class ValidationMessage {
    public static data = {
        SecuritySettingForm: {
            PasswordExpiryIndays: {
                [ValidationType.required]: "*"
            },
            SessionTimeOutInMins: {
                [ValidationType.required]: "*"
            },
        },
        GroupCompanyForm: {
            Name: {
                [ValidationType.required]: "*"
            },
            Code: {
                [ValidationType.required]: "*"
            },
            Address: {
                [ValidationType.required]: "*"
            },
            PinCode: {
                [ValidationType.required]: "*"
            },
            ContcatNo: {
                [ValidationType.required]: "*"
            },
            WebSite: {
                [ValidationType.required]: "*"
            },
        }
    }
}