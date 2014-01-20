// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration100CreateCompetitionTable.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The migration which creates the competition table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// The migration which creates the competition table.
    /// </summary>
    [Migration(100)]
    public class Migration100CreateCompetitionTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        internal const string CompetitionTableName = "Competition";

        /// <summary>
        /// Upgrading the database.
        /// </summary>
        public override void Up()
        {
            this.Create.Table(CompetitionTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("Name").AsString()
                .WithColumn("StartDate").AsDateTime()
                .WithColumn("CompetitionType").AsInt32()
                .WithColumn("UseNorwegianCount").AsBoolean()
                .WithColumn("IsPublic").AsBoolean();
        }

        /// <summary>
        /// Downgrading the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.Table(CompetitionTableName);
        }
    }
}
