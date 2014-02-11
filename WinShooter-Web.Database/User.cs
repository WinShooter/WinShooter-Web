// --------------------------------------------------------------------------------------------------------------------
// <copyright file="User.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
    using System.Text;

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
            this.IsSystemAdmin = false;
            this.HasAcceptedTerms = 0;
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
        /// Gets or sets a value indicating whether this is the first authentication.
        /// </summary>
        public virtual int HasAcceptedTerms { get; set; }

        /// <summary>
        /// Gets or sets the last updated.
        /// </summary>
        public virtual DateTime LastUpdated { get; set; }

        /// <summary>
        /// Gets or sets the time of the last login.
        /// </summary>
        public virtual DateTime LastLogin { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is system admin.
        /// This value is NOT set from the database but during the authentication process.
        /// </summary>
        public virtual bool IsSystemAdmin { get; set; }

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

            toReturn.AppendFormat("User [ Id={0}, ", this.Id);
            if (this.Givenname != null)
            {
                toReturn.AppendFormat("Givenname=\"{0}\", ", this.Givenname);
            }

            if (this.Surname != null)
            {
                toReturn.AppendFormat("Surname=\"{0}\", ", this.Surname);
            }

            if (this.CardNumber != null)
            {
                toReturn.AppendFormat("CardNumber=\"{0}\", ", this.CardNumber);
            }

            if (this.Email != null)
            {
                toReturn.AppendFormat("Email=\"{0}\", ", this.Email);
            }

            if (!this.ClubId.Equals(Guid.Empty))
            {
                toReturn.AppendFormat("ClubId={0}, ", this.ClubId);
            }

            toReturn.AppendFormat("LastLogin={0}, ", this.LastLogin);
            toReturn.AppendFormat("LastUpdated={0} ]", this.LastUpdated);

            return toReturn.ToString();
        }

        /// <summary>
        /// Update this user from other.
        /// </summary>
        /// <param name="user">
        /// The user.
        /// </param>
        public virtual void UpdateFromOther(User user)
        {
            this.CardNumber = user.CardNumber;
            this.ClubId = user.ClubId;
            this.Email = user.Email;
            this.Givenname = user.Givenname;
            this.HasAcceptedTerms = user.HasAcceptedTerms;
            this.LastLogin = user.LastLogin;
            this.Surname = user.Surname;
            this.LastUpdated = DateTime.Now;
        }
    }
}
