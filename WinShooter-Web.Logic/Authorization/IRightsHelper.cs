// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRightsHelper.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The interface for <see cref="RightsHelper" />.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Authorization
{
    using System;
    using System.Collections.Generic;

    using WinShooter.Database;
    using WinShooter.Logic.Authentication;

    /// <summary>
    /// The interface for <see cref="RightsHelper"/>.
    /// </summary>
    public interface IRightsHelper
    {
        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        CustomPrincipal CurrentUser { get; set; }

        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="includePublic">
        /// If all public competitions should be included.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/> array.
        /// </returns>
        Guid[] GetCompetitionIdsTheUserHasRightsOn(bool includePublic);

        /// <summary>
        /// Get competition rights the user has.
        /// </summary>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        WinShooterCompetitionPermissions[] GetRightsForCompetitionIdAndTheUser(Guid competitionId);

        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        string[] GetRolesForCompetitionIdAndTheUser(Guid competitionId);

        /// <summary>
        /// Get the permissions for a certain club.
        /// </summary>
        /// <returns>The club permissions.</returns>
        WinShooterCompetitionPermissions[] GetClubRightsForTheUser(Club clubGuid);

        /// <summary>
        /// Get the system rights the user has.
        /// </summary>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        WinShooterCompetitionPermissions[] GetSystemRightsForTheUser();

        /// <summary>
        /// Get the system roles for the user.
        /// </summary>
        /// <returns>The system role name array</returns>
        string[] GetSystemRolesForTheUser();

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
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        WinShooterCompetitionPermissions[] AddRightsWithNoDuplicate(
            IEnumerable<WinShooterCompetitionPermissions> rights1,
            IEnumerable<WinShooterCompetitionPermissions> rights2);
    }
}