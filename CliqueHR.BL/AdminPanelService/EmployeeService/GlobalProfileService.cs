using CliqueHR.BL.Implementation.AdminPanel.Employee;
using CliqueHR.Common.Models;
using CliqueHR.DL.AdminPanel.Employee;
using CliqueHR.DL.Implementation.AdminPanel.Employee;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.AdminPanelService.EmployeeService
{
    public class GlobalProfileService : IGlobalProfileService
    {
        private IGlobalProfileRepository _globalProfileRepository;
        private GlobalProfileValidation _modelValidation;
        private ProfileFieldSettingValidation _modelProfileValidation;
        public GlobalProfileService()
        {
            _globalProfileRepository = new GlobalProfileRepository();
            _modelProfileValidation = new ProfileFieldSettingValidation();
        }

        #region Global Profile

        public List<GlobalProfileModel> GetAllGlobalProfile(UserContextModel objUser)
        {
            try
            {
                var data = _globalProfileRepository.GetAllGlobalProfile(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public GlobalProfileModel GetGlobalProfileById(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _globalProfileRepository.GetGlobalProfileById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public ApplicationResponse UpdateGlobalProfile(List<GlobalProfileModel> model, UserContextModel objUser)
        {
            try
            {
                var validationRespons = _modelValidation.Validate(GlobalProfileValidation.ValidateAll_key, model, null);

                List<GlobalProfileModel> enableList = new List<GlobalProfileModel>();
                if (model != null && model.Count > 0)
                {
                    enableList = model.Where(x => x.IsEnable == true).ToList();
                    if (enableList.Count <= 0)
                    {
                        validationRespons.Add(
                   new ValidationResponse
                   {
                       Messages =
                           new List<ValidationMessage> {
                                new ValidationMessage { Message = "Select At least one entry", Property = "Selection" }
                           }
                   }
               );
                    }
                }

                if (validationRespons != null && validationRespons.Count > 0)
                {
                    throw new ValidationException(validationRespons);
                }
                model.ForEach(x => x.CreatedBy = objUser.EmployeeId);

                var data = _globalProfileRepository.UpdateGlobalProfile(model, objUser.CompanyCode);
                return data;

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        #endregion

        #region Prifile Setting

        public void SaveProfileSetting(ProfileFieldSetting model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelProfileValidation.Validate(ApprovalWorkflowValidation.ValidateAll_key, model, "ProfileFieldSetting can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;

                var data = _globalProfileRepository.SaveProfileFieldSetting(model, objUser.CompanyCode);
             

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        //public void UpdateProfileSetting(ProfileFieldSetting model, UserContextModel objUser)
        //{
        //    try
        //    {
        //        var responseValidation = Validator.GetValidationResponseInstance();
        //        var validationResponse = _modelProfileValidation.Validate(ProfileFieldSettingValidation.ValidateAll_key, model, "ProfileFieldSetting model can not be empty.");
        //        if (validationResponse.Messages != null)
        //        {
        //            throw new ValidationException(validationResponse);
        //        }
        //        model.CreatedBy = objUser.EmployeeId;
        //        var data = _globalProfileRepository.UpdateProfileFieldSetting(model, objUser.CompanyCode);
        //        if (data.Code == 2)
        //        {
        //            responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "ProfileFieldSetting" });
        //            throw new ValidationException(responseValidation);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        var helper = new BusinessException(ex);
        //        throw helper.GetException();
        //    }
        //}

        public ProfileFieldSetting GetProfileSettingById(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _globalProfileRepository.GetProfileFieldSettingById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public ProfileFieldSetting GetProfileSetting(UserContextModel objUser)
        {
            try
            {
                var data = _globalProfileRepository.GetProfileFieldSetting(objUser.CompanyCode);
                return data;
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
