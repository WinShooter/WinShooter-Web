﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitorResultsFromDatabase.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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

    using WinShooter_Web.DatabaseMigrations;

    /// <summary>
    /// Read and write clubs from database.
    /// </summary>
    [TestClass]
    public class CompetitorResultsFromDatabase
    {
        private const string CompetitionName = "UnitTestCompetitionName";

        private const string ClubName = "UnitTestClubName";

        private const string ShooterSurname = "UnitTestShooterName";

        private const string ShooterGivenname = "UnitTestShooterName";

        private const string WeaponManufacturer = "UnitTestWeaponManufacturer";

        private Competition testCompetition;

        private Club testClub;

        private Weapon testWeapon;

        private Patrol testPatrol;

        private Station testStation;

        private Competitor testCompetitor;

        private Shooter testShooter;

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

                // Make sure there is a test station
                this.testStation = (from station in databaseSession.Query<Station>()
                                 where station.Competition == this.testCompetition
                                 select station).FirstOrDefault();

                if (this.testStation == null)
                {
                    // No test club found
                    this.testStation = new Station
                    {
                        Competition = this.testCompetition,
                        Distinguish = false,
                        NumberOfShots = 6,
                        NumberOfTargets = 6,
                        Points = 10,
                        StationNumber = 1
                    };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testStation);
                        transaction.Commit();
                    }
                }

                // Make sure there is a test weapon
                this.testWeapon = (from weapon in databaseSession.Query<Weapon>()
                                 where weapon.Manufacturer == WeaponManufacturer
                                 select weapon).FirstOrDefault();

                if (this.testWeapon == null)
                {
                    // No test weapon found
                    this.testWeapon = new Weapon
                    {
                        Caliber = WeaponManufacturer,
                        Class = WeaponClassEnum.A1,
                        LastUpdated = DateTime.Now,
                        Manufacturer = WeaponManufacturer,
                        Model = WeaponManufacturer
                    };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testWeapon);
                        transaction.Commit();
                    }
                }

                // Make sure there is a test patrol
                this.testPatrol =
                    (from patrol in databaseSession.Query<Patrol>()
                     where patrol.Competition == this.testCompetition
                     select patrol).FirstOrDefault();

                if (this.testPatrol == null)
                {
                    this.testPatrol = new Patrol
                    {
                        Competition = this.testCompetition,
                        PatrolClass = PatrolClassEnum.A,
                        PatrolId = 1,
                        StartTime = DateTime.Now,
                        StartTimeDisplay = DateTime.Now
                    };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testPatrol);
                        transaction.Commit();
                    }
                }

                // Make sure there is a test shooter
                this.testShooter = (from shooter in databaseSession.Query<Shooter>()
                                    where
                                        shooter.Competition == this.testCompetition
                                        && shooter.Surname == ShooterSurname
                                        && shooter.Givenname == ShooterGivenname 
                                        select shooter).FirstOrDefault();

                if (this.testShooter == null)
                {
                    this.testShooter = new Shooter
                    {
                        Competition = this.testCompetition,
                        CardNumber = "123",
                        Class = ShootersClassEnum.Klass1,
                        Club = this.testClub,
                        Email = string.Empty,
                        Givenname = ShooterGivenname,
                        Surname = ShooterSurname,
                        HasArrived = true,
                        LastUpdated = DateTime.Now
                    };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testPatrol);
                        transaction.Commit();
                    }
                }

                // Make sure there is a competitor
                this.testCompetitor = (from competitor in databaseSession.Query<Competitor>()
                                       where competitor.Competition == this.testCompetition
                                       select competitor).FirstOrDefault();

                if (this.testCompetitor == null)
                {
                    // No test competitor found
                    this.testCompetitor = new Competitor
                                              {
                                                  Competition = this.testCompetition,
                                                  FinalShootingPlace = 100,
                                                  Patrol = this.testPatrol,
                                                  PatrolLane = 1,
                                                  Shooter = this.testShooter,
                                                  ShooterClass = ShootersClassEnum.Klass1,
                                                  Weapon = this.testWeapon
                                              };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testCompetitor);
                        transaction.Commit();
                    }
                }

                // Clear out the current competitor results
                var competitors = from competitor in databaseSession.Query<CompetitorResult>()
                                  where competitor.Competitor == this.testCompetitor
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
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitorResults = from competitorResult in databaseSession.Query<CompetitorResult>()
                                  where competitorResult.Competitor == this.testCompetitor
                                  select competitorResult;

                Assert.IsNotNull(competitorResults);
                Assert.AreEqual(0, competitorResults.Count());

                var toAdd = new CompetitorResult
                                {
                                    Competitor = this.testCompetitor,
                                    Station = this.testStation
                                };
                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(toAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitorResults = from competitorResult in databaseSession.Query<CompetitorResult>()
                                        where competitorResult.Competitor == this.testCompetitor
                                        select competitorResult;

                Assert.IsNotNull(competitorResults);
                Assert.AreEqual(1, competitorResults.Count());

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(competitorResults.ToArray()[0]);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitorResults = from competitorResult in databaseSession.Query<CompetitorResult>()
                                        where competitorResult.Competitor == this.testCompetitor
                                        select competitorResult;

                Assert.IsNotNull(competitorResults);
                Assert.AreEqual(0, competitorResults.Count());
            }
        }
    }
}
