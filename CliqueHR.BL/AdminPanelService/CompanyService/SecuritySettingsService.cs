using System;
using System.Collections.Generic;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
namespace CliqueHR.BL
{
    public class SecuritySettingsService : ISecuritySettingService
    {
        private ISecuritySettingsRepository _securitysettingsRepository;
        private SecuritySettingValidation _modelValidation;
        public SecuritySettingsService()
        {
            _securitysettingsRepository = new SecuritySettingsRepository();
            _modelValidation = new SecuritySettingValidation();
        }
        public void AddUpdateSecuritySettings(SecuritySettings model, UserContextModel objUser)
        {
            try
            {

                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(SecuritySettingValidation.ValidateAll_key, model, "Security settings  model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.TransType = "SAVE";
                model.CreatedBy = objUser.EmployeeId;
                var data = _securitysettingsRepository.AddUpdateSecuritySettings(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "SecuritySettings" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public SecuritySettings GetSecuritySettings(UserContextModel objUser)
        {
            try
            {
                SecuritySettings model = new SecuritySettings();
                model.TransType = "FETCH";
                var data = _securitysettingsRepository.GetSecuritySettings(model, objUser.CompanyCode);
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
