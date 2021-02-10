using CliqueHR.Api.Application;
using CliqueHR.BL.AdminPanelService.EmployeeService;
using CliqueHR.BL.Implementation.AdminPanel.Employee;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CliqueHR.Api.Controllers.AdminPanel.Employee
{
    public class GlobalProfileController : ApiController
    {
        private IGlobalProfileService _globalProfileService;

        public GlobalProfileController()
        {
            _globalProfileService = new GlobalProfileService();
        }

      
        [HttpPost]
        public HttpResponseMessage UpdateGlobalProfile(List<GlobalProfileModel> model)
        {
            try
            {
                Log.Info("GlobalProfileController:UpdateGlobalProfile", "Update GlobalProfileModel START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "EmployeeDB";
                var data = _globalProfileService.UpdateGlobalProfile(model, objUser);
                Log.Info("GlobalProfileController:UpdateGlobalProfile", "Update GlobalProfileModel END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetAllGlobalProfile()
        {
            try
            {
                Log.Info("GlobalProfileController:GetAllGlobalProfile", "Get All GlobalProfileModel START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "EmployeeDB";
                var data = _globalProfileService.GetAllGlobalProfile(objUser);
                Log.Info("GlobalProfileController:GetAllGlobalProfile", "Get All GlobalProfileModel END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetGlobalProfileById(int Id)
        {
            try
            {
                Log.Info("GlobalProfileController:GetGlobalProfileById", "Get CourseType By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "EmployeeDB";
                var data = _globalProfileService.GetGlobalProfileById(Id, objUser);
                Log.Info("GlobalProfileController:GetGlobalProfileById", "Get CourseType By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }



        #region ProfileFieldSetting

        [HttpPost]
        public HttpResponseMessage SaveProfileFieldSetting(ProfileFieldSetting model)
        {
            try
            {

                Log.Info("GlobalProfileController:SaveProfileSetting", "Add SaveProfileSetting START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                 _globalProfileService.SaveProfileSetting(model, objUser);
                Log.Info("GlobalProfileController:SaveProfileSetting", "Add SaveProfileSetting END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data saved Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        //[HttpPost]
        //public HttpResponseMessage UpdateProfileFieldSetting(ProfileFieldSetting model)
        //{
        //    try
        //    {
        //        Log.Info("GlobalProfileController:UpdateProfileFieldSetting", "Update UpdateProfileFieldSetting START", string.Empty);
        //        UserContextModel objUser = new UserContextModel();
        //        objUser.EmployeeId = 1;
        //        objUser.CompanyCode = "DevDB";
        //         _globalProfileService.UpdateProfileSetting(model, objUser);
        //        Log.Info("GlobalProfileController:UpdateProfileFieldSetting", "Update UpdateProfileFieldSetting END", string.Empty);
        //        return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data updated Successfully!" });
        //    }
        //    catch (Exception ex)
        //    {
        //        var helper = new AppPresentationException(ex);
        //        return helper.GetResponse(this);
        //    }
        //}

        [HttpGet]
        public HttpResponseMessage GetProfileFieldSettingById(int Id)
        {
            try
            {
                Log.Info("GlobalProfileController:GetProfileFieldSettingById", "Get GetProfileFieldSettingById START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _globalProfileService.GetProfileSettingById(Id,objUser);
                Log.Info("GlobalProfileController:GetProfileFieldSettingById", "Get GetProfileFieldSettingById END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetProfileFieldSetting()
        {
            try
            {
                Log.Info("GlobalProfileController:GetProfileFieldSetting", "Get GetProfileFieldSetting START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _globalProfileService.GetProfileSetting(objUser);
                Log.Info("GlobalProfileController:GetProfileFieldSetting", "Get GetProfileFieldSetting END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }   

        #endregion


    }
}
