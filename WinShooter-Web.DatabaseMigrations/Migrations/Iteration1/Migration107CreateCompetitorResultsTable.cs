// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration107CreateCompetitorResultsTable.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Creates the competitor results table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Creates the competitor results table.
    /// </summary>
    [Migration(107)]
    public class Migration107CreateCompetitorResultsTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        public const string CompetitorResultsTableName = "CompetitorResults";

        /// <summary>
        /// Upgrades the database.
        /// </summary>
        public override void Up()
        {
            this.Create.Table(CompetitorResultsTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("CompetitorId").AsGuid().Nullable()
                .WithColumn("StationId").AsGuid()
                .WithColumn("Points").AsInt32()
                .WithColumn("TargetHits").AsInt32();

            this.Create.PrimaryKey(string.Format("PK_{0}", CompetitorResultsTableName))
                .OnTable(CompetitorResultsTableName)
                .Column("Id")
                .Clustered();
        }

        /// <summary>
        /// Downgrades the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.Table(CompetitorResultsTableName);
        }
    }
}
