﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionParamMap.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Creates the mapping between the <see cref="CompetitionParam"/> class and the database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Creates the mapping between the <see cref="CompetitionParam"/> class and the database.
    /// </summary>
    public class CompetitionParamMap : ClassMap<CompetitionParam>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionParamMap"/> class.
        /// </summary>
        public CompetitionParamMap()
        {
            this.Id(x => x.Id);

            this.Map(x => x.CompetitionParameterName).Column("CompetitionParam");
            this.Map(x => x.Value);

            this.References(x => x.Competition).Column("CompetitionId");
            this.Table("CompetitionParams");
        }
    }
}
