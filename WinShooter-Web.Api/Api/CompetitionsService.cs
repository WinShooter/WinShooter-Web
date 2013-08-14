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
    using System.Collections.Generic;

    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authorization;

    /// <summary>
    /// The competitions service.
    /// </summary>
    public class CompetitionsService : Service
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
        [Authenticate]
        [RequiredWinShooterCompetitionPermission("ReadCompetition")]
        public List<CompetitionResponse> Get(Competitions request)
        {
            var sess = this.GetSession();

            if (sess.IsAuthenticated)
            {
                return new List<CompetitionResponse>
                           {
                               new CompetitionResponse { Name = "Tävlingen1", CompetitionId = "1" },
                               new CompetitionResponse { Name = "Tävlingen2", CompetitionId = "2" },
                               new CompetitionResponse { Name = "Tävlingen3", CompetitionId = "3" },
                               new CompetitionResponse { Name = "Tävlingen4", CompetitionId = "4" }
                           };
            }

            return new List<CompetitionResponse>
                           {
                               new CompetitionResponse { Name = "Tävlingen1", CompetitionId = "1" }
                           };
        }
    }
}
