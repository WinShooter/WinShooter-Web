// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinShooterAppSettings.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   A number of static strings to use for keys in app settings.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.WinShooterConstants
{
    /// <summary>
    /// A number of static strings to use for keys in app settings.
    /// </summary>
    public static class WinShooterAppSettings
    {
        /// <summary>
        /// The app settings key for Google Client ID.
        /// </summary>
        public static readonly string GoogleOauthClientId = "GoogleOauthClientId";

        /// <summary>
        /// The app settings key for Google Client ID.
        /// </summary>
        public static readonly string GoogleOauthSecret = "GoogleOauthSecret";

        /// <summary>
        /// The app settings key for the admin email address.
        /// </summary>
        public static readonly string AdminEmailAddresses = "AdminEmailAddresses";

        /// <summary>
        /// The app settings key for current condition level.
        /// </summary>
        public static readonly string CurrentConditionLevel = "CurrentConditionLevel";
    }
}
