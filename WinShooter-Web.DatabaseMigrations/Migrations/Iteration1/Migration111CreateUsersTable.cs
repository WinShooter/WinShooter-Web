// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration111CreateUsersTable.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
    [Migration(111)]
    public class Migration111CreateUsersTable : Migration
    {
        /// <summary>
        /// The users table name.
        /// </summary>
        internal const string UsersTableName = "Users";

        /// <summary>
        /// The users login info table name.
        /// </summary>
        private const string UsersLoginInfoTableName = "UsersLoginInfo";

        /// <summary>
        /// The name of the foreign key between <see cref="UsersTableName"/> and <see cref="UsersLoginInfoTableName"/> tables.
        /// </summary>
        private const string UsersLoginInfoForeignKeyName = "FK_Users_UsersLoginInfo";

        /// <summary>
        /// Migrates the database up.
        /// </summary>
        public override void Up()
        {
            this.Create.Table(UsersTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("CardNumber").AsString()
                .WithColumn("Surname").AsString()
                .WithColumn("Givenname").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("ClubId").AsGuid()
                .WithColumn("LastUpdated").AsDateTime()
                .WithColumn("LastLogin").AsDateTime();

            this.Create.PrimaryKey(string.Format("PK_{0}", UsersTableName))
                .OnTable(UsersTableName)
                .Column("Id")
                .Clustered();

            this.Create.Table(UsersLoginInfoTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("UserId").AsGuid()
                .WithColumn("IdentityProvider").AsString().Indexed()
                .WithColumn("ProviderUserId").AsString().Indexed()
                .WithColumn("Email").AsString()
                .WithColumn("LastLogin").AsDateTime();

            this.Create.PrimaryKey(string.Format("PK_{0}", UsersLoginInfoTableName))
                .OnTable(UsersLoginInfoTableName)
                .Column("Id")
                .Clustered();

            this.Create.ForeignKey(UsersLoginInfoForeignKeyName)
                .FromTable(UsersLoginInfoTableName).ForeignColumn("UserId")
                .ToTable(UsersTableName).PrimaryColumn("Id");
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
            this.Delete.ForeignKey(UsersLoginInfoForeignKeyName);
            this.Delete.Table(UsersTableName);
            this.Delete.Table(UsersLoginInfoTableName);
        }
    }
}
