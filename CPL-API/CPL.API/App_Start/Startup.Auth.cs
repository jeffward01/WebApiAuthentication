using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CPL.API.Providers;
using CPL.Core.Managers;
using CPL.Data;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace CPL.API.App_Start
{

    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        //Todo Use a consturctor and DI instead
        public static IAuthManager _authManager = new AuthManager(new AuthRepository(), new PreLaunchNotyUserRepository());

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {

            ////configure autofac
            //var builder = new ContainerBuilder();

            //// STANDARD WEB API SETUP:

            //// Get your HttpConfiguration. In OWIN, you'll create one
            //// rather than using GlobalConfiguration.
            //var config = new HttpConfiguration();

            //// Register your Web API controllers.

            //// Run other optional steps, like registering filters,
            //// per-controller-type services, etc., then set the dependency resolver
            //// to be Autofac.
            //var container = builder.Build();
            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //// OWIN WEB API SETUP:

            //// Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            //// and finally the standard Web API middleware.
            //app.UseAutofacMiddleware(container);
            //app.UseAutofacWebApi(config);
            //app.UseWebApi(config);



            // Configure the db context and UserProfile manager to use a single instance per request
            //Autofac handles this
            // app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<IdentityConfig.ApplicationUserManager>(IdentityConfig.ApplicationUserManager.Create);

            //   app.UseCors(CorsOptions.AllowAll);


            PublicClientId = "self";
            /*
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, _authManager),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // Note: Remove the following line before you deploy to production:
                AllowInsecureHttp = true
            };

            */
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, _authManager),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);


            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication
            (
                new OAuthBearerAuthenticationOptions
                {
                    Provider = new OAuthBearerAuthenticationProvider()
                }
            );
            /*
            // Configure the db context and UserProfile manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            // Enable the application to use a cookie to store information for the signed in UserProfile
            // and to use a cookie to temporarily store information about a UserProfile logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            //Enable or disable cors
            app.UseCors(CorsOptions.AllowAll);

            //Configure Auth0
            ConfigureAuthZero(app);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            */
        }
    }
}