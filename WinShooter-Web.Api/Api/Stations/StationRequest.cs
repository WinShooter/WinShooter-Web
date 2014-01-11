// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationRequest.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Api.Stations
{
    using System;
    using System.Text;

    using ServiceStack.ServiceHost;

    /// <summary>
    /// Represents a competition from the client.
    /// </summary>
    [Route("/stations")]
    public class StationRequest
    {
        /// <summary>
        /// Gets or sets the <see cref="Guid"/>.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether distinguish.
        /// </summary>
        public bool Distinguish { get; set; }

        /// <summary>
        /// Gets or sets the number of shots.
        /// </summary>
        public int NumberOfShots { get; set; }

        /// <summary>
        /// Gets or sets the number of targets.
        /// </summary>
        public int NumberOfTargets { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        public bool Points { get; set; }

        /// <summary>
        /// Gets or sets the station number.
        /// </summary>
        public int StationNumber { get; set; }

        /// <summary>
        /// Gets or sets the station id.
        /// </summary>
        public string StationId { get; set; }

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
                "StationsRequest [StationId: {0}, " +
                "CompetitionId: {1}, " +
                "StationNumber: {2}, " +
                "Distinguish: {3}, " +
                "NumberOfShots: {4}, " +
                "NumberOfTargets{5}, " +
                "Points: {6}, ]",
                this.StationId,
                this.CompetitionId,
                this.StationNumber,
                this.Distinguish,
                this.NumberOfShots,
                this.NumberOfTargets,
                this.Points);

            return toReturn.ToString();
        }

        /// <summary>
        /// Get a database station.
        /// </summary>
        /// <returns>
        /// The <see cref="Database.Station"/>.
        /// </returns>
        public Database.Station GetDatabaseStation()
        {
            var newStation = new Database.Station
            {
                Distinguish = this.Distinguish,
                Id = this.StationId == null ? Guid.Empty : Guid.Parse(this.StationId),
                NumberOfShots = this.NumberOfShots,
                NumberOfTargets = this.NumberOfTargets,
                Points = this.Points,
                StationNumber = this.StationNumber
            };

            return newStation;
        }
    }
}
