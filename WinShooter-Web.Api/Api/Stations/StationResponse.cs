// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationResponse.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Api.Stations
{
    using System.Text;

    /// <summary>
    /// Represents the competition response.
    /// </summary>
    public class StationResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StationResponse"/> class.
        /// </summary>
        /// <param name="dbstation">
        /// The database station
        /// </param>
        public StationResponse(Database.Station dbstation)
        {
            this.Id = dbstation.Id.ToString();
            this.CompetitionId = dbstation.Competition.Id.ToString();
            this.Distinguish = dbstation.Distinguish;
            this.NumberOfShots = dbstation.NumberOfShots;
            this.NumberOfTargets = dbstation.NumberOfTargets;
            this.Points = dbstation.Points;
            this.StationNumber = dbstation.StationNumber;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StationResponse"/> class.
        /// </summary>
        public StationResponse()
        {
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the competition id.
        /// </summary>
        public string CompetitionId { get; set; }

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
        /// Gets or sets a value indicating whether the station contains points.
        /// </summary>
        public bool Points { get; set; }

        /// <summary>
        /// Gets or sets the station number.
        /// </summary>
        public int StationNumber { get; set; }

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
                "StationResponse [Id: {0}, CompetitionId: {1}, Distinguish: {2}, NumberOfShots: {3}, NumberOfTargets: {4}, Points: {5}, StationNumber: {6}]", 
                this.Id,
                this.CompetitionId,
                this.Distinguish,
                this.NumberOfShots,
                this.NumberOfTargets,
                this.Points,
                this.StationNumber);

            return toReturn.ToString();
        }
    }
}
