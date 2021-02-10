using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CliqueHR.Api.Application
{
    public class AccessTokenProvider: AuthenticationTokenProvider
    {
        public override void Create(AuthenticationTokenCreateContext context)
        {
            int systemTimeOut = Convert.ToInt32(context.Ticket.Properties.Dictionary["SystemTimeOut"]);
            context.Ticket.Properties.ExpiresUtc = DateTime.UtcNow.AddSeconds(90);
            context.Ticket.Properties.IssuedUtc = DateTime.UtcNow;
            context.Ticket.Properties.AllowRefresh = true;

            context.SetToken(context.SerializeTicket());
        }
    }
}