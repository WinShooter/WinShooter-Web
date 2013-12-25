// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RightsHelper.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   A helper class to help the logic classes handle rights in a coherent way.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Authorization
{
    using System;
    using System.Linq;

    using NHibernate.Linq;

    using WinShooter.Database;

    /// <summary>
    /// A helper class to help the logic classes handle rights in a coherent way.
    /// </summary>
    internal static class RightsHelper
    {
        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="includePublic">
        /// If all public competitions should be included.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/> array.
        /// </returns>
        internal static Guid[] GetCompetitionIdsTheUserHasRightsOn(Guid userId, bool includePublic)
        {
            using (var dbsession = NHibernateHelper.OpenSession())
            {
                var competitionIds = from userRolesInfo in dbsession.Query<UserRolesInfo>()
                                     where userRolesInfo.User.Id.Equals(userId) &&
                                     (userRolesInfo.Competition.IsPublic && includePublic)
                                     select userRolesInfo.Competition.Id;

                return competitionIds.ToArray();
            }
        }

        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        internal static WinShooterCompetitionPermissions[] GetRightsForCompetitionIdAndTheUser(Guid userId, Guid competitionId)
        {
            using (var dbsession = NHibernateHelper.OpenSession())
            {
                var userRoleIds = from userRolesInfo in dbsession.Query<UserRolesInfo>()
                                     where userRolesInfo.User.Id.Equals(userId) &&
                                     userRolesInfo.Competition.Id.Equals(competitionId)
                                     select userRolesInfo.Role.Id;

                var rightStrings = from roleRightsInfo in dbsession.Query<RoleRightsInfo>()
                                   where userRoleIds.Contains(roleRightsInfo.Role.Id)
                                   select roleRightsInfo.Right.Name;

                var rights = from rightString in rightStrings
                             select
                                 (WinShooterCompetitionPermissions)
                                 Enum.Parse(typeof(WinShooterCompetitionPermissions), rightString);

                return rights.ToArray();
            }
        }
    }
}
