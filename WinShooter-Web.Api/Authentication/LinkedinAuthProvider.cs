// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkedinAuthProvider.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The LINKEDIN authentication provider, <see cref="http://www.binoot.com/2013/03/30/linkedin-provider-for-servicestack-authentication/" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using ServiceStack.Configuration;
    using ServiceStack.ServiceHost;
    using ServiceStack.ServiceInterface;
    using ServiceStack.ServiceInterface.Auth;
    using ServiceStack.Text;

    /// <summary>
    /// The LINKEDIN authentication provider, <see cref="http://www.binoot.com/2013/03/30/linkedin-provider-for-servicestack-authentication/"/>
    /// </summary>
    public class LinkedinAuthProvider : OAuthProvider
    {
        /// <summary>
        /// The provider name.
        /// </summary>
        public static readonly string Name = "linkedin";

        /// <summary>
        /// The realm.
        /// </summary>
        public static readonly string Realm = "https://www.linkedin.com/uas/oauth2/";

        /// <summary>
        /// The people data url.
        /// </summary>
        public static readonly string PeopleDataUrl =
            "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,formatted-name,industry,email-address)?format=json&";

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedinAuthProvider"/> class.
        /// </summary>
        /// <param name="appSettings">
        /// The app settings.
        /// </param>
        public LinkedinAuthProvider(IResourceManager appSettings)
            : base(appSettings, Realm, Name)
        {
        }

        /// <summary>
        /// The authenticate.
        /// </summary>
        /// <param name="authService">
        /// The authentication service.
        /// </param>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public override object Authenticate(
            IServiceBase authService, IAuthSession session, Auth request)
        {
            var tokens = this.Init(authService, ref session, request);
            var code = authService.RequestContext.Get<IHttpRequest>().QueryString["code"];
            var error = authService.RequestContext.Get<IHttpRequest>().QueryString["error"];
            var isPreAuthError = !string.IsNullOrEmpty(error);
            if (isPreAuthError)
            {
                return authService.Redirect(session.ReferrerUrl);
            }

            var isPreAuthCallback = !string.IsNullOrEmpty(code);
            if (!isPreAuthCallback)
            {
                var preAuthUrl = Realm
                                 + "authorization?response_type=code&client_id={0}&scope={1}&state={2}&redirect_uri={3}";
                preAuthUrl = preAuthUrl.Fmt(
                    this.ConsumerKey,
                    "r_fullprofile%20r_emailaddress",
                    Guid.NewGuid().ToString(),
                    this.CallbackUrl.UrlEncode());

                authService.SaveSession(session, this.SessionExpiry);
                return authService.Redirect(preAuthUrl);
            }

            var accessTokenUrl = Realm
                                 + "accessToken?grant_type=authorization_code&code={0}&redirect_uri={1}&client_id={2}&client_secret={3}";
            accessTokenUrl = accessTokenUrl.Fmt(
                code, this.CallbackUrl.UrlEncode(), this.ConsumerKey, this.ConsumerSecret);

            try
            {
                var contents = accessTokenUrl.GetStringFromUrl();
                var authInfo = JsonObject.Parse(contents);
                tokens.AccessTokenSecret = authInfo["access_token"];
                session.IsAuthenticated = true;
                authService.SaveSession(session, this.SessionExpiry);
                this.OnAuthenticated(authService, session, tokens, authInfo.ToDictionary());

                return authService.Redirect(session.ReferrerUrl.AddHashParam("s", "1"));
            }
            catch (WebException we)
            {
                var statusCode = ((HttpWebResponse)we.Response).StatusCode;
                if (statusCode == HttpStatusCode.BadRequest)
                {
                    return authService.Redirect(session.ReferrerUrl.AddHashParam("f", "AccessTokenFailed"));
                }
            }

            return null;
        }

        /// <summary>
        /// The load user provider.
        /// </summary>
        /// <param name="authSession">
        /// The authentication session.
        /// </param>
        /// <param name="tokens">
        /// The tokens.
        /// </param>
        public override void LoadUserOAuthProvider(IAuthSession authSession, IOAuthTokens tokens)
        {
            var userSession = authSession as AuthUserSession;
            if (userSession == null)
            {
                return;
            }

            userSession.DisplayName = tokens.DisplayName ?? userSession.DisplayName;
            userSession.FirstName = tokens.FirstName ?? userSession.FirstName;
            userSession.LastName = tokens.LastName ?? userSession.LastName;
            userSession.PrimaryEmail = tokens.Email ?? userSession.PrimaryEmail ?? userSession.Email;
        }

        /// <summary>
        /// Load user authentication information.
        /// </summary>
        /// <param name="userSession">
        /// The user session.
        /// </param>
        /// <param name="tokens">
        /// The tokens.
        /// </param>
        /// <param name="authInfo">
        /// The authentication information.
        /// </param>
        protected override void LoadUserAuthInfo(
            AuthUserSession userSession, IOAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            var url = PeopleDataUrl + "oauth2_access_token={0}";
            url = url.Fmt(authInfo["access_token"]);
            var json = url.GetStringFromUrl();

            var obj = JsonObject.Parse(json);
            tokens.UserId = obj.Get("id");
            tokens.UserName = obj.Get("emailAddress");
            tokens.DisplayName = obj.Get("formattedName");
            tokens.FirstName = obj.Get("firstName");
            tokens.LastName = obj.Get("lastName");
            tokens.Email = obj.Get("emailAddress");

            this.LoadUserOAuthProvider(userSession, tokens);
        }
    }
}