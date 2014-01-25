// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RightsHelper.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
    using System.Collections.Generic;
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
        /// The public competition rights.
        /// </summary>
        private readonly WinShooterCompetitionPermissions[] publicCompetitionRights =
            {
                WinShooterCompetitionPermissions
                    .ReadClub,
                WinShooterCompetitionPermissions
                    .ReadCompetition,
                WinShooterCompetitionPermissions
                    .ReadCompetitorResult,
                WinShooterCompetitionPermissions
                    .ReadPatrol,
                WinShooterCompetitionPermissions
                    .ReadPublicUser,
                WinShooterCompetitionPermissions
                    .ReadStation,
                WinShooterCompetitionPermissions
                    .ReadWeapon
            };

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
        /// Gets or sets the current user.
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="includePublic">
        /// If all public competitions should be included.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/> array.
        /// </returns>
        public Guid[] GetCompetitionIdsTheUserHasRightsOn(bool includePublic)
        {
            var competitionIds =
                from userRolesInfo in
                    this.userRolesInfoRepository.FilterBy(
                        x => x.User.Id.Equals(this.CurrentUser.Id) && x.Competition.IsPublic == includePublic)
                select userRolesInfo.Competition.Id;

            return competitionIds.ToArray();
        }

        /// <summary>
        /// Get rights for the user has rights on.
        /// </summary>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        public WinShooterCompetitionPermissions[] GetRightsForCompetitionIdAndTheUser(Guid competitionId)
        {
            if (this.CurrentUser == null)
            {
                // Anonymous user
                return this.publicCompetitionRights;
            }

            if (this.CurrentUser.IsSystemAdmin)
            {
                // I must... obey.
                return (WinShooterCompetitionPermissions[])Enum.GetValues(typeof(WinShooterCompetitionPermissions));
            }

            var userRoleIds =
                from userRolesInfo in
                     this.userRolesInfoRepository.FilterBy(
                         x => x.User.Id.Equals(this.CurrentUser.Id) && x.Competition.Id.Equals(competitionId))
                 select userRolesInfo.Role.Id;

            var rightStrings =
                from roleRightsInfo in this.roleRightsInfoRepository.FilterBy(x => userRoleIds.Contains(x.Role.Id))
                select roleRightsInfo.Right.Name;
            
            var rights = from rightString in rightStrings
                         select
                         (WinShooterCompetitionPermissions)
                         Enum.Parse(typeof(WinShooterCompetitionPermissions), rightString);

            return this.AddRightsWithNoDuplicate(rights, this.publicCompetitionRights);
        }

        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        public string[] GetRolesForCompetitionIdAndTheUser(Guid competitionId)
        {
            var userRoles =
                from userRolesInfo in
                    this.userRolesInfoRepository.FilterBy(
                        x => x.User.Id.Equals(this.CurrentUser.Id) && x.Competition.Id.Equals(competitionId))
                select userRolesInfo.Role.RoleName;

            return userRoles.ToArray();
        }

        /// <summary>
        /// Get the system rights the user has.
        /// </summary>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        public WinShooterCompetitionPermissions[] GetSystemRightsForTheUser()
        {
            var listOfuserRights =
                (from userRolesInfo in
                     this.userRolesInfoRepository.FilterBy(
                         x => x.User.Id.Equals(this.CurrentUser.Id) && x.Role.RoleName.StartsWith("System"))
                 select (from roleRightsInfo in userRolesInfo.Role.RoleRightsInfos select roleRightsInfo.Right)).ToArray();

            // Flatten array of arrays
            var userRights = listOfuserRights.SelectMany(rights => rights).ToArray();

            return (from userRight in userRights
                    select
                        (WinShooterCompetitionPermissions)
                        Enum.Parse(typeof(WinShooterCompetitionPermissions), userRight.Name)).ToArray();
        }

        /// <summary>
        /// Get the system roles for the user.
        /// </summary>
        /// <returns>The system role name array</returns>
        public string[] GetSystemRolesForTheUser()
        {
            var userRoles =
                (from userRolesInfo in
                     this.userRolesInfoRepository.FilterBy(
                         x => x.User.Id.Equals(this.CurrentUser.Id) && x.Role.RoleName.StartsWith("System"))
                 select userRolesInfo.Role.RoleName).ToArray();

            return userRoles;
        }

        /// <summary>
        /// Add two arrays of rights to one, leaving out the duplicates.
        /// </summary>
        /// <param name="rights1">
        /// The rights 1.
        /// </param>
        /// <param name="rights2">
        /// The rights 2.
        /// </param>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/>s.
        /// </returns>
        public WinShooterCompetitionPermissions[] AddRightsWithNoDuplicate(
            IEnumerable<WinShooterCompetitionPermissions> rights1,
            IEnumerable<WinShooterCompetitionPermissions> rights2)
        {
            var rightsList = new List<WinShooterCompetitionPermissions>(rights1);
            rightsList.AddRange((from right in rights2 where !rightsList.Contains(right) select right).ToArray());

            return rightsList.ToArray();
        }
    }
}
