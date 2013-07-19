// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using NHibernate.Linq;

    using WinShooter.Database;

    using log4net;

    using Microsoft.Web.WebPages.OAuth;

    using WebMatrix.WebData;

    using WinShooter.Models;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            this.log = LogManager.GetLogger(this.GetType());
        }

        /// <summary>
        /// Called by GET: /Home/
        /// </summary>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return this.RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Called by GET: /Home/?returnUrl=...
        /// </summary>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return this.View(OAuthWebSecurity.RegisteredClientData);
        }

        /// <summary>
        /// Called by POST: /Account/LogOff
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Called by POST: /Account/ExternalLogin
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }
        
        /// <summary>
        /// Called by GET: /Account/ExternalLoginCallback.
        /// </summary>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl)
        {
            var result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful)
            {
                return this.RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
            {
                return this.RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated)
            {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return this.RedirectToLocal(returnUrl);
            }

            // User is new, ask for their desired membership name
            var loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
            this.ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
            this.ViewBag.ReturnUrl = returnUrl;
            return this.View("ExternalLoginConfirmation", new RegisterExternalLoginModel { Email = result.UserName, ExternalLoginData = loginData });
        }

        /// <summary>
        /// Called by GET: /Account/ExternalLoginFailure
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            this.log.Info("External login failed.");
            return this.View();
        }

        /// <summary>
        /// Called by POST: /Account/ExternalLoginConfirmation.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
        {
            string provider;
            string providerUserId;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
            {
                return this.RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Insert the new user into the database
                using (var session = NHibernateHelper.OpenSession())
                {
                    var userLoginInfo = (from info in session.Query<UserLoginInfo>()
                                         where
                                             info.IdentityProvider == provider && info.IdentityProviderId == providerUserId
                                         select info).SingleOrDefault();
                    if (userLoginInfo == null)
                    {
                        using (var transaction = session.BeginTransaction())
                        {
                            var newUser = new User
                                              {
                                                  // TODO Add ClubId
                                                  CardNumber = model.ShooterCardNumber,
                                                  Surname = model.Surname,
                                                  Givenname = model.GivenName,
                                                  Email = model.Email,
                                                  LastLogin = DateTime.Now,
                                                  LastUpdated = DateTime.Now
                                              };
                            var newUserLoginInfo = new UserLoginInfo
                                                       {
                                                           IdentityProvider = provider,
                                                           IdentityProviderId = providerUserId,
                                                           LastLogin = DateTime.Now,
                                                           User = newUser
                                                       };
                            session.Save(newUser);
                            session.Save(newUserLoginInfo);
                            transaction.Commit();
                        }
                    }
                }

                OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.Email);
                OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                return this.RedirectToLocal(returnUrl);
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return this.View(model);
        }

        /// <summary>
        /// Redirects to a local action.
        /// </summary>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// The external login result.
        /// </summary>
        internal class ExternalLoginResult : ActionResult
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="ExternalLoginResult"/> class.
            /// </summary>
            /// <param name="provider">
            /// The provider.
            /// </param>
            /// <param name="returnUrl">
            /// The return url.
            /// </param>
            public ExternalLoginResult(string provider, string returnUrl)
            {
                this.Provider = provider;
                this.ReturnUrl = returnUrl;
            }

            /// <summary>
            /// Gets the provider.
            /// </summary>
            public string Provider { get; private set; }

            /// <summary>
            /// Gets the return url.
            /// </summary>
            public string ReturnUrl { get; private set; }

            /// <summary>
            /// The execute result.
            /// </summary>
            /// <param name="context">
            /// The context.
            /// </param>
            public override void ExecuteResult(ControllerContext context)
            {
                OAuthWebSecurity.RequestAuthentication(this.Provider, this.ReturnUrl);
            }
        }
    }
}
