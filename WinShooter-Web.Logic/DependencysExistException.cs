// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DependencysExistException.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//    Thrown when there is a dependency that could not be deleted.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    /// <summary>
    /// Thrown when there is a dependency that could not be deleted.
    /// </summary>
    [Serializable]
    public class DependencysExistException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencysExistException"/> class.
        /// </summary>
        public DependencysExistException()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DependencysExistException"/> class.
        /// </summary>
        public DependencysExistException(ConflictingDepencencyEnum conflictingDepencency)
        {
            this.ConflictingDepencency = conflictingDepencency;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencysExistException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="inner">
        /// The inner.
        /// </param>
        public DependencysExistException(string message, Exception inner) 
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencysExistException"/> class.
        /// This constructor is needed for serialization.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        protected DependencysExistException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("ConflictingDepencency", this.ConflictingDepencency);
        }

        /// <summary>
        /// The types of dependencies to conflict.
        /// </summary>
        public enum ConflictingDepencencyEnum
        {
            Shooter,
            Competition
        }

        /// <summary>
        /// The conflicting dependency.
        /// </summary>
        public ConflictingDepencencyEnum ConflictingDepencency { get; set; }
    }
}
