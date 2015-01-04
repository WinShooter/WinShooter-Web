// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomPrincipal.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Represents the current principal.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Security.Principal;
    using System.Text;

    using WinShooter.Database;

    /// <summary>
    /// Represents the current principal.
    /// </summary>
    public class CustomPrincipal : ICustomPrincipal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPrincipal" /> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        public CustomPrincipal(string identity)
        {
            this.Identity = new GenericIdentity(identity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPrincipal" /> class.
        /// </summary>
        /// <param name="user">The user to create the principal from.</param>
        public CustomPrincipal(User user)
        {
            this.Identity = new GenericIdentity(user.Id.ToString());

            this.UserId = user.Id;
            this.FirstName = user.Givenname;
            this.LastName = user.Surname;
            this.IsSystemAdmin = user.IsSystemAdmin;
        }

        /// <summary>
        /// Gets the identity of the current principal.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Security.Principal.IIdentity"/> object associated with the current principal.
        /// </returns>
        public IIdentity Identity { get; private set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the user last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is a system admin.
        /// </summary>
        public bool IsSystemAdmin { get; set; }

        /// <summary>
        ///     Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <returns>
        ///     True if the current principal is a member of the specified role; otherwise, false.
        /// </returns>
        /// <param name="role">
        ///     The name of the role for which to check membership.
        /// </param>
        public bool IsInRole(string role)
        {
            return false;
        }

        /// <summary>
        /// Returns a string representation of the object.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            var toReturn = new StringBuilder();

            toReturn.AppendFormat("UserID: \"{0}\", ", this.UserId);
            toReturn.AppendFormat("FirstName: \"{0}\", ", this.FirstName);
            toReturn.AppendFormat("LastName: \"{0}\", ", this.LastName);
            toReturn.AppendFormat("IsSystemAdmin: {0}", this.IsSystemAdmin);

            return toReturn.ToString();
        }
    }
}
