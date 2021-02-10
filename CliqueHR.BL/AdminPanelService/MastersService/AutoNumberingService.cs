using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using CliqueHR.DL.AdminPanel.Masters;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;

namespace CliqueHR.BL.AdminPanelService.MastersService
{
    public class AutoNumberingService : Implementation.AdminPanel.MastersService.IAutoNumberingService
    {

        private AutoNumberingRepository _autoNumberingRepository;
        private AutoNumberValidation _autoNumberValidation;
        public AutoNumberingService()
        {
            _autoNumberingRepository = new AutoNumberingRepository();
            _autoNumberValidation = new AutoNumberValidation();
        }

        public void AddLocationAutoNumber(AutoNumber model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _autoNumberValidation.Validate(AutoNumberValidation.ValidateAll_key, model, "AutoNumberValidation can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;

                var data = _autoNumberingRepository.AddLocationAutoNumber(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "AutoNumber-Master-Location" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<AutoNumber> GetAllLocationAutoNumber(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _autoNumberingRepository.GetAllLocationAutoNumber(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public AutoNumber GetLocationAutoNumberById(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _autoNumberingRepository.GetLocationAutoNumberById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateLocationAutoNumber(AutoNumber model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _autoNumberValidation.Validate(AutoNumberValidation.ValidateAll_key, model, "AutoNumberValidation can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;

                var data = _autoNumberingRepository.UpdateLocationAutoNumber(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "AutoNumber-Master-Location" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
    }
}
