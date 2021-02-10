using CliqueHR.BL.Implementation.AdminPanel.MastersService;
using CliqueHR.Common.Models;
using CliqueHR.DL.AdminPanel.Masters;
using CliqueHR.DL.Implementation.AdminPanel.Masters;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL.AdminPanelService.MastersService
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;
        private EmployeeTypeValidation _employeeTypeModelValidation;
        private BandTypeModelValidation _bandTypeModelValidation;
        private GradeTypeModelValidation _gradeTypeModelValidation;
        private CenterTypeValidation _centerTypeValidation;
        private CostCenterValidation _costCenterValidation;
        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
            _employeeTypeModelValidation = new EmployeeTypeValidation();
            _bandTypeModelValidation = new BandTypeModelValidation();
            _gradeTypeModelValidation = new GradeTypeModelValidation();
            _centerTypeValidation = new CenterTypeValidation();
            _costCenterValidation = new CostCenterValidation();
        }

        #region Employee Type
        public void AddEmployeeTypeData(EmployeeType model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _employeeTypeModelValidation.Validate(EmployeeTypeValidation.ValidateAll_key, model, "EmployeeModelValidation can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
              
                var data = _employeeRepository.AddEmployeeType(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "EmployeeType" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateEmployeeTypeData(EmployeeType model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _employeeTypeModelValidation.Validate(EmployeeTypeValidation.ValidateEditFields_key, model, "EmployeeModelValidation model can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data =  _employeeRepository.UpdateEmployeeType(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "EmployeeType" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public EmployeeType GetEmployeeTypeByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _employeeRepository.GetEmployeeTypeById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<EmployeeType> GetAllEmployeeType(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _employeeRepository.GetAllEmployeeType(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion

        #region Band Type

        public void AddBandTypeData(BandType model, UserContextModel objUser)
        {

            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _bandTypeModelValidation.Validate(BandTypeModelValidation.ValidateAll_key, model, "BandType model can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _employeeRepository.AddBandType(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "BandType" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateBandTypeData(BandType model, UserContextModel objUser)
        {

            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _bandTypeModelValidation.Validate(BandTypeModelValidation.ValidateEditFields_key, model, "BandType model can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _employeeRepository.UpdateBandType(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "BandType" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public BandType GetBandTypeByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _employeeRepository.GetBandTypeById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<BandType> GetAllBandType(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _employeeRepository.GetAllBandType(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }


        #endregion

        #region Grade Type

        public void AddGradeTypeData(GradeType model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse =  _gradeTypeModelValidation.Validate(GradeTypeModelValidation.ValidateAll_key, model, "GradeType model can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _employeeRepository.AddGradeType(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "GradeType" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateGradeTypeData(GradeType model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _gradeTypeModelValidation.Validate(GradeTypeModelValidation.ValidateEditFields_key, model, "GradeType model can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data =  _employeeRepository.UpdateGradeType(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "GradeType" });
                    throw new ValidationException(responseValidation);
                }

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public GradeType GetGradeTypeByID(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _employeeRepository.GetGradeTypeById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<GradeType> GetAllGradeType(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _employeeRepository.GetAllGradeType(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<GradeType> GetGradeList(UserContextModel objUser)
        {
            try
            {
                var data = _employeeRepository.GetGradeList(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion

        #region Center Type
        public void AddCenterTypeData(CenterTypeModel model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _centerTypeValidation.Validate(CenterTypeValidation.ValidateAll_key, model, "CenterType model can not be empty.");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _employeeRepository.AddCenterTypeData(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Center Type" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateCenterTypeData(CenterTypeModel model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _centerTypeValidation.Validate(CenterTypeValidation.ValidateAll_key_Id_Name, model, "CenterType model can not be empty");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _employeeRepository.UpdateCenterTypeData(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Center Type" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<CenterTypeModel> GetAllCenterType(PaginationModel paginationModel, UserContextModel objUser)
        {
            try
            {
                var data = _employeeRepository.GetAllCenterType(paginationModel, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public CenterTypeModel GetCenterTypeByID(int id, UserContextModel objUser)
        {
            var responseValidation = Validator.GetValidationResponseInstance();
            try
            {
                if (id == 0)
                {
                    responseValidation.Messages.Add(Validator.CreateValidationMessageInstance("CenterTypeModel", "Id cannot be blank!"));
                    throw new ValidationException(responseValidation);
                }
                var data = _employeeRepository.GetCenterTypeByID(id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<CenterTypeModel> GetCenterType(UserContextModel objUser)
        {
            
            try
            {               
                return _employeeRepository.GetCenterType(objUser.CompanyCode);
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        #endregion
        #region Cost Center Type
        public void AddCostCenter(CostCenterModel model, UserContextModel objUser)
        {
            var responseValidation = Validator.GetValidationResponseInstance();
            try
            {
                var validationResponse = _costCenterValidation.Validate(CostCenterValidation.ValidateAll_key, model, "Test");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _employeeRepository.AddCostCenter(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Cost Center Type" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateCostCenter(CostCenterModel model,UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _costCenterValidation.Validate(CostCenterValidation.ValidateAll_key_Id, model, "CostCenter model can not be empty");
                if (validationResponse.Messages != null)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _employeeRepository.UpdateCostCenter(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Center Type" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public CostCenterModel GetCostCenterByID(int id, UserContextModel objUser)
        {
            var responseValidation = Validator.GetValidationResponseInstance();
            try
            {
                if (id == 0)
                {
                    responseValidation.Messages.Add(Validator.CreateValidationMessageInstance("CenterTypeModel", "Id cannot be blank!"));
                    throw new ValidationException(responseValidation);
                }
                var data = _employeeRepository.GetCostCenterByID(id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<CostCenterModel> GetAllCostCenter(PaginationModel paginationModel, UserContextModel objUser)
        {
            try
            {
                var data = _employeeRepository.GetAllCostCenter(paginationModel, objUser.CompanyCode);
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
