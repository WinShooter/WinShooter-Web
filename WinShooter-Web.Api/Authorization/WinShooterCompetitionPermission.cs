// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinShooterCompetitionPermission.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Authorization
{
    /// <summary>
    /// The permissions (rights) on WinShooter connected to a competition.
    /// </summary>
    public class WinShooterCompetitionPermission
    {
        public const string CreateCompetition = "CreateCompetition";
        public const string ReadCompetition = "ReadCompetition";
        public const string UpdateCompetition = "UpdateCompetition";
        public const string DeleteCompetition = "DeleteCompetition";

        public const string CreateClub = "CreateClub";
        public const string ReadClub = "ReadClub";
        public const string UpdateClub = "UpdateClub";
        public const string DeleteClub = "DeleteClub";

        public const string CreatePatrol = "CreatePatrol";
        public const string ReadPatrol = "ReadPatrol";
        public const string UpdatePatrol = "UpdatePatrol";
        public const string DeletePatrol = "DeletePatrol";

        public const string CreateWeapon = "CreateWeapon";
        public const string ReadWeapon = "ReadWeapon";
        public const string DeleteWeapon = "DeleteWeapon";
        public const string UpdateWeapon = "UpdateWeapon";

        public const string CreateStation = "CreateStation";
        public const string ReadStation = "ReadStation";
        public const string DeleteStation = "DeleteStation";
        public const string UpdateStation = "UpdateStation";

        public const string CreateShooter = "CreateShooter";
        public const string ReadShooter = "ReadShooter";
        public const string UpdateShooter = "UpdateShooter";
        public const string DeleteShooter = "DeleteShooter";

        public const string CreateCompetitor = "CreateCompetitor";
        public const string ReadCompetitor = "ReadCompetitor";
        public const string UpdateCompetitor = "UpdateCompetitor";
        public const string DeleteCompetitor = "DeleteCompetitor";

        public const string CreateTeam = "CreateTeam";
        public const string ReadTeam = "ReadTeam";
        public const string UpdateTeam = "UpdateTeam";
        public const string DeleteTeam = "DeleteTeam";

        public const string CreateTeamToCompetitor = "CreateTeamToCompetitor";
        public const string ReadTeamToCompetitor = "ReadTeamToCompetitor";
        public const string UpdateTeamToCompetitor = "UpdateTeamToCompetitor";
        public const string DeleteTeamToCompetitor = "DeleteTeamToCompetitor";

        public const string CreateCompetitorResult = "CreateCompetitorResult";
        public const string ReadCompetitorResult = "ReadCompetitorResult";
        public const string UpdateCompetitorResult = "UpdateCompetitorResult";
        public const string DeleteCompetitorResult = "DeleteCompetitorResult";

        public const string GetCompetitionResults = "GetCompetitionResults";

        public const string CreateUserCompetitionRole = "CreateUserCompetitionRole";
        public const string ReadUserCompetitionRole = "ReadUserCompetitionRole";
        public const string UpdateUserCompetitionRole = "UpdateUserCompetitionRole";
        public const string DeleteUserCompetitionRole = "DeleteUserCompetitionRole";
    }
}
