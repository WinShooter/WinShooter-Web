// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionsFromDatabase.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Read and write competitions from database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database.Tests
{
    using System;
    using System.Configuration;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NHibernate.Linq;

    using WinShooter.Web.DatabaseMigrations;

    using WinShooter_Web.DatabaseMigrations;

    /// <summary>
    /// Read and write competitions from database.
    /// </summary>
    [TestClass]
    public class CompetitionsFromDatabase
    {
        /// <summary>
        /// Make sure the database is latest version.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var sqlDatabaseMigrator = new SqlDatabaseMigrator();
            sqlDatabaseMigrator.MigrateToLatest(ConfigurationManager.ConnectionStrings["WinShooterConnection"].ConnectionString);
        }

        /// <summary>
        /// Fetch all clubs, add one, fetch all and check it has been added.
        /// Remove club, fetch all and check it has been added
        /// </summary>
        [TestMethod]
        public void WriteAndReadCompetitions()
        {
            var tempName = Guid.NewGuid().ToString();
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitions = from competition in databaseSession.Query<Competition>()
                                   where competition.Name == tempName
                                   select competition;

                Assert.IsNotNull(competitions);
                Assert.AreEqual(0, competitions.Count());

                var toAdd = new Competition
                                {
                                    Name = tempName,
                                    CompetitionType = CompetitionType.Field,
                                    IsPublic = true,
                                    StartDate = DateTime.Now,
                                    UseNorwegianCount = false
                                };

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(toAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitions = from competition in databaseSession.Query<Competition>()
                                   where competition.Name == tempName
                                   select competition;

                Assert.IsNotNull(competitions);
                Assert.AreEqual(1, competitions.Count());

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(competitions.ToArray()[0]);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitions = from competition in databaseSession.Query<Competition>()
                                   where competition.Name == tempName
                                   select competition;

                Assert.IsNotNull(competitions);
                Assert.AreEqual(0, competitions.Count());
            }
        }
    }
}
