using CliqueHR.Api.Application;
using CliqueHR.BL.AdminPanelService.MastersService;
using CliqueHR.BL.Implementation.AdminPanel.MastersService;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CliqueHR.Api.Controllers.AdminPanel.Masters
{
    public class AutoNumberController : ApiController
    {
        private IAutoNumberingService _iAutoNumberingService;
        public AutoNumberController()
        {
            _iAutoNumberingService = new AutoNumberingService();
        }

        [HttpPost]
        public HttpResponseMessage AddAutoNumber(AutoNumber autoNumberModel)
        {
            try
            {
                Log.Info("AutoNumberingController:AddAutoNumber", "AddAutoNumber START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _iAutoNumberingService.AddLocationAutoNumber(autoNumberModel, objUser);
                Log.Info("AutoNumberingController:AddAutoNumber", "AddAutoNumber END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateAutoNumber(AutoNumber autoNumberModel)
        {
            try
            {
                Log.Info("AutoNumberingController:UpdateAutoNumber", "UpdateAutoNumber START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _iAutoNumberingService.UpdateLocationAutoNumber(autoNumberModel, objUser);
                Log.Info("AutoNumberingController:UpdateAutoNumber", "UpdateAutoNumber END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data updated Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAutoNumberById(int Id)
        {
            try
            {
                Log.Info("AutoNumberingController:GetAutoNumberById", "Get Auto Number Type By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _iAutoNumberingService.GetLocationAutoNumberById(Id, objUser);
                Log.Info("AutoNumberingController:GetAutoNumberById", "Get Auto Number Type By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllAutoNumber(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("AutoNumberingController:GetAllAutoNumber", "Get All AutoNumber START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _iAutoNumberingService.GetAllLocationAutoNumber(objUser, paginationModel);
                Log.Info("EmployeeController:GetAllAutoNumber", "Get All AutoNumber END", string.Empty);
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
