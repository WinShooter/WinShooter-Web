// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionParamTypeDefaultValues.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The competition parameter type default values.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The competition parameter type default values.
    /// </summary>
    public static class CompetitionParamTypeDefaultValues
    {
        /// <summary>
        /// Get the default value.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetDefaultIntValue(CompetitionParamType type)
        {
            switch (type)
            {
                case CompetitionParamType.MinutesBetweenPatrols:
                    return 15;
                default:
                    throw new NotSupportedException(type.ToString());
            }
        }

        /// <summary>
        /// Get the default value.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool GetDefaultBooleanValue(CompetitionParamType type)
        {
            switch (type)
            {
                default:
                    throw new NotSupportedException(type.ToString());
            }
        }
    }
}
