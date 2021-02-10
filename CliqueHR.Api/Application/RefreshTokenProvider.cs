using CliqueHR.BL;
using CliqueHR.Helpers.Logger;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace CliqueHR.Api
{
    [Serializable]
    public class RefreshTokenTicket
    {
        public ClaimsIdentity claims { get; set; }
        public IDictionary<string, string> properties { get; set; }
        public DateTimeOffset ExpiresUtc { get; set; }
        public DateTimeOffset? IssuedUtc { get; set; }
    }
    public class RefreshTokenProvider: IAuthenticationTokenProvider
    {
        private IAuthService authService;
        public RefreshTokenProvider()
        {
            authService = new AuthService();
        }
        #region[CreateAsync]
        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid().ToString();
            /* Copy claims from previous token
             ***********************************/
            string companyCode = context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == "CompanyCode")?.Value;
            int systemTimeOut = Convert.ToInt32(context.Ticket.Properties.Dictionary["SystemTimeOut"]);
            guid = companyCode + "_" + guid;

            try
            {
                authService.AddRefreshToken(companyCode, guid, new RefreshTokenTicket { 
                    claims = context.Ticket.Identity,
                    properties = context.Ticket.Properties.Dictionary,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(systemTimeOut),
                    IssuedUtc = context.Ticket.Properties.IssuedUtc
                });

                // consider storing only the hash of the handle  
                await Task.Run(() => context.SetToken(guid));
            }
            catch (Exception ex)
            {
                Log.Error("RefreshTokenProvider:CreateAsync", "Error while creating refreshtoken",
                    string.Format("company code = {0}, token = {1}", companyCode, guid), ex);
            }
        }
        #endregion

        #region[ReceiveAsync]
        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            string header = await Task.Run(() => context.OwinContext.Request.Headers["Authorization"]);
            string companyCode = string.Empty;
            try
            {
                companyCode = context.Token.Split('_')[0];
                var ticketObject = authService.GetRefreshTokenTicket(companyCode, context.Token);
                if (ticketObject != null && ticketObject is RefreshTokenTicket)
                {
                    var authTicket = (RefreshTokenTicket)ticketObject;
                    var refreshTokenProperties = new AuthenticationProperties(authTicket.properties)
                    {
                        IssuedUtc = authTicket.IssuedUtc,
                        ExpiresUtc = authTicket.ExpiresUtc
                    };
                    var refreshTokenTicket = await Task.Run(() => new AuthenticationTicket(authTicket.claims, refreshTokenProperties));
                    context.SetTicket(refreshTokenTicket);
                }
                else
                {
                    throw new Exception("ticket not present against token = " + context.Token);
                }

            }
            catch (Exception ex)
            {
                Log.Error("RefreshTokenProvider:ReceiveAsync", "Error while read refreshtoken",
                    string.Format("company code = {0}, token = {1}", companyCode, context.Token), ex);
            }
        }
        #endregion

        #region[Create & Receive Synchronous methods]
        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }
        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}