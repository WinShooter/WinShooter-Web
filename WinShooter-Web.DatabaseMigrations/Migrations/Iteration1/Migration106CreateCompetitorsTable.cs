// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration106CreateCompetitorsTable.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Creates the competitors table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Creates the competitors table.
    /// </summary>
    [Migration(106)]
    public class Migration106CreateCompetitorsTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        public const string CompetitorsTableName = "Competitors";

        /// <summary>
        /// Upgrades the database.
        /// </summary>
        public override void Up()
        {
            this.Create.Table(CompetitorsTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("ShooterId").AsGuid().Nullable()
                .WithColumn("ShooterClass").AsInt32()
                .WithColumn("WeaponId").AsGuid()
                .WithColumn("PatrolId").AsGuid()
                .WithColumn("Lane").AsInt32()
                .WithColumn("FinalShootingPlace").AsInt32()
                .WithColumn("CompetitionId").AsGuid();

            this.Create.PrimaryKey(string.Format("PK_{0}", CompetitorsTableName))
                .OnTable(CompetitorsTableName)
                .Column("Id")
                .Clustered();
        }

        /// <summary>
        /// Downgrades the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.Table(CompetitorsTableName);
        }
    }
}
