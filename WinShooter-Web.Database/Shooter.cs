// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Shooter.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The shooter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The shooter.
    /// </summary>
    public class Shooter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shooter"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public Shooter()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public virtual Guid Id { get; set; }

        public virtual Competition Competition { get; set; }

        public virtual string CardNumber { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Givenname { get; set; }
        public virtual string Email { get; set; }

        public virtual Club Club { get; set; }
        public virtual int Paid { get; set; }
        public virtual ShootersClassEnum Class { get; set; }
        public virtual bool HasArrived { get; set; }
        public virtual bool SendResultsByEmail { get; set; }
        public virtual DateTime LastUpdated { get; set; }
    }
}
