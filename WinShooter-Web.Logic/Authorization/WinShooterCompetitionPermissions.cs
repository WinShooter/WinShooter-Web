// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinShooterCompetitionPermissions.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The permissions (rights) on WinShooter connected to a competition.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Authorization
{
    /// <summary>
    /// The permissions (rights) on WinShooter connected to a competition.
    /// </summary>
    public enum WinShooterCompetitionPermissions
    {
        /// <summary>
        /// The right to create a competition.
        /// </summary>
        CreateCompetition,

        /// <summary>
        /// The right to read a competition.
        /// </summary>
        ReadCompetition,

        /// <summary>
        /// The right to update a competition.
        /// </summary>
        UpdateCompetition, 

        /// <summary>
        /// The right to delete a competition.
        /// </summary>
        DeleteCompetition,
        
        /// <summary>
        /// The right to delete a club.
        /// </summary>
        CreateClub,

        /// <summary>
        /// The right to read a club.
        /// </summary>
        ReadClub,

        /// <summary>
        /// The right to update a club.
        /// </summary>
        UpdateClub,

        /// <summary>
        /// The right to delete a club.
        /// </summary>
        DeleteClub,
        
        /// <summary>
        /// The right to create a patrol.
        /// </summary>
        CreatePatrol,

        /// <summary>
        /// The right to read a patrol.
        /// </summary>
        ReadPatrol,

        /// <summary>
        /// The right to update a patrol.
        /// </summary>
        UpdatePatrol,

        /// <summary>
        /// The right to delete a patrol.
        /// </summary>
        DeletePatrol,

        /// <summary>
        /// The right to create a weapon.
        /// </summary>
        CreateWeapon,

        /// <summary>
        /// The right to read a weapon.
        /// </summary>
        ReadWeapon,

        /// <summary>
        /// The right to delete a weapon.
        /// </summary>
        DeleteWeapon,

        /// <summary>
        /// The right to update a weapon.
        /// </summary>
        UpdateWeapon,

        /// <summary>
        /// The right to create a station.
        /// </summary>
        CreateStation,

        /// <summary>
        /// The right to read a station.
        /// </summary>
        ReadStation,

        /// <summary>
        /// The right to delete a station.
        /// </summary>
        DeleteStation,

        /// <summary>
        /// The right to update a station.
        /// </summary>
        UpdateStation,

        /// <summary>
        /// The right to create a shooter.
        /// </summary>
        CreateShooter,

        /// <summary>
        /// The right to read a shooter.
        /// </summary>
        ReadShooter,

        /// <summary>
        /// The right to update a shooter.
        /// </summary>
        UpdateShooter,

        /// <summary>
        /// The right to delete a shooter.
        /// </summary>
        DeleteShooter,

        /// <summary>
        /// The right to create a competitor.
        /// </summary>
        CreateCompetitor,

        /// <summary>
        /// The right to read a competitor.
        /// </summary>
        ReadCompetitor,

        /// <summary>
        /// The right to update a competitor.
        /// </summary>
        UpdateCompetitor,

        /// <summary>
        /// The right to delete a competitor.
        /// </summary>
        DeleteCompetitor,

        /// <summary>
        /// The right to create a team.
        /// </summary>
        CreateTeam,

        /// <summary>
        /// The right to read a team.
        /// </summary>
        ReadTeam,

        /// <summary>
        /// The right to update a team.
        /// </summary>
        UpdateTeam,

        /// <summary>
        /// The right to delete a team.
        /// </summary>
        DeleteTeam,

        /// <summary>
        /// The right to add a competitor to a team.
        /// </summary>
        CreateTeamToCompetitor,

        /// <summary>
        /// The right to read which competitors are in a team.
        /// </summary>
        ReadTeamToCompetitor,

        /// <summary>
        /// The right to update which competitors are in a team.
        /// </summary>
        UpdateTeamToCompetitor,

        /// <summary>
        /// The right to delete which competitors are in a team.
        /// </summary>
        DeleteTeamToCompetitor,

        /// <summary>
        /// The right to create results.
        /// </summary>
        CreateCompetitorResult,

        /// <summary>
        /// The right to read results.
        /// </summary>
        ReadCompetitorResult,

        /// <summary>
        /// The right to update results.
        /// </summary>
        UpdateCompetitorResult,

        /// <summary>
        /// The right to delete results.
        /// </summary>
        DeleteCompetitorResult,

        /// <summary>
        /// The right to get competition results.
        /// </summary>
        GetCompetitionResults,

        /// <summary>
        /// The right to create users
        /// </summary>
        CreateUser,

        /// <summary>
        /// The right to read users
        /// </summary>
        ReadUser,

        /// <summary>
        /// The right to read public users.
        /// </summary>
        ReadPublicUser,

        /// <summary>
        /// The right to update users.
        /// </summary>
        UpdateUser,

        /// <summary>
        /// The right to delete users.
        /// </summary>
        DeleteUser,

        /// <summary>
        /// The right to create user roles for a competition.
        /// </summary>
        CreateUserCompetitionRole,

        /// <summary>
        /// The right to read user roles for a competition.
        /// </summary>
        ReadUserCompetitionRole,

        /// <summary>
        /// The right to update user roles for a competition.
        /// </summary>
        UpdateUserCompetitionRole,

        /// <summary>
        /// The right to delete user roles for a competition.
        /// </summary>
        DeleteUserCompetitionRole
    }
}
