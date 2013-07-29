namespace WinShooter.Api
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
    /// The linkedin auth provider, <see cref="http://www.binoot.com/2013/03/30/linkedin-provider-for-servicestack-authentication/"/>
    /// </summary>
    public class LinkedinAuthProvider : OAuthProvider
    {
        public const string Name = "linkedin";

        public static string Realm = "https://www.linkedin.com/uas/oauth2/";

        public static string PeopleDataUrl =
            "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,formatted-name,industry,email-address)?format=json&";

        public LinkedinAuthProvider(IResourceManager appSettings)
            : base(appSettings, Realm, Name)
        {
        }

        public override object Authenticate(
            IServiceBase authService, IAuthSession session, ServiceStack.ServiceInterface.Auth.Auth request)
        {
            var tokens = Init(authService, ref session, request);
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
                    ConsumerKey,
                    "r_fullprofile%20r_emailaddress",
                    Guid.NewGuid().ToString(),
                    this.CallbackUrl.UrlEncode());

                authService.SaveSession(session, SessionExpiry);
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
                authService.SaveSession(session, SessionExpiry);
                OnAuthenticated(authService, session, tokens, authInfo.ToDictionary());


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

            LoadUserOAuthProvider(userSession, tokens);
        }

        public override void LoadUserOAuthProvider(IAuthSession authSession, IOAuthTokens tokens)
        {
            var userSession = authSession as AuthUserSession;
            if (userSession == null) return;

            userSession.DisplayName = tokens.DisplayName ?? userSession.DisplayName;
            userSession.FirstName = tokens.FirstName ?? userSession.FirstName;
            userSession.LastName = tokens.LastName ?? userSession.LastName;
            userSession.PrimaryEmail = tokens.Email ?? userSession.PrimaryEmail ?? userSession.Email;
        }
    }
}