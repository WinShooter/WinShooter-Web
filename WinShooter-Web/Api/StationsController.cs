// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationsController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The stations API.
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
    /// The stations API.
    /// </summary>
    public class StationsController : BaseApiController
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The logic.
        /// </summary>
        private readonly StationsLogic logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="StationsController"/> class.
        /// </summary>
        public StationsController()
        {
            this.log = LogManager.GetLogger(this.GetType());
            this.logic = new StationsLogic(this.DatabaseSession);
        }

        /// <summary>
        /// The any.
        /// </summary>
        /// <param name="competitionId">
        /// The competition id.
        /// </param>
        /// <returns>
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        [HttpGet]
        public List<StationResponse> Get(string competitionId)
        {
            try
            {
                this.log.DebugFormat("Got GET request with competitionId=\"{0}\".", competitionId);
                competitionId.Require("competitonId").NotNull().IsGuid();

                var competitionGuid = Guid.Parse(competitionId);

                this.logic.CurrentUser = this.Principal;

                var stations = this.logic.GetStations(competitionGuid);

                var responses = (from dbstation in stations
                                 select new StationResponse(dbstation)).ToList().OrderBy(x => x.StationNumber);

                return responses.ToList();
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("Exception while retrieving stations from request ({0}): {1}", competitionId, exception);
                throw;
            }
        } 
        
        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        [Authorize]
        [HttpPost]
        public StationResponse Post(StationRequest request)
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

            if (string.IsNullOrEmpty(request.StationId))
            {
                this.log.Debug("Adding a station.");

                return new StationResponse(
                    this.logic.AddStation(request.CompetitionId, request.GetDatabaseStation()));
            }

            this.log.Debug("Updating a station.");
            return new StationResponse(
                this.logic.UpdateStation(
                request.CompetitionId,
                request.GetDatabaseStation()));
        }

        /// <summary>
        ///     Delete a station.
        /// </summary>
        /// <param name="stationId">
        ///     The id of the station to delete.
        /// </param>
        /// <returns>
        ///     The <see cref="StationResponse"/>.
        /// </returns>
        [Authorize]
        [HttpDelete]
        public StationResponse Delete(string stationId)
        {
            this.log.DebugFormat("Got DELETE request. stationId=\"{0}\".", stationId);

            stationId.Require("stationId").NotNull().IsGuid();

            if (this.Principal == null)
            {
                // This really shouldn't happen since we have attributed for authenticate
                this.log.Error("Session is null in POST request.");
                throw new Exception("You need to authenticate");
            }

            this.logic.CurrentUser = this.Principal;
            this.log.Debug("User is " + this.logic.CurrentUser);

            var stationGuid = Guid.Parse(stationId);

            if (stationGuid.Equals(Guid.Empty))
            {
                throw new Exception("There has to be a station id");
            }

            this.logic.DeleteStation(stationGuid);

            // This is needed to get IE to be happy.
            return new StationResponse();
        }
    }
}
