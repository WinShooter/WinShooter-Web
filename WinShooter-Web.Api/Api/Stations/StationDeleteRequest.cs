// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationDeleteRequest.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
    [Route("/stations/{StationId}", Verbs = "Delete")]
    public class StationDeleteRequest
    {
        /// <summary>
        /// Gets or sets the station id.
        /// </summary>
        public Guid StationId { get; set; }

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
                "StationDeleteRequest [StationId: {0} ]",
                this.StationId);

            return toReturn.ToString();
        }
    }
}
