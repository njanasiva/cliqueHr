using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;

namespace CliqueHR.BL
{
    public class MasterService : IMasterService
    {
        private IMasterRepository _masterRepository;
        private CurrencyMappingValidation _modelValidation;
        private RegionModelValidator _regionValidator;
        private DesignationValidation _designationValidation;
        private FunctionalRoleValidation _functionalRoleValidator;
        private LocationValidation _locationValidator;
        private IStorageService _storageService;
        private DesiAutoNumberingValidation _desiAutoNumbering;
        public MasterService()
        {
            _masterRepository = new MasterRepository();
            _modelValidation = new CurrencyMappingValidation();
            _regionValidator = new RegionModelValidator();
            _functionalRoleValidator = new FunctionalRoleValidation();
            _storageService = new FileStorageService();
            _locationValidator = new LocationValidation();
            _designationValidation = new DesignationValidation();
            _desiAutoNumbering = new DesiAutoNumberingValidation();
        }

        #region Currency
        public void AddCurrencyMapping(CurrancyMapping model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(CurrencyMappingValidation.ValidateAll_key, model, "Currency mapping model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.AddCurrencyMapping(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Currency" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<CurrancyMapping> GetAllCurrencyMapping(UserContextModel objUser, PaginationModel paginationModel)
        {
            try
            {
                var data = _masterRepository.GetAllCurrencyMapping(objUser.CompanyCode, paginationModel);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public CurrancyMapping GetCurrencyMappingById(int ID, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetCurrencyMappingById(ID, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateCurrencyMapping(CurrancyMapping model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _modelValidation.Validate(CourseTypeValidation.ValidateEditFields_key, model, "CurrancyMapping model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.UpdateCurrencyMapping(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "CurrancyMapping" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public List<Currancy> GetAllCurrency(UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetAllCurrency(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion
        
        #region Region code
        public void AddRegion(RegionModel model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _regionValidator.Validate(RegionModelValidator.ValidateAll_key, model, "Region model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.AddRegion(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Region" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateRegion(RegionModel model, UserContextModel objUser)
        {
                var responseValidation = Validator.GetValidationResponseInstance();
            try
            {
                var validationResponse = _regionValidator.Validate(RegionModelValidator.ValidateId_key, model, "Region model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.UpdateRegion(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Region" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public RegionModel GetRegionById(int Id, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetRegionById(Id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<RegionModel> GetAllRegionData(PaginationModel model, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetAllRegionData(model,objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion
        #region Designation
        public void AddDesignation(DesignationModel model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _designationValidation.Validate(DesignationValidation.ValidateAll_key, model, "Designation model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.AddDesignation(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Designation" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateDesignation(DesignationModel model, UserContextModel objUser)
        {
            var responseValidation = Validator.GetValidationResponseInstance();
            try
            {
                var validationResponse = _designationValidation.Validate(DesignationValidation.ValidateAll_key, model, "Designation model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.UpdateDesignation(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Region" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DesignationResponseModel GetDesignationById(int Id, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetDesignationById(Id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<DesignationResponseModel> GetAllDesignation(PaginationModel model, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetAllDesignation(model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion

        #region Functional Role 
        public void AddFunctionalRole(HttpRequest request, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                string modelStr = request.Form["data"];
                if (string.IsNullOrEmpty(modelStr))
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = "Data can not be null", Property = "FunctionalRole" });
                    throw new ValidationException(responseValidation);
                }
                var model = JsonConvert.DeserializeObject<FunctionalRole>(modelStr);
                var validationResponse = _functionalRoleValidator.Validate(FunctionalRoleValidation.ValidateAll_key, model, "Functional Role model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                StorageRequest uploadRequest = null;
                if (request.Files.Count != 0 && request.Files["file"] != null && request.Files[0].ContentLength > 0)
                {
                    string strfileExtn = ".pdf";
                    var constructorInfo = typeof(HttpPostedFile).GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)[0];
                    var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { request.Files[0].FileName.ToLower(), request.Files[0].ContentType, request.Files[0].InputStream });
                    string validateHTMLFile = FileValidators.ValidateHTMLFile(obj, strfileExtn);
                    if (validateHTMLFile.ToLower() != "correct")
                    {
                        responseValidation.Messages.Add(new ValidationMessage { Message = "Invalid File Type", Property = "FunctionalRole" });
                        throw new ValidationException(responseValidation);
                    }
                    uploadRequest = new StorageRequest
                    {
                        ContainerName = objUser.CompanyCode,
                        FileName = request.Files["file"].FileName,
                        FolderName = new string[] { "Master", "FunctionalRole" },
                        FileStream = request.Files["file"].InputStream
                    };
                    model.AttachmentFile = Utility.GetFolderPath("Master", "FunctionalRole", uploadRequest.FileName);
                }
                model.CreatedBy = objUser.EmployeeId;             
                var data = _masterRepository.AddFunctionalRole(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "FunctionalRole" });
                    throw new ValidationException(responseValidation);
                }
                
                if (uploadRequest != null)
                {
                    UploadFileResponse responseUpload = new UploadFileResponse();
                    responseUpload = _storageService.UploadFile(uploadRequest);
                    model.AttachmentFile = responseUpload.FilePath + responseUpload.FileName;

                    var OldAttachment = _masterRepository.UpdateFunctRoleAttachment(model.Id, model.AttachmentFile, model.CreatedBy, objUser.CompanyCode);
                    if (!string.IsNullOrEmpty(OldAttachment))
                        _storageService.DeleteFileData(OldAttachment, false, objUser.CompanyCode);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateFunctionalRole(HttpRequest request, UserContextModel objUser)
        {         
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                string modelStr = request.Form["data"];
                if (string.IsNullOrEmpty(modelStr))
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = "Data can not be null", Property = "FunctionalRole" });
                    throw new ValidationException(responseValidation);
                }
                var model = JsonConvert.DeserializeObject<FunctionalRole>(modelStr);
                var validationResponse = _functionalRoleValidator.Validate(FunctionalRoleValidation.ValidateEditFields_key, model, "FunctionalRole model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                StorageRequest uploadRequest = null;
                if (request.Files.Count != 0 && request.Files["file"] != null && request.Files[0].ContentLength > 0)
                {
                    string strfileExtn = ".jpg,.jpeg,.pdf,.doc,.docx";
                    var constructorInfo = typeof(HttpPostedFile).GetConstructors(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)[0];
                    var obj = (HttpPostedFile)constructorInfo.Invoke(new object[] { request.Files[0].FileName.ToLower(), request.Files[0].ContentType, request.Files[0].InputStream });
                    string validateHTMLFile = FileValidators.ValidateHTMLFile(obj, strfileExtn);
                    if (validateHTMLFile.ToLower() != "correct")
                    {
                        responseValidation.Messages.Add(new ValidationMessage { Message = "Invalid File Type", Property = "FunctionalRole" });
                        throw new ValidationException(responseValidation);
                    }
                    uploadRequest = new StorageRequest
                    {
                        ContainerName = objUser.CompanyCode,
                        FileName = request.Files["file"].FileName,
                        FolderName = new string[] { "Master", "FunctionalRole" },
                        FileStream = request.Files["file"].InputStream
                    };
                    model.AttachmentFile = Utility.GetFolderPath("Master", "FunctionalRole", uploadRequest.FileName);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.UpdateFunctionalRole(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "FunctionalRole" });
                    throw new ValidationException(responseValidation);
                }
                
                if (uploadRequest != null)
                {
                    UploadFileResponse responseUpload = new UploadFileResponse();
                    responseUpload = _storageService.UploadFile(uploadRequest);
                    model.AttachmentFile = responseUpload.FilePath + responseUpload.FileName;

                    var OldAttachment = _masterRepository.UpdateFunctRoleAttachment(model.Id, model.AttachmentFile, model.CreatedBy, objUser.CompanyCode);
                    if (!string.IsNullOrEmpty(OldAttachment))
                        _storageService.DeleteFileData(OldAttachment, false, objUser.CompanyCode);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public FunctionalRole GetFunctionalRoleById(int Id, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetFunctionalRoleById(Id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<FunctionalRole> GetAllFunctionalRole(PaginationModel model, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetAllFunctionalRole(model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion

        #region Location
        public void AddLocation(Location model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _locationValidator.Validate(LocationValidation.ValidateAll_key, model, "Location model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.AddLocation(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Location" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateLocation(Location model, UserContextModel objUser)
        {
            var responseValidation = Validator.GetValidationResponseInstance();
            try
            {
                var validationResponse = _locationValidator.Validate(LocationValidation.ValidateEditFields_key, model, "Location model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.UpdateLocation(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Location" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public Location GetLocationById(int Id, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetLocationById(Id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<Location> GetAllLocationData(PaginationModel model, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetAllLocation(model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public List<Location> GetLocationList(UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetLocationList(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public string GetLocationCode(UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var data = _masterRepository.GetLocationCode(objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Location" });
                    throw new ValidationException(responseValidation);
                }
                return data.Message;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        #endregion
        #region Designation Auto Numbering
        public void AddDesignationAutoNum(DesiAutoNumberingModel model, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                var validationResponse = _desiAutoNumbering.Validate(DesiAutoNumberingValidation.ValidateAll_key, model, "Designation Auto Numbering model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.AddDesignationAutoNum(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Designation" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void UpdateDesignationAutoNum(DesiAutoNumberingModel model, UserContextModel objUser)
        {
            var responseValidation = Validator.GetValidationResponseInstance();
            try
            {
                var validationResponse = _desiAutoNumbering.Validate(DesiAutoNumberingValidation.ValidateAllWithId_key, model, "Designation Auto number model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.CreatedBy = objUser.EmployeeId;
                var data = _masterRepository.UpdateDesignationAutoNum(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Designation" });
                    throw new ValidationException(responseValidation);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public DesiAutoNumberingByidModel GetDesignationAutoNumById(int Id, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetDesignationAutoNumById(Id, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PaginationData<DesignationResponseModel> GetAllDesignationAutoNum(PaginationModel model, UserContextModel objUser)
        {
            try
            {
                var data = _masterRepository.GetAllDesignationAutoNum(model, objUser.CompanyCode);
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
