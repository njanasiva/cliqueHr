using CliqueHR.Api.Application;
using CliqueHR.BL.AdminPanelService.SetupService;
using CliqueHR.BL.Implementation.AdminPanel.SetupService;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CliqueHR.Api.Controllers.AdminPanel.Setup
{
    [RoutePrefix("api/role")]
    public class RoleController : ApiController
    {
        private ISetupService _setupService;
            
        public RoleController()
        {
            _setupService = new SetupService();
        }


        #region Role Creation
        [Route("modules/{roleid}")]
        [HttpGet]
        public HttpResponseMessage GetAllModule(int roleid)
        {
            try
            {
                Log.Info("RoleController:GetAllModule", "Get All Module START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetAllModule(roleid,objUser);
                Log.Info("RoleController:GetAllModule", "Get All Module END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        [Route("rolelist")]
        public HttpResponseMessage GetRoleList(PaginationModel param)
        {
            try
            {
                Log.Info("RoleController:GetRoleList", "Fetch GetRoleList START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetRoleList(param, objUser);
                Log.Info("RoleController:GetRoleList", "Fetch GetRoleList END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        [Route("roles")]
        public HttpResponseMessage GetAllRole()
        {
            try
            {
                Log.Info("RoleController:GetAllRole", "Get All Role START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetAllRole(objUser);
                Log.Info("RoleController:GetAllRole", "Get All Role END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        [Route("modules/{id}/{roleid}")]
        public HttpResponseMessage GetModuleById(int Id,int roleid)
        {
            try
            {
                Log.Info("RoleController:GetModuleById", "Get Module By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetModuleById(roleid,Id, objUser);
                Log.Info("RoleController:GetModuleById", "Get Module By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        [Route("roles/{id}")]
        public HttpResponseMessage GetRoleById(int Id)
        {
            try
            {
                Log.Info("RoleController:GetRoleById", "Get Role By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetRoleById(Id, objUser);
                var modules = _setupService.GetAllModule(data.Id, objUser);
                string strModule = string.Join(", ", from item in modules select item.Id);
                var submodule = _setupService.GetSubModuleByModuleId(data.Id, strModule, objUser);
                var submodulefield = _setupService.GetSubModuleFieldByModuleId(data.Id, strModule, objUser);
                CreateRolePostModel objRole = new CreateRolePostModel();
                objRole.RoleId = data.Id;
                objRole.RoleName = data.Name;
                objRole.objModule = modules;
                objRole.objSubModule = submodule;
                objRole.objSubModuleFields = submodulefield;
                Log.Info("RoleController:GetRoleById", "Get Role By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, objRole);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        [Route("submodules/{id}/{roleid}")]
        public HttpResponseMessage GetSubModuleByModuleId(string Id, int roleid)
        {
            try
            {
                Log.Info("RoleController:GetSubModuleByModuleId", "Get List of SubModule By  Module Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetSubModuleByModuleId(roleid,Id, objUser);
                Log.Info("RoleController:GetSubModuleByModuleId", "Get List of SubModule By  Module Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        [Route("submodulefield/{moduleid}/{roleid}")]
        public HttpResponseMessage GetSubModuleFieldByModuleId(string moduleid,int roleid)
        {
            try
            {
                Log.Info("RoleController:GetSubModuleByModuleId", "Get List of SubModule By  Module Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetSubModuleFieldByModuleId(roleid,moduleid, objUser);
                Log.Info("RoleController:GetSubModuleByModuleId", "Get List of SubModule By  Module Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        [Route("role")]
        public HttpResponseMessage RoleCreation(CreateRolePostModel model)
        {
            try
            {
                Log.Info("RoleController:RoleCreation", "Update CreateRolePostModel START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.RoleCreation(model, objUser);
                Log.Info("RoleController:RoleCreation", "Update CreateRolePostModel END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        [Route("assignrole")]
        public HttpResponseMessage AssignRole(AssignRole model)
        {
            try
            {
                Log.Info("RoleController:AssignRole", "Update AssignRole START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.AssignRole(model, objUser);
                Log.Info("RoleController:AssignRole", "Update AssignRole END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        [Route("assignrole/{assignId}")]
        public HttpResponseMessage GetAssignRoleById(int assignId)
        {
            try
            {
                Log.Info("RoleController:GetAssignRoleById", "Get object of assign role By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetAssignRoleById(assignId, objUser);
                Log.Info("RoleController:GetAssignRoleById", "Get object of assign role By Id  END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        [Route("assignrolelist")]
        public HttpResponseMessage GetAssignRoleList(PaginationModel param)
        {
            try
            {
                Log.Info("RoleController:GetAssignRoleList", "Fetch GetAssignRoleList START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _setupService.GetAssignRoleList(param, objUser);
                Log.Info("RoleController:GetAssignRoleList", "Fetch GetAssignRoleList END", string.Empty);
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
