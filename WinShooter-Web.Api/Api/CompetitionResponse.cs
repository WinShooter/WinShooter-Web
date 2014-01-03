// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionResponse.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Api
{
    using System;

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
            this.StartDate = dbcompetition.StartDate.ToString("yyyy-MM-dd hh:mm");
            this.IsPublic = dbcompetition.IsPublic;
            this.UseNorwegianCount = dbcompetition.UseNorwegianCount;

            this.UserCanUpdateCompetition = false;
            this.UserCanDeleteCompetition = false;
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
        /// Gets or sets a value indicating whether the user can edit this competition.
        /// </summary>
        public bool UserCanUpdateCompetition { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user can delete this competition.
        /// </summary>
        public bool UserCanDeleteCompetition { get; set; }
    }
}
