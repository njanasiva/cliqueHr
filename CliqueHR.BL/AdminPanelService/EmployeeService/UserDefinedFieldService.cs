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
    public class UserDefinedFieldService : IUserDefinedFieldService
    {
        private IUserDefinedFieldRepository _userDefinedFieldRepository;
        private UserDefineValidation _modelValidation;
        public UserDefinedFieldService()
        {
            _userDefinedFieldRepository = new UserDefinedFieldRepository();
            _modelValidation = new UserDefineValidation();
        }

        #region User Defined Service
        public List<FieldType> GetAllFieldType(UserContextModel objUser)
        {
            try
            {
                var data = _userDefinedFieldRepository.GetAllFieldType(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public FieldType GetFieldTypeById(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _userDefinedFieldRepository.GetFieldTypeById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<FieldValueMaster> GetAllFieldValue(UserContextModel objUser)
        {
            try
            {
                var data = _userDefinedFieldRepository.GetAllFieldValue(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<FieldValueMaster> GetFieldValueByTypeId(int TypeID, UserContextModel objUser)
        {
            try
            {
                var data = _userDefinedFieldRepository.GetFieldValueByTypeId(TypeID,objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<UserDefinedField> GetAllUserDefinedField(UserContextModel objUser)
        {
            try
            {
                var data = _userDefinedFieldRepository.GetAllUserDefinedField(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public UserDefinedField GetUserDefinedFieldById(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _userDefinedFieldRepository.GetUserDefinedFieldById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public ApplicationResponse UpdateUserDefinedField(List<UserDefinedField> model, UserContextModel objUser)
        {
            try
            {
                var validationRespons = _modelValidation.Validate(UserDefineValidation.ValidateAll_key, model, null);               
                if (validationRespons != null && validationRespons.Count > 0)
                {
                    throw new ValidationException(validationRespons);
                }
               

                model.ForEach(x => x.CreatedBy = objUser.EmployeeId);

                var data = _userDefinedFieldRepository.UpdateUserDefinedField(model, objUser.CompanyCode);
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
