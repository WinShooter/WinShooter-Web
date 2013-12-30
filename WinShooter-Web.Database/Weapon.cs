// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Weapon.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The weapon.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The weapon.
    /// </summary>
    public class Weapon
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Weapon"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public Weapon()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            this.LastUpdated = DateTime.Now;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the manufacturer.
        /// </summary>
        public virtual string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the model.
        /// </summary>
        public virtual string Model { get; set; }

        /// <summary>
        /// Gets or sets the caliber.
        /// </summary>
        public virtual string Caliber { get; set; }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        public virtual WeaponClassEnum Class { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public virtual DateTime LastUpdated { get; set; }
    }
}
