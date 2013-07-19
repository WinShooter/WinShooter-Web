// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserLoginInfo.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The representation of the database user login info.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The representation of the database user login info.
    /// </summary>
    public class UserLoginInfo
    {
        public virtual Guid Id { get; set; }
        public virtual User User { get; set; }
        public virtual string IdentityProvider { get; set; }
        public virtual string IdentityProviderId { get; set; }
        public virtual DateTime LastLogin { get; set; }
    }
}
