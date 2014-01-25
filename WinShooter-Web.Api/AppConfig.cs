// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppConfig.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Defines the AppConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api
{
    using System.Collections.Generic;
    using System.Linq;

    using ServiceStack.Configuration;

    /// <summary>
    /// The app config.
    /// </summary>
    internal class AppConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfig"/> class.
        /// </summary>
        /// <param name="appSettings">
        /// The app settings.
        /// </param>
        public AppConfig(IResourceManager appSettings)
        {
            this.AdminEmailAddresses = (from adminUserName in appSettings.GetList("AdminEmailAddresses")
                 select adminUserName.ToUpper().Trim()).ToList();
        }

        /// <summary>
        /// Gets or sets the admin user names.
        /// </summary>
        public List<string> AdminEmailAddresses { get; set; }

        /// <summary>
        /// The is admin user.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAdminUser(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            var upperEmail = email.Trim().ToUpper();
            return (from adminUser in this.AdminEmailAddresses where upperEmail == adminUser select adminUser).Any();
        }
    }
}