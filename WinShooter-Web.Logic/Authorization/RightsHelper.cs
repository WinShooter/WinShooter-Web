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

    using WinShooter.Database;

    /// <summary>
    /// A helper class to help the logic classes handle rights in a coherent way.
    /// </summary>
    public class RightsHelper : IRightsHelper
    {
        /// <summary>
        /// The <see cref="UserRolesInfo"/> repository.
        /// </summary>
        private readonly IRepository<UserRolesInfo> userRolesInfoRepository;

        /// <summary>
        /// The <see cref="RoleRightsInfo"/> repository.
        /// </summary>
        private readonly IRepository<RoleRightsInfo> roleRightsInfoRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RightsHelper"/> class.
        /// </summary>
        /// <param name="userRolesInfoRepository">
        ///     The <see cref="UserRolesInfo"/> repository.
        /// </param>
        /// <param name="roleRightsInfoRepository">
        ///     The <see cref="RoleRightsInfo"/> repository.
        /// </param>
        public RightsHelper(IRepository<UserRolesInfo> userRolesInfoRepository, IRepository<RoleRightsInfo> roleRightsInfoRepository)
        {
            this.userRolesInfoRepository = userRolesInfoRepository;
            this.roleRightsInfoRepository = roleRightsInfoRepository;
        }

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
        public Guid[] GetCompetitionIdsTheUserHasRightsOn(Guid userId, bool includePublic)
        {
            var competitionIds =
                from userRolesInfo in
                    this.userRolesInfoRepository.FilterBy(
                        x => x.User.Id.Equals(userId) && x.Competition.IsPublic == includePublic)
                select userRolesInfo.Competition.Id;

            return competitionIds.ToArray();
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
        public WinShooterCompetitionPermissions[] GetRightsForCompetitionIdAndTheUser(Guid userId, Guid competitionId)
        {
            // TODO If userId is Guid.Empty, handle this differently.
            var userRoleIds =
                from userRolesInfo in
                     this.userRolesInfoRepository.FilterBy(
                         x => x.User.Id.Equals(userId) && x.Competition.Id.Equals(competitionId))
                 select userRolesInfo.Role.Id;

            var rightStrings =
                from roleRightsInfo in this.roleRightsInfoRepository.FilterBy(x => userRoleIds.Contains(x.Role.Id))
                select roleRightsInfo.Right.Name;
            
            var rights = from rightString in rightStrings
                         select
                         (WinShooterCompetitionPermissions)
                         Enum.Parse(typeof(WinShooterCompetitionPermissions), rightString);
            
            return rights.ToArray();
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
        public string[] GetRolesForCompetitionIdAndTheUser(Guid userId, Guid competitionId)
        {
            var userRoles =
                from userRolesInfo in
                    this.userRolesInfoRepository.FilterBy(
                        x => x.User.Id.Equals(userId) && x.Competition.Id.Equals(competitionId))
                select userRolesInfo.Role.RoleName;

            return userRoles.ToArray();
        }
    }
}
