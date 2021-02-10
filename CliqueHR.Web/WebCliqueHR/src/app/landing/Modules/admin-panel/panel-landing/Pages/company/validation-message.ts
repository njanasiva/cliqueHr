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
        },
        entityForm: {
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
                [ValidationType.required]: "*",
                [ValidationType.maxlength]: "Invalid Contact No."
            },
            WebSite: {
                [ValidationType.required]: "*"
            },
        },
        AutoNoFormDept:{
            Prefix: {
                [ValidationType.required]: "Prefix required"
            },
            AppendNumber: {
                [ValidationType.required]: "Append Number required"
            }   
        },
        OrgUnitForm: {
            Name: {
                [ValidationType.required]: "Name required"
            },
            Code: {
                [ValidationType.required]: "Code required"
            } 
        },
        DepartmentForm: {
            Name: {
                [ValidationType.required]: "Name required"
            },
            Code: {
                [ValidationType.required]: "Code required"
            } 
        }
    }
}