// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionParam.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The representation of the database competition parameter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using System;

    /// <summary>
    /// The representation of the database competition parameter.
    /// </summary>
    public class CompetitionParam : IWinshooterDatabaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionParam"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors", Justification = "We need to create the GUID")]
        public CompetitionParam()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets or sets the competition ID.
        /// </summary>
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the competition ID.
        /// </summary>
        public virtual Competition Competition { get; set; }

        /// <summary>
        /// Gets or sets the competition parameter name.
        /// </summary>
        public virtual string CompetitionParameterName { get; set; }

        /// <summary>
        /// Gets or sets the competition parameter name.
        /// </summary>
        public virtual CompetitionParamType CompetitionParameterType
        {
            get
            {
                return (CompetitionParamType)Enum.Parse(typeof(CompetitionParamType), this.CompetitionParameterName);
            }

            set
            {
                this.CompetitionParameterName = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the parameter value.
        /// </summary>
        public virtual string Value { get; set; }
    }
}
