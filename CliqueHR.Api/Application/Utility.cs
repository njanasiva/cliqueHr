using CliqueHR.Common.Application;
using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace CliqueHR.Api.Application
{
    public static class Utility
    {
        public static UserContextModel GetUserLoginContext(this HttpRequestMessage request)
        {
            var loginContextManager = new LoginContextDataManager(request);
            return loginContextManager.Build();
        }  
    }
}