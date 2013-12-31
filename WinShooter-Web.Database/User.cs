// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The representation of the database user.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The representation of the database user.
    /// </summary>
    public class User : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public User()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            this.CardNumber = string.Empty;
            this.Surname = string.Empty;
            this.Givenname = string.Empty;
            this.Email = string.Empty;
            this.LastLogin = DateTime.Now;
            this.LastUpdated = DateTime.Now;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        /// <summary>
        /// Gets or sets the <see cref="Guid"/>.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the card number.
        /// </summary>
        public virtual string CardNumber { get; set; }

        /// <summary>
        /// Gets or sets the surname.
        /// </summary>
        public virtual string Surname { get; set; }

        /// <summary>
        /// Gets or sets the given name.
        /// </summary>
        public virtual string Givenname { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// Gets or sets the club Id.
        /// </summary>
        public virtual Guid ClubId { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public virtual DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the time of the last login.
        /// </summary>
        public virtual DateTime LastLogin { get; set; }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public virtual string DisplayName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Givenname) && !string.IsNullOrEmpty(this.Surname))
                {
                    return this.Givenname + " " + this.Surname;
                }

                return this.Email;
            }
        }
    }
}
