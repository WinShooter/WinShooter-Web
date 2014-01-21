// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration112CreateRolesTable.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

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
            this.Create.Table(RolesTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("RoleName").AsString().NotNullable();

            this.Create.PrimaryKey(string.Format("PK_{0}", RolesTableName))
                .OnTable(RolesTableName)
                .Column("Id")
                .Clustered();

            this.Create.Table(UserRolesInfoTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("UserId").AsGuid().NotNullable()
                .WithColumn("RoleId").AsGuid().NotNullable()
                .WithColumn("CompetitionId").AsGuid().Nullable();

            this.Create.PrimaryKey(string.Format("PK_{0}", UserRolesInfoTableName))
                .OnTable(UserRolesInfoTableName)
                .Column("Id")
                .Clustered();

            this.Create.ForeignKey(UserRolesInfoRolesForeignKeyName)
                .FromTable(UserRolesInfoTableName).ForeignColumn("RoleId")
                .ToTable(RolesTableName).PrimaryColumn("Id");

            this.Create.ForeignKey(UserRolesInfoUsersForeignKeyName)
                .FromTable(UserRolesInfoTableName).ForeignColumn("UserId")
                .ToTable(Migration111CreateUsersTable.UsersTableName).PrimaryColumn("Id");

            this.Create.ForeignKey(UserRolesInfoCompetitionForeignKeyName)
                .FromTable(UserRolesInfoTableName).ForeignColumn("CompetitionId")
                .ToTable(Migration100CreateCompetitionTable.CompetitionTableName).PrimaryColumn("Id");
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
            this.Delete.ForeignKey(UserRolesInfoCompetitionForeignKeyName);
            this.Delete.ForeignKey(UserRolesInfoUsersForeignKeyName);
            this.Delete.ForeignKey(UserRolesInfoRolesForeignKeyName);
            this.Delete.Table(RolesTableName);
            this.Delete.Table(UserRolesInfoTableName);
        }
    }
}
