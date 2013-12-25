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

    using WinShooter.Logic;

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
        public List<CompetitionResponse> Get(Competitions request)
        {
            var sess = this.GetSession();

            var logic = new CompetitionsLogic();

            var competitions = logic.GetCompetitions(
                sess.IsAuthenticated 
                    ? Guid.Parse(sess.UserAuthId) 
                    : Guid.Empty);

            return
                (from dbcompetition in competitions 
                 select new CompetitionResponse(dbcompetition)).ToList();
        }
    }
}
