// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserRolesInfosFromDatabase.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Read and write user roles from database.
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
    /// Read and write roles from database.
    /// </summary>
    [TestClass]
    public class UserRolesInfosFromDatabase
    {
        /// <summary>
        /// The name of the unit test competition
        /// </summary>
        private const string CompetitionName = "UnitTestCompetitionName";

        /// <summary>
        ///  The name of the unit test club.
        /// </summary>
        private const string ClubName = "UnitTestClubName";

        /// <summary>
        /// The name of the unit test user.
        /// </summary>
        private const string UserName = "UnitTestUser";

        /// <summary>
        /// The name of the unit test user.
        /// </summary>
        private const string UserCardNumber = "12345678";

        /// <summary>
        /// The name of the unit test role.
        /// </summary>
        private const string RoleName = "UnitTestRole";

        /// <summary>
        /// The competition used in unit tests.
        /// </summary>
        private Competition testCompetition;

        /// <summary>
        /// The club used in unit tests.
        /// </summary>
        private Club testClub;

        /// <summary>
        /// The user used in unit tests.
        /// </summary>
        private User testUser;

        /// <summary>
        /// The role used in unit tests.
        /// </summary>
        private Role testRole;

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
                // Check there is a test competition
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

                // Check there is a test club
                this.testClub =
                    (from club in databaseSession.Query<Club>() 
                     where club.Name == ClubName
                     select club)
                        .FirstOrDefault();

                if (this.testClub == null)
                {
                    // No test club found
                    this.testClub = new Club
                                        {
                                            Id = Guid.NewGuid(),
                                            Name = ClubName,
                                            Bankgiro = string.Empty,
                                            ClubId = "123-123",
                                            Country = "SE",
                                            Email = string.Empty,
                                            LastUpdated = DateTime.Now,
                                            Plusgiro = string.Empty
                                        };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testClub);
                        transaction.Commit();
                    }
                }

                // Check there is a test user
                this.testUser =
                    (from user in databaseSession.Query<User>()
                     where user.Givenname == UserName && user.Surname == UserName && user.CardNumber == UserCardNumber
                     select user).FirstOrDefault();

                if (this.testUser == null)
                {
                    this.testUser = new User
                                        {
                                            CardNumber = UserCardNumber,
                                            Surname = UserName,
                                            Givenname = UserName,
                                            ClubId = this.testClub.Id,
                                            Email = string.Empty,
                                            Id = Guid.NewGuid(),
                                            LastLogin = DateTime.Now,
                                            LastUpdated = DateTime.Now,
                                        };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testUser);
                        transaction.Commit();
                    }
                }

                // Check there is a test role
                this.testRole =
                    (from role in databaseSession.Query<Role>() where role.RoleName == RoleName select role)
                        .FirstOrDefault();

                if (this.testRole == null)
                {
                    // There is no test role, create
                    this.testRole = new Role { Id = Guid.NewGuid(), RoleName = RoleName };

                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        databaseSession.Save(this.testRole);
                        transaction.Commit();
                    }
                }

                // Make sure there are no existing roles
                var userRolesInfos = from userRolesInfo in databaseSession.Query<UserRolesInfo>()
                                     where userRolesInfo.User.Id.Equals(this.testUser.Id)
                                     select userRolesInfo;

                if (userRolesInfos.Any())
                {
                    using (var transaction = databaseSession.BeginTransaction())
                    {
                        foreach (var userRolesInfo in userRolesInfos)
                        {
                            databaseSession.Delete(userRolesInfo);
                        }

                        transaction.Commit();
                    }
                }
            }
        }

        /// <summary>
        /// Fetch all user roles, add one, fetch all and check it has been added.
        /// Remove user role, fetch all and check it has been added
        /// </summary>
        [TestMethod]
        public void WriteAndRead()
        {
            var tempName = Guid.NewGuid().ToString();
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var userRolesInfos = from userRolesInfo in databaseSession.Query<UserRolesInfo>()
                                   where userRolesInfo.User.Id.Equals(this.testUser.Id)
                                   select userRolesInfo;

                Assert.IsNotNull(userRolesInfos);
                Assert.AreEqual(0, userRolesInfos.Count());

                var toAdd = new UserRolesInfo
                                {
                                    Competition = this.testCompetition,
                                    Role = this.testRole,
                                    User = this.testUser
                                };

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(toAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var userRolesInfos = from userRolesInfo in databaseSession.Query<UserRolesInfo>()
                                     where userRolesInfo.User.Id.Equals(this.testUser.Id)
                                     select userRolesInfo;

                Assert.IsNotNull(userRolesInfos);
                Assert.AreEqual(1, userRolesInfos.Count());

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(userRolesInfos.ToArray()[0]);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var userRolesInfos = from userRolesInfo in databaseSession.Query<UserRolesInfo>()
                                     where userRolesInfo.User.Id.Equals(this.testUser.Id)
                                     select userRolesInfo;

                Assert.IsNotNull(userRolesInfos);
                Assert.AreEqual(0, userRolesInfos.Count());
            }
        }
    }
}
