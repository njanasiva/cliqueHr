using CliqueHR.Common.Application;
using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;

namespace CliqueHR.Api.Application
{
    public interface ILoginContextDataManager
    {
        UserContextModel Build();
    }
    public class LoginContextDataManager : ILoginContextDataManager
    {
        LoginContextDataBuilder loginContextDataBuilder = new LoginContextDataBuilder();
        public LoginContextDataManager(HttpRequestMessage request)
        {
            ClaimsPrincipal principle = request.GetOwinContext().Authentication.User;
            loginContextDataBuilder.EmployeeId = Convert.ToInt32(principle.FindFirst(x => x.Type == "EmployeeId")?.Value);
            loginContextDataBuilder.EntityId = Convert.ToInt32(principle.FindFirst(x => x.Type == "EntityId")?.Value);
            loginContextDataBuilder.EmployeeCode = Convert.ToString(principle.FindFirst(x => x.Type == "EmployeeCode")?.Value);
            loginContextDataBuilder.CompanyCode = Convert.ToString(principle.FindFirst(x => x.Type == "CompanyCode")?.Value);
            loginContextDataBuilder.CachingConfig = Convert.ToString(principle.FindFirst(x => x.Type == "CachingConfig")?.Value);
            loginContextDataBuilder.AccessParameters = Convert.ToString(principle.FindFirst(ClaimTypes.Role)?.Value);
        }
        public UserContextModel Build()
        {
            return loginContextDataBuilder.Build();
        }
    }
}