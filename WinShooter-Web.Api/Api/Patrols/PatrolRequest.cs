// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatrolRequest.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Api.Patrols
{
    using System;
    using System.Globalization;
    using System.Text;

    using ServiceStack.ServiceHost;

    using WinShooter.Database;

    /// <summary>
    /// Represents a competition from the client.
    /// </summary>
    [Route("/patrols")]
    [Route("/patrols/{PatrolId}")]
    public class PatrolRequest : RequestBase
    {
        /// <summary>
        /// Gets or sets the <see cref="Guid"/>.
        /// </summary>
        public Guid CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the patrol id.
        /// </summary>
        public string PatrolId { get; set; }

        /// <summary>
        /// Gets or sets the patrol number.
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

        /// <summary>
        /// Get a database patrol.
        /// </summary>
        /// <returns>
        /// The <see cref="Database.Patrol"/>.
        /// </returns>
        public Patrol GetDatabasePatrol()
        {
            var patrol = new Patrol 
            {
                Id = this.ParsePatrolId(),
                PatrolNumber = this.PatrolNumber,
                StartTime = this.ParseStartTime(),
                PatrolClass = this.ParsePatrolClass()
            };

            return patrol;
        }

        /// <summary>
        /// Parse patrol class number to  <see cref="PatrolClassEnum"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="PatrolClassEnum"/>.
        /// </returns>
        public PatrolClassEnum ParsePatrolClass()
        {
            return (PatrolClassEnum)Enum.Parse(
                typeof(PatrolClassEnum), 
                this.PatrolClass.ToString(CultureInfo.InvariantCulture));
        }

        /// <summary>
        /// Parse the <see cref="PatrolId"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        public Guid ParsePatrolId()
        {
            return this.ParseGuidString(this.PatrolId);
        }

        /// <summary>
        /// Parse <see cref="StartTime"/> string.
        /// </summary>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public DateTime ParseStartTime()
        {
            return this.ParseDateTimeString(this.StartTime);
        }
    }
}
