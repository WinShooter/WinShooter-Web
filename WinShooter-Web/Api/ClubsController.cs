// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClubsController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The clubs API.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using log4net;

    using WinShooter.Database;
    using WinShooter.Logic;
    using WinShooter.Web.DataValidation;

    /// <summary>
    /// The clubs API.
    /// </summary>
    public class ClubsController : BaseApiController
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The logic.
        /// </summary>
        private readonly ClubsLogic logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClubsController"/> class.
        /// </summary>
        public ClubsController()
        {
            this.log = LogManager.GetLogger(this.GetType());
            this.logic = new ClubsLogic(this.DatabaseSession);
        }

        private string AdminBaseLink
        {
            get
            {
                return this.GetApplicationPath() + "Home/Users/";
            }
        }

        private string NoAdminLink
        {
            get
            {
                return this.GetApplicationPath() + "Help/ClubOwnerMissing";
            }
        }

        /// <summary>
        /// The any.
        /// </summary>
        /// <returns>
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        [HttpGet]
        public List<ClubResponse> Get()
        {
            try
            {
                this.log.DebugFormat("Got GET request.");
                this.logic.CurrentUser = this.Principal;

                var clubs = this.logic.GetClubs();

                return clubs
                    .Select(club => new ClubResponse(club, this.AdminBaseLink, this.NoAdminLink))
                    .OrderBy(club => club.Name)
                    .ToList();
            }
            catch (Exception exception)
            {
                this.log.Error("An exception occurred: " + exception);
                throw;
            }
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="ClubResponse"/>.
        /// </returns>
        [Authorize]
        [HttpPost]
        public ClubResponse Post(ClubResponse request)
        {
            try
            {
                this.log.Debug("Got POST request: " + request);

                if (this.Principal == null)
                {
                    // This really shouldn't happen since we have attributed for authenticate
                    this.log.Error("Session is null in POST request.");
                    throw new Exception("You need to authenticate");
                }

                this.logic.CurrentUser = this.Principal;
                this.log.Debug("User is " + this.logic.CurrentUser);

                var club = this.logic.GetClub(request.ClubId);

                if (club == null)
                {
                    this.log.Debug("Adding a club.");

                    var newClub = request.UpdateClub(new Club());
                    newClub.AdminUser = this.GetUser();

                    var addedClub = this.logic.AddClub(newClub);

                    return new ClubResponse(addedClub, this.AdminBaseLink, this.NoAdminLink);
                }

                this.log.Debug("Updating a club.");
                request.UpdateClub(club);
                var updatedClub = this.logic.UpdateClub(club);
                return new ClubResponse(
                    updatedClub,
                    this.AdminBaseLink,
                    this.NoAdminLink);
            }
            catch (Exception exception)
            {
                this.log.Error("An exception occurred: " + exception);
                throw;
            }
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="clubId">
        /// The clubId to delete.
        /// </param>
        /// <returns>
        /// The <see cref="ClubResponse"/>.
        /// </returns>
        [Authorize]
        [HttpDelete]
        public ClubResponse Delete(string clubId)
        {
            try
            {
                this.log.DebugFormat("Got DELETE request for club \"{0}\".", clubId);
                clubId.Require("clubId").NotNull().IsGuid();

                if (this.Principal == null)
                {
                    // This really shouldn't happen since we have attributed for authenticate
                    this.log.Error("Session is null in POST request.");
                    throw new Exception("You need to authenticate");
                }

                this.logic.CurrentUser = this.Principal;
                this.log.Debug("User is " + this.logic.CurrentUser);

                var clubGuid = Guid.Parse(clubId);

                if (clubGuid.Equals(Guid.Empty))
                {
                    throw new Exception("There has to be a patrol id");
                }

                try
                {
                    this.logic.DeleteClub(clubGuid);
                }
                catch (DependencysExistException exception)
                {
                    // TODO send this to the client
                }

                // This is needed to get IE to be happy.
                return new ClubResponse();
            }
            catch (Exception exception)
            {
                this.log.Error("An exception occurred: " + exception);
                throw;
            }
        }
    }
}
