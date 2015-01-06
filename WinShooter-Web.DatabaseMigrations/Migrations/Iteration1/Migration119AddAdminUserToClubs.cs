// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration119AddAdminUserToClubs.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Create the competition parameters table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// Update the club table with a reference to user table.
    /// </summary>
    [Migration(119)]
    public class Migration119AddAdminUserToClubs : Migration
    {
        /// <summary>
        /// The column name to use.
        /// </summary>
        private const string ColumnName = "AdminUserId";

        /// <summary>
        /// Upgrading the database.
        /// </summary>
        public override void Up()
        {
            this.Alter.Table(Migration104CreateClubsTable.ClubsTableName)
                .AddColumn(ColumnName).AsGuid().Nullable();
        }

        /// <summary>
        /// Downgrading the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.Column(ColumnName)
                .FromTable(Migration104CreateClubsTable.ClubsTableName);
        }
    }
}
