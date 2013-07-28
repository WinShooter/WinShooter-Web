// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomUserSession.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Create your own strong-typed Custom AuthUserSession where you can add additional AuthUserSession
//   fields required for your application. The base class is automatically populated with
//   User Data as and when they authenticate with your application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate.Linq;

    using ServiceStack.ServiceInterface;
    using ServiceStack.ServiceInterface.Auth;

    using WinShooter.Database;

    /// <summary>
    /// Create your own strong-typed Custom <see cref="AuthUserSession"/> where you can add additional <see cref="AuthUserSession"/> 
    /// fields required for your application. The base class is automatically populated with 
    /// User Data as and when they authenticate with your application. 
    /// </summary>
    public class CustomUserSession : AuthUserSession
    {
        /// <summary>
        /// Gets or sets the authenticated user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Called when the user authenticates.
        /// </summary>
        /// <param name="authService">
        /// The authentication service.
        /// </param>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="tokens">
        /// The tokens.
        /// </param>
        /// <param name="authInfo">
        /// The authentication info.
        /// </param>
        public override void OnAuthenticated(IServiceBase authService, IAuthSession session, IOAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            base.OnAuthenticated(authService, session, tokens, authInfo);

            var userLoginInfos = new List<UserLoginInfo>();

            using (var dbsession = NHibernateHelper.OpenSession())
            {
                foreach (var token in session.ProviderOAuthAccess)
                {
                    var userLoginInfo = (from info in dbsession.Query<UserLoginInfo>()
                                         where
                                             info.IdentityProvider == token.Provider
                                             && info.IdentityProviderId == token.UserId
                                         select info).SingleOrDefault();

                    if (userLoginInfo == null)
                    {
                        continue;
                    }

                    userLoginInfos.Add(userLoginInfo);

                    userLoginInfo.LastLogin = DateTime.Now;

                    using (var transaction = dbsession.BeginTransaction())
                    {
                        dbsession.Update(userLoginInfo);
                        transaction.Commit();
                    }
                }
            }

            UserLoginInfo currentUserLoginInfo;
            if (userLoginInfos.Count == 0)
            {
                currentUserLoginInfo = CreateNewUser(session.ProviderOAuthAccess);
            }
            else
            {
                currentUserLoginInfo = userLoginInfos[0];
            }

            session.UserAuthId = currentUserLoginInfo.User.Id.ToString();
            session.UserAuthName = currentUserLoginInfo.User.Email;

            this.User = currentUserLoginInfo.User;

            if (WinShooterApiHost.AppConfig.AdminUserNames.Contains(currentUserLoginInfo.User.Email)
                && !session.HasRole(RoleNames.Admin))
            {
                using (var assignRoles = authService.ResolveService<AssignRolesService>())
                {
                    assignRoles.Post(new AssignRoles
                    {
                        UserName = session.UserAuthName,
                        Roles = { RoleNames.Admin }
                    });
                }
            }

            // Resolve the DbFactory from the IOC and persist the user info
            // authService.TryResolve<IDbConnectionFactory>().Run(db => db.Save(user));
        }

        /// <summary>
        /// Crates a  new user.
        /// </summary>
        /// <param name="providerOAuthAccess">
        /// The provider information.
        /// </param>
        /// <returns>
        /// The <see cref="UserLoginInfo"/>.
        /// </returns>
        private static UserLoginInfo CreateNewUser(List<IOAuthTokens> providerOAuthAccess)
        {
            if (providerOAuthAccess.Count == 0)
            {
                throw new ArgumentOutOfRangeException("providerOAuthAccess");
            }

            UserLoginInfo currentUserLoginInfo = null;
            string userDisplayName = null;
            string firstName = null;
            string lastName = null;
            using (var dbsession = NHibernateHelper.OpenSession())
            {
                using (var transaction = dbsession.BeginTransaction())
                {
                    var user = new User { LastLogin = DateTime.Now, LastUpdated = DateTime.Now };

                    dbsession.SaveOrUpdate(user);

                    foreach (var authToken in providerOAuthAccess)
                    {
                        var userLoginInfo = new UserLoginInfo
                                                {
                                                    User = user,
                                                    LastLogin = DateTime.Now,
                                                    IdentityProvider = authToken.Provider,
                                                    IdentityProviderId = authToken.UserId,
                                                    IdentityProviderUsername = authToken.Email
                                                };
                        currentUserLoginInfo = userLoginInfo;

                        user.Email = authToken.Email;

                        if (authToken.DisplayName != null && userDisplayName == null)
                        {
                            userDisplayName = authToken.DisplayName;
                        }

                        if (authToken.FirstName != null && firstName == null)
                        {
                            firstName = authToken.FirstName;
                        }

                        if (authToken.LastName != null && lastName == null)
                        {
                            lastName = authToken.LastName;
                        }

                        dbsession.SaveOrUpdate(userLoginInfo);
                    }

                    dbsession.SaveOrUpdate(user);
                    transaction.Commit();
                }
            }

            return currentUserLoginInfo;
        }
    }
}