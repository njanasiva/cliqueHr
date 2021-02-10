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
    public class OrgUnitsController : ApiController
    {
        private IOrgUnitsService _OrgUnitsService;

        public OrgUnitsController()
        {
            _OrgUnitsService = new OrgUnitsService();
        }

        [HttpPost]
        public HttpResponseMessage AddUpdateOrgUnits(OrgUnits model)
        {
            try
            {
                Log.Info("OrgUnitsController:AddOrgUnits", "Add OrgUnits Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _OrgUnitsService.AddUpdateOrgUnits(model, objUser);
                Log.Info("OrgUnitsController:AddOrgUnits", "Add OrgUnits  End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetOrgUnits(PaginationModel model)
        {
            try
            {
                Log.Info("OrgUnitsController:GetOrgUnits", "Get All OrgUnits  Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _OrgUnitsService.GetOrgUnits(model, objUser);
                Log.Info("GroupCompanyController:GetGroupCompany", "Get All OrgUnits End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetOrgUnitsById(int Id)
        {
            try
            {
                Log.Info("OrgUnitsController:GetOrgUnitsById", "Get  OrgUnits By Id  Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _OrgUnitsService.GetOrgUnitsById(Id, objUser);
                Log.Info("OrgUnitsController:GetOrgUnitsById", "Get  OrgUnits By Id  End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEntityOrgunitTree(int? OrUnitId)
        {
            try
            {
                Log.Info("OrgUnitController:GetEntityOrgunitTree", "Get EntityOrgunit Tree Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _OrgUnitsService.GetEntityOrgunitTreeData(objUser, OrUnitId);
                Log.Info("OrgUnitController:GetParentOrgUnits", "Get EntityOrgunit Tree  End", string.Empty);
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
