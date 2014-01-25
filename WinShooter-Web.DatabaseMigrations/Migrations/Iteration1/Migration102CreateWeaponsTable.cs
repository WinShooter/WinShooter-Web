// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration102CreateWeaponsTable.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Creates the weapons table.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;
    using FluentMigrator.Runner.Extensions;

    /// <summary>
    /// Creates the weapons table.
    /// </summary>
    [Migration(102)]
    public class Migration102CreateWeaponsTable : Migration
    {
        /// <summary>
        /// The table name.
        /// </summary>
        private const string WeaponsTableName = "Weapons";

        /// <summary>
        /// Function for upgrading the database.
        /// </summary>
        public override void Up()
        {
            this.Create.Table(WeaponsTableName)
                .WithColumn("Id").AsGuid()
                .WithColumn("Manufacturer").AsString()
                .WithColumn("Model").AsString()
                .WithColumn("Caliber").AsString()
                .WithColumn("Class").AsInt32()
                .WithColumn("LastUpdated").AsDateTime();

            this.Create.PrimaryKey(string.Format("PK_{0}", WeaponsTableName))
                .OnTable(WeaponsTableName)
                .Column("Id")
                .Clustered();
        }

        /// <summary>
        /// Function for downgrading the database.
        /// </summary>
        public override void Down()
        {
            this.Delete.Table(WeaponsTableName);
        }
    }
}
