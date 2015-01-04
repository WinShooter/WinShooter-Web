// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatrolsController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The patrols API.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using log4net;

    using WinShooter.Logic;
    using WinShooter.Web.DataValidation;

    /// <summary>
    /// The stations service.
    /// </summary>
    public class PatrolsController : BaseApiController
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The logic.
        /// </summary>
        private readonly PatrolsLogic logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatrolsController"/> class.
        /// </summary>
        public PatrolsController()
        {
            this.log = LogManager.GetLogger(this.GetType());
            this.logic = new PatrolsLogic(this.DatabaseSession);
        }

        /// <summary>
        /// The any.
        /// </summary>
        /// <param name="competitionId">
        /// The requested competition ID.
        /// </param>
        /// <returns>
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        [HttpGet]
        public List<PatrolResponse> Get(string competitionId)
        {
            try
            {
                this.log.DebugFormat("Got GET request for competitionId \"{0}\".", competitionId);
                competitionId.Require("competitionId").NotNull().IsGuid();

                this.logic.CurrentUser = this.Principal;

                var stations = this.logic.GetPatrols(Guid.Parse(competitionId));

                var responses = (from dbstation in stations
                                 select new PatrolResponse(dbstation)).ToList().OrderBy(x => x.PatrolNumber);

                return responses.ToList();
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("Exception while retrieving patrols from request ({0}): {1}", competitionId, exception);
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
        /// The <see cref="PatrolResponse"/>.
        /// </returns>
        [Authorize]
        [HttpPost]
        public PatrolResponse Post(PatrolRequest request)
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

            if (string.IsNullOrEmpty(request.PatrolId))
            {
                this.log.Debug("Adding a patrol.");

                return new PatrolResponse(
                    this.logic.AddPatrol(request.CompetitionId));
            }

            this.log.Debug("Updating a patrol.");
            return new PatrolResponse(
                this.logic.UpdatePatrol(
                request.CompetitionId,
                request.GetDatabasePatrol()));
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="patrolId">
        /// The patrolId to delete.
        /// </param>
        /// <returns>
        /// The <see cref="PatrolResponse"/>.
        /// </returns>
        [Authorize]
        [HttpDelete]
        public PatrolResponse Delete(string patrolId)
        {
            this.log.DebugFormat("Got DELETE request for station \"{0}\".", patrolId);
            patrolId.Require("patrolId").NotNull().IsGuid();

            if (this.Principal == null)
            {
                // This really shouldn't happen since we have attributed for authenticate
                this.log.Error("Session is null in POST request.");
                throw new Exception("You need to authenticate");
            }

            this.logic.CurrentUser = this.Principal;
            this.log.Debug("User is " + this.logic.CurrentUser);

            var patrolGuid = Guid.Parse(patrolId);

            if (patrolGuid.Equals(Guid.Empty))
            {
                throw new Exception("There has to be a patrol id");
            }

            this.logic.DeletePatrol(patrolGuid);

            // This is needed to get IE to be happy.
            return new PatrolResponse();
        }
    }
}
