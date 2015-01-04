// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionResponse.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api
{
    using System.Text;

    /// <summary>
    /// Represents the competition response.
    /// </summary>
    public class CompetitionResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionResponse"/> class.
        /// </summary>
        /// <param name="dbcompetition">
        /// The database competition.
        /// </param>
        public CompetitionResponse(Database.Competition dbcompetition)
        {
            this.CompetitionId = dbcompetition.Id.ToString();
            this.CompetitionType = dbcompetition.CompetitionType.ToString();
            this.Name = dbcompetition.Name;
            this.StartDate = dbcompetition.StartDate.ToUniversalTime().ToString("yyyy-MM-dd\\THH:mm:ss.fff\\Z");
            this.IsPublic = dbcompetition.IsPublic;
            this.UseNorwegianCount = dbcompetition.UseNorwegianCount;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionResponse"/> class.
        /// </summary>
        public CompetitionResponse()
        {
        }

        /// <summary>
        /// Gets or sets the competition id.
        /// </summary>
        public string CompetitionId { get; set; }

        /// <summary>
        /// Gets or sets the competition type.
        /// </summary>
        public string CompetitionType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the competition is public.
        /// </summary>
        public bool IsPublic { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether use norwegian count.
        /// </summary>
        public bool UseNorwegianCount { get; set; }

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
