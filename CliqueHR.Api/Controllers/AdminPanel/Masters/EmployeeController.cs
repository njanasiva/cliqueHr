using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CliqueHR.Api.Application;
using CliqueHR.BL.AdminPanelService.MastersService;
using CliqueHR.BL.Implementation.AdminPanel.MastersService;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;

namespace CliqueHR.Api.Controllers.AdminPanel.Masters
{
    public class EmployeeController : ApiController
    {
        private IEmployeeService _employeeService;
        public EmployeeController()
        {
            _employeeService = new EmployeeService();
        }


        #region Employee Type
        [HttpPost]
        public HttpResponseMessage AddEmployeeType(EmployeeType employeeModel)
        {
            try
            {
                Log.Info("EmployeeController:AddEmployeeType", "AddEmployeeType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.AddEmployeeTypeData(employeeModel, objUser);
                Log.Info("EmployeeController:AddEmployeeType", "AddEmployeeType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateEmployeeType(EmployeeType employeeModel)
        {
            try
            {
                Log.Info("EmployeeController:UpdateEmployeeType", "Update EmployeeType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.UpdateEmployeeTypeData(employeeModel, objUser);
                Log.Info("EmployeeController:UpdateEmployeeType", "Update EmployeeType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data updated Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEmployeeTypeById(int Id)
        {
            try
            {
                Log.Info("EmployeeController:GetEmployeeTypeById", "Get Employee Type By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetEmployeeTypeByID(Id, objUser);
                Log.Info("EmployeeController:GetEmployeeTypeById", "Get Employee Type By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllEmployeeType(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("EmployeeController:GetAllEmployeeType", "Get All EmployeeType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetAllEmployeeType(objUser, paginationModel);
                Log.Info("EmployeeController:GetAllEmployeeType", "Get All EmployeeType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        #endregion

        #region Band Type

        [HttpPost]
        public HttpResponseMessage AddBandType(BandType bandTypeModel)
        {
            try
            {
                Log.Info("EmployeeController:AddBandType", "Add BandType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.AddBandTypeData(bandTypeModel, objUser);
                Log.Info("EmployeeController:AddBandType", "Add BandType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateBandType(BandType bandTypeModel)
        {
            try
            {
                Log.Info("EmployeeController:UpdateBandType", "Update BandType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.UpdateBandTypeData(bandTypeModel, objUser);
                Log.Info("EmployeeController:UpdateBandType", "Update BandType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data updated Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetBandTypeById(int Id)
        {
            try
            {
                Log.Info("EmployeeController:GetBandTypeById", "Get Band Type By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetBandTypeByID(Id, objUser);
                Log.Info("EmployeeController:GetBandTypeById", "Get Employee Type By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllBandType(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("EmployeeController:GetAllBandType", "Get All GetAllBandType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetAllBandType(objUser, paginationModel);
                Log.Info("EmployeeController:GetAllBandType", "Get All GetAllBandType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        #endregion

        #region Grade Type
        [HttpPost]
        public HttpResponseMessage AddGradeType(GradeType gradeTypeModel)
        {
            try
            {
                Log.Info("EmployeeController:AddGradeType", "Add GradeType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.AddGradeTypeData(gradeTypeModel, objUser);
                Log.Info("EmployeeController:AddGradeType", "Add GradeType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateGradeType(GradeType gradeTypeModel)
        {
            try
            {
                Log.Info("EmployeeController:UpdateGradeType", "Update GradeType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.UpdateGradeTypeData(gradeTypeModel, objUser);
                Log.Info("EmployeeController:UpdateGradeType", "Update GradeType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data updated Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpGet]
        public HttpResponseMessage GetGradeTypeById(int Id)
        {
            try
            {
                Log.Info("EmployeeController:GetGradeTypeById", "Get Grade Type By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetGradeTypeByID(Id, objUser);
                Log.Info("EmployeeController:GetGradeTypeById", "Get Grade Type By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage GetAllGradeType(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("EmployeeController:GetAllGradeType", "Get All GetAllGradeType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetAllGradeType(objUser, paginationModel);
                Log.Info("EmployeeController:GetAllGradeType", "Get All GetAllGradeType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
       
        [HttpGet]
        public HttpResponseMessage GetGradeList()
        {
            try
            {
                Log.Info("EmployeeController:GetGradeList", " GetGradeList START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetGradeList(objUser);
                Log.Info("EmployeeController:GetGradeList", "GetGradeList END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        #endregion

        #region Center type
        [HttpPost]
        public HttpResponseMessage AddCenterTypeData(CenterTypeModel centerType)
        {
            try
            {
                Log.Info("EmployeeController:AddCenterTypeData", "add CenterType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.AddCenterTypeData(centerType, objUser);
                Log.Info("EmployeeController:AddCenterTypeData", "add CenterType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateCenterTypeData(CenterTypeModel centerType)
        {
            try
            {
                Log.Info("EmployeeController:UpdateCenterTypeData", "Update CenterType START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.UpdateCenterTypeData(centerType, objUser);
                Log.Info("EmployeeController:UpdateCenterTypeData", "Update CenterType END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data Update Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllCenterType(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("EmployeeController:GetAllCenterType", "Get CenterallType data START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data =_employeeService.GetAllCenterType(paginationModel, objUser);
                Log.Info("EmployeeController:GetAllCenterType", "Get CenterallType data END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCenterTypeByID(int Id)
        {
            try
            {
                Log.Info("EmployeeController:GetCenterTypeByID", "Get Center Type By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetCenterTypeByID(Id, objUser);
                Log.Info("EmployeeController:GetCenterTypeByID", "Get center Type By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
       
        [HttpGet]
        public HttpResponseMessage GetCenterType()
        {
            try
            {
                Log.Info("EmployeeController:GetCenterType", "Get Center Type START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetCenterType(objUser);
                Log.Info("EmployeeController:GetCenterType", "Get center Type  END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        #endregion

        #region CostCenter Type
        [HttpPost]
        public HttpResponseMessage AddCostCenter(CostCenterModel costCenter)
        {
            try
            {
                Log.Info("EmployeeController:AddCostCenter", "AddCostCenter START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.AddCostCenter(costCenter, objUser);
                Log.Info("EmployeeController:AddCostCenter", "AddCostCenter END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data added Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage UpdateCostCenter(CostCenterModel centerType)
        {
            try
            {
                Log.Info("EmployeeController:UpdateCostCenter", "Update cost center START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                _employeeService.UpdateCostCenter(centerType, objUser);
                Log.Info("EmployeeController:UpdateCostCenter", "Update cost center END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, new { Messsage = "Data Update Successfully!" });
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCostCenterByID(int Id)
        {
            try
            {
                Log.Info("EmployeeController:GetCostCenterByID", "Get Cost center Type By Id START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetCostCenterByID(Id, objUser);
                Log.Info("EmployeeController:GetCostCenterByID", "Get Cost center Type By Id END", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }
        [HttpPost]
        public HttpResponseMessage GetAllCostCenter(PaginationModel paginationModel)
        {
            try
            {
                Log.Info("EmployeeController:GetAllCostCenter", "Get all Cost Center data START", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "DevDB";
                var data = _employeeService.GetAllCostCenter(paginationModel, objUser);
                Log.Info("EmployeeController:GetAllCostCenter", "Get all Cost Center data END", string.Empty);
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