// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppConfig.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
            this.AdminUserNames = appSettings.Get("AdminUserNames", new List<string>());
        }

        public List<string> AdminUserNames { get; set; }
    }
}