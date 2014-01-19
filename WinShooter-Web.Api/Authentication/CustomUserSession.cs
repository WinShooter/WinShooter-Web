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

    using NHibernate;
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
        /// The default session validity.
        /// </summary>
        private readonly TimeSpan defaultSessionValidity = new TimeSpan(1, 0, 0, 0);

        /// <summary>
        /// Gets or sets the authenticated user.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets the array of <see cref="UserLoginInfo"/>.
        /// </summary>
        public List<UserLoginInfo> UserLoginInfos { get; private set; }

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
                // Using all tokens, search for a user
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

                if (userLoginInfos.Count == 0)
                {
                    userLoginInfos.Add(CreateNewUser(dbsession, session.ProviderOAuthAccess));
                }

                var currentUserLoginInfo = userLoginInfos[0];
                this.User = currentUserLoginInfo.User;
                this.UserLoginInfos = userLoginInfos;

                session.UserAuthId = currentUserLoginInfo.User.Id.ToString();
                session.UserAuthName = currentUserLoginInfo.User.Email;

                if (session.ProviderOAuthAccess.Count != userLoginInfos.Count)
                {
                    // Add all provider infos to the same user
                    this.AddAllTokensToUser(dbsession, this.User, session.ProviderOAuthAccess);
                }

                if (WinShooterApiHost.AppConfig.IsAdminUser(currentUserLoginInfo.User.Email))
                {
                    this.User.IsSystemAdmin = true;
                }

                // Save the session
                authService.SaveSession(session, this.defaultSessionValidity);
            }
        }

        /// <summary>
        /// Crates a  new user.
        /// </summary>
        /// <param name="dbsession">Database session</param>
        /// <param name="providerOAuthAccess">
        ///     The provider information.
        /// </param>
        /// <returns>
        /// The <see cref="UserLoginInfo"/>.
        /// </returns>
        private static UserLoginInfo CreateNewUser(ISession dbsession, List<IOAuthTokens> providerOAuthAccess)
        {
            if (providerOAuthAccess.Count == 0)
            {
                throw new ArgumentOutOfRangeException("providerOAuthAccess");
            }

            UserLoginInfo currentUserLoginInfo = null;
            string userDisplayName = null;
            string firstName = null;
            string lastName = null;

            using (var transaction = dbsession.BeginTransaction())
            {
                var user = new User { LastLogin = DateTime.Now, LastUpdated = DateTime.Now };

                dbsession.Save(user);

                foreach (var authToken in providerOAuthAccess)
                {
                    var userLoginInfo = CreateLoginInfoFromAuthToken(authToken, user);
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

                    dbsession.Save(userLoginInfo);
                }

                transaction.Commit();
            }

            return currentUserLoginInfo;
        }

        /// <summary>
        /// Create a <see cref="UserLoginInfo"/> from a provider token.
        /// </summary>
        /// <param name="authToken">
        /// The token.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <returns>
        /// The <see cref="UserLoginInfo"/>.
        /// </returns>
        private static UserLoginInfo CreateLoginInfoFromAuthToken(IOAuthTokens authToken, User user)
        {
            return new UserLoginInfo
            {
                User = user,
                LastLogin = DateTime.Now,
                IdentityProvider = authToken.Provider,
                IdentityProviderId = authToken.UserId,
                IdentityProviderUsername = authToken.Email
            };
        }

        /// <summary>
        /// Make sure to have all tokens in database for the user.
        /// </summary>
        /// <param name="dbsession">
        /// The database session.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        /// <param name="providerTokens">
        /// The provider tokens.
        /// </param>
        private void AddAllTokensToUser(ISession dbsession, User user, IEnumerable<IOAuthTokens> providerTokens)
        {
            // Using all tokens, search for a user
            foreach (var authToken in providerTokens)
            {
                var userLoginInfo = (from info in dbsession.Query<UserLoginInfo>()
                                     where
                                         info.IdentityProvider == authToken.Provider
                                         && info.IdentityProviderId == authToken.UserId
                                     select info).SingleOrDefault();

                if (userLoginInfo != null)
                {
                    // A connection already exist
                    continue;
                }

                using (var transaction = dbsession.BeginTransaction())
                {
                    var newUserLoginInfo = CreateLoginInfoFromAuthToken(authToken, user);
                    dbsession.SaveOrUpdate(newUserLoginInfo);
                    transaction.Commit();
                    this.UserLoginInfos.Add(newUserLoginInfo);
                }
            }
        }
    }
}