using CliqueHR.Api.Application;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CliqueHR.Api.Controllers
{
    public class PageSettingsController : ApiController
    {
        private IPageSettingService _pageSettingService;

        public PageSettingsController()
        {
            _pageSettingService = new PageSettingsService();
        }

        [HttpPost]
        public HttpResponseMessage AddUpdatePageSettings(PageSettings model)
        {
            try
            {
                Log.Info("PageSettingsController:AddUpdatePageSettings", "Add Page Settings Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _pageSettingService.AddUpdatePageSettings(model, objUser);
                Log.Info("PageSettingsController:AddUpdateSecuritySettings", "Add Page Settings End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddUpdatePageSettingImages()
        {
            try
            {
                Log.Info("PageSettingsController:AddUpdatePageSettings", "Add Page Settings Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _pageSettingService.AddUpdatePageSettingImages(HttpContext.Current.Request, objUser);
                Log.Info("PageSettingsController:AddUpdateSecuritySettings", "Add Page Settings End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage DeletePageSettingImages(PageSettingImages model)
        {
            try
            {
                Log.Info("PageSettingsController:DeletePageSettingImages", "Delete Page Settings Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _pageSettingService.DeletePageSettingImage(model, objUser);
                Log.Info("PageSettingsController:DeletePageSettingImages", "Delete Page Settings End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetPageSettings()
        {
            try
            {
                Log.Info("PageSettingsController:GetPageSettings", "Get Page Settings Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _pageSettingService.GetPageSettings(objUser);
                Log.Info("PageSettingsController:GetPageSettings", "Get Page Settings End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetPageSettingImages()
        {
            try
            {
                Log.Info("PageSettingsController:GetPageSettingImages", "Get Page Setting Images Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _pageSettingService.GetPageSettingImages(objUser);
                Log.Info("PageSettingsController:GetPageSettingImages", "Get Page Setting Images End", string.Empty);
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
