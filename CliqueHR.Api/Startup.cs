﻿using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using CliqueHR.Helpers.Logger;
using CliqueHR.Api.Application;

[assembly: OwinStartup(typeof(CliqueHR.Api.Startup))]
namespace CliqueHR.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/login"),
                Provider = new AuthorizationServer(),
                AccessTokenProvider = new AccessTokenProvider(),
                RefreshTokenProvider = new RefreshTokenProvider()
            };

            //Token Generation
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            app.UseWebApi(config);
            app.UseLogger("CloudHR");

        }

    }
}