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
    public class EmployeeGroupController : ApiController
    {

        private IEmployeeGroupService _employeeGroupService;

        public EmployeeGroupController()
        {
            _employeeGroupService = new EmployeeGroupService();
        }


        [HttpPost]
        public HttpResponseMessage AddEmployeeGroup(EmployeeGroup model)
        {
            try
            {
                Log.Info("EmployeeGroupController:AddEmployeeGroup", "Add EmployeeGroup START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _employeeGroupService.AddEmployeeGroup(model, objUser);
                Log.Info("EmployeeGroupController:AddEmployeeGroup", "Add EmployeeGroup END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {

                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateEmployeeGroup(EmployeeGroup model)
        {
            try
            {
                Log.Info("EmployeeGroupController:UpdateEmployeeGroup", "Update EmployeeGroup START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _employeeGroupService.UpdateEmployeeGroup(model, objUser);
                Log.Info("EmployeeGroupController:UpdateEmployeeGroup", "Update EmployeeGroup END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetEmployeeGroupById(int Id, PaginationModel model)
        {
            try
            {
                Log.Info("EmployeeGroupController:GetEmployeeGroupById", "get EmployeeGroup by id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _employeeGroupService.GetEmployeeGroupById(Id, model, objUser);
                Log.Info("EmployeeGroupController:GetEmployeeGroupById", "get EmployeeGroup by id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllEmployeeGroup(PaginationModel model)
        {
            try
            {
                Log.Info("EmployeeGroupController:GetAllEmployeeGroup", "Get all EmployeeGroup START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _employeeGroupService.GetAllEmployeeGroup(model, objUser);
                Log.Info("EmployeeGroupController:GetAllEmployeeGroup", "Get all EmployeeGroup  END", string.Empty);
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
