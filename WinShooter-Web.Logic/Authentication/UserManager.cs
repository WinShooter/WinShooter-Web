namespace WinShooter.Logic.Authentication
{
    using System;
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

            var loginInfos =
                userLoginInfoRepository.FilterBy(
                    userLoginInfo =>
                    userLoginInfo.ProviderUserId == uniqueId && userLoginInfo.IdentityProvider == provider)
                    .FirstOrDefault();

            return loginInfos == null ? this.CreateDefaultUser(uniqueId, provider, email, loginTime) : loginInfos.User;
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
    }
}
