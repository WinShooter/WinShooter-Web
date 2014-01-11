// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Station.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Defines the Station type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The station.
    /// </summary>
    public class Station : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Station"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public Station()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        /// Gets or sets the <see cref="Station"/> Id.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Station"/> competition.
        /// </summary>
        public virtual Competition Competition { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Station"/> number of targets.
        /// </summary>
        public virtual int NumberOfTargets { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Station"/> number of shots.
        /// </summary>
        public virtual int NumberOfShots { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Station"/> has points.
        /// </summary>
        public virtual bool Points { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Station"/> is a station for distinguishing between shooters.
        /// </summary>
        public virtual bool Distinguish { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Station"/> station number.
        /// </summary>
        public virtual int StationNumber { get; set; }
    }
}
