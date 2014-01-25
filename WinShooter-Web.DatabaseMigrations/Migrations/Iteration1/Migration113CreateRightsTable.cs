// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration113CreateRightsTable.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
    [Migration(113)]
    public class Migration113CreateRightsTable : Migration
    {
        /// <summary>
        /// The users table name.
        /// </summary>
        internal const string RightsTableName = "Rights";

        /// <summary>
        /// The users login info table name.
        /// </summary>
        internal const string RoleRightsInfoTableName = "RoleRightsInfo";

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
            this.Create.Table(RightsTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("Name").AsString().NotNullable();

            this.Create.PrimaryKey(string.Format("PK_{0}", RightsTableName))
                .OnTable(RightsTableName)
                .Column("Id")
                .Clustered();

            this.Create.Table(RoleRightsInfoTableName)
                .WithColumn("Id").AsInt32().Identity()
                .WithColumn("RoleId").AsGuid().NotNullable()
                .WithColumn("RightId").AsGuid().NotNullable();

            this.Create.PrimaryKey(string.Format("PK_{0}", RoleRightsInfoTableName))
                .OnTable(RoleRightsInfoTableName)
                .Column("Id")
                .Clustered();

            this.Create.ForeignKey(RoleRightsInfoRolesForeignKeyName)
                .FromTable(RoleRightsInfoTableName).ForeignColumn("RoleId")
                .ToTable(Migration112CreateRolesTable.RolesTableName).PrimaryColumn("Id");

            this.Create.ForeignKey(RoleRightsInfoRightsForeignKeyName)
                .FromTable(RoleRightsInfoTableName).ForeignColumn("RightId")
                .ToTable(RightsTableName).PrimaryColumn("Id");
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
            this.Delete.ForeignKey(RoleRightsInfoRightsForeignKeyName);
            this.Delete.ForeignKey(RoleRightsInfoRolesForeignKeyName);
            this.Delete.Table(RightsTableName);
            this.Delete.Table(RoleRightsInfoTableName);
        }
    }
}
