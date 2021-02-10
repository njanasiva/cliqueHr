using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CliqueHR.Api.Application;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using CliqueHR.Helpers.Logger;

namespace CliqueHR.Api.Controllers
{
    public class EntityController : ApiController
    {
        private IEntityService _entityService;

        public EntityController()
        {
            _entityService = new EntityService();
        }

        [HttpPost]
        public HttpResponseMessage AddEntity()
        {
            try
            {
                Log.Info("EntityController:AddEntity", "Add Entity Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _entityService.AddEntity(HttpContext.Current.Request, objUser);
                Log.Info("EntityController:AddEntity", "Add Entity  End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        [AccessFilter]
        public HttpResponseMessage UpdateEntity()
        {
            try
            {
                Log.Info("EntityController:UpdateEntity", "Update Entity Start", string.Empty);
                UserContextModel objUser = Request.GetUserLoginContext();
                _entityService.UpdateEntity(HttpContext.Current.Request, objUser);
                Log.Info("EntityController:UpdateEntity", "Update Entity  End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        [AccessFilter]
        public HttpResponseMessage GetEntity(PaginationModel model)
        {
            try
            {
                Log.Info("EntityController:GetEntity", "Get All Entities  Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _entityService.GetEntity(model, objUser);
                Log.Info("GroupCompanyController:GetGroupCompany", "Get All Entities End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetEntityById(int Id)
        {
            try
            {
                Log.Info("EntityController:GetEntityById", "Get  Entities By Id  Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _entityService.GetEntityById(Id, objUser);
                Log.Info("EntityController:GetEntityById", "Get  Entities By Id  End", string.Empty);
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
