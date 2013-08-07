namespace WinShooter.Api.Tests.Authorization
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WinShooter.Api.Authorization;
    using WinShooter.Database;

    [TestClass]
    public class UserCompetitionRightsTests
    {
        [TestMethod]
        public void TestCorrectRights()
        {
            var competitionId = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9");

            var rights = new UserCompetitionRights(competitionId, new List<string>() { "AddCompetition" });
            Assert.IsTrue(rights.HasPermission("AddCompetition"));
            Assert.IsFalse(rights.HasPermission("DestroyEverything"));
        }

        [TestMethod]
        public void TestFetchingRightsFromDatabase()
        {
            var user = new User { Id = "731bc7fd-1ab6-49ae-8056-92b507eef5f0" };
            var competitionId = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9");

            var rights = new UserCompetitionRights(competitionId, user);
            rights.Permissions.Add("AddCompetition");
            Assert.IsTrue(rights.HasPermission("AddCompetition"));
            Assert.IsFalse(rights.HasPermission("DestroyEverything"));
        }
    }
}
