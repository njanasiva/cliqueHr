using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CliqueHR.Api.Application
{
    public class AccessFilter: ActionFilterAttribute
    {   
        public string Access { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string[] allowedAccess = null;
            var isAuthenticated = actionContext.Request.GetOwinContext().Authentication.User.Identity.IsAuthenticated;
            if (!isAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                return;
            }
            if (!string.IsNullOrEmpty(Access))
            {
                allowedAccess = Access.Split(',');
            }
            if (allowedAccess != null && allowedAccess.Length != 0)
            {
                var loginData = actionContext.Request.GetUserLoginContext();
                if (!allowedAccess.Any(x => loginData.AccessParameters.Contains(x)))
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                }
            }
        }
    }
}