// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShootersFromDatabase.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Read and write shooters from database.
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
    /// Read and write shooters from database.
    /// </summary>
    [TestClass]
    public class ShootersFromDatabase
    {
        /// <summary>
        /// The competition name.
        /// </summary>
        private const string CompetitionName = "UnitTest_ShootersFromDatabase";

        /// <summary>
        /// The club name.
        /// </summary>
        private const string ClubName = "UnitTest_ShootersFromDatabase";

        /// <summary>
        /// The shooter name for test WriteAndRead
        /// </summary>
        private const string ShooterNameForWriteAndRead = "ShootersFromDatabase_WriteAndRead";

        /// <summary>
        /// The shooter name for test DeleteCompetitionAndCascade.
        /// </summary>
        private const string ShoooterNameForDeleteCompetitionAndCascade = "ShootersFromDatabase_DeleteCompetitionAndCascade";

        /// <summary>
        /// The competition.
        /// </summary>
        private Competition testCompetition;

        /// <summary>
        /// The club.
        /// </summary>
        private Club testClub;

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

                this.testClub = (from club in databaseSession.Query<Club>()
                                 where club.Name == ClubName
                                 select club).FirstOrDefault();

                if (this.testClub == null)
                {
                    // No test club found
                    this.testClub = new Club
                    {
                        ClubId = "1-123",
                        Country = "SE",
                        Name = ClubName
                    };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testClub);
                        transaction.Commit();
                    }
                }

                using (var transaction = databaseSession.BeginTransaction())
                {
                    foreach (var shooter in (from dbShooter in databaseSession.Query<Shooter>()
                                             where
                                                 dbShooter.Surname == ShooterNameForWriteAndRead
                                                 || dbShooter.Surname == ShoooterNameForDeleteCompetitionAndCascade
                                             select dbShooter).ToArray())
                    {
                        databaseSession.Delete(shooter);
                    }

                    transaction.Commit();
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
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var shooters = from shooter in databaseSession.Query<Shooter>()
                               where shooter.Surname == ShooterNameForWriteAndRead &&
                                   shooter.Givenname == ShooterNameForWriteAndRead
                                   select shooter;

                Assert.IsNotNull(shooters);
                Assert.AreEqual(0, shooters.Count());

                var toAdd = new Shooter
                                {
                                    Surname = ShooterNameForWriteAndRead,
                                    Givenname = ShooterNameForWriteAndRead,
                                    Competition = this.testCompetition,
                                    CardNumber = "123",
                                    Class = ShootersClassEnum.Klass1,
                                    Club = this.testClub,
                                    Email = string.Empty,
                                    HasArrived = false,
                                    LastUpdated = DateTime.Now, 
                                    Paid = 0, 
                                    SendResultsByEmail = false
                                };

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(toAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var shooters = (from shooter in databaseSession.Query<Shooter>()
                               where shooter.Surname == ShooterNameForWriteAndRead &&
                               shooter.Givenname == ShooterNameForWriteAndRead
                               select shooter).ToArray();

                Assert.IsNotNull(shooters);
                Assert.AreEqual(1, shooters.Count());
                Assert.IsNotNull(shooters[0].Competition);

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(shooters[0]);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var shooters = from shooter in databaseSession.Query<Shooter>()
                               where shooter.Surname == ShooterNameForWriteAndRead &&
                               shooter.Givenname == ShooterNameForWriteAndRead
                               select shooter;

                Assert.IsNotNull(shooters);
                Assert.AreEqual(0, shooters.Count());
            }
        }

        /// <summary>
        /// Tests deleting competition which should cascade.
        /// </summary>
        [TestMethod]
        public void DeleteCompetitionAndCascade()
        {
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var shooters = (from shooter in databaseSession.Query<Shooter>()
                               where shooter.Surname == ShoooterNameForDeleteCompetitionAndCascade &&
                               shooter.Givenname == ShoooterNameForDeleteCompetitionAndCascade
                               select shooter).ToArray();

                Assert.IsNotNull(shooters);
                Assert.AreEqual(0, shooters.Count());

                var toAdd = new Shooter
                {
                    Surname = ShoooterNameForDeleteCompetitionAndCascade,
                    Givenname = ShoooterNameForDeleteCompetitionAndCascade,
                    Competition = this.testCompetition,
                    CardNumber = "123",
                    Class = ShootersClassEnum.Klass1,
                    Club = this.testClub,
                    Email = string.Empty,
                    HasArrived = false,
                    LastUpdated = DateTime.Now,
                    Paid = 0,
                    SendResultsByEmail = false
                };

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(toAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competition = (from dbcompetition in databaseSession.Query<Competition>()
                                   where dbcompetition.Id.Equals(this.testCompetition.Id)
                                   select dbcompetition).First();

                var shooters = from shooter in databaseSession.Query<Shooter>()
                               where shooter.Surname == ShoooterNameForDeleteCompetitionAndCascade &&
                               shooter.Givenname == ShoooterNameForDeleteCompetitionAndCascade
                               select shooter;

                Assert.IsNotNull(shooters);
                Assert.AreEqual(1, shooters.Count());

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(competition);
                    transaction.Commit();
                }

                shooters = from shooter in databaseSession.Query<Shooter>()
                               where shooter.Surname == ShoooterNameForDeleteCompetitionAndCascade &&
                               shooter.Givenname == ShoooterNameForDeleteCompetitionAndCascade
                               select shooter;

                Assert.IsNotNull(shooters);
                Assert.AreEqual(0, shooters.Count());
            }
        }
    }
}
