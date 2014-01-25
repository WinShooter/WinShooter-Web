// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExtenderClass.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The validation extender with the generic validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The validation extender with the generic validation.
    /// </summary>
    public static class ValidationExtenderClass
    {
        /// <summary>
        /// The not null.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <typeparam name="T">
        /// The type to be checked.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Validation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When the item is null.
        /// </exception>
        [DebuggerHidden]
        public static Validation<T> NotNull<T>(this Validation<T> item) where T : class
        {
            if (item.Value == null)
            {
                ThrowHelper.ThrowArgumentNullException(item.ArgName);
            }

            return item;
        }
    }
}