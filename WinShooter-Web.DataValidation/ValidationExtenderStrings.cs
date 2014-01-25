// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExtenderStrings.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The validation extender with the string validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation
{
    using System;
    using System.Diagnostics;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The validation extender with the string validation.
    /// </summary>
    public static class ValidationExtenderStrings
    {
        /// <summary>
        /// Make sure the string is shorter than the limit.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="maxLength">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="Validation"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the item is longer than the limit>
        /// </exception>
        [DebuggerHidden]
        public static Validation<string> ShorterThan(this Validation<string> item, int maxLength)
        {
            if (item.Value == null)
            {
                ThrowHelper.ThrowArgumentNullException(item.ArgName);

                // The return statement is only done to fool ReSharper
                return item;
            }

            if (item.Value.Length >= maxLength)
            {
                ThrowHelper.ThrowArgumentException(
                    string.Format("Parameter {0} must be shorter than {1} chars", item.ArgName, maxLength));
            }

            return item;
        }

        /// <summary>
        /// Make sure the string is longer than the limit.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="minimumLength">
        /// The limit.
        /// </param>
        /// <returns>
        /// The <see cref="Validation"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the item is longer than the limit>
        /// </exception>
        [DebuggerHidden]
        public static Validation<string> LongerThan(this Validation<string> item, int minimumLength)
        {
            if (item.Value == null)
            {
                ThrowHelper.ThrowArgumentNullException(item.ArgName);

                // The return statement is only done to fool ReSharper
                return item;
            }

            if (item.Value.Length < minimumLength)
            {
                ThrowHelper.ThrowArgumentException(
                    string.Format("Parameter {0} must be longer than {1} chars", item.ArgName, minimumLength));
            }

            return item;
        }

        /// <summary>
        /// Validators the email address.
        /// </summary>
        /// <param name="item">The item to validate</param>
        /// <returns>The validated item</returns>
        /// <exception cref="ArgumentException">
        /// Thrown when the string isn't an email address.
        /// </exception>
        [DebuggerHidden]
        public static Validation<string> ValidEmailAddress(this Validation<string> item)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            if (!regex.IsMatch(item.Value))
            {
                ThrowHelper.ThrowArgumentException(item.ArgName + " has to be a valid email address.");
            }

            return item;
        }
    }
}
