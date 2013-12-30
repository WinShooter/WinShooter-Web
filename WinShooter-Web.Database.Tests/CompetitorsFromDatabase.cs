// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitorsFromDatabase.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Defines the ClubsFromDatabase type.
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
    /// Read and write clubs from database.
    /// </summary>
    [TestClass]
    public class CompetitorsFromDatabase
    {
        /// <summary>
        /// The competition name.
        /// </summary>
        private const string CompetitionName = "UnitTestCompetitionName";

        /// <summary>
        /// The club name.
        /// </summary>
        private const string ClubName = "UnitTestClubName";

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
                // Make sure there is a test competition
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

                // Make sure there is a test club
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

                // Clear out the current competitors
                var competitors = from competitor in databaseSession.Query<Competitor>()
                                  where competitor.Competition == this.testCompetition
                                  select competitor;

                using (var transaction = databaseSession.BeginTransaction())
                {
                    foreach (var competitor in competitors)
                    {
                        databaseSession.Delete(competitor);
                    }
                    
                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Fetch all weapons, add one, fetch all and check it has been added.
        /// Remove weapon, fetch all and check it has been added
        /// </summary>
        [TestMethod]
        public void WriteAndRead()
        {
            var tempName = Guid.NewGuid().ToString();
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitors = from competitor in databaseSession.Query<Competitor>()
                                  where competitor.Competition == this.testCompetition
                                  select competitor;

                Assert.IsNotNull(competitors);
                Assert.AreEqual(0, competitors.Count());

                var clubToAdd = new Weapon { Manufacturer = tempName, Caliber = "Caliber", Class = WeaponClassEnum.A1, Model = "Model" };
                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(clubToAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var weapons = from weapon in databaseSession.Query<Weapon>()
                              where weapon.Manufacturer == tempName
                              select weapon;

                Assert.IsNotNull(weapons);
                Assert.AreEqual(1, weapons.Count());

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(weapons.ToArray()[0]);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var weapons = from weapon in databaseSession.Query<Weapon>()
                              where weapon.Manufacturer == tempName
                              select weapon;

                Assert.IsNotNull(weapons);
                Assert.AreEqual(0, weapons.Count());
            }
        }
    }
}
