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
            const string Url = "/Competition/731bc7fd-1ab6-49ae-8056-92b507eef5e9";

            var guid = RequiredWinShooterPermissionAttribute.GetCompetitionIdFromUrl(Url);

            Assert.AreEqual(Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9"), guid);
        }

        [TestMethod]
        public void TestRetrievingMissingCompetitionIdFromUrl()
        {
            const string Url = "/Competition/";

            var guid = RequiredWinShooterPermissionAttribute.GetCompetitionIdFromUrl(Url);

            Assert.AreEqual(guid, Guid.Empty);
        }

        [TestMethod]
        public void TestRetrievingPartialCompetitionIdFromUrl()
        {
            const string Url = "/Competition/731bc7fd-1ab6-49ae-/";

            var guid = RequiredWinShooterPermissionAttribute.GetCompetitionIdFromUrl(Url);

            Assert.AreEqual(Guid.Empty, guid);
        }

        [TestMethod]
        public void TestCorrectRights()
        {
            var user = new User();
            var competitionId = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9");
            Trace.WriteLine(competitionId.ToString());

            var rights = new UserCompetitionRights(competitionId, user);
            rights.Permissions.Add("AddCompetition");

            throw new NotImplementedException();
        }
    }
}
