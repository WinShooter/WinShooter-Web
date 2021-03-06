﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitorResult.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Defines the CompetitorResult type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The competitor result.
    /// </summary>
    public class CompetitorResult : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitorResult"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public CompetitorResult()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        /// Gets or sets the <see cref="CompetitorResult"/> ID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the competitor.
        /// </summary>
        public virtual Competitor Competitor { get; set; }

        /// <summary>
        /// Gets or sets the station.
        /// </summary>
        public virtual Station Station { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CompetitorResult"/> points.
        /// </summary>
        public virtual int Points { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="CompetitorResult"/> target hits.
        /// </summary>
        public virtual int TargetHits { get; set; }
    }
}
