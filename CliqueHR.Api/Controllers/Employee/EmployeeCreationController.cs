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

namespace CliqueHR.Api.Controllers.Employee
{
    public class EmployeeCreationController : ApiController
    {
        private IEmployeeCreationService _employeeCreationService;

        public EmployeeCreationController()
        {
            _employeeCreationService = new EmployeeCreationService();
        }

        [HttpPost]
        public HttpResponseMessage GetAllEmployees(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("EmployeeCreationController:GetAllEmployees", "GetAllEmployees START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeCreationService.GetAllEmployees(objUser,paginationModel);
                Log.Info("EmployeeCreationController:GetAllEmployees", "GetAllEmployees END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetEmployeeMultiselect(EmployeeFilter employeeFilter)
        {
            try
            {
                Log.Info("EmployeeCreationController:GetEmployeeMultiselect", "GetEmployeeMultiselect START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeCreationService.GetEmployeeMultiselect(objUser,employeeFilter);
                Log.Info("EmployeeCreationController:GetEmployeeMultiselect", "GetEmployeeMultiselect END", string.Empty);
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