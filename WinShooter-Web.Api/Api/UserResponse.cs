// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserResponse.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The user response.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api
{
    /// <summary>
    /// The user response.
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether the user is logged in.
        /// </summary>
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// Gets or sets the user display name.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        public string Email { get; set; }
    }
}
