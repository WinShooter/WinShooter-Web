// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WeaponClassEnum.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   WeaponClass
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// Weapon Classes.
    /// </summary>
    [Serializable]
    public enum WeaponClassEnum
    {
        /// <summary>
        /// Unknown weapons.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// The A1 class.
        /// </summary>
        A1 = 1,

        /// <summary>
        /// The A2 class.
        /// </summary>
        A2 = 2,

        /// <summary>
        /// The A3 class.
        /// </summary>
        A3 = 3,

        /// <summary>
        /// The B class.
        /// </summary>
        B = 11,

        /// <summary>
        /// The C class.
        /// </summary>
        C = 21,

        /// <summary>
        /// The R class.
        /// </summary>
        R = 31,

        /// <summary>
        /// The M class.
        /// </summary>
        M = 41,

        /// <summary>
        /// The M1 class.
        /// </summary>
        M1 = 42,

        /// <summary>
        /// The M2 class.
        /// </summary>
        M2 = 43,

        /// <summary>
        /// The M3 class.
        /// </summary>
        M3 = 44,

        /// <summary>
        /// The M4 class.
        /// </summary>
        M4 = 45,

        /// <summary>
        /// The M5 class.
        /// </summary>
        M5 = 46,

        /// <summary>
        /// The M6 class.
        /// </summary>
        M6 = 47,

        /// <summary>
        /// The M7 class.
        /// </summary>
        M7 = 48,

        /// <summary>
        /// The M8 class.
        /// </summary>
        M8 = 49,

        /// <summary>
        /// The M9 class.
        /// </summary>
        M9 = 50
    }
}
