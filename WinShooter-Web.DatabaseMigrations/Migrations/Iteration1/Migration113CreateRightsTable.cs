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
    [Migration(113)]
    public class Migration113CreateRightsTable : Migration
    {
        /// <summary>
        /// The users table name.
        /// </summary>
        private const string RightsTableName = "Rights";

        /// <summary>
        /// The users login info table name.
        /// </summary>
        private const string RoleRightsInfoTableName = "RoleRightsInfo";

        /// <summary>
        /// The name of the foreign key between <see cref="RightsTableName"/> and <see cref="RoleRightsInfoTableName"/> tables.
        /// </summary>
        private const string RoleRightsInfoRolesForeignKeyName = "FK_RoleRightsInfo_Roles";

        /// <summary>
        /// The name of the foreign key between <see cref="RightsTableName"/> and <see cref="RoleRightsInfoTableName"/> tables.
        /// </summary>
        private const string RoleRightsInfoRightsForeignKeyName = "FK_RoleRightsInfo_Rights";

        /// <summary>
        /// Migrates the database up.
        /// </summary>
        public override void Up()
        {
            Create.Table(RightsTableName)
                .WithColumn("Id").AsString().NotNullable().PrimaryKey()
                .WithColumn("Right").AsString().NotNullable();

            Create.Table(RoleRightsInfoTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("RoleId").AsString().NotNullable()
                .WithColumn("RightId").AsString().NotNullable();

            Create.ForeignKey(RoleRightsInfoRolesForeignKeyName)
                .FromTable(RoleRightsInfoTableName).ForeignColumn("RoleId")
                .ToTable(Migration112CreateRolesTable.RolesTableName).PrimaryColumn("Id");

            Create.ForeignKey(RoleRightsInfoRightsForeignKeyName)
                .FromTable(RoleRightsInfoTableName).ForeignColumn("RightId")
                .ToTable(RightsTableName).PrimaryColumn("Id");
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
            Delete.ForeignKey(RoleRightsInfoRightsForeignKeyName);
            Delete.ForeignKey(RoleRightsInfoRolesForeignKeyName);
            Delete.Table(RightsTableName);
            Delete.Table(RoleRightsInfoTableName);
        }
    }
}
