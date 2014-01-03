// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionsService.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authentication;
    using WinShooter.Database;
    using WinShooter.Logic;
    using WinShooter.Logic.Authorization;

    /// <summary>
    /// The competitions service.
    /// </summary>
    public class CompetitionsService : Service
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
        /// The rights helper.
        /// </summary>
        private readonly IRightsHelper rightsHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsService"/> class.
        /// </summary>
        public CompetitionsService()
        {
            this.databaseSession = NHibernateHelper.OpenSession();
            this.rightsHelper = new RightsHelper(new Repository<UserRolesInfo>(this.databaseSession), new Repository<RoleRightsInfo>(this.databaseSession));
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
        public List<CompetitionResponse> Get(CompetitionsRequest request)
        {
            var authSession = this.GetSession() as CustomUserSession;

            var userId = authSession == null || authSession.User == null ? Guid.Empty : authSession.User.Id;

            var competitions = this.logic.GetCompetitions(userId);

            var responses = (from dbcompetition in competitions 
                 select new CompetitionResponse(dbcompetition)).ToList();

            if (userId.Equals(Guid.Empty))
            {
                // Anonymous user
                return responses;
            }

            foreach (var competitionResponse in responses)
            {
                var userRights = this.rightsHelper.GetRightsForCompetitionIdAndTheUser(
                    userId,
                    Guid.Parse(competitionResponse.CompetitionId));
                competitionResponse.UserCanDeleteCompetition = userRights.Contains(WinShooterCompetitionPermissions.DeleteCompetition);
                competitionResponse.UserCanUpdateCompetition = userRights.Contains(WinShooterCompetitionPermissions.UpdateCompetition);
            }

            return responses;
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
