// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration103CreateShootersTable.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Creates shooters table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter_Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// Creates shooters table.
    /// </summary>
    [Migration(103)]
    public class Migration103CreateShootersTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        private const string ShootersTableName = "Shooters";

        /// <summary>
        /// Function for upgrading the database.
        /// </summary>
        public override void Up()
        {
            Create.Table(ShootersTableName)
                .WithColumn("Id").AsGuid().PrimaryKey().Indexed()
                .WithColumn("CompetitionId").AsGuid().Indexed()
                .WithColumn("CardNumber").AsString()
                .WithColumn("Surname").AsString()
                .WithColumn("Givenname").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("ClubId").AsGuid()
                .WithColumn("Paid").AsInt32()
                .WithColumn("Class").AsInt32()
                .WithColumn("HasArrived").AsBoolean()
                .WithColumn("SendResultsByEmail").AsBoolean()
                .WithColumn("LastUpdated").AsDateTime();
        }

        /// <summary>
        /// Function for downgrading the database.
        /// </summary>
        public override void Down()
        {
            Delete.Table(ShootersTableName);
        }
    }
}
