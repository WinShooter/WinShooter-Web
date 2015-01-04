namespace WinShooter.Api
{
    using System;
    using System.Linq;

    using log4net;

    using WinShooter.Database;
    using WinShooter.Logic.Authentication;
    using WinShooter.Logic.Authorization;

    public class CurrentUserController : BaseApiController
    {
        /// <summary>
        /// The rights helper.
        /// </summary>
        private readonly IRightsHelper rightsHelper;

        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        public CurrentUserController(IRightsHelper rightsHelper)
        {
            this.rightsHelper = rightsHelper;
            this.log = LogManager.GetLogger(this.GetType());
        }

        public CurrentUserController()
        {
            var dbSession = NHibernateHelper.OpenSession();
            this.rightsHelper = new RightsHelper(new Repository<UserRolesInfo>(dbSession), new Repository<RoleRightsInfo>(dbSession));
        }

        public CurrentUserResponse Get(CurrentUserRequest request)
        {
            try
            {
                if (this.Principal == null)
                {
                    var anonymousRights = new string[0];
                    if (request != null && !string.IsNullOrEmpty(request.CompetitionId))
                    {
                        anonymousRights = (from right in this.rightsHelper.GetRightsForCompetitionIdAndTheUser(Guid.Parse(request.CompetitionId))
                                           select right.ToString()).ToArray();
                    }

                    return new CurrentUserResponse
                    {
                        IsLoggedIn = false,
                        CompetitionRights = anonymousRights,
                        DisplayName = string.Empty,
                        Email = string.Empty,
                        HasAcceptedTerms = 0
                    };
                }

                this.rightsHelper.CurrentUser = this.Principal;
                User user;
                using (var dbSession = NHibernateHelper.OpenSession())
                {
                    var userManager = new UserManager(dbSession);
                    user = userManager.GetUser(this.Principal.Id);
                }

                var rights = new WinShooterCompetitionPermissions[0];
                if (request != null && !string.IsNullOrEmpty(request.CompetitionId))
                {
                    rights = this.rightsHelper.GetRightsForCompetitionIdAndTheUser(Guid.Parse(request.CompetitionId));
                }

                rights = this.rightsHelper.AddRightsWithNoDuplicate(rights, this.rightsHelper.GetSystemRightsForTheUser());

                return new CurrentUserResponse
                {
                    IsLoggedIn = true,
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    HasAcceptedTerms = user.HasAcceptedTerms,
                    CompetitionRights = (from right in rights select right.ToString()).ToArray()
                };
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("Exception: {0}", exception);
                throw;
            }
        }
    }
}
