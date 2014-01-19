// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionService.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The competition service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api.Competition
{
    using System;

    using log4net;

    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authentication;
    using WinShooter.Database;
    using WinShooter.Logic;

    /// <summary>
    /// The competition service.
    /// </summary>
    public class CompetitionService : Service
    {
        /// <summary>
        /// The database session.
        /// </summary>
        private readonly NHibernate.ISession databaseSession;

        /// <summary>
        /// The logic.
        /// </summary>
        private readonly CompetitionsLogic logic;

        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionService"/> class.
        /// </summary>
        public CompetitionService()
        {
            this.log = LogManager.GetLogger(this.GetType());
            this.databaseSession = NHibernateHelper.OpenSession();
            this.logic = new CompetitionsLogic(this.databaseSession);
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
        public CompetitionResponse Get(CompetitionRequest request)
        {
            try
            {
                this.log.Debug("Got GET request: " + request);
                var requestedCompetitionId = Guid.Parse(request.CompetitionId);

                var session = this.GetSession() as CustomUserSession;

                this.logic.CurrentUser = session == null || session.User == null ? null : session.User;
                if (this.logic.CurrentUser == null)
                {
                    this.log.Debug("User is anonymous.");
                }
                else
                {
                    this.log.Debug("User is " + this.logic.CurrentUser);
                }

                var dbcompetition = this.logic.GetCompetition(requestedCompetitionId);

                if (dbcompetition != null)
                {
                    this.log.Debug("Returned competition: " + dbcompetition);
                    return new CompetitionResponse(dbcompetition);
                }
            }
            catch (Exception exception)
            {
                this.log.Warn(exception);
                throw;
            }

            this.log.Warn("Could not find competition accordng to search criteria.");
            throw new Exception(string.Format("Could not find competition with Guid {0}", request.CompetitionId));
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
        public CompetitionResponse Post(CompetitionRequest request)
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

            if (string.IsNullOrEmpty(request.CompetitionId))
            {
                this.log.Debug("Adding a competition.");
                request.CompetitionId = this.logic.AddCompetition(session.User, request.GetDatabaseCompetition()).Id.ToString();

                this.log.DebugFormat("New competition ID is {0}.", request.CompetitionId);
            }
            else
            {
                this.log.Debug("Updating a competition.");
                this.logic.UpdateCompetition(request.GetDatabaseCompetition());
            }

            // We have updated, read from database and return.
            return this.Get(request);
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
        public CompetitionResponse Delete(CompetitionRequest request)
        {
            this.log.Debug("Got DELETE request: " + request);
            var session = this.GetSession() as CustomUserSession;
            if (session == null || session.User == null)
            {
                // This really shouldn't happen since we have attributed for authenticate
                this.log.Error("Session is null in DELETE request.");
                throw new Exception("You need to authenticate");
            }

            if (string.IsNullOrEmpty(request.CompetitionId))
            {
                throw new Exception("You didn't supply a competition ID");
            }

            this.logic.CurrentUser = session.User;
            this.log.Debug("User is " + this.logic.CurrentUser);

            this.logic.DeleteCompetition(Guid.Parse(request.CompetitionId));

            // This is needed to get IE to be happy.
            return new CompetitionResponse();
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.databaseSession.Dispose();

                base.Dispose();
            }
        }
    }
}
