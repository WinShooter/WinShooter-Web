﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Migration116AdoptPatrolsTable.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Adopt patrols table to implementation.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations.Migrations.Iteration1
{
    using FluentMigrator;

    /// <summary>
    /// Adopt patrols table to implementation.
    /// </summary>
    [Migration(116)]
    public class Migration116AdoptPatrolsTable : Migration
    {
        /// <summary>
        /// Migrates the database up.
        /// </summary>
        public override void Up()
        {
            Delete.Column("[StartTimeDisplay]").FromTable(Migration105CreatePatrolsTable.PatrolsTableName);
        }

        /// <summary>
        /// Migrates the database down.
        /// </summary>
        public override void Down()
        {
        }
    }
}
