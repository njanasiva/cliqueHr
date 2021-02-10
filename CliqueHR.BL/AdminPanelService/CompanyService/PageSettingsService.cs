using System;
using System.Collections.Generic;
using System.Web;
using CliqueHR.BL.CachingService;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using Newtonsoft.Json;
namespace CliqueHR.BL
{
    public class PageSettingsService : IPageSettingService
    {

        private IPageSettingsRepository _pagesettingsRepository;
        private IStorageService _storageService;

        public PageSettingsService()
        {
            _pagesettingsRepository = new PageSettingsRepository();
            _storageService = new FileStorageService();
        }
        public void AddUpdatePageSettings(PageSettings model, UserContextModel objUser)
        {
            try
            {
                model.TransType = "SAVE";
                model.CreatedBy = objUser.EmployeeId;
                _pagesettingsRepository.AddUpdatePageSettings(model, objUser.CompanyCode);

            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PageSettings GetPageSettings(UserContextModel objUser)
        {
            try
            {
                PageSettings model = new PageSettings();
                model.TransType = "FETCH";
                var data = _pagesettingsRepository.GetPageSettings(model, objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public List<PageSettingImages> GetPageSettingImages(UserContextModel objUser)
        {
            try
            {
                var data = _pagesettingsRepository.GetPageSettingImages(objUser.CompanyCode);
                return data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public void DeletePageSettingImage(PageSettingImages model, UserContextModel objUser)
        {
            try
            {
                model.CreatedBy = objUser.EmployeeId;
                var OldImage = _pagesettingsRepository.DeleteImage(model.Id, objUser.CompanyCode);
                if (!string.IsNullOrEmpty(OldImage))
                    _storageService.DeleteFileData(OldImage, false, objUser.CompanyCode);
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public PageSettingImages AddUpdatePageSettingImages(HttpRequest request, UserContextModel objUser)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                string modelStr = request.Form["data"];
                if (string.IsNullOrEmpty(modelStr))
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = "Data can not be null", Property = "PageSettings" });
                    throw new ValidationException(responseValidation);
                }
                var model = JsonConvert.DeserializeObject<PageSettingImages>(modelStr);
                model.CreatedBy = objUser.EmployeeId;
                StorageRequest uploadRequest = null;
                if (request.Files.Count != 0 && request.Files["file"] != null)
                {
                    uploadRequest = new StorageRequest
                    {
                        ContainerName = objUser.CompanyCode,
                        FileName = request.Files["file"].FileName,
                        FolderName = new string[] { "PageSettings", "BackgroundImage" },
                        FileStream = request.Files["file"].InputStream
                    };
                    model.ImagePath = Utility.GetFolderPath("PageSettings", "BackgroundImage", uploadRequest.FileName);
                }

                var data = _pagesettingsRepository.AddUpdatePageSettingImages(model, objUser.CompanyCode);
                if (data.Code == 2)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Message = data.Message, Property = "PageSettings" });
                    throw new ValidationException(responseValidation);
                }
                if (uploadRequest != null)
                {
                    UploadFileResponse responseUpload = new UploadFileResponse();
                    responseUpload = _storageService.UploadFile(uploadRequest);
                    model.ImagePath = responseUpload.FilePath + responseUpload.FileName;
                    _pagesettingsRepository.AddUpdatePageSettingImages(model, objUser.CompanyCode);
                }

                // update cache
                CompanyCaching.Instance.UpdateCacheTimestamp(new CompanyModelParam
                {
                    Company = objUser.CompanyCode,
                    Key = model.Id.ToString(),
                    pageSettingImages = model
                });

                return new PageSettingImages
                {
                    ImageType = model.ImageType,
                    ImagePath = objUser.CompanyCode + model.ImagePath,
                    Id = model.Id,
                    CreatedBy = model.CreatedBy
                };
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
    }
}
