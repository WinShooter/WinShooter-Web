// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration115AddAcceptedTermsToUser.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Add columns HasAcceptedTerms to users table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// Add columns HasAcceptedTerms to users table.
    /// </summary>
    [Migration(115)]
    public class Migration115AddAcceptedTermsToUser : Migration
    {
        /// <summary>
        /// The column name.
        /// </summary>
        private const string HasAcceptedTermsColumnName = "HasAcceptedTerms";

        /// <summary>
        /// Migrates the database up.
        /// </summary>
        public override void Up()
        {
            Alter.Table(Migration111CreateUsersTable.UsersTableName).AddColumn(HasAcceptedTermsColumnName).AsInt32().WithDefaultValue(0);
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
            Delete.Column(HasAcceptedTermsColumnName).FromTable(Migration111CreateUsersTable.UsersTableName);
        }
    }
}
