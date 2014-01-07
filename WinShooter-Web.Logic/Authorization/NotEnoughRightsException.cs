// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotEnoughRightsException.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The not enough rights exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Authorization
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The not enough rights exception.
    /// </summary>
    [Serializable]
    public class NotEnoughRightsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughRightsException"/> class.
        /// </summary>
        public NotEnoughRightsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughRightsException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public NotEnoughRightsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughRightsException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public NotEnoughRightsException(string message, Exception inner) 
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotEnoughRightsException"/> class.
        /// This constructor is needed for serialization.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected NotEnoughRightsException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
