// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RightsFromDatabase.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Read and write rights from database.
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

    /// <summary>
    /// Read and write rights from database.
    /// </summary>
    [TestClass]
    public class RightsFromDatabase
    {
        /// <summary>
        /// The competition name.
        /// </summary>
        private const string CompetitionName = "UnitTest_RightsFromDatabase";

        /// <summary>
        /// The competition.
        /// </summary>
        private Competition testCompetition;

        /// <summary>
        /// Make sure the database is latest version.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            var sqlDatabaseMigrator = new SqlDatabaseMigrator();
            sqlDatabaseMigrator.MigrateToLatest(ConfigurationManager.ConnectionStrings["WinShooterConnection"].ConnectionString);

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                this.testCompetition =
                    (from competition in databaseSession.Query<Competition>()
                     where competition.Name == CompetitionName
                     select competition).FirstOrDefault();

                if (this.testCompetition == null)
                {
                    // No test competition found
                    this.testCompetition = new Competition
                                               {
                                                   CompetitionType = CompetitionType.Field,
                                                   Name = CompetitionName,
                                                   StartDate = DateTime.Now
                                               };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testCompetition);
                        transaction.Commit();
                    }
                }
            }
        }

        /// <summary>
        /// Fetch all shooters, add one, fetch all and check it has been added.
        /// Remove shooter, fetch all and check it has been added
        /// </summary>
        [TestMethod]
        public void WriteAndRead()
        {
            var tempName = Guid.NewGuid().ToString();
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var rights = from right in databaseSession.Query<Right>()
                                   where right.Name == tempName
                                   select right;

                Assert.IsNotNull(rights);
                Assert.AreEqual(0, rights.Count());

                var toAdd = new Right
                                {
                                    Id = Guid.NewGuid(),
                                    Name = tempName
                                };

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(toAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var rights = from right in databaseSession.Query<Right>()
                             where right.Name == tempName
                             select right;

                Assert.IsNotNull(rights);
                Assert.AreEqual(1, rights.Count());

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(rights.ToArray()[0]);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var rights = from right in databaseSession.Query<Right>()
                             where right.Name == tempName
                             select right;

                Assert.IsNotNull(rights);
                Assert.AreEqual(0, rights.Count());
            }
        }

        /// <summary>
        /// Tests deleting competition which should cascade.
        /// </summary>
        [TestMethod]
        public void DeleteCompetitionAndCascade()
        {
            var tempName = Guid.NewGuid().ToString();
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var rights = from right in databaseSession.Query<Right>()
                             where right.Name == tempName
                             select right;

                Assert.IsNotNull(rights);
                Assert.AreEqual(0, rights.Count());

                var toAdd = new Right
                {
                    Id = Guid.NewGuid(),
                    Name = tempName
                };

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(toAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var rights = from right in databaseSession.Query<Right>()
                             where right.Name == tempName
                             select right;

                Assert.IsNotNull(rights);
                Assert.AreEqual(1, rights.Count());

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(this.testCompetition);
                    transaction.Commit();
                }

                rights = from right in databaseSession.Query<Right>()
                             where right.Name == tempName
                             select right;

                Assert.IsNotNull(rights);
                Assert.AreEqual(0, rights.Count());
            }
        }
    }
}
