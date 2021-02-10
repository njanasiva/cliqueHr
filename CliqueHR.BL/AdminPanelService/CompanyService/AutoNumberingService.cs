using System;
using System.Collections.Generic;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;

namespace CliqueHR.BL
{
    public class AutoNumberingService : IAutoNumberingService
    {
        private IAutoNumberingRepository _autoNumberingRepository;
        private AutoNumberingValidation _modelValidation;
        public AutoNumberingService()
        {
            _autoNumberingRepository = new AutoNumberingRepository();
            _modelValidation = new AutoNumberingValidation();
        }
        public void AddUpdateAutoNumbering(AutoNumbering model, UserContextModel objUser)
        {
            try
            {

                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(AutoNumberingValidation.ValidateAll_key, model, "Auto Numbering  model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _autoNumberingRepository.AddUpdateAutoNumbering(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "AutoNumbering" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public AutoNumbering GetAutoNumbering(UserContextModel objUser)
        {
            try
            {
                var data = _autoNumberingRepository.GetAutoNumbering(objUser.CompanyCode);
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
