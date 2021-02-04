import { UiDataTableConfig } from 'projects/clique-hrui/src/lib/ui-models';
import { Country, State } from 'src/app/landing/Modules/common-model';
import { isNullOrUndefined } from 'util';

export const CourceTypeDTConfig: UiDataTableConfig = {
    Columns: [
        { fieldId: 'ID', fieldName: '', columnClass: 'text-nowrap' },
        { fieldId: 'CourseType', fieldName: 'Course Type', columnClass: 'text-nowrap' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'ID', direction: 'asc' },
    UniqueRowCol: 'ID',
    isEditable: true
};
export const CourceTitleDTConfig: UiDataTableConfig = {
    Columns: [
        { fieldId: 'ID', fieldName: '', columnClass: 'text-nowrap' },
        { fieldId: 'CourseTitle', fieldName: 'Course Title', columnClass: 'text-nowrap' },
        { fieldId: 'CourseType', fieldName: 'Course Type', columnClass: 'text-nowrap' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'ID', direction: 'asc' },
    UniqueRowCol: 'ID',
    isEditable: true
};
export const MajorDTConfig: UiDataTableConfig = {
    Columns: [
        { fieldId: 'ID', fieldName: '', columnClass: 'text-nowrap' },
        { fieldId: 'MajorName', fieldName: 'Major Name', columnClass: 'text-nowrap' },
        { fieldId: 'CourseType', fieldName: 'Course Type', columnClass: 'text-nowrap' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'ID', direction: 'asc' },
    UniqueRowCol: 'ID',
    isEditable: true
};
export const UniversityDTConfig: UiDataTableConfig = {
    Columns: [
        { fieldId: 'ID', fieldName: '', columnClass: 'text-nowrap' },
        { fieldId: 'UniversityName', fieldName: 'University Name', columnClass: 'text-nowrap' },
        { fieldId: 'Country', fieldName: 'Country', columnClass: 'text-nowrap' },
        { fieldId: 'State', fieldName: 'State', columnClass: 'text-nowrap' },
        { fieldId: 'City', fieldName: 'City', columnClass: 'text-nowrap' },
        { fieldId: 'Blacklist', fieldName: 'Blacklist', columnClass: 'text-nowrap' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'ID', direction: 'asc' },
    UniqueRowCol: 'ID',
    isEditable: true
};
export const InstituteDTConfig: UiDataTableConfig = {
    Columns: [
        { fieldId: 'ID', fieldName: '', columnClass: 'text-nowrap' },
        { fieldId: 'InstituteName', fieldName: 'Institute Name', columnClass: 'text-nowrap' },
        { fieldId: 'UniversityName', fieldName: 'University Name', columnClass: 'text-nowrap' },
        { fieldId: 'Country', fieldName: 'Country', columnClass: 'text-nowrap' },
        { fieldId: 'State', fieldName: 'State', columnClass: 'text-nowrap' },
        { fieldId: 'City', fieldName: 'City', columnClass: 'text-nowrap' },
        { fieldId: 'Blacklist', fieldName: 'Blacklist', columnClass: 'text-nowrap' }
    ],
    Pagination: true,
    PaginationPageSize: 10,
    DefaultSort: { fieldId: 'ID', direction: 'asc' },
    UniqueRowCol: 'ID',
    isEditable: true
};

export function Dereference<T>(data: any) {
    return JSON.parse(JSON.stringify(data)) as T;
}