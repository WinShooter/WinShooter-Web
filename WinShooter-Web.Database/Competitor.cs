// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Competitor.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The competitor.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The competitor.
    /// </summary>
    public class Competitor : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Competitor"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public Competitor()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        /// Gets or sets the competitor ID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the competitor competition.
        /// </summary>
        public virtual Competition Competition { get; set; }

        /// <summary>
        /// Gets or sets the competitor shooters.
        /// </summary>
        public virtual Shooter Shooter { get; set; }

        /// <summary>
        /// Gets or sets the competitor shooter class.
        /// </summary>
        public virtual ShootersClassEnum ShooterClass { get; set; }

        /// <summary>
        /// Gets or sets the competitor weapon.
        /// </summary>
        public virtual Weapon Weapon { get; set; }

        /// <summary>
        /// Gets or sets the competitor patrol.
        /// </summary>
        public virtual Patrol Patrol { get; set; }

        /// <summary>
        /// Gets or sets the competitor patrol lane.
        /// </summary>
        public virtual int PatrolLane { get; set; }

        /// <summary>
        /// Gets or sets the competitor shooting place in the final round.
        /// </summary>
        public virtual int FinalShootingPlace { get; set; }

        /// <summary>
        /// Gets or sets the competitor results.
        /// </summary>
        public virtual IList<CompetitorResult> CompetitorResults { get; set; }
    }
}
