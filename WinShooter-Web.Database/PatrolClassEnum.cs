// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatrolClassEnum.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   PatrolClass
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// PatrolClass
    /// </summary>
    [Serializable]
    public enum PatrolClassEnum
    {
        /// <summary>
        /// 
        /// </summary>
        Okänd = 0,
        /// <summary>
        /// 
        /// </summary>
        A = 1,
        /// <summary>
        /// 
        /// </summary>
        B = 2,
        /// <summary>
        /// 
        /// </summary>
        C = 3,
        /// <summary>
        /// 
        /// </summary>
        R = 4,
        /// <summary>
        /// 
        /// </summary>
        M = 5,
        /// <summary>
        /// 
        /// </summary>
        AR = 11,
        /// <summary>
        /// 
        /// </summary>
        BC = 12,
        /// <summary>
        /// 
        /// </summary>
        ABCRM = 99
    }
}
