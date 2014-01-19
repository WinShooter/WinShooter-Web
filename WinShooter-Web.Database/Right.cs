// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Right.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The representation of the database right.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The representation of the database right.
    /// </summary>
    public class Right : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Gets or sets the Id of the right.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the right.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="RoleRightsInfo"/>s.
        /// </summary>
        public virtual IList<RoleRightsInfo> RoleRightsInfos { get; set; }
    }
}
