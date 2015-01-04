namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate;

    using WinShooter.Database;

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
    }
}
