using CliqueHR.BL;
using CliqueHR.Helpers.ExceptionHelper;
using CliqueHR.Helpers.Validation;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CliqueHR.Api
{
    public class AuthorizationServer : OAuthAuthorizationServerProvider
    {
        private IAuthService authService;
        public AuthorizationServer()
        {
            this.authService = new AuthService();
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() => context.Validated());
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                var user = this.authService.LoginUser(Convert.ToString(context.Request.ReadFormAsync().Result["CompanyCode"]), context.UserName, context.Password);
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Role, user.RoleParam));
                identity.AddClaim(new Claim("EmployeeId", user.EmployeeId.ToString()));
                identity.AddClaim(new Claim("EntityId", user.EntityId.ToString()));
                identity.AddClaim(new Claim("CompanyCode", user.CompanyCode));
                identity.AddClaim(new Claim("EmployeeCode", user.EmployeeCode));
                identity.AddClaim(new Claim("CachingConfig", user.CachingConfig));
                Dictionary<string, string> userData = new Dictionary<string, string>();
                userData.Add("AccessFactor", user.RoleParam);
                userData.Add("SystemTimeOut", user.SystemTimeOut.ToString());
                var props = new AuthenticationProperties(userData);
                var ticket = new AuthenticationTicket(identity, props);
                await Task.Run(() => context.Validated(ticket));
            }
            catch (Exception ex)
            {
                if (ex is ValidationStrategy)
                {
                    var data = (ex as ValidationStrategy).GetData() as ValidationResponse;
                    context.SetError(data.Messages[0].Property, data.Messages[0].Message);
                }
                else
                {
                    context.SetError("Server_Error", ex.Message);
                }
            }
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            // Change authentication ticket for refresh token requests 
            string companyCode = context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == "CompanyCode")?.Value;
            int employeeId = Convert.ToInt32(context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == "EmployeeId")?.Value);
            int entityId = Convert.ToInt32(context.Ticket.Identity.Claims.FirstOrDefault(x => x.Type == "EntityId")?.Value);

            var timestampConfig = authService.GetCacheConfig(companyCode, employeeId, entityId);
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);
            newIdentity.AddClaim(new Claim("CachingConfig", timestampConfig));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}