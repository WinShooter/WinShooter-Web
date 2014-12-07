// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitorResponse.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Represents the competition response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api.Competitor
{
    using System;
    using System.Text;

    using WinShooter.Database;

    /// <summary>
    /// Represents the competitor response.
    /// </summary>
    public class CompetitorResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitorResponse"/> class.
        /// </summary>
        /// <param name="dbcompetitor">
        /// The database competitor.
        /// </param>
        public CompetitorResponse(Competitor dbcompetitor)
        {
            this.CompetitorId = dbcompetitor.Id.ToString();
            this.CompetitionId = dbcompetitor.Competition.Id;
            this.PatrolLane = dbcompetitor.PatrolLane;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitorResponse"/> class.
        /// </summary>
        public CompetitorResponse()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/>.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the patrol id.
        /// </summary>
        public string CompetitorId { get; set; }

        /// <summary>
        /// Gets or sets the patrol lane.
        /// </summary>
        public int PatrolLane { get; set; }

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
                "CompetitorResponse [CompetitorId: {0}, " +
                "CompetitionId: {1}, " + 
                "PatrolLane: {2} ]",
                this.CompetitorId,
                this.CompetitionId,
                this.PatrolLane);

            return toReturn.ToString();
        }
    }
}
