// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExtenderDateTimes.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Validation of <see cref="DateTime"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Validation of <see cref="DateTime"/>.
    /// </summary>
    public static class ValidationExtenderDateTimes
    {
        /// <summary>
        /// Validates that a <see cref="DateTime"/> is default value.
        /// </summary>
        /// <param name="item">The item to validate.</param>
        /// <returns>The item that has been validated.</returns>
        [DebuggerHidden]
        public static Validation<DateTime> NotDefault(this Validation<DateTime> item)
        {
            if (!DateTime.MinValue.Equals(item.Value))
            {
                return item;
            }

            ThrowHelper.ThrowArgumentOutOfRangeException(item.ArgName, "Has to be other than default date.");

            // The return statement is only done to fool ReSharper
            return item;
        }

        /// <summary>
        /// Validates that a <see cref="DateTime"/> is default value.
        /// </summary>
        /// <param name="item">
        /// The item to validate.
        /// </param>
        /// <param name="maxNumberOfYears">
        /// The max Number Of Years.
        /// </param>
        /// <returns>
        /// The item that has been validated.
        /// </returns>
        [DebuggerHidden]
        public static Validation<DateTime> WithinYears(this Validation<DateTime> item, int maxNumberOfYears)
        {
            if ((item.Value - DateTime.Now).TotalDays < 365 * maxNumberOfYears)
            {
                return item;
            }

            ThrowHelper.ThrowArgumentOutOfRangeException(item.ArgName, string.Format("Has to be within {0} years of current date", maxNumberOfYears));

            // The return statement is only done to fool ReSharper
            return item;
        }
    }
}
