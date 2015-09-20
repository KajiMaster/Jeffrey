using System;
using System.Linq;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using Microsoft.Experimental.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Framework.Configuration;

namespace RallyNow.Controllers
{
    public class OutlookController : Controller
    {
        // The required scopes for our app
        private static string[] scopes = { "https://outlook.office.com/mail.read" };
        private IConfiguration _configuration;

        public string ClientId => _configuration.GetSection("Authentication")["ClientID"];
        public string ClientSecret => _configuration.GetSection("Authentication")["ClientSecret"];


        public OutlookController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> SignIn()
        {
            string authority = "https://login.microsoftonline.com/common";
            AuthenticationContext authContext = new AuthenticationContext(authority);

            Uri redirectUri = new Uri(Url.Action("Authorize", "Outlook", null, "http"));

            // Generate the parameterized URL for Azure signin
            Uri authUri = await authContext.GetAuthorizationRequestUrlAsync(scopes, null, ClientId,
                redirectUri, UserIdentifier.AnyUser, null);

            // Redirect the browser to the Azure signin page
            return Redirect(authUri.ToString());
        }

        public async Task<IActionResult> Authorize()
        {
            // Get the 'code' parameter from the Azure redirect
            string authCode = Request.Query["code"];

            string authority = "https://login.microsoftonline.com/common";
            AuthenticationContext authContext = new AuthenticationContext(authority);

            // The same url we specified in the auth code request
            Uri redirectUri = new Uri(Url.Action("Authorize", "Outlook", null, "http"));

            // Use client ID and secret to establish app identity
            ClientCredential credential = new ClientCredential(ClientId, ClientSecret);

            try
            {
                // Get the token
                var authResult = await authContext.AcquireTokenByAuthorizationCodeAsync(
                    authCode, redirectUri, credential, scopes);

                // Save the token in the session
                Context.Session.SetString("access_token", authResult.Token);

                // Try to get user info
                Context.Session.SetString("user_email", GetUserEmail(authContext, ClientId));

                return Content("Access Token: " + authResult.Token);
            }
            catch (AdalException ex)
            {
                return Content($"ERROR retrieving token: {ex.Message}");
            }
        }

        private string GetUserEmail(AuthenticationContext context, string clientId)
        {
            // ADAL caches the ID token in its token cache by the client ID
            foreach (TokenCacheItem item in context.TokenCache.ReadItems())
            {
                if (item.Scope.Contains(clientId))
                {
                    return GetEmailFromIdToken(item.Token);
                }
            }
            return string.Empty;
        }

        private string GetEmailFromIdToken(string token)
        {
            // JWT is made of three parts, separated by a '.' 
            // First part is the header 
            // Second part is the token 
            // Third part is the signature 
            string[] tokenParts = token.Split('.');
            if (tokenParts.Length < 3)
            {
                // Invalid token, return empty
            }
            // Token content is in the second part, in urlsafe base64
            string encodedToken = tokenParts[1];
            // Convert from urlsafe and add padding if needed
            int leftovers = encodedToken.Length % 4;
            if (leftovers == 2)
            {
                encodedToken += "==";
            }
            else if (leftovers == 3)
            {
                encodedToken += "=";
            }
            encodedToken = encodedToken.Replace('-', '+').Replace('_', '/');
            // Decode the string
            var base64EncodedBytes = System.Convert.FromBase64String(encodedToken);
            string decodedToken = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            // Load the decoded JSON into a dynamic object
            dynamic jwt = Newtonsoft.Json.JsonConvert.DeserializeObject(decodedToken);
            // User's email is in the preferred_username field
            return jwt.preferred_username;
        }
    }
}
