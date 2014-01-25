// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration101CreateStationsTable.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Create the stations table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Create the stations table.
    /// </summary>
    [Migration(101)]
    public class Migration101CreateStationsTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        private const string StationsTableName = "Stations";

        /// <summary>
        /// Upgrading the database.
        /// </summary>
        public override void Up()
        {
            this.Create.Table(StationsTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("CompetitionId").AsGuid().Nullable()
                .WithColumn("NumberOfTargets").AsInt32()
                .WithColumn("NumberOfShots").AsInt32()
                .WithColumn("Points").AsBoolean()
                .WithColumn("Distinguish").AsBoolean()
                .WithColumn("StationNumber").AsInt32();

            this.Create.PrimaryKey(string.Format("PK_{0}", StationsTableName))
                .OnTable(StationsTableName)
                .Column("Id")
                .Clustered();
        }

        /// <summary>
        /// Downgrading the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.Table(StationsTableName);
        }
    }
}
