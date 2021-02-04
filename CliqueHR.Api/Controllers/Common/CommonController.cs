using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CliqueHR.Api.Application;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;


namespace CliqueHR.Api.Controllers
{
    public class CommonController : ApiController
    {
        private ICommonService _CommonService;

        public CommonController()
        {
            _CommonService = new CommonService();
        }

        [HttpGet]
        public HttpResponseMessage GetAllCompanyType()
        {
            try
            {
                Log.Info("CommonController:GetAllCompanyType", "Get All Company Types  Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "devDB";
                var data = _CommonService.GetAllCompanyType(objUser);
                Log.Info("CommonController:GetAllCompanyType", "Get All Company Types End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAllCountry()
        {
            try
            {
                Log.Info("CommonController:GetAllCountry", "Get All Country Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "devDB";
                var data = _CommonService.GetAllCountry(objUser);
                Log.Info("CommonController:GetAllCountry", "Get All Country End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetAllState(int CountryId)
        {
            try
            {
                Log.Info("CommonController:GetAllState", "Get All State Start", string.Empty);

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "devDB";
                var data = _CommonService.GetAllState(CountryId, objUser);
                Log.Info("CommonController:GetAllState", "Get All State End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAllCity(int StateId)
        {
            try
            {
                Log.Info("CommonController:GetAllCity", "Get All City Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "devDB";
                var data = _CommonService.GetAllCity(StateId, objUser);
                Log.Info("CommonController:GetAllCity", "Get All City End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
    }
}
