using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Cors;
using CPL.Core.Managers;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace CPL.API.Providers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly IAuthManager _authRepository;

        public ApplicationOAuthProvider(string publicClientId, IAuthManager authRepository)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            _authRepository = authRepository;

            _publicClientId = publicClientId;
        }

        //public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        //{
        //  //  var userProfileManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
        //  var userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<ApplicationUser>>();

        //    ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

        //    if (user == null)
        //    {
        //        context.SetError("invalid_grant", "The UserProfile name or password is incorrect.");
        //        return;
        //    }

        //    ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
        //       OAuthDefaults.AuthenticationType);
        //    ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
        //        CookieAuthenticationDefaults.AuthenticationType);

        //    AuthenticationProperties properties = CreateProperties(user.UserName);
        //    AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
        //    context.Validated(ticket);
        //    context.Request.Context.Authentication.SignIn(cookiesIdentity);
        //}

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            /*
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null) allowedOrigin = "*";
            */

            if (!context.OwinContext.Response.Headers.Any(_ => _.Key.Contains("Allow-Origin")))
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            }
            IdentityUser user = await _authRepository.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            //identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            // identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
            /*
            var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    }
                });
            var ticket = new AuthenticationTicket(identity, props);
            context.Validated(ticket);
            */
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            //TODO: UseRefreshTokens
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
            //string clientId = string.Empty;
            //string clientSecret = string.Empty;
            //Client client = null;

            //if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            //{
            //    context.TryGetFormCredentials(out clientId, out clientSecret);
            //}

            //if (context.ClientId == null)
            //{
            //    //Remove the comments from the below line context.SetError, and invalidate context
            //    //if you want to force sending clientId/secrects once obtain access tokens.
            //    context.Validated();
            //    //context.SetError("invalid_clientId", "ClientId should be sent.");
            //    return Task.FromResult<object>(null);
            //}

            //using (AuthRepository _repo = new AuthRepository())
            //{
            //    client = _repo.FindClient(context.ClientId);
            //}

            //if (client == null)
            //{
            //    context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", context.ClientId));
            //    return Task.FromResult<object>(null);
            //}

            //if (client.ApplicationType == Models.ApplicationTypes.NativeConfidential)
            //{
            //    if (string.IsNullOrWhiteSpace(clientSecret))
            //    {
            //        context.SetError("invalid_clientId", "Client secret should be sent.");
            //        return Task.FromResult<object>(null);
            //    }
            //    else
            //    {
            //        if (client.Secret != Helper.GetHash(clientSecret))
            //        {
            //            context.SetError("invalid_clientId", "Client secret is invalid.");
            //            return Task.FromResult<object>(null);
            //        }
            //    }
            //}

            //if (!client.Active)
            //{
            //    context.SetError("invalid_clientId", "Client is inactive.");
            //    return Task.FromResult<object>(null);
            //}

            //context.OwinContext.Set<string>("as:clientAllowedOrigin", client.AllowedOrigin);
            //context.OwinContext.Set<string>("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            //context.Validated();
            //return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_clientId", "Refresh token is issued to a different clientId.");
                return Task.FromResult<object>(null);
            }

            // Change auth ticket for refresh token requests
            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newClaim = newIdentity.Claims.Where(c => c.Type == "newClaim").FirstOrDefault();
            if (newClaim != null)
            {
                newIdentity.RemoveClaim(newClaim);
            }
            newIdentity.AddClaim(new Claim("newClaim", "newValue"));

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
    //TODO: Upgrade to refresh Tokens: http://bitoftech.net/2014/07/16/enable-oauth-refresh-tokens-angularjs-app-using-asp-net-web-api-2-owin/
    //public class Client
    //{
    //    [Key]
    //    public string Id { get; set; }

    //    [Required]
    //    public string Secret { get; set; }

    //    [Required]
    //    [MaxLength(100)]
    //    public string Name { get; set; }

    //    public ApplicationTypes ApplicationType { get; set; }
    //    public bool Active { get; set; }
    //    public int RefreshTokenLifeTime { get; set; }

    //    [MaxLength(100)]
    //    public string AllowedOrigin { get; set; }
    //}

    //public class RefreshToken
    //{
    //    [Key]
    //    public string Id { get; set; }

    //    [Required]
    //    [MaxLength(50)]
    //    public string Subject { get; set; }

    //    [Required]
    //    [MaxLength(50)]
    //    public string ClientId { get; set; }

    //    public DateTime IssuedUtc { get; set; }
    //    public DateTime ExpiresUtc { get; set; }

    //    [Required]
    //    public string ProtectedTicket { get; set; }
    //}
    //public enum ApplicationTypes
    //{
    //    JavaScript = 0,
    //    NativeConfidential = 1
    //};
}
