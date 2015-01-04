// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentUserRequest.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Defines the RightsRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api
{
    /// <summary>
    /// The user request.
    /// </summary>
    public class CurrentUserRequest : RequestBase
    {
        /// <summary>
        /// Gets or sets the competitionID the user wants rights to be included for.
        /// </summary>
        public string CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user has accepted terms.
        /// </summary>
        public int HasAcceptedTerms { get; set; }
    }
}
