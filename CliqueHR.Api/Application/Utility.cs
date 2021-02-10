<<<<<<< HEAD
﻿using CliqueHR.Common.Application;
using CliqueHR.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
>>>>>>> change
using System.Web;

namespace CliqueHR.Api.Application
{
    public static class Utility
    {
<<<<<<< HEAD
        public static UserContextModel GetUserLoginContext(this HttpRequestMessage request)
        {
            var loginContextManager = new LoginContextDataManager(request);
            return loginContextManager.Build();
        }  
=======
       
>>>>>>> change
    }
}