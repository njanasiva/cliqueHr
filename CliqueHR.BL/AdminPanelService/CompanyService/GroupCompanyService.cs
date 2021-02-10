using System;
using System.Collections.Generic;
using System.Web;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using Newtonsoft.Json;

namespace CliqueHR.BL
{
    public class GroupCompanyService : IGroupCompanyService
    {
        private IGroupCompanyRepository _groupCompanyRepository;
        private GroupCompanyValidation _modelValidation;
     
        public GroupCompanyService()
        {
            _groupCompanyRepository = new GroupCompanyRepository();
            _modelValidation = new GroupCompanyValidation();
        }
        public void AddUpdateGroupCompany(GroupCompany model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(GroupCompanyValidation.ValidateAll_key, model, "Group company  model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.TransType = "SAVE";
                model.CreatedBy = objUser.EmployeeId;
                var data = _groupCompanyRepository.AddUpdateGroupCompany(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "GroupCompany" });
                    throw new ValidationException(responseValidation);
                }
                
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public GroupCompany GetGroupCompany(UserContextModel objUser)
        {
            try
            {
                GroupCompany model = new GroupCompany();
                model.TransType = "FETCH";
                var data = _groupCompanyRepository.GetGroupCompany(model, objUser.CompanyCode);
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
