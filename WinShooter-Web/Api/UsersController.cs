namespace WinShooter.Api
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using log4net;

    using WinShooter.Database;
    using WinShooter.Logic.Authorization;
    using WinShooter.Web.DataValidation;

    /// <summary>
    /// The API for updating users.
    /// </summary>
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        public UsersController()
        {
            this.log = LogManager.GetLogger(this.GetType());
        }

        [Authorize]
        public UserResponse Get()
        {
            try
            {
                if (this.Principal == null)
                {
                    throw new Exception("You must be authenticated.");
                }

                return this.Get(this.Principal.UserId.ToString());
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("Exception: " + exception);
                throw;
            }
        }

        [Authorize]
        public UserResponse Get(string userId)
        {
            try
            {
                userId.Require("userId").NotNull().IsGuid();

                if (this.Principal == null)
                {
                    throw new Exception("You must be authenticated.");
                }

                var userGuid = Guid.Parse(userId);
                if (userGuid.Equals(this.Principal.UserId))
                {
                    return new UserResponse(this.GetUser());
                }

                var rightsHelper = new RightsHelper(
                    new Repository<UserRolesInfo>(this.DatabaseSession),
                    new Repository<RoleRightsInfo>(this.DatabaseSession));

                if (!rightsHelper.GetSystemRightsForTheUser()
                        .Any(right => right == WinShooterCompetitionPermissions.ReadUser))
                {
                    throw new NotEnoughRightsException("User don't have the right to read user.");
                }

                var userRepositor = new Repository<User>(this.DatabaseSession);

                var requestedUser = userRepositor.FilterBy(user => user.Id.Equals(userGuid)).FirstOrDefault();

                if (requestedUser == null)
                {
                    throw new Exception("There is no such user");
                }

                return new UserResponse(requestedUser);
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("Exception: " + exception);
                throw;
            }
        }
    }
}