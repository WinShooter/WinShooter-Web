namespace WinShooter.Database.Tests
{
    using System.Configuration;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NHibernate.Linq;

    using WinShooter_Web.DatabaseMigrations;

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
