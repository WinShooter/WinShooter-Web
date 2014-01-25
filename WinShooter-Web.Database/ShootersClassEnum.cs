// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShootersClassEnum.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The Shooter class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The Shooter class.
    /// </summary>
    [Serializable]
    public enum ShootersClassEnum
    {
        /// <summary>
        /// Unknown class.
        /// </summary>
        Okänd = 0,
        /// <summary>
        /// 
        /// </summary>
        Klass1 = 1,
        /// <summary>
        /// 
        /// </summary>
        Klass2 = 2,
        /// <summary>
        /// 
        /// </summary>
        Klass3 = 3,
        /// <summary>
        /// 
        /// </summary>
        Klass = 4,
        /// <summary>
        /// 
        /// </summary>
        Damklass = 10,
        /// <summary>
        /// 
        /// </summary>
        Damklass1 = 11,
        /// <summary>
        /// 
        /// </summary>
        Damklass2 = 12,
        /// <summary>
        /// 
        /// </summary>
        Damklass3 = 13,
        /// <summary>
        /// 
        /// </summary>
        Juniorklass = 20,
        /// <summary>
        /// 
        /// </summary>
        VeteranklassYngre = 30,
        /// <summary>
        /// 
        /// </summary>
        VeteranklassÄldre = 40,
        /// <summary>
        /// 
        /// </summary>
        Öppen = 50
    }
}
