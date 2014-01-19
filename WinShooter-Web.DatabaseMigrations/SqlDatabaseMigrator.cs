// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SqlDatabaseMigrator.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Class for migrating to latest version of the database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DatabaseMigrations
{
    using System.Reflection;

    using FluentMigrator;
    using FluentMigrator.Runner;
    using FluentMigrator.Runner.Announcers;
    using FluentMigrator.Runner.Initialization;

    using WinShooter_Web.DatabaseMigrations;

    /// <summary>
    /// Class for migrating to latest version of the database.
    /// </summary>
    public class SqlDatabaseMigrator : ISqlDatabaseMigrator
    {
        /// <summary>
        /// Migrate to latest version.
        /// </summary>
        /// <param name="connectionString">
        /// The connection string.
        /// </param>
        public void MigrateToLatest(string connectionString)
        {
            var announcer = new TextWriterAnnouncer(s => System.Diagnostics.Debug.WriteLine(s));
            var assembly = Assembly.GetExecutingAssembly();

            var migrationContext = new RunnerContext(announcer)
            {
                Target = assembly.FullName
            };

            var options = new MigrationOptions { PreviewOnly = false, Timeout = 60 };
            var factory = new FluentMigrator.Runner.Processors.SqlServer.SqlServer2012ProcessorFactory();
            var processor = factory.Create(connectionString, announcer, options);
            var runner = new MigrationRunner(assembly, migrationContext, processor);
            runner.MigrateUp(true);
        }

        /// <summary>
        /// The migration options.
        /// </summary>
        private class MigrationOptions : IMigrationProcessorOptions
        {
            /// <summary>
            /// Gets or sets a value indicating whether the migration is preview only.
            /// </summary>
            public bool PreviewOnly { get; set; }

            /// <summary>
            /// Gets or sets the timeout value.
            /// </summary>
            public int Timeout { get; set; }

            /// <summary>
            /// Gets the provider switches.
            /// </summary>
            public string ProviderSwitches { get; private set; }
        }
    }
}
