// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api
{
    using System;
    using System.Web.Http;

    using log4net;

    using WinShooter.Logic;
    using WinShooter.Web.DataValidation;

    /// <summary>
    /// The competition service.
    /// </summary>
    public class CompetitionController : BaseApiController
    {
        /// <summary>
        /// The logic.
        /// </summary>
        private readonly CompetitionsLogic logic;

        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionController"/> class.
        /// </summary>
        public CompetitionController()
        {
            this.log = LogManager.GetLogger(this.GetType());
            this.logic = new CompetitionsLogic(this.DatabaseSession);
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
        public CompetitionResponse Get(string competitionId)
        {
            this.log.DebugFormat("Got GET request with competitionID=\"{0}\".", competitionId);
            competitionId.Require("CompetitionId").NotNull().LongerThan(5);

            try
            {
                var requestedCompetitionId = Guid.Parse(competitionId);

                this.logic.CurrentUser = this.Principal;
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
            throw new Exception(string.Format("Could not find competition with Guid {0}", competitionId));
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
        public CompetitionResponse Post(CompetitionRequest request)
        {
            this.log.Debug("Got POST request: " + request);
            if (this.Principal == null)
            {
                // This really shouldn't happen since we have attributed for authenticate
                this.log.Error("Principal is null in POST request.");
                throw new Exception("You need to authenticate");
            }

            this.logic.CurrentUser = this.Principal;
            var user = this.GetUser();
            this.log.Debug("User is " + user);

            if (string.IsNullOrEmpty(request.CompetitionId))
            {
                request.ValidateValues(true);
                this.log.Debug("Adding a competition.");
                request.CompetitionId = this.logic.AddCompetition(user, request.GetDatabaseCompetition()).Id.ToString();

                this.log.DebugFormat("New competition ID is {0}.", request.CompetitionId);
            }
            else
            {
                request.ValidateValues(false);
                this.log.Debug("Updating a competition.");
                this.logic.UpdateCompetition(request.GetDatabaseCompetition());
            }

            // We have updated, read from database and return.
            return this.Get(request.CompetitionId);
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
        public CompetitionResponse Delete(CompetitionRequest request)
        {
            this.log.Debug("Got DELETE request: " + request);
            if (this.Principal == null)
            {
                // This really shouldn't happen since we have attributed for authenticate
                this.log.Error("Session is null in DELETE request.");
                throw new Exception("You need to authenticate");
            }

            if (string.IsNullOrEmpty(request.CompetitionId))
            {
                throw new Exception("You didn't supply a competition ID");
            }

            this.logic.CurrentUser = this.Principal;
            this.log.Debug("User is " + this.logic.CurrentUser);

            this.logic.DeleteCompetition(Guid.Parse(request.CompetitionId));

            // This is needed to get IE to be happy.
            return new CompetitionResponse();
        }
    }
}
