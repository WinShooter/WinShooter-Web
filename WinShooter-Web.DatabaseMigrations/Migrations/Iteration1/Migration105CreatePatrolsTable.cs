// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration105CreatePatrolsTable.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Defines the Migration105CreatePatrolsTable type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Creates the patrols table.
    /// </summary>
    [Migration(105)]
    public class Migration105CreatePatrolsTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        public const string PatrolsTableName = "Patrols";

        /// <summary>
        /// Upgrades the database.
        /// </summary>
        public override void Up()
        {
            this.Create.Table(PatrolsTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("PatrolId").AsInt32()
                .WithColumn("StartTime").AsDateTime()
                .WithColumn("CompetitionId").AsGuid().Nullable()
                .WithColumn("PatrolClass").AsInt32()
                .WithColumn("StartTimeDisplay").AsDateTime();

            this.Create.PrimaryKey(string.Format("PK_{0}", PatrolsTableName))
                .OnTable(PatrolsTableName)
                .Column("Id")
                .Clustered();
        }

        /// <summary>
        /// Downgrades the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.Table(PatrolsTableName);
        }
    }
}
