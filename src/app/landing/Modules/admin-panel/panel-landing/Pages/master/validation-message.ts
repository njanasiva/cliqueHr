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
        }
    }
}