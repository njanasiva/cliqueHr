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

namespace CliqueHR.Api.Controllers
{
    public class AutoNumberingController : ApiController
    {
        private IAutoNumberingService _autoNumberingService;

        public AutoNumberingController()
        {
            _autoNumberingService = new AutoNumberingService();
        }

        [HttpPost]
        public HttpResponseMessage AddUpdateAutoNumbering(AutoNumbering model)
        {
            try
            {
                Log.Info("AutoNumberingController:AddUpdateAutoNumbering", "Add Auto Numbering Start", string.Empty);

                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                _autoNumberingService.AddUpdateAutoNumbering(model, objUser);
                Log.Info("AutoNumberingController:AddUpdateAutoNumbering", "Add Auto Numbering End", string.Empty);
                return this.Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAutoNumbering()
        {
            try
            {
                Log.Info("AutoNumberingController:GetAutoNumbering", "Get Auto Numbering Start", string.Empty);
                UserContextModel objUser = new UserContextModel();
                objUser.EmployeeId = 1;
                objUser.CompanyCode = "devDB";
                var data = _autoNumberingService.GetAutoNumbering(objUser);
                Log.Info("AutoNumberingController:GetAutoNumbering", "Get Auto Numbering END", string.Empty);
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
