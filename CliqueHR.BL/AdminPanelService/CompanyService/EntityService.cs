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
    public class EntityService : IEntityService
    {
        private IEntityRepository _entityRepository;
        private EntityValidation _modelValidation;
        private IStorageService _storageService;
        public EntityService()
        {
            _entityRepository = new EntityRepository();
            _modelValidation = new EntityValidation();
            _storageService = new FileStorageService();
        }

        public void AddEntity(HttpRequest request, UserContextModel objUser)
        {
            try
            {
<<<<<<< HEAD
=======

>>>>>>> change
                var responseValidation = Validator.GetValidationResponseInstance();
                string modelStr = request.Form["data"];
                if (string.IsNullOrEmpty(modelStr))
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = "Data can not be null", Property = "Entity" });
                    throw new ValidationException(responseValidation);
                }
                var model = JsonConvert.DeserializeObject<Entity>(modelStr);
                var validationResponse = _modelValidation.Validate(EntityValidation.ValidateAll_key, model, "Entity model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.TransType = "SAVE";
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                StorageRequest uploadRequest = null;
                if (request.Files.Count != 0 && request.Files["file"] != null)
                {
                    uploadRequest = new StorageRequest
                    {
                        ContainerName = objUser.CompanyCode,
                        FileName = request.Files["file"].FileName,
                        FolderName = new string[] { "Entity", "Logo" },
                        FileStream = request.Files["file"].InputStream
                    };
                    model.Logo = Utility.GetFolderPath("Entity", "Logo", uploadRequest.FileName);
                }

                var data = _entityRepository.AddEntity(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Entity" });
                    throw new ValidationException(responseValidation);
                }
                if (uploadRequest != null)
                {
                    UploadFileResponse responseUpload = new UploadFileResponse();
                    responseUpload = _storageService.UploadFile(uploadRequest);
                    model.Logo = responseUpload.FilePath + responseUpload.FileName;
                    

                    var OldLogo = _entityRepository.UpdateLogo(model.Id, model.Logo, objUser.CompanyCode);
                    if (!string.IsNullOrEmpty(OldLogo))
                        _storageService.DeleteFileData(OldLogo, false, objUser.CompanyCode);
                }
<<<<<<< HEAD
=======

>>>>>>> change
               
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void UpdateEntity(HttpRequest request, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                string modelStr = request.Form["data"];
                if (string.IsNullOrEmpty(modelStr))
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = "Data can not be null", Property = "Entity" });
                    throw new ValidationException(responseValidation);
                }
                var model = JsonConvert.DeserializeObject<Entity>(modelStr);
                var validationResponse = _modelValidation.Validate(EntityValidation.ValidateAll_key, model, "Entity model can not be empty.");
                if (validationResponse.Messages != null && validationResponse.Messages.Count != 0)
                {
                    throw new ValidationException(validationResponse);
                }
                model.TransType = "SAVE";
<<<<<<< HEAD
                model.CreatedBy = objUser.EmployeeId;
=======
                model.CreatedBy = objUser.UserID;
>>>>>>> change
                var data = _entityRepository.UpdateEntity(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "Entity" });
                    throw new ValidationException(responseValidation);
                }

                StorageRequest uploadRequest = null;
                if (request.Files.Count != 0 && request.Files["file"] != null)
                {
                    uploadRequest = new StorageRequest
                    {
                        ContainerName = objUser.CompanyCode,
                        FileName = request.Files["file"].FileName,
                        FolderName = new string[] { "Entity", "Logo" },
                        FileStream = request.Files["file"].InputStream
                    };
                    model.Logo = Utility.GetFolderPath("Entity", "Logo", uploadRequest.FileName);
                }

               
                if (uploadRequest != null)
                {
                    UploadFileResponse responseUpload = new UploadFileResponse();
                    responseUpload = _storageService.UploadFile(uploadRequest);
                    model.Logo = responseUpload.FilePath + responseUpload.FileName;


                    var OldLogo = _entityRepository.UpdateLogo(model.Id, model.Logo, objUser.CompanyCode);
                    if (!string.IsNullOrEmpty(OldLogo))
                        _storageService.DeleteFileData(OldLogo, false, objUser.CompanyCode);

                }

<<<<<<< HEAD
                // Update Cache
                EntityCaching.Instance.UpdateCacheTimestamp(new EntityModelParams
                {
                    Company = objUser.CompanyCode,
                    entity = model,
                    Key = model.Id.ToString()
                });
=======
             


>>>>>>> change
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public PaginationData<Entity> GetEntity(PaginationModel model, UserContextModel objUser)
        {
            try
            {
                var data = _entityRepository.GetEntity(model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public Entity GetEntityById(int Id, UserContextModel objUser)
        {
            try
            {

                var data = _entityRepository.GetEntityById(Id, objUser.CompanyCode);
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
