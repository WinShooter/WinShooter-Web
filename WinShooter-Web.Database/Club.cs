// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Club.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The club.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The club.
    /// </summary>
    public class Club : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Club"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public Club()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            this.Email = string.Empty;
            this.Plusgiro = string.Empty;
            this.Bankgiro = string.Empty;
            this.LastUpdated = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the club ID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the national club ID.
        /// </summary>
        public virtual string ClubId { get; set; }

        /// <summary>
        /// Gets or sets the club name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the club country.
        /// </summary>
        public virtual string Country { get; set; }

        /// <summary>
        /// Gets or sets the club email.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the club PLUSGIRO.
        /// </summary>
        public virtual string Plusgiro { get; set; }

        /// <summary>
        /// Gets or sets the club BANKGIRO.
        /// </summary>
        public virtual string Bankgiro { get; set; }

        /// <summary>
        /// Gets or sets the club last updated.
        /// </summary>
        public virtual DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the administrator of the club.
        /// </summary>
        public virtual User AdminUser { get; set; }
    }
}
