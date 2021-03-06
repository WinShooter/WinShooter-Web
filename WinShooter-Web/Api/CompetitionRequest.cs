﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionRequest.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api
{
    using System;
    using System.Text;

    using WinShooter.Database;
    using WinShooter.Web.DataValidation;

    /// <summary>
    /// Represents a competition from the client.
    /// </summary>
    public class CompetitionRequest : RequestBase
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
        /// Gets or sets the start date.
        /// </summary>
        public string StartDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Norwegian count should be used.
        /// </summary>
        public bool UseNorwegianCount { get; set; }

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
                StartDate = this.ParseStartDate(),
                UseNorwegianCount = this.UseNorwegianCount
            };

            return toReturn;
        }

        /// <summary>
        /// Parse start date as a <see cref="DateTime"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        public DateTime ParseStartDate()
        {
            return this.ParseDateTimeString(this.StartDate);
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

        /// <summary>
        /// Validate values.
        /// </summary>
        /// <param name="isNewCompetition">
        /// The is new competition.
        /// </param>
        public void ValidateValues(bool isNewCompetition)
        {
            if (!isNewCompetition)
            {
                this.CompetitionId.Require("CompetitionId").NotNull().LongerThan(5).ShorterThan(50);
            }

            this.CompetitionType.Require("CompetitionType").NotNull().LongerThan(3).ShorterThan(10);
            this.Name.Require("Name").NotNull().LongerThan(5).ShorterThan(255);
            this.StartDate.Require("StartDate").NotNull().LongerThan(23).ShorterThan(25);
            this.ParseStartDate().Require("StartDate").NotDefault().WithinYears(2);
        }
    }
}
