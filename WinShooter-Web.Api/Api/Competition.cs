﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Competition.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Represents a competition from the client.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api
{
    using System;

    using ServiceStack.ServiceHost;

    using WinShooter.Database;

    /// <summary>
    /// Represents a competition from the client.
    /// </summary>
    [Route("/competition/{CompetitionId}")]
    public class Competition
    {
        /// <summary>
        /// Gets or sets the <see cref="Guid"/>.
        /// </summary>
        public string CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the type of competition. 
        /// Matches to <see cref="CompetitionType"/> enumeration.
        /// </summary>
        public string CompetitionType { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/>.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Norwegian count should be used.
        /// </summary>
        public bool UseNorwegianCount { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Get a database competition.
        /// </summary>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Database.Competition GetDatabaseCompetition()
        {
            var competitionType = (CompetitionType)Enum.Parse(typeof(CompetitionType), this.CompetitionType);

            var toReturn = new Database.Competition
                       {
                           CompetitionType = competitionType,
                           Id = Guid.Parse(this.CompetitionId),
                           IsPublic = true,
                           Name = this.Name,
                           StartDate = this.StartDate,
                           UseNorwegianCount = this.UseNorwegianCount
                       };

            return toReturn;
        }
    }
}
