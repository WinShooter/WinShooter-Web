// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
//   This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; either version 2
//   of the License, or (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
// </copyright>
// <summary>
//   The controller handles the account.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;
    using System.Web.Security;

    using WinShooter.Database;
    using WinShooter.Logic;
    using WinShooter.Logic.Authentication;
    using WinShooter.Logic.WinShooterConstants;

    /// <summary>
    /// The controller handles the account.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The application configuration.
        /// </summary>
        private readonly AppConfig appConfig;

        public AccountController()
        {
            this.appConfig = new AppConfig();
        }

        /// <summary>
        /// The login view.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Login()
        {
            this.ViewBag.GoogleUrl = this.CreateGoogleUrl();
            return this.View();
        }

        /// <summary>
        /// The login view.
        /// </summary>
        /// <returns>The <see cref="ActionResult"/>.</returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Where the user can edit his/her details.
        /// </summary>
        /// <returns></returns>
        public ActionResult EditAccount()
        {
            return this.View();
        }

        /// <summary>
        /// Where the user can edit his/her details.
        /// </summary>
        /// <returns></returns>
        public ActionResult NewAccount()
        {
            return this.View("EditAccount");
        }

        /// <summary>
        /// Return from a google authentication.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> SigninGoogle()
        {
            // TODO What does this look like if auth failed?
            var state = this.Request.QueryString["state"];
            // TODO verify state with security token

            var code = this.Request.QueryString["code"];
            var authuser = this.Request.QueryString["authuser"];
            var numSessions = this.Request.QueryString["num_sessions"];
            var sessionState = this.Request.QueryString["session_state"];
            var prompt = this.Request.QueryString["prompt"];

            var googleToken = await GetGoogleIdToken(
                code,
                this.GetRedirectUrl());

            if (!googleToken.IsValid())
            {
                return this.RedirectToAction("Index", "Home");
            }

            using (var dbsession = NHibernateHelper.OpenSession())
            {
                var userManager = new UserManager(dbsession);
                var user = userManager.GetUser(googleToken.IdentityProviderId, "google", googleToken.Email, DateTime.Now);

                var serializeModel = new CustomPrincipalSerializeModel
                {
                    Id = user.Id,
                    FirstName = user.Givenname,
                    LastName = user.Surname,
                    IsSystemAdmin = user.IsSystemAdmin
                };

                var serializer = new JavaScriptSerializer();

                var userData = serializer.Serialize(serializeModel);

                var authTicket = new FormsAuthenticationTicket(
                    1,
                    user.Id.ToString(),
                    DateTime.Now,
                    DateTime.Now.AddMinutes(15),
                    false,
                    userData);

                var encTicket = FormsAuthentication.Encrypt(authTicket);
                var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { HttpOnly = true };
                this.Response.Cookies.Add(faCookie);
                
                return user.HasAcceptedTerms < this.appConfig.CurrentConditionLevel
                    ? this.RedirectToAction("NewAccount")
                    : this.RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// Create the google login URL.
        /// </summary>
        /// <returns>The URL</returns>
        private string CreateGoogleUrl()
        {
            return
                string.Format(
                    "https://accounts.google.com/o/oauth2/auth?response_type={0}&client_id={1}&redirect_uri={2}&scope={3}&state={4}",
                    "code",
                    ConfigurationManager.AppSettings[WinShooterAppSettings.GoogleOauthClientId],
                    UrlEncode(this.GetRedirectUrl()),
                    UrlEncode("openid email"),
                    SecurityToken());
        }

        private string GetRedirectUrl()
        {
            return this.Url.Action("SigninGoogle", "Account", null, this.Request.Url.Scheme);
        }

        /// <summary>
        /// Create a security token
        /// </summary>
        /// <returns></returns>
        private static string SecurityToken()
        {
            // TODO Implement
            return "qwerrtyuop0181";
        }

        private static string UrlEncode(string input)
        {
            return HttpUtility.UrlEncode(input);
        }

        private static async Task<GoogleToken> GetGoogleIdToken(
            string code,
            string redirectUri)
        {
            var url = new Uri("https://www.googleapis.com/oauth2/v3/token");
            var clientId = ConfigurationManager.AppSettings[WinShooterAppSettings.GoogleOauthClientId];
            var clientSecret = ConfigurationManager.AppSettings[WinShooterAppSettings.GoogleOauthSecret];

            var jsonString = await GetIdToken(url, code, clientId, clientSecret, redirectUri);
            var jsonSerializer = new JavaScriptSerializer();
            var result = jsonSerializer.Deserialize<Dictionary<string, string>>(jsonString);
            return new GoogleToken(result["id_token"], clientId, GoogleTrustCertificateFetcher.GetInstance);
        }

        private static async Task<string> GetIdToken(Uri url, string code, string clientId, string clientSecret, string redirectUri)
        {
            var client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var parameters = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "redirect_uri", redirectUri },
                { "grant_type", "authorization_code" }
            };

            var responseJsonBytes = await client.UploadDataTaskAsync(url, Encoding.UTF8.GetBytes(PrepareUploadParams(parameters)));
            var responseJson = Encoding.UTF8.GetString(responseJsonBytes);

            return responseJson;
        }

        private static string PrepareUploadParams(Dictionary<string, string> parameters)
        {
            var toReturn = new StringBuilder();

            foreach (var key in parameters.Keys)
            {
                if (toReturn.Length > 0)
                {
                    toReturn.Append("&");
                }

                toReturn.Append(key);
                toReturn.Append("=");
                toReturn.Append(parameters[key]);
            }

            return toReturn.ToString();
        }
    }
}