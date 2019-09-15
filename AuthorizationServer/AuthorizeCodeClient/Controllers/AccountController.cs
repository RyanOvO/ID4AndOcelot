using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AuthorizeCodeClient.Controllers
{
    public class AccountController : Controller
    {
        // 客户端
        private const string clientBaseUri = @"http://localhost:6744";
        private const string validIssuer = "SomeSecureCompany";

        //IdentityServer4
        private const string idPServerBaseUri = @"http://localhost:5000";
        //IdentityServer4本身提供的url
        private const string idPServerAuthUri = idPServerBaseUri + @"/connect/authorize";
        private const string idPServerTokenUriFragment = @"connect/token";
        private const string idPServerEndSessionUri = idPServerBaseUri + @"/connect/endsession";

        //此配置同样也在Idp项目中注册
        private const string redirectUri = clientBaseUri + @"/account/oAuth2";
        private const string clientId = "AuthorizeCodeClient";
        private const string clientSecret = "secret";
        private const string audience = "SomeSecureCompany/resources";
        private const string scope = "api";
        private const string response_type = "code";
        private const string grantType = "authorization_code";

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SignIn()
        {
            var state = Guid.NewGuid().ToString("N");

            //Store state using cookie-based authentication middleware
            await SaveState(state);

            //Redirect to IdP to get an Authorization Code
            var url = idPServerAuthUri +
                "?client_id=" + clientId +
                "&response_type=" + response_type +
                "&redirect_uri=" + redirectUri +
                "&scope=" + scope +
                "&state=" + state;

            return Redirect(url); //performs a GET
        }

        [HttpGet]
        public async Task<ActionResult> OAuth2(string code, string state)
        {
            var authorizationCode = code;

            //Defend against CSRF attacks http://www.twobotechnologies.com/blog/2014/02/importance-of-state-in-oauth2.html
            await ValidateStateAsync(state);

            //Exchange Authorization Code for an Access Token by POSTing to the IdP's token endpoint
            string json = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(idPServerBaseUri);
                var content = new FormUrlEncodedContent(new[]
                {
                     new KeyValuePair<string, string>("grant_type", grantType)
                    ,new KeyValuePair<string, string>("code", authorizationCode)
                    ,new KeyValuePair<string, string>("redirect_uri", redirectUri)
                    ,new KeyValuePair<string, string>("client_id", clientId)              //TODO: consider sending via basic authentication header
                    ,new KeyValuePair<string, string>("client_secret", clientSecret)
                });
                var httpResponseMessage = client.PostAsync(idPServerTokenUriFragment, content).Result;
                json = httpResponseMessage.Content.ReadAsStringAsync().Result;
            }

            //Extract the Access Token
            dynamic results = JsonConvert.DeserializeObject<dynamic>(json);
            string accessToken = results.access_token;

            //Validate token crypto and build claims identity principle
            var claims = await ValidateToken(accessToken);                    //For OpenId Connect, this passed/validated state too, but with Authentication Code flow's extra hop for Access Token, that validation is required higher up (or CSRF attacks possible)
            var id = new ClaimsIdentity(claims, "Cookies");              //"Cookie" refers back to the middleware named in Startup.cs
            var claimsPrincipal = new ClaimsPrincipal(id);
            //Sign into the middleware so we can navigate around secured parts of this site (Try /Home/Secured)        
            await Request.HttpContext.SignInAsync(claimsPrincipal);

            return this.Redirect("/Home");
        }

        private async Task<IEnumerable<Claim>> ValidateToken(string token)
        {
            //Discard temp cookie and cookie-based middleware authentication objects (we just needed it for storing State)
            //Grab certificate for verifying JWT signature
            //In prod, we'd get this from the certificate store or similar
            var certPath = Path.Combine(Directory.GetCurrentDirectory(), "SscSign.pfx");
            var cert = new X509Certificate2(certPath);
            var x509SecurityKey = new X509SecurityKey(cert);

            var parameters = new TokenValidationParameters
            {
                RequireSignedTokens = true,
                ValidAudience = audience,
                ValidIssuer = validIssuer,
                IssuerSigningKey = x509SecurityKey,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            //Validate the token and retrieve ClaimsPrinciple
            var handler = new JwtSecurityTokenHandler();
            SecurityToken jwt;
            var id = handler.ValidateToken(token, parameters, out jwt);
            await Request.HttpContext.SignOutAsync("TempCookies");
            return id.Claims;
        }

        private async Task<AuthenticateResult> ValidateStateAsync(string state)
        {
            var authenticateResult = await Request.HttpContext.AuthenticateAsync("TempCookies");

            if (authenticateResult == null)
                throw new InvalidOperationException("No temp cookie");

            if (state != authenticateResult.Principal.FindFirst("state").Value)
                throw new InvalidOperationException("invalid state");

            return authenticateResult;
        }

        private async Task SaveState(string state)
        {
            var tempId = new ClaimsIdentity();
            tempId.AddClaim(new Claim("state", state));
            var claimsPrincipal = new ClaimsPrincipal(tempId);
            // 在上面注册AddAuthentication时，指定了默认的Scheme，在这里便可以不再指定Scheme。
            await Request.HttpContext.SignInAsync("TempCookies", claimsPrincipal);
        }

        public async Task <ActionResult> SignOut()
        {
            await Request.HttpContext.SignOutAsync("Cookies");
            return this.Redirect(idPServerEndSessionUri);
        }

    }
}