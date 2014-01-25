// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Validation.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The validation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation
{
    /// <summary>
    /// The validation.
    /// </summary>
    /// <typeparam name="T">
    /// The type to validate.
    /// </typeparam>
    public class Validation<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Validation{T}"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="argName">
        /// The argument name.
        /// </param>
        public Validation(T value, string argName)
        {
            this.Value = value;
            this.ArgName = argName;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Gets the argument name.
        /// </summary>
        public string ArgName { get; private set; }
    }
}
