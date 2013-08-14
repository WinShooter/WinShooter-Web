
namespace WinShooter.Api.Tests.Authorization
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authentication;
    using WinShooter.Api.Authorization;
    using WinShooter.Api.Tests.Dummys;
    using WinShooter.Database;

    /// <summary>
    /// The WinShooter permission attribute tests.
    /// </summary>
    [TestClass]
    public class RequiredWinShooterCompetitionPermissionAttributeTests
    {
        [TestMethod]
        public void TestRetrievingCompetitionIdFromUrl()
        {
            const string Url = "/Competition/731bc7fd-1ab6-49ae-8056-92b507eef5e9";

            var guid = RequiredWinShooterCompetitionPermissionAttribute.GetCompetitionIdFromUrl(Url);

            Assert.AreEqual(Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9"), guid);
        }

        [TestMethod]
        public void TestRetrievingCompetitionIdFromShooterUrl()
        {
            const string Url = "/Competition/731bc7fd-1ab6-49ae-8056-92b507eef5e9/Shooter/aa1bc7fd-1ab6-49ae-8056-92b507eef5e9/";

            var guid = RequiredWinShooterCompetitionPermissionAttribute.GetCompetitionIdFromUrl(Url);

            Assert.AreEqual(Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9"), guid);
        }

        [TestMethod]
        public void TestRetrievingMissingCompetitionIdFromUrl()
        {
            const string Url = "/Competition/";

            var guid = RequiredWinShooterCompetitionPermissionAttribute.GetCompetitionIdFromUrl(Url);

            Assert.AreEqual(guid, Guid.Empty);
        }

        [TestMethod]
        public void TestRetrievingPartialCompetitionIdFromUrl()
        {
            const string Url = "/Competition/731bc7fd-1ab6-49ae-/";

            var guid = RequiredWinShooterCompetitionPermissionAttribute.GetCompetitionIdFromUrl(Url);

            Assert.AreEqual(Guid.Empty, guid);
        }

        [TestMethod]
        public void TestCorrectRights()
        {
            var user = new User();
            var competitionId = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9");

            var rights = new UserCompetitionRights(competitionId, new List<string>(new []{"AddCompetition"}));

            var requiredWinShooterPermissionAttribute = new RequiredWinShooterCompetitionPermissionAttribute(
                ApplyTo.All,
                rights,
                "AddCompetition");

            var session = new CustomUserSession { User = user };

            var httpRequest = new DummyHttpRequest { PathInfo = "/Competition/" + competitionId + "/" };

            var userAuthRepo = new DummyUserAuthRepository();

            Assert.IsTrue(requiredWinShooterPermissionAttribute.HasAllPermissions(httpRequest, session, userAuthRepo));
        }

        [TestMethod]
        public void TestIncorrectRights()
        {
            var user = new User();
            var competitionId = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9");

            var rights = new UserCompetitionRights(competitionId, new List<string>(new[] { "AddCompetition" }));

            var requiredWinShooterPermissionAttribute = new RequiredWinShooterCompetitionPermissionAttribute(
                ApplyTo.All,
                rights,
                "RemoveCompetition");

            var session = new CustomUserSession { User = user };

            var httpRequest = new DummyHttpRequest { PathInfo = "/Competition/" + competitionId + "/" };

            var userAuthRepo = new DummyUserAuthRepository(); 
            
            Assert.IsFalse(requiredWinShooterPermissionAttribute.HasAllPermissions(httpRequest, session, userAuthRepo));
        }
    }
}
