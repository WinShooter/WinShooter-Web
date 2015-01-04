// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionsController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api 
{
    using System.Collections.Generic;
    using System.Linq;

    using WinShooter.Logic;

    /// <summary>
    /// The competitions service.
    /// </summary>
    public class CompetitionsController : BaseApiController
    {
        /// <summary>
        /// The logic.
        /// </summary>
        private readonly CompetitionsLogic logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsController"/> class.
        /// </summary>
        public CompetitionsController()
        {
            this.logic = new CompetitionsLogic(this.DatabaseSession);
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
            this.logic.CurrentUser = this.Principal;

            var competitions = this.logic.GetCompetitions();

            var responses = (from dbcompetition in competitions 
                 select new CompetitionResponse(dbcompetition)).ToList();

            return responses;
        }
    }
}
