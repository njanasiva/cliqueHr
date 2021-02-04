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
    public class EngagementController : ApiController
    {

        private IEngagementService _iEngagementService;
        public EngagementController()
        {
            _iEngagementService = new EngagementService();
        }
        #region Engagement

        [HttpPost]
        public HttpResponseMessage EngagementMarketPlace(EngagementGroups engagementGroups)
        {
            try
            {
                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _iEngagementService.EngagementMarketPlace(objUser, engagementGroups);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage EngagementMaster(EngagementGroups engagementGroups)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _iEngagementService.EngagementMaster(objUser, engagementGroups);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }


        [HttpPost]
        public HttpResponseMessage AddUpdateMarketPlace(AddUpdateMarketPlace addUpdateMarketPlace)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _iEngagementService.AddUpdateMarketPlace(objUser, addUpdateMarketPlace);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddUpdateEngagementSurvey(AddUpdateMarketPlace addUpdateMarketPlace)
        {
            try
            {

                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _iEngagementService.AddUpdateEngagementSurvey(objUser, addUpdateMarketPlace);
                return this.Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                var helper = new AppPresentationException(ex);
                return helper.GetResponse(this);
            }
        }



        [HttpPost]
        public HttpResponseMessage AddModifyDailyContent(DailyContent dailyContent)
        {
            try
            {
                UserContextModel objUser = new UserContextModel();
                objUser.UserID = 1;
                objUser.CompanyCode = "cliquehrsql";
                var data = _iEngagementService.AddModifyDailyContent(objUser, dailyContent);
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

