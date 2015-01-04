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

namespace WinShooter.Logic
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;

    using WinShooter.Logic.WinShooterConstants;

    /// <summary>s
    /// The app config.
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfig"/> class.
        /// </summary>
        public AppConfig()
        {
            this.AdminEmailAddresses =
                GetList(ConfigurationManager.AppSettings[WinShooterAppSettings.AdminEmailAddresses]);
        }

        /// <summary>
        /// Gets or sets the admin user names.
        /// </summary>
        public List<string> AdminEmailAddresses { get; set; }

        /// <summary>
        /// Gets the google OAUTH client id.
        /// </summary>
        public string GoogleOauthClientId
        {
            get
            {
                return ConfigurationManager.AppSettings[WinShooterAppSettings.GoogleOauthClientId];
            }
        }

        /// <summary>
        /// Gets the google OAUTH client secret.
        /// </summary>
        public string GoogleOauthSecret
        {
            get
            {
                return ConfigurationManager.AppSettings[WinShooterAppSettings.GoogleOauthSecret];
            }
        }

        /// <summary>
        /// Gets the current condition level.
        /// </summary>
        public int CurrentConditionLevel
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings[WinShooterAppSettings.CurrentConditionLevel]);
            }
        }

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
            return (from adminUser in this.AdminEmailAddresses where upperEmail == adminUser.ToUpper() select adminUser).Any();
        }

        /// <summary>
        /// Creates a list from a string with separated values.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>A list of parameters</returns>
        private static List<string> GetList(string input)
        {
            if (input == null)
            {
                return new List<string>();
            }

            var partList = input.Split(';', ',').Select(part => part.Trim()).ToList();
            return partList;
        }
    }
}