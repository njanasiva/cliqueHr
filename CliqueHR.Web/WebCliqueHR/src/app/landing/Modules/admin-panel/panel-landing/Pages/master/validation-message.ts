import { ValidationType } from 'src/Application/Types/Constants';

export class ValidationMessage {
    public static data = {
        CourseTypeForm: {
            TypeName: {
                [ValidationType.required]: "Type Name Required."
            }
        },
        CourseTitleForm: {
            TitleName: {
                [ValidationType.required]: "Title Name Required."
            }
        },
        CourseMajorForm: {
            MajorName: {
                [ValidationType.required]: "Major Name Required."
            }
        },
        CourseUniversityForm: {
            UniversityName: {
                [ValidationType.required]: "University Name Required."
            }
        },
        CourseInstituteForm: {
            InstituteName: {
                [ValidationType.required]: "Institute Name Required."
            }
        },
        CenterTypeForm: {
            CenterTypeName: {
                [ValidationType.required]: "CentreType Name Required."
            }
        },
        EmployeeTypeForm: {
            TypeName: {
                [ValidationType.required]: "EmployeeType Name Required."
            },
            MinAge: {
                [ValidationType.required]: "Minimum age Required.",
                [ValidationType.min]: "age should be greater than 18"
            }
        },
        GradeForm: {
            TypeName: {
                [ValidationType.required]: "Grade Name Required."
            },
            MinSalary: {
                [ValidationType.required]: "Minimum salary Required.",
                [ValidationType.min]:"Minimum Salary should be greater then 0."
            },
            MaxSalary: {
                [ValidationType.required]: "Maximum salary Required.",
                [ValidationType.valueMinMax]: "Maximum salary should be greater than Min salary."
            },
        },
        BandForm: {
            TypeName: {
                [ValidationType.required]: "Band Name Required."
            }
        },
        FunctionalRoleForm: {
            FRoleName: {
                [ValidationType.required]: "Role Name Required."
            },
            FRoleCode: {
                [ValidationType.required]: "Role Code Required."
            }
        },
        DesignationForm: {
            Name: {
                [ValidationType.required]: "Name Required."
            },
            Code: {
                [ValidationType.required]: "Code Required."
            }
        },
        LocationForm: {
            Name: {
                [ValidationType.required]: "Name Required."
            },
            Code: {
                [ValidationType.required]: "Code Required."
            },
            Address:{
                [ValidationType.required]: "Address Required."
            },
            PinCode:{
                [ValidationType.required]: "Address Required."
            },
            Phone:{
                [ValidationType.required]: "Address Required.",
                [ValidationType.maxlength]: "Invalid phone no."
            },
        },
        RegionForm: {
            Name: {
                [ValidationType.required]: "Name Required."
            }
        }
    }
}