// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration117AdoptCompetitionTable.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Adopt patrols table to implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// Adopt competitions table with patrols info.
    /// </summary>
    [Migration(117)]
    public class Migration117AdoptCompetitionTable : Migration
    {
        /// <summary>
        /// Migrates the database up.
        /// </summary>
        public override void Up()
        {
            Alter.Table(Migration106CreateCompetitorsTable.CompetitorsTableName)
                .AlterColumn("PatrolId")
                .AsGuid()
                .Nullable();
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
            Alter.Table(Migration106CreateCompetitorsTable.CompetitorsTableName)
                .AlterColumn("PatrolId")
                .AsGuid()
                .NotNullable();
        }
    }
}
