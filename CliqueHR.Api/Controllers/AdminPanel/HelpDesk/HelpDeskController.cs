using CliqueHR.Api.Application;
using CliqueHR.BL;
using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CliqueHR.Api.Controllers
{
    public class HelpDeskController : ApiController
    {

        private IHelpDeskService _iHelpDeskService;
        public HelpDeskController()
        {
            _iHelpDeskService = new HelpDeskService();
        }

        [HttpPost]
        public HttpResponseMessage GetHelpDiskMaster(GetHelpDiskMaster getHelpDiskMaster)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _iHelpDeskService.GetHelpDiskMaster(objUser, getHelpDiskMaster);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddModifyCategory(AddModifyCategory addModifyCategory)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _iHelpDeskService.AddModifyCategory(objUser, addModifyCategory);
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
