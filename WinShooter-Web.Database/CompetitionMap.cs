// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionMap.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Creates the mapping between the competition class and the database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database
{
    using FluentNHibernate.Mapping;

    /// <summary>
    /// Creates the mapping between the competition class and the database.
    /// </summary>
    public class CompetitionMap : ClassMap<Competition>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionMap"/> class.
        /// </summary>
        public CompetitionMap()
        {
            this.Id(x => x.Id);

            this.Map(x => x.Name);
            this.Map(x => x.StartDate);
            this.Map(x => x.CompetitionType).CustomType<CompetitionType>();
            this.Map(x => x.UseNorwegianCount);
            this.Map(x => x.IsPublic);

            HasMany(x => x.Shooters).KeyColumn("CompetitionId").Cascade.AllDeleteOrphan();
            HasMany(x => x.Patrols).KeyColumn("CompetitionId").Cascade.AllDeleteOrphan();
            HasMany(x => x.Stations).KeyColumn("CompetitionId").Cascade.AllDeleteOrphan();
            HasMany(x => x.UserRoleInfos).KeyColumn("CompetitionId").Cascade.AllDeleteOrphan();

            this.Table("Competition");
        }
    }
}
