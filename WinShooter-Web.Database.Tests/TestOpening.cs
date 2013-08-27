// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestOpening.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Defines the TestOpening type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database.Tests
{
    using System.Configuration;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NHibernate.Linq;

    using WinShooter_Web.DatabaseMigrations;

    /// <summary>
    /// Test opening database connection.
    /// </summary>
    [TestClass]
    public class TestOpening
    {
        [TestInitialize]
        public void Setup()
        {
            var sqlDatabaseMigrator = new SqlDatabaseMigrator();
            sqlDatabaseMigrator.MigrateToLatest(ConfigurationManager.ConnectionStrings["WinShooterConnection"].ConnectionString);
        }

        [TestMethod]
        public void SmallOpeningTest()
        {
            using (var dbsession = NHibernateHelper.OpenSession())
            {
                var userLoginInfo = (from info in dbsession.Query<UserLoginInfo>()
                                     where
                                         info.IdentityProvider == "google" && info.IdentityProviderId == "something"
                                     select info).SingleOrDefault();
                Assert.IsNull(userLoginInfo);
            }
        }
    }
}
