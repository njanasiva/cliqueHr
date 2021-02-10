using System;
using System.Collections.Generic;
using System.Linq;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;

namespace CliqueHR.BL
{
    public class EmployeeGroupService : IEmployeeGroupService
    {

        private IEmployeeGroupRepository _employeeGroupRepository;
        private EmployeeGroupValidation _modelValidation;

        public EmployeeGroupService()
        {
            _employeeGroupRepository = new EmployeeGroupRepository();
            _modelValidation = new EmployeeGroupValidation();
        }

        public void AddEmployeeGroup(EmployeeGroup model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(EmployeeGroupValidation.ValidateAll_key, model, "Employee group model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.Id = 0;
                model.CreatedBy = objUser.EmployeeId;
                var data = _employeeGroupRepository.AddEmployeeGroup(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "EmployeeGroup" });
                    throw new ValidationException(responseValidation);
                }
            }

            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateEmployeeGroup(EmployeeGroup model, UserContextModel objUser)
        {
            var responseValidation = Validator.GetValidationResponseInstance();
            try
            {
                var validationResponse = _modelValidation.Validate(EmployeeGroupValidation.ValidateAll_key, model, "Employee group model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.ModifiedBy = objUser.EmployeeId;
                var data = _employeeGroupRepository.UpdateEmployeeGroup(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "EmployeeGroup" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public EmployeeGroupByIdResponse GetEmployeeGroupById(int Id, PaginationModel model, UserContextModel objUser)
        {
            try
            {
                var data = _employeeGroupRepository.GetEmployeeGroupById(Id, model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<EmployeeGroupResponse> GetAllEmployeeGroup(PaginationModel model, UserContextModel objUser)
        {
            try
            {
                var data = _employeeGroupRepository.GetAllEmployeeGroup(model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
    }
}
