// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitorMap.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
    /// Creates the mapping between the competitor class and the database.
    /// </summary>
    public class CompetitorMap : ClassMap<Competitor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitorMap"/> class.
        /// </summary>
        public CompetitorMap()
        {
            this.Id(x => x.Id);

            this.Map(x => x.ShooterClass).CustomType<ShootersClassEnum>();
            this.Map(x => x.PatrolLane).Column("Lane");
            this.Map(x => x.FinalShootingPlace);

            this.References(x => x.Shooter).Column("ShooterId");
            this.References(x => x.Weapon).Column("WeaponId");
            this.References(x => x.Patrol).Column("PatrolId");
            this.References(x => x.Competition).Column("CompetitionId");

            this.Table("Competitors");
        }
    }
}
