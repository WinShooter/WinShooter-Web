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
    [Migration(112)]
    public class Migration112CreateRolesTable : Migration
    {
        /// <summary>
        /// The users table name.
        /// </summary>
        internal const string RolesTableName = "Roles";

        /// <summary>
        /// The users login info table name.
        /// </summary>
        private const string UserRolesInfoTableName = "UserRolesInfo";

        /// <summary>
        /// The name of the foreign key between <see cref="RolesTableName"/> and <see cref="UserRolesInfoTableName"/> tables.
        /// </summary>
        private const string UserRolesInfoRolesForeignKeyName = "FK_UserRolesInfo_Roles";

        /// <summary>
        /// The name of the foreign key between <see cref="RolesTableName"/> and <see cref="UserRolesInfoTableName"/> tables.
        /// </summary>
        private const string UserRolesInfoUsersForeignKeyName = "FK_UserRolesInfo_Users";

        /// <summary>
        /// The name of the foreign key between <see cref="RolesTableName"/> and <see cref="UserRolesInfoTableName"/> tables.
        /// </summary>
        private const string UserRolesInfoCompetitionForeignKeyName = "FK_UserRolesInfo_Competition";

        /// <summary>
        /// Migrates the database up.
        /// </summary>
        public override void Up()
        {
            Create.Table(RolesTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("RoleName").AsString().NotNullable();

            Create.Table(UserRolesInfoTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("RoleId").AsGuid().NotNullable()
                .WithColumn("CompetitionId").AsGuid().NotNullable();

            Create.ForeignKey(UserRolesInfoRolesForeignKeyName)
                .FromTable(UserRolesInfoTableName).ForeignColumn("RoleId")
                .ToTable(RolesTableName).PrimaryColumn("Id");

            Create.ForeignKey(UserRolesInfoUsersForeignKeyName)
                .FromTable(UserRolesInfoTableName).ForeignColumn("UserId")
                .ToTable(Migration111CreateUsersTable.UsersTableName).PrimaryColumn("Id");

            Create.ForeignKey(UserRolesInfoCompetitionForeignKeyName)
                .FromTable(UserRolesInfoTableName).ForeignColumn("CompetitionId")
                .ToTable(Migration100CreateCompetitionTable.CompetitionTableName).PrimaryColumn("Id");
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
            Delete.ForeignKey(UserRolesInfoCompetitionForeignKeyName);
            Delete.ForeignKey(UserRolesInfoUsersForeignKeyName);
            Delete.ForeignKey(UserRolesInfoRolesForeignKeyName);
            Delete.Table(RolesTableName);
            Delete.Table(UserRolesInfoTableName);
        }
    }
}
