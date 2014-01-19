// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration109CreateTeamToCompetitorTable.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Create the Team to Competitor table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// Create the Team to Competitor table.
    /// </summary>
    [Migration(109)]
    public class Migration109CreateTeamToCompetitorTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        public const string TeamToCompetitorTableName = "TeamToCompetitor";

        /// <summary>
        /// Upgrades the database.
        /// </summary>
        public override void Up()
        {
            this.Create.Table(TeamToCompetitorTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("TeamId").AsGuid()
                .WithColumn("CompetitorId").AsGuid();
        }

        /// <summary>
        /// Downgrades the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.Table(TeamToCompetitorTableName);
        }
    }
}
