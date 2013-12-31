// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Shooter.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The shooter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The shooter.
    /// </summary>
    public class Shooter : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shooter"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public Shooter()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the competition.
        /// </summary>
        public virtual Competition Competition { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        public virtual string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the Surname.
        /// </summary>
        public virtual string Surname { get; set; }

        /// <summary>
        /// Gets or sets the Given name.
        /// </summary>
        public virtual string Givenname { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the club.
        /// </summary>
        public virtual Club Club { get; set; }

        /// <summary>
        /// Gets or sets how much the shooter has paid.
        /// </summary>
        public virtual int Paid { get; set; }

        /// <summary>
        /// Gets or sets the shooter class.
        /// </summary>
        public virtual ShootersClassEnum Class { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the shooter has arrived.
        /// </summary>
        public virtual bool HasArrived { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to send results by email.
        /// </summary>
        public virtual bool SendResultsByEmail { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public virtual DateTime LastUpdated { get; set; }
    }
}
