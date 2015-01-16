// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentUserController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The current user controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

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
    /// The current user controller.
    /// </summary>
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="CurrentUserController" /> class.
        /// </summary>
        /// <param name="rightsHelper">
        ///     A rights helper to use.
        /// </param>
        public CurrentUserController(IRightsHelper rightsHelper)
        {
            this.rightsHelper = rightsHelper;
            this.log = LogManager.GetLogger(this.GetType());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserController" /> class.
        /// </summary>
        public CurrentUserController()
        {
            var databaseSession = NHibernateHelper.OpenSession();
            this.rightsHelper = new RightsHelper(
                new Repository<UserRolesInfo>(databaseSession), 
                new Repository<RoleRightsInfo>(databaseSession));
        }

        /// <summary>
        /// Gets the current user information.
        /// </summary>
        /// <returns>The current user information.</returns>
        //[HttpGet]
        //public CurrentUserResponse Get()
        //{
        //    return this.Get(null);
        //}

        /// <summary>
        /// Gets the current user information.
        /// </summary>
        /// <param name="currentUserRequest">The competition request</param>
        /// <returns>The current user information.</returns>
        [HttpGet]
        public CurrentUserResponse Get([FromUri]CurrentUserRequest currentUserRequest)
        {
            try
            {
                if (this.Principal == null)
                {
                    var anonymousRights = new string[0];
                    if (currentUserRequest != null && !string.IsNullOrEmpty(currentUserRequest.CompetitionId))
                    {
                        anonymousRights = (from right in this.rightsHelper.GetRightsForCompetitionIdAndTheUser(Guid.Parse(currentUserRequest.CompetitionId))
                                           select right.ToString()).ToArray();
                    }

                    return new CurrentUserResponse(anonymousRights);
                }

                this.rightsHelper.CurrentUser = this.Principal;
                User user;
                using (var databaseSession = NHibernateHelper.OpenSession())
                {
                    var userManager = new UserManager(databaseSession);
                    user = userManager.GetUser(this.Principal.UserId);
                }

                var rights = new WinShooterCompetitionPermissions[0];
                if (!string.IsNullOrEmpty(currentUserRequest.CompetitionId))
                {
                    rights = this.rightsHelper.GetRightsForCompetitionIdAndTheUser(Guid.Parse(currentUserRequest.CompetitionId));
                }

                if (!string.IsNullOrEmpty(currentUserRequest.ClubId))
                {
                    var clubGuid = Guid.Parse(currentUserRequest.ClubId);
                    var clubRepository = new Repository<Club>(this.DatabaseSession);
                    var club = clubRepository.FilterBy(dbclub => dbclub.Id.Equals(clubGuid)).FirstOrDefault();
                    rights = this.rightsHelper.AddRightsWithNoDuplicate(
                        rights,
                        this.rightsHelper.GetClubRightsForTheUser(club));
                }

                rights = this.rightsHelper.AddRightsWithNoDuplicate(
                    rights, 
                    this.rightsHelper.GetSystemRightsForTheUser());

                return new CurrentUserResponse(user, rights.Select(right => right.ToString()).ToArray());
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("Exception: {0}", exception);
                throw;
            }
        }

        [Authorize]
        [HttpPost]
        public CurrentUserResponse Post([FromBody]CurrentUserResponse request)
        {
            request.Require("request").NotNull();
            request.UserId.Require("UserId").NotNull().IsGuid();

            if (this.Principal == null)
            {
                throw new Exception("You need to be authenticated.");
            }

            if (request.UserId != this.Principal.UserId.ToString())
            {
                throw new Exception("Only the logged in user can be edited.");
            }

            var requestUserGuid = Guid.Parse(request.UserId);

            var userRepository = new Repository<User>(this.DatabaseSession);
            var user = userRepository.FilterBy(dbUser => dbUser.Id.Equals(requestUserGuid)).FirstOrDefault();

            if (user != null)
            {
                using (var transaction = this.DatabaseSession.BeginTransaction())
                {
                    user.HasAcceptedTerms = request.HasAcceptedTerms;
                    transaction.Commit();
                }

                return request;
            }

            this.log.ErrorFormat("Could not find the logged in user {0}.", request.UserId);
            throw new Exception("There is no such user.");
        }
    }
}
