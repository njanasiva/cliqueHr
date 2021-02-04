using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public interface IQualificationService
    {
        #region Course Type
        void AddCourseType(CourseType model, UserContextModel objUser);
        PaginationData<CourseType> GetAllCourseType(UserContextModel objUser, PaginationModel paginationModel);
        CourseType GetCourseTypeByID(int ID, UserContextModel objUser);
        void UpdateCourseType(CourseType model, UserContextModel objUser);
        #endregion

        #region CourseTitle
        void AddCourseTitle(CourseTitle model, UserContextModel objUser);
        PaginationData<CourseTitle> GetAllCourseTitle(UserContextModel objUser, PaginationModel paginationModel);
        CourseTitle GetCourseTitleByID(int ID, UserContextModel objUser);
        void UpdateCourseTitle(CourseTitle model, UserContextModel objUser);
        #endregion

        #region Major
        void AddMajor(Major model, UserContextModel objUser);
        PaginationData<Major> GetAllMajor(UserContextModel objUser, PaginationModel paginationModel);
        Major GetMajorByID(int ID, UserContextModel objUser);
        void UpdateMajor(Major model, UserContextModel objUser);
        #endregion

        #region University
        void AddUniversity(University model, UserContextModel objUser);
        PaginationData<University> GetAllUniversity(UserContextModel objUser, PaginationModel paginationModel);
        University GetUniversityByID(int ID, UserContextModel objUser);
        void UpdateUniversity(University model, UserContextModel objUser);
        #endregion

        #region Institute
        void AddInstitute(Institute model, UserContextModel objUser);
        PaginationData<Institute> GetAllInstitute(UserContextModel objUser, PaginationModel paginationModel);
        Institute GetInstituteByID(int ID, UserContextModel objUser);
        void UpdateInstitute(Institute model, UserContextModel objUser);
        #endregion

    }
}
