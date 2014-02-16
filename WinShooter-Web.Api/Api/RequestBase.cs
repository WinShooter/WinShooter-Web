// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequestBase.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The request base.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api
{
    using System;
    using System.Globalization;

    using WinShooter.Web.DataValidation;

    /// <summary>
    /// The request base.
    /// </summary>
    public abstract class RequestBase
    {
        /// <summary>
        /// Parse a <see cref="Guid"/> string.
        /// </summary>
        /// <param name="guidString">
        /// The <see cref="Guid"/> string.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/>.
        /// </returns>
        protected Guid ParseGuidString(string guidString)
        {
            return string.IsNullOrEmpty(guidString) ? Guid.Empty : Guid.Parse(guidString);
        }

        /// <summary>
        /// Parses date time string.
        /// </summary>
        /// <param name="dateTimeString">
        /// The date time string.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        protected DateTime ParseDateTimeString(string dateTimeString)
        {
            dateTimeString.Require("dateTimeString").NotNull().ShorterThan(25).LongerThan(23);

            return DateTime.ParseExact(
                dateTimeString,
                "yyyy-MM-dd'T'HH:mm:ss.fff'Z'",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal);
        }
    }
}
