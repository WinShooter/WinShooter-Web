// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatrolResponse.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Api.Patrols
{
    using System;
    using System.Text;

    /// <summary>
    /// Represents the competition response.
    /// </summary>
    public class PatrolResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PatrolResponse"/> class.
        /// </summary>
        /// <param name="dbpatrol">
        /// The database station
        /// </param>
        public PatrolResponse(Database.Patrol dbpatrol)
        {
            this.PatrolId = dbpatrol.Id.ToString();
            this.CompetitionId = dbpatrol.Competition.Id;
            this.PatrolNumber = dbpatrol.PatrolNumber;
            this.StartTime = dbpatrol.StartTime.ToUniversalTime().ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z");
            this.PatrolClass = (int)dbpatrol.PatrolClass;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatrolResponse"/> class.
        /// </summary>
        public PatrolResponse()
        {
        }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/>.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the patrol id.
        /// </summary>
        public string PatrolId { get; set; }

        /// <summary>
        /// Gets or sets the station number.
        /// </summary>
        public int PatrolNumber { get; set; }

        /// <summary>
        /// Gets or sets the number of shots.
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// Gets or sets the number of targets.
        /// </summary>
        public int PatrolClass { get; set; }

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
                "PatrolRequest [PatrolId: {0}, " +
                "CompetitionId: {1}, " +
                "PatrolNumber: {2}, " +
                "StartTime: {3}, " +
                "PatrolClass: {4} ]",
                this.PatrolId,
                this.CompetitionId,
                this.PatrolNumber,
                this.StartTime,
                this.PatrolClass);

            return toReturn.ToString();
        }
    }
}
