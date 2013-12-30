// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RolesFromDatabase.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Read and write roles from database.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database.Tests
{
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
    public class RolesFromDatabase
    {
        /// <summary>
        /// The name of the unit test role.
        /// </summary>
        private const string RoleName = "UnitTestRole";

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
                var roles = from role in databaseSession.Query<Role>()
                            where role.RoleName == RoleName
                            select role;

                using (var transaction = databaseSession.BeginTransaction())
                {
                    foreach (var role in roles)
                    {
                        databaseSession.Delete(role);
                    }

                    transaction.Commit();
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
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var roles = from role in databaseSession.Query<Role>()
                            where role.RoleName == RoleName
                            select role;

                Assert.IsNotNull(roles);
                Assert.AreEqual(0, roles.Count());

                var toAdd = new Role { RoleName = RoleName };

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Save(toAdd);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var roles = from role in databaseSession.Query<Role>()
                            where role.RoleName == RoleName
                            select role;

                Assert.IsNotNull(roles);
                Assert.AreEqual(1, roles.Count());

                using (var transaction = databaseSession.BeginTransaction())
                {
                    databaseSession.Delete(roles.ToArray()[0]);
                    transaction.Commit();
                }
            }

            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var roles = from role in databaseSession.Query<Role>()
                            where role.RoleName == RoleName
                            select role;

                Assert.IsNotNull(roles);
                Assert.AreEqual(0, roles.Count());
            }
        }
    }
}
