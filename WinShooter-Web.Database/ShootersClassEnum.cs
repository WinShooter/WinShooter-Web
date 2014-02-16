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
        /// Class 1
        /// </summary>
        Klass1 = 1,

        /// <summary>
        /// Class 2
        /// </summary>
        Klass2 = 2,

        /// <summary>
        /// Class 3
        /// </summary>
        Klass3 = 3,

        /// <summary>
        /// All classes
        /// </summary>
        Klass = 4,

        /// <summary>
        /// Ladies class.
        /// </summary>
        Damklass = 10,

        /// <summary>
        /// Lades class 1
        /// </summary>
        Damklass1 = 11,

        /// <summary>
        /// Ladies class 2
        /// </summary>
        Damklass2 = 12,

        /// <summary>
        /// Lades class 2
        /// </summary>
        Damklass3 = 13,

        /// <summary>
        /// Junior class
        /// </summary>
        Juniorklass = 20,

        /// <summary>
        /// Veteran class, younger.
        /// </summary>
        VeteranklassYngre = 30,

        /// <summary>
        /// Veteran class, older
        /// </summary>
        VeteranklassÄldre = 40,

        /// <summary>
        /// Open class
        /// </summary>
        Öppen = 50
    }
}
