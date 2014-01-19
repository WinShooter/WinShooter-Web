// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration104CreateClubsTable.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Creates the clubs table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// Creates the clubs table.
    /// </summary>
    [Migration(104)]
    public class Migration104CreateClubsTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        private const string ClubsTableName = "Clubs";

        /// <summary>
        /// Function for upgrading the database.
        /// </summary>
        public override void Up()
        {
            Create.Table(ClubsTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("ClubId").AsString()
                .WithColumn("Name").AsString()
                .WithColumn("Country").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("Plusgiro").AsString()
                .WithColumn("Bankgiro").AsString()
                .WithColumn("LastUpdated").AsDateTime();
        }

        /// <summary>
        /// Function for downgrading the database.
        /// </summary>
        public override void Down()
        {
            Delete.Table(ClubsTableName);
        }
    }
}
