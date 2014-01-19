// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Extends the generic classes with a Require method.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation
{
    /// <summary>
    /// Extends the generic classes with a Require method.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The require argument extension method.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <param name="argName">
        /// The argument name.
        /// </param>
        /// <typeparam name="T">
        /// The type that will be extended.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Validation"/>.
        /// </returns>
        public static Validation<T> Require<T>(this T item, string argName)
        {
            return new Validation<T>(item, argName);
        }
    }
}
