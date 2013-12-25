namespace WinShooter.Api.Tests.Dummys
{
    using System.Collections.Generic;

    using ServiceStack.ServiceInterface.Auth;

    class DummyUserAuthRepository : IUserAuthRepository
    {
        public UserAuth CreateUserAuth(UserAuth newUser, string password)
        {
            throw new System.NotImplementedException();
        }

        public UserAuth UpdateUserAuth(UserAuth existingUser, UserAuth newUser, string password)
        {
            throw new System.NotImplementedException();
        }

        public UserAuth GetUserAuthByUserName(string userNameOrEmail)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAuthenticate(string userName, string password, out UserAuth userAuth)
        {
            throw new System.NotImplementedException();
        }

        public bool TryAuthenticate(
            Dictionary<string, string> digestHeaders,
            string privateKey,
            int nonceTimeOut,
            string sequence,
            out UserAuth userAuth)
        {
            throw new System.NotImplementedException();
        }

        public void LoadUserAuth(IAuthSession session, IOAuthTokens tokens)
        {
            throw new System.NotImplementedException();
        }

        public UserAuth GetUserAuth(string userAuthId)
        {
            throw new System.NotImplementedException();
        }

        public void SaveUserAuth(IAuthSession authSession)
        {
            throw new System.NotImplementedException();
        }

        public void SaveUserAuth(UserAuth userAuth)
        {
            throw new System.NotImplementedException();
        }

        public List<UserOAuthProvider> GetUserOAuthProviders(string userAuthId)
        {
            throw new System.NotImplementedException();
        }

        public UserAuth GetUserAuth(IAuthSession authSession, IOAuthTokens tokens)
        {
            throw new System.NotImplementedException();
        }

        public string CreateOrMergeAuthSession(IAuthSession authSession, IOAuthTokens tokens)
        {
            throw new System.NotImplementedException();
        }
    }
}
