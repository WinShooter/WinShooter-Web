// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Patrol.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The patrol.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The patrol.
    /// </summary>
    public class Patrol : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Patrol"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public Patrol()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        /// Gets or sets the <see cref="Patrol"/> ID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Patrol"/> number.
        /// </summary>
        public virtual int PatrolId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Patrol"/> start time.
        /// </summary>
        public virtual DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Patrol"/> competition.
        /// </summary>
        public virtual Competition Competition { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Patrol"/> class.
        /// </summary>
        public virtual PatrolClassEnum PatrolClass { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Patrol"/> start time to display.
        /// </summary>
        public virtual DateTime StartTimeDisplay { get; set; }
    }
}
