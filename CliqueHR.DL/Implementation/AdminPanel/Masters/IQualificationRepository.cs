using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.DL
{
    public interface IQualificationRepository
    {
        #region Course Type
        ApplicationResponse AddCourseType(CourseType model, string DBName);
        PaginationData<CourseType> GetAllCourseType(string DBName ,PaginationModel model);
        CourseType GetCourseTypeById(int Id, string DBName);
        ApplicationResponse UpdateCourseType(CourseType model, string DBName);
        #endregion
        
        #region Course Title
        ApplicationResponse AddCourseTitle(CourseTitle model, string DBName);
        PaginationData<CourseTitle> GetAllCourseTitle(string DBName, PaginationModel model);
        CourseTitle GetCourseTitleById(int Id, string DBName);
        ApplicationResponse UpdateCourseTitle(CourseTitle model, string DBName);
        #endregion
        
        #region Course Major
        ApplicationResponse AddCourseMajor(Major model, string DBName);
        PaginationData<Major> GetAllCourseMajor(string DBName, PaginationModel model);
        Major GetCourseMajorById(int Id, string DBName);
        ApplicationResponse UpdateCourseMajor(Major model, string DBName);
        #endregion
        
        #region Course University
        ApplicationResponse AddCourseUniversity(University model, string DBName);
        PaginationData<University> GetAllCourseUniversity(string DBName, PaginationModel model);
        University GetCourseUniversityById(int Id, string DBName);
        ApplicationResponse UpdateCourseUniversity(University model, string DBName);
        #endregion
        
        #region Course Institute
        ApplicationResponse AddCourseInstitute(Institute model, string DBName);
        PaginationData<Institute> GetAllCourseInstitute(string DBName, PaginationModel model);
        Institute GetCourseInstituteById(int Id, string DBName);
        ApplicationResponse UpdateCourseInstitute(Institute model, string DBName);
        #endregion
    }
}
