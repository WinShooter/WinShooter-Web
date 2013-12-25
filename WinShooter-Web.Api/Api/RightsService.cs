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
    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authentication;

    /// <summary>
    /// The competition service.
    /// </summary>
    public class RightsService : Service
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
        public RightsResponse Get(Rights request)
        {
            var session = this.GetSession() as CustomUserSession;

            // TODO Implement
            return new RightsResponse { Rights = new[] { "CreateCompetition" } };
        }
    }
}
