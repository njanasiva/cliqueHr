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
    public class DepartmentController : ApiController
    {
        private IDepartmentService _DepartmentService;

        public DepartmentController()
        {
            _DepartmentService = new DepartmentService();
        }

        [HttpPost]
        public HttpResponseMessage AddUpdateDepartment(Department model)
        {
            try
            {
                Log.Info("DepartmentController:AddUpdateDepartment", "Add Update Department Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _DepartmentService.AddUpdateDepartment(model, objUser);
                Log.Info("DepartmentController:AddUpdateDepartment", "Add Update Department  End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetDepartments(PaginationModel model)
        {
            try
            {
                Log.Info("DepartmentController:GetDepartment", "Get All Departments  Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _DepartmentService.GetDepartments(model, objUser);
                Log.Info("GroupCompanyController:GetGroupCompany", "Get All Departments End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetDepartmentById(int Id)
        {
            try
            {
                Log.Info("DepartmentController:GetDepartmentById", "Get  Departments By Id  Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _DepartmentService.GetDepartmentById(Id, objUser);
                Log.Info("DepartmentController:GetDepartmentById", "Get  Departments By Id  End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEntityOrgunitDeptTree(int? DepartmentId)
        {
            try
            {
                Log.Info("DepartmentController:GetEntityOrgunitDeptTree", "Get EntityOrgunit Department Tree Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _DepartmentService.GetEntityOrgunitDeptTreeData(objUser, DepartmentId);
                Log.Info("DepartmentController:GetEntityOrgunitDeptTree", "Get EntityOrgunit Department Tree  End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetDepartmentCode()
        {
            try
            {
                Log.Info("DepartmentController:GetDepartmentCode", "Get GetDepartment Code Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _DepartmentService.GetDeptCode(objUser);
                Log.Info("DepartmentController:GetDepartmentCode", "Get  GetDepartment Code  End", string.Empty);
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
