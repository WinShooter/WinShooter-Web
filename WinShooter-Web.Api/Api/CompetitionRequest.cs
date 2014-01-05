// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionRequest.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
    using System.Text;

    using ServiceStack.ServiceHost;

    using WinShooter.Database;

    /// <summary>
    /// Represents a competition from the client.
    /// </summary>
    [Route("/competition")]
    public class CompetitionRequest
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
        /// Gets or sets a value indicating whether the competition is public.
        /// </summary>
        public bool IsPublic { get; set; }

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
        /// The <see cref="CompetitionRequest"/>.
        /// </returns>
        public Competition GetDatabaseCompetition()
        {
            var competitionType = (CompetitionType)Enum.Parse(typeof(CompetitionType), this.CompetitionType);
            var competitionId = string.IsNullOrEmpty(this.CompetitionId) ? Guid.Empty : Guid.Parse(this.CompetitionId);

            var toReturn = new Competition
                       {
                           CompetitionType = competitionType,
                           Id = competitionId,
                           IsPublic = this.IsPublic,
                           Name = this.Name,
                           StartDate = this.StartDate,
                           UseNorwegianCount = this.UseNorwegianCount
                       };

            return toReturn;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            var toReturn = new StringBuilder();

            toReturn.AppendFormat(
                "CompetitionId: {0}, CompetitionType: {1}, IsPublic: {2}, Name: {3}, StartDate: {4}, UseNorwegianCount: {5}",
                this.CompetitionId,
                this.CompetitionType,
                this.IsPublic,
                this.Name,
                this.StartDate,
                this.UseNorwegianCount);

            return toReturn.ToString();
        }
    }
}
