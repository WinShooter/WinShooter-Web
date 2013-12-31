// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionRightsService.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The competition rights service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api
{
    using System;
    using System.Linq;

    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authentication;
    using WinShooter.Database;
    using WinShooter.Logic.Authorization;

    /// <summary>
    /// The competition service.
    /// </summary>
    public class CompetitionRightsService : Service
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionRightsService"/> class.
        /// </summary>
        public CompetitionRightsService()
        {
            var databaseSession = NHibernateHelper.OpenSession();

            this.RightsHelper = new RightsHelper(new Repository<UserRolesInfo>(databaseSession), new Repository<RoleRightsInfo>(databaseSession));
        }

        /// <summary>
        /// The rights helper.
        /// </summary>
        public IRightsHelper RightsHelper { get; set; }

        /// <summary>
        /// The any.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        public CompetitionRightsResponse Get(CompetitionRights request)
        {
            var session = this.GetSession() as CustomUserSession;
            var userId = session != null ? session.User.Id : Guid.Empty;

            var rights = RightsHelper.GetRightsForCompetitionIdAndTheUser(userId, Guid.Parse(request.CompetitionId));

            return new CompetitionRightsResponse
                       {
                           Rights = (from right in rights select right.ToString()).ToArray()
                       };
        }
    }
}
