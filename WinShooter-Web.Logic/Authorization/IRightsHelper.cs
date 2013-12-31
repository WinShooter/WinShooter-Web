// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRightsHelper.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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

    /// <summary>
    /// The interface for <see cref="RightsHelper"/>.
    /// </summary>
    public interface IRightsHelper
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
        Guid[] GetCompetitionIdsTheUserHasRightsOn(Guid userId, bool includePublic);

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
        WinShooterCompetitionPermissions[] GetRightsForCompetitionIdAndTheUser(Guid userId, Guid competitionId);

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
        string[] GetRolesForCompetitionIdAndTheUser(Guid userId, Guid competitionId);
    }
}