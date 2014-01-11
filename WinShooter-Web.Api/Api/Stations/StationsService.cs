// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationsService.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The competitions service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api.Stations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using log4net;

    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Api.Competition;
    using WinShooter.Api.Authentication;
    using WinShooter.Database;
    using WinShooter.Logic;

    /// <summary>
    /// The stations service.
    /// </summary>
    public class StationsService : Service
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The database session.
        /// </summary>
        private readonly NHibernate.ISession databaseSession;

        /// <summary>
        /// The logic.
        /// </summary>
        private readonly StationsLogic logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="StationsService"/> class.
        /// </summary>
        public StationsService()
        {
            this.log = LogManager.GetLogger(this.GetType());
            this.databaseSession = NHibernateHelper.OpenSession();
            this.logic = new StationsLogic(this.databaseSession);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public sealed override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The any.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        public List<StationResponse> Get(StationRequest request)
        {
            try
            {
                this.log.DebugFormat("Got GET request: {0}.", request);
                var authSession = this.GetSession() as CustomUserSession;

                this.logic.CurrentUser = authSession == null ? null : authSession.User;

                var stations = this.logic.GetStations(request.CompetitionId);

                var responses = (from dbstation in stations
                                 select new StationResponse(dbstation)).ToList();

                return responses;
            }
            catch (Exception exception)
            {
                this.log.ErrorFormat("Exception while retrieving stations from request ({0}): {1}", request, exception);
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
        [Authenticate]
        public StationResponse Post(StationRequest request)
        {
            this.log.Debug("Got POST request: " + request);
            var session = this.GetSession() as CustomUserSession;
            if (session == null)
            {
                // This really shouldn't happen since we have attributed for authenticate
                this.log.Error("Session is null in POST request.");
                throw new Exception("You need to authenticate");
            }

            this.logic.CurrentUser = session.User;
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
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            this.databaseSession.Dispose();

            base.Dispose();
        }
    }
}
