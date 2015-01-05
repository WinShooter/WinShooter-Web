// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserManager.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The user manager.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate;

    using WinShooter.Database;
    using WinShooter.Web.DataValidation;

    /// <summary>
    /// The user manager.
    /// </summary>
    public class UserManager
    {
        private readonly ISession dbSession;

        public UserManager(ISession dbSession)
        {
            this.dbSession = dbSession;
        }

        public User GetUser(string uniqueId, string provider, string email, DateTime loginTime)
        {
            var userLoginInfoRepository = new Repository<UserLoginInfo>(this.dbSession);

            var loginInfo =
                userLoginInfoRepository.FilterBy(
                    userLoginInfo =>
                    userLoginInfo.ProviderUserId == uniqueId && userLoginInfo.IdentityProvider == provider)
                    .FirstOrDefault();

            var userEmails = new string[] { email };
            if (loginInfo != null)
            {
                userEmails = loginInfo.User.LoginInfos.Select(userLoginInfo => userLoginInfo.Email).ToArray();
            }

            var user = loginInfo == null ? this.CreateDefaultUser(uniqueId, provider, email, loginTime) : loginInfo.User;

            return ComplementSettingsAdmin(user, userEmails);
        }

        public User CreateDefaultUser(string uniqueId, string provider, string email, DateTime loginTime)
        {
            using (var transaction = this.dbSession.BeginTransaction())
            {
                var userLoginInfoRepository = new Repository<UserLoginInfo>(this.dbSession);
                var userRepository = new Repository<User>(this.dbSession);

                var user = new User
                {
                    Email = email, 
                    LastLogin = loginTime, 
                    LastUpdated = loginTime
                };

                var userLoginInfo = new UserLoginInfo
                {
                    IdentityProvider = provider,
                    Email = email,
                    ProviderUserId = uniqueId,
                    LastLogin = loginTime,
                    User = user
                };

                userRepository.Add(user);
                userLoginInfoRepository.Add(userLoginInfo);

                transaction.Commit();

                return user;
            }
        }

        public User GetUser(Guid userId)
        {
            var userRepository = new Repository<User>(this.dbSession);

            var user = userRepository.FilterBy(
                    userInfo =>
                    userInfo.Id == userId)
                    .FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            var userEmails = user.LoginInfos.Select(info => info.Email).ToArray();

            return ComplementSettingsAdmin(user, userEmails);
        }

        private static User ComplementSettingsAdmin(User user, IEnumerable<string> userLoginEmails)
        {
            if (user == null)
            {
                return null;
            }

            if (userLoginEmails == null)
            {
                return user;
            }

            var config = new AppConfig();

            if (userLoginEmails.Any(email => email != null && config.IsAdminUser(email)))
            {
                user.IsSystemAdmin = true;
            }

            return user;
        }

        public void UpdateUser(User user)
        {
            user.Require("user").NotNull();

            var userRepository = new Repository<User>(this.dbSession);

            using (var transaction = this.dbSession.BeginTransaction())
            {
                userRepository.Update(user);
                transaction.Commit();
            }
        }
    }
}
