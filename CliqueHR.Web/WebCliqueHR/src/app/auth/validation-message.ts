import { ValidationType } from 'src/Application/Types/Constants';

export class ValidationMessage {
    public static data = {
        loginForm:{
            EmployeeCode: {
                [ValidationType.required]: "EmployeeCode required"
            },
            Password: {
                [ValidationType.required]: "Password required"
            }   
        }
    }
}