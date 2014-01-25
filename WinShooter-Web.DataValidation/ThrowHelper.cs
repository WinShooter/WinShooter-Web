// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ThrowHelper.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Internal helper for the validation classes.
//   Performance is much higher for them when the exception throwing is done outside of that class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation
{
    using System;

    /// <summary>
    /// Internal helper for the validation classes.
    /// Performance is much higher for them when the exception throwing is done outside of that class.
    /// </summary>
    internal static class ThrowHelper
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        internal static void ThrowArgumentNullException(string argumentName)
        {
            throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        internal static void ThrowArgumentException(string message)
        {
            throw new ArgumentException(message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        internal static void ThrowArgumentOutOfRangeException(string paramName, string message)
        {
            throw new ArgumentOutOfRangeException(paramName, message);
        }
    }
}
