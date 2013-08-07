// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration111CreateUsersTable.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Creates the users users table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// Creates the users users table.
    /// </summary>
    [Migration(114)]
    public class Migration114AddRoleRights : Migration
    {
        /// <summary>
        /// Migrates the database up.
        /// </summary>
        public override void Up()
        {
            Execute.EmbeddedScript("Migration114AddRoleRights.sql");
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
            Execute.Sql("DELETE FROM " + Migration112CreateRolesTable.RolesTableName);
            Execute.Sql("DELETE FROM " + Migration113CreateRightsTable.RightsTableName);
            Execute.Sql("DELETE FROM " + Migration113CreateRightsTable.RoleRightsInfoTableName);
        }
    }
}
