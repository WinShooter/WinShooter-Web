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

namespace WinShooter.Api.Api
{
    using System;

    using ServiceStack.ServiceHost;
    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authentication;
    using WinShooter.Logic;

    /// <summary>
    /// The competition service.
    /// </summary>
    public class CompetitionService : Service
    {
        /// <summary>
        /// The any.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        [Route("/competitions")]
        public CompetitionResponse Get(CompetitionRequest request)
        {
            var requestedCompetitionId = Guid.Parse(request.CompetitionId);

            var session = this.GetSession() as CustomUserSession;
            var userId = session == null ? Guid.Empty : session.User.Id;

            var logic = new CompetitionsLogic();
            var dbcompetition = logic.GetCompetition(userId, requestedCompetitionId);

            if (dbcompetition != null)
            {
                return new CompetitionResponse(dbcompetition);
            }

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
            var session = this.GetSession() as CustomUserSession;
            if (session == null)
            {
                // This really shouldn't happen since we have attributed for authenticate
                throw new Exception("You need to authenticate");
            }

            var logic = new CompetitionsLogic();
            if (string.IsNullOrEmpty(request.CompetitionId))
            {
                request.CompetitionId = 
                    logic.AddCompetition(session.User.Id, request.GetDatabaseCompetition()).ToString();
            }
            else
            {
                logic.UpdateCompetition(session.User.Id, request.GetDatabaseCompetition());
            }

            // We have updated, read from database and return.
            return this.Get(request);
        }
    }
}
