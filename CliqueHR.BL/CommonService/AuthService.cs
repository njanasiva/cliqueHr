using CliqueHR.BL.Application;
using CliqueHR.Common.Models;
using CliqueHR.DL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Logger;
using CliqueHR.Helpers.Validation;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace CliqueHR.BL
{
    public class AuthService : IAuthService
    {
        private IAuthRepository authRepository;
        private IPageSettingsRepository pageSettingsRepository;

        private const int defaultSystemTimeOutInMin = 10;

        public AuthService()
        {
            authRepository = new AuthRepository();
            pageSettingsRepository = new PageSettingsRepository();
        }
        public LoginInfo LoginUser(string CompanyCode, string EmployeeCode, string Password)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                if (string.IsNullOrEmpty(CompanyCode) || string.IsNullOrEmpty(EmployeeCode) || string.IsNullOrEmpty(Password))
                {
                    responseValidation.Messages.Add(new ValidationMessage { Property = "Validation", Message = "Invalid parameter" });
                    throw new ValidationException(responseValidation);
                }
                Password = Cryptography.Encrypt(Password, LoginEncryptionKey);
                var response = authRepository.CheckCompanyCodeExists(CompanyCode);
                if (response.Code != 1)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Property = "Validation", Message = response.Message });
                    throw new ValidationException(responseValidation);
                }
                var loginResponse = authRepository.LoginUser(CompanyCode, EmployeeCode, Password);
                if (loginResponse.Code != 1)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Property = "Validation", Message = loginResponse.Message });
                    throw new ValidationException(responseValidation);
                }
                if (loginResponse.Data.SystemTimeOut == 0)
                {
                    // set default system timeout value
                    loginResponse.Data.SystemTimeOut = defaultSystemTimeOutInMin;

                }
                // Get CacheConfig
                loginResponse.Data.CachingConfig = GetCacheConfig(loginResponse.Data.CompanyCode, loginResponse.Data.EmployeeId, loginResponse.Data.EntityId);
                return loginResponse.Data;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void AddRefreshToken(string companyCode, string token, object tokenTicket)
        {
            try
            {
                byte[] tokenByte = ObjectToByteArray(tokenTicket);
                authRepository.AddRefreshToken(companyCode, token, tokenByte);
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public void RemoveRefreshToken(string companyCode, string token)
        {
            try
            {
                if (!string.IsNullOrEmpty(token))
                {
                    authRepository.RemoveRefreshToken(companyCode, token);
                }
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public object GetRefreshTokenTicket(string companyCode, string token)
        {
            try
            {
                object ticket = null;
                var byteArr = authRepository.GetRefreshTokenTicket(companyCode, token);
                if (byteArr != null && byteArr.Length != 0)
                {
                    ticket = ByteArrayToObject(byteArr);
                }
                return ticket;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }
        public string GetCacheConfig(string companyCode, long employeeId, int entityId)
        {
            try
            {
                var cacheList = authRepository.GetCacheConfig(companyCode, employeeId, entityId);
                if (cacheList != null)
                {
                    return string.Join(",", cacheList);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        public LoginPageModel GetLoginPageDetails(string companyCode)
        {
            try
            {
                var responseValidation = Validator.GetValidationResponseInstance();
                LoginPageModel loginPageModel = null;
                var response = authRepository.CheckCompanyCodeExists(companyCode);
                if (response.Code != 1)
                {
                    responseValidation.Messages.Add(new ValidationMessage { Property = "Validation", Message = response.Message });
                    throw new ValidationException(responseValidation);
                }
                var data = pageSettingsRepository.GetPageSettingImages(companyCode);
                if(data != null)
                {
                    loginPageModel = new LoginPageModel();
                    var images = data.Where(x => x.ImageType == "Login").ToList();
                    if (images.Count > 0)
                    {
                        loginPageModel.loginPageImages = images;
                    }
                }
                return loginPageModel;
            }
            catch (Exception ex)
            {
                var helper = new BusinessException(ex);
                throw helper.GetException();
            }
        }

        private string LoginEncryptionKey
        {
            get
            {
                return Convert.ToString(ConfigurationManager.AppSettings["Login.EncryptionKey"]);
            }
        }

        // Convert an object to a byte array
        private byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        // Convert a byte array to an Object
        private object ByteArrayToObject(byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            object obj = (object)binForm.Deserialize(memStream);

            return obj;
        }
    }
}
