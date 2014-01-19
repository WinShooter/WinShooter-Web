// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FilterConfig.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The configuration of MVC filters.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter
{
    using System.Web.Mvc;

    /// <summary>
    /// The configuration of MVC filters.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Handles the registration of global filters.
        /// </summary>
        /// <param name="filters">
        /// The <see cref="GlobalFilterCollection"/> that handles the configuration.
        /// </param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}