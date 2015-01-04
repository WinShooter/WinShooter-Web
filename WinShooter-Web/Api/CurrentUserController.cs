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
    using System.Web.Mvc;

    using log4net;

    using WinShooter.Database;
    using WinShooter.Logic.Authentication;
    using WinShooter.Logic.Authorization;

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
        [HttpPost]
        [HttpGet]
        public CurrentUserResponse Get()
        {
            return this.Get(null);
        }

        /// <summary>
        /// Gets the current user information.
        /// </summary>
        /// <param name="competitionId">The competition request</param>
        /// <returns>The current user information.</returns>
        [HttpPost]
        [HttpGet]
        public CurrentUserResponse Get(string competitionId)
        {
            try
            {
                if (this.Principal == null)
                {
                    var anonymousRights = new string[0];
                    if (!string.IsNullOrEmpty(competitionId))
                    {
                        anonymousRights = (from right in this.rightsHelper.GetRightsForCompetitionIdAndTheUser(Guid.Parse(competitionId))
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
                using (var databaseSession = NHibernateHelper.OpenSession())
                {
                    var userManager = new UserManager(databaseSession);
                    user = userManager.GetUser(this.Principal.UserId);
                }

                var rights = new WinShooterCompetitionPermissions[0];
                if (!string.IsNullOrEmpty(competitionId))
                {
                    rights = this.rightsHelper.GetRightsForCompetitionIdAndTheUser(Guid.Parse(competitionId));
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
