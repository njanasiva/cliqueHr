using CliqueHR.Api.Application;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CliqueHR.Api.Controllers.AdminPanel.Masters
{
    public class MasterController : ApiController
    {
        private IMasterService _masterService;
        public MasterController()
        {
            _masterService = new MasterService();
        }

        #region Currency mapping
        [HttpPost]
        public HttpResponseMessage AddCurrencyMapping(CurrancyMapping model)
        {
            try
            {
                Log.Info("MasterController:AddCurrencyMapping", "Add Currency mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.AddCurrencyMapping(model, objUser);
                Log.Info("MasterController:AddCurrencyMapping", "Add Currency mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateCurrencyMapping(CurrancyMapping model)
        {
            try
            {
                Log.Info("MasterController:UpdateCurrencyMapping", "Update currency mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.UpdateCurrencyMapping(model, objUser);
                Log.Info("MasterController:UpdateCurrencyMapping", "Update currency mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllCurrencyMapping(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("MasterController:GetAllCurrencyMapping", "Get all currency mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetAllCurrencyMapping(objUser, paginationModel);
                Log.Info("MasterController:GetAllCurrencyMapping", "Get all currency mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetCurrencyMappingById(int Id)
        {
            try
            {
                Log.Info("MasterController:GetCurrencyMappingById", "Update Currency mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetCurrencyMappingById(Id, objUser);
                Log.Info("MasterController:GetCurrencyMappingById", "Update Currency mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetAllCurrency()
        {
            try
            {
                Log.Info("MasterController:GetAllCurrency", "Get All Currency START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetAllCurrency(objUser);
                Log.Info("MasterController:GetAllCurrency", "Get All Currency END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        #endregion

        #region region code
        [HttpPost]
        public HttpResponseMessage AddRegion(RegionModel model)
        {
            try
            {
                Log.Info("MasterController:AddRegion", "Add Region mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.AddRegion(model, objUser);
                Log.Info("MasterController:AddRegion", "Add region mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateRegion(RegionModel model)
        {
            try
            {
                Log.Info("MasterController:UpdateRegion", "Update Region mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.UpdateRegion(model, objUser);
                Log.Info("MasterController:UpdateRegion", "Update Region mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetRegionById(int Id)
        {
            try
            {
                Log.Info("MasterController:GetRegionById", "get Region START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetRegionById(Id, objUser);
                Log.Info("MasterController:GetRegionById", "get Region END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllRegionData(PaginationModel model)
        {
            try
            {
                Log.Info("MasterController:GetAllCurrencyMapping", "Get all currency mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetAllRegionData(model, objUser);
                Log.Info("MasterController:GetAllCurrencyMapping", "Get all currency mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        #endregion

        #region Function Role 
        [HttpPost]
        public HttpResponseMessage AddFunctionalRole()
        {
            try
            {
                Log.Info("MasterController:AddFunctionalRole", "Add FunctionalRole mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.AddFunctionalRole(HttpContext.Current.Request, objUser);
                Log.Info("MasterController:AddFunctionalRole", "Add FunctionalRole mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateFunctionalRole()
        {
            try
            {
                Log.Info("MasterController:UpdateFunctionalRole", "Update FunctionalRole mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.UpdateFunctionalRole(HttpContext.Current.Request, objUser);
                Log.Info("MasterController:UpdateFunctionalRole", "Update FunctionalRole mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetFunctionalRoleById(int Id)
        {
            try
            {
                Log.Info("MasterController:GetFunctionalRoleById", "get FunctionalRole START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetFunctionalRoleById(Id, objUser);
                Log.Info("MasterController:GetFunctionalRoleById", "get FunctionalRole END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllFunctionalRole(PaginationModel model)
        {
            try
            {
                Log.Info("MasterController:GetAllFunctionalRole", "Get all FunctionalRole mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetAllFunctionalRole(model, objUser);
                Log.Info("MasterController:GetAllFunctionalRole", "Get all FunctionalRole mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        #endregion

        #region Location
        [HttpPost]
        public HttpResponseMessage AddLocation(Location model)
        {
            try
            {
                Log.Info("MasterController:AddLocation", "AddLocation START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _masterService.AddLocation(model, objUser);
                Log.Info("MasterController:AddLocation", "AddLocation END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateLocation(Location model)
        {
            try
            {
                Log.Info("MasterController:UpdateLocation", "UpdateLocation START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _masterService.UpdateLocation(model, objUser);
                Log.Info("MasterController:UpdateLocation", "UpdateLocation END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data updated Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetLocationById(int Id)
        {
            try
            {
                Log.Info("MasterController:GetLocationById", "GetLocationById START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _masterService.GetLocationById(Id, objUser);
                Log.Info("MasterController:GetLocationById", "GetLocationById END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllLocation(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("MasterController:GetAllLocation", "GetAllLocation START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _masterService.GetAllLocationData(paginationModel, objUser);
                Log.Info("MasterController:GetAllLocation", "GetAllLocation END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetLocationList()
        {
            try
            {
                Log.Info("MasterController:GetLocationList", " GetLocationList START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _masterService.GetLocationList(objUser);
                Log.Info("MasterController:GetLocationList", "GetLocationList END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetLocationCode()
        {
            try
            {
                Log.Info("MasterController:GetLocationCode", "Get LocationCode Code Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetLocationCode(objUser);
                Log.Info("MasterController:GetLocationCode", "Get  LocationCode Code  End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        #endregion

        #region Designation
        [HttpPost]
        public HttpResponseMessage AddDesignation(DesignationModel model)
        {
            try
            {
                Log.Info("MasterController:AddDesignation", "Add Designation mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.AddDesignation(model, objUser);
                Log.Info("MasterController:AddDesignation", "Add Designation mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {

                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateDesignation(DesignationModel model)
        {
            try
            {
                Log.Info("MasterController:UpdateDesignation", "Update Designation mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.UpdateDesignation(model, objUser);
                Log.Info("MasterController:UpdateDesignation", "Update Designation mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetDesignationById(int Id)
        {
            try
            {
                Log.Info("MasterController:GetDesignationById", "get Designation by id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetDesignationById(Id, objUser);
                Log.Info("MasterController:GetDesignationById", "get Designation by id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllDesignation(PaginationModel model)
        {
            try
            {
                Log.Info("MasterController:GetAllDesignation", "Get all Designation mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetAllDesignation(model, objUser);
                Log.Info("MasterController:GetAllDesignation", "Get all Designation mapping END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        #endregion

        #region Auto Number Designation
        [HttpPost]
        public async Task<HttpResponseMessage> AddDesignationAutoNum(DesiAutoNumberingModel model)
        {
            try
            {
                Log.Info("MasterController:AddDesignationAutoNum", "Add Designation Auto Numbering START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.AddDesignationAutoNum(model, objUser);
                Log.Info("MasterController:AddDesignationAutoNum", "Add Designation Auto Numbering END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public async Task<HttpResponseMessage> UpdateDesignationAutoNum(DesiAutoNumberingModel model)
        {
            try
            {
                Log.Info("MasterController:UpdateDesignationAutoNum", "Update Designation Auto Numbering START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _masterService.UpdateDesignationAutoNum(model, objUser);
                Log.Info("MasterController:UpdateDesignationAutoNum", "Update Designation Auto Numbering END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data Update Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpGet]
        public HttpResponseMessage GetDesignationAutoNumById(int Id)
        {
            try
            {
                Log.Info("MasterController:GetDesignationAutoNumById", "get Designation Auto Numbering by id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetDesignationAutoNumById(Id, objUser);
                Log.Info("MasterController:GetDesignationAutoNumById", "get Designation Auto Numbering by id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllDesignationAutoNum(PaginationModel model)
        {
            try
            {
                Log.Info("MasterController:GetAllDesignationAutoNum", "Get all Designation Auto Numbering mapping START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _masterService.GetAllDesignationAutoNum(model, objUser);
                Log.Info("MasterController:GetAllDesignationAutoNum", "Get all Designation Auto Numbering mapping END", string.Empty);
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