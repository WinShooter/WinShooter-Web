namespace WinShooter.Api
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using log4net;

    using WinShooter.Database;
    using WinShooter.Logic.Authentication;
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
        public UserResponse[] Get()
        {
            try
            {
                if (this.Principal == null)
                {
                    throw new Exception("You must be authenticated.");
                }

                var rightsHelper = new RightsHelper(
                    new Repository<UserRolesInfo>(this.DatabaseSession),
                    new Repository<RoleRightsInfo>(this.DatabaseSession)) { CurrentUser = this.Principal };

                if (!rightsHelper.GetSystemRightsForTheUser()
                         .Any(right => right == WinShooterCompetitionPermissions.ReadUser))
                {
                    throw new NotEnoughRightsException("User is missing ReadUser right.");
                }

                var userRepository = new Repository<User>(this.DatabaseSession);
                return userRepository.All().Select(user => new UserResponse(user)).ToArray();
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

                var userManager = new UserManager(this.DatabaseSession);
                if (userGuid.Equals(this.Principal.UserId))
                {
                    return new UserResponse(userManager.GetUser(userGuid));
                }

                var rightsHelper = new RightsHelper(
                    new Repository<UserRolesInfo>(this.DatabaseSession),
                    new Repository<RoleRightsInfo>(this.DatabaseSession)) { CurrentUser = this.Principal };

                if (!rightsHelper.GetSystemRightsForTheUser()
                        .Any(right => right == WinShooterCompetitionPermissions.ReadUser))
                {
                    throw new NotEnoughRightsException("User don't have the right to read user.");
                }

                var requestedUser = userManager.GetUser(userGuid);

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

        [Authorize]
        public UserResponse Post([FromBody]UserResponse request)
        {
            try
            {
                request.Require("request").NotNull();

                if (this.Principal == null)
                {
                    throw new Exception("You must be authenticated.");
                }

                var userGuid = Guid.Parse(request.UserId);

                var userManager = new UserManager(this.DatabaseSession);
                if (userGuid.Equals(this.Principal.UserId))
                {
                    var user = userManager.GetUser(userGuid);
                    user.CardNumber = request.CardNumber;
                    user.Email = request.Email;
                    user.Givenname = request.Givenname;
                    user.Surname = request.Surname;
                    user.HasAcceptedTerms = request.HasAcceptedTerms;
                    user.LastUpdated = DateTime.Now;

                    userManager.UpdateUser(user);

                    return new UserResponse(userManager.GetUser(userGuid));
                }

                var rightsHelper = new RightsHelper(
                    new Repository<UserRolesInfo>(this.DatabaseSession),
                    new Repository<RoleRightsInfo>(this.DatabaseSession)) { CurrentUser = this.Principal };

                if (!rightsHelper.GetSystemRightsForTheUser()
                        .Any(right => right == WinShooterCompetitionPermissions.ReadUser))
                {
                    throw new NotEnoughRightsException("User don't have the right to read user.");
                }

                var requestedUser = userManager.GetUser(userGuid);

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