namespace WinShooter.Api.Tests.Authorization
{
    using System;
    using System.Diagnostics;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WinShooter.Api.Authorization;
    using WinShooter.Database;

    [TestClass]
    public class RequiredWinShooterPermissionAttributeTests
    {
        [TestMethod]
        public void TestRetrievingCompetitionIdFromUrl()
        {
            var url = "/Competition/731bc7fd-1ab6-49ae-8056-92b507eef5e9";

            var guid = RequiredWinShooterPermissionAttribute.GetCompetitionIdFromUrl(url);

            Assert.AreEqual(guid, Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9"));
        }

        [TestMethod]
        public void TestCorrectRights()
        {
            var user = new User();
            var competitionId = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9");
            Trace.WriteLine(competitionId.ToString());

            var rights = new UserCompetitionRights(competitionId, user);
            rights.Permissions.Add("AddCompetition");


        }
    }
}
