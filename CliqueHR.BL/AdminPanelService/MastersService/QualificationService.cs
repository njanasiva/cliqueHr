using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace CliqueHR.BL
{
    public class QualificationService : IQualificationService
    {
        private IQualificationRepository _qualificationRepository;
        private CourseTypeValidation _modelValidation;
        private CourseTitleValidation _titleValidation;
        private MajorValidation _majorValidation;
        private UniversityValidation _universityValidation;
        private InstituteValidation _instituteValidation;

        public QualificationService()
        {
            _qualificationRepository = new QualificationRepository();
            _modelValidation = new CourseTypeValidation();
            _titleValidation = new CourseTitleValidation();
            _majorValidation = new MajorValidation();
            _universityValidation = new UniversityValidation();
            _instituteValidation = new InstituteValidation();
        }

        #region Course Type
        public void AddCourseType(CourseType model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(CourseTypeValidation.ValidateAll_key, model, "Course type model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.AddCourseType(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "CourseType" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<CourseType> GetAllCourseType(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _qualificationRepository.GetAllCourseType(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public CourseType GetCourseTypeByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _qualificationRepository.GetCourseTypeById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateCourseType(CourseType model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(CourseTypeValidation.ValidateEditFields_key, model, "Course type model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.UpdateCourseType(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "CourseType" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion
        
        #region Course Title
        public void AddCourseTitle(CourseTitle model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _titleValidation.Validate(CourseTitleValidation.ValidateAll_key, model, "Course title model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.AddCourseTitle(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "CourseTitle" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<CourseTitle> GetAllCourseTitle(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _qualificationRepository.GetAllCourseTitle(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public CourseTitle GetCourseTitleByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _qualificationRepository.GetCourseTitleById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateCourseTitle(CourseTitle model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _titleValidation.Validate(CourseTypeValidation.ValidateEditFields_key, model, "Course title model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.UpdateCourseTitle(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "CourseTitle" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion
        
        #region Course Major
        public void AddMajor(Major model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _majorValidation.Validate(MajorValidation.ValidateAll_key, model, "Course Major model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.AddCourseMajor(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Major" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<Major> GetAllMajor(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _qualificationRepository.GetAllCourseMajor(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public Major GetMajorByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _qualificationRepository.GetCourseMajorById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateMajor(Major model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _majorValidation.Validate(MajorValidation.ValidateEditFields_key, model, "Course Major model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.UpdateCourseMajor(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Major" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion
        
        #region Course University
        public void AddUniversity(University model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _universityValidation.Validate(UniversityValidation.ValidateAll_key, model, "University model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.AddCourseUniversity(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "University" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<University> GetAllUniversity(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _qualificationRepository.GetAllCourseUniversity(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public University GetUniversityByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _qualificationRepository.GetCourseUniversityById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateUniversity(University model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _universityValidation.Validate(UniversityValidation.ValidateEditFields_key, model, "University model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.UpdateCourseUniversity(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "University" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion
        
        #region Course Institute
        public void AddInstitute(Institute model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _instituteValidation.Validate(InstituteValidation.ValidateAll_key, model, "Institute model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.AddCourseInstitute(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Institute" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<Institute> GetAllInstitute(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _qualificationRepository.GetAllCourseInstitute(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public Institute GetInstituteByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _qualificationRepository.GetCourseInstituteById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateInstitute(Institute model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _instituteValidation.Validate(InstituteValidation.ValidateEditFields_key, model, "Institute model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _qualificationRepository.UpdateCourseInstitute(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Institute" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion

    }
}
