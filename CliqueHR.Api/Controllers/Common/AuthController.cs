using CliqueHR.Api.Application;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CliqueHR.Api.Controllers
{
    public class AuthController : ApiController
    {

        private IAuthService authService;

        public AuthController()
        {
            authService = new AuthService();
        }

        [HttpGet]
        public HttpResponseMessage GetLoginPageDetails(string companyCode)
        {
            try
            {
                Log.Info("AuthController:GetLoginPageDetails", "GetLoginPageDetails Start", string.Empty);;
                var data = authService.GetLoginPageDetails(companyCode);
                Log.Info("AuthController:GetPageSettingImages", "GetLoginPageDetails End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        [AccessFilter]
        public HttpResponseMessage Logout(string token)
        {
            try
            {
                UserContextModel objUser = Request.GetUserLoginContext();
                Log.Info("AuthController:Logout", "Logout Start", string.Empty); ;
                authService.RemoveRefreshToken(objUser.CompanyCode, token);
                Log.Info("AuthController:Logout", "Logout End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
    }
}
