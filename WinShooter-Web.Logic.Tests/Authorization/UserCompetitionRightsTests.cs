// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCompetitionRightsTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Defines the UserCompetitionRightsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Tests.Authorization
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using WinShooter.Database;
    using WinShooter.Logic.Authorization;

    /// <summary>
    /// Tests the <see cref="UserCompetitionRights"/> class.
    /// </summary>
    [TestClass]
    public class UserCompetitionRightsTests
    {
        /// <summary>
        /// Test with correct rights.
        /// </summary>
        [TestMethod]
        public void TestCorrectRights()
        {
            var competitionId = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9");

            var rights = new UserCompetitionRights(competitionId, new List<WinShooterCompetitionPermissions> { WinShooterCompetitionPermissions.CreateCompetition });
            Assert.IsTrue(rights.HasPermission(WinShooterCompetitionPermissions.CreateCompetition));
            Assert.IsFalse(rights.HasPermission(WinShooterCompetitionPermissions.CreateUserCompetitionRole));
        }

        /// <summary>
        /// Test with incorrect rights.
        /// </summary>
        [TestMethod]
        public void TestFetchingRightsFromDatabase()
        {
            var user = new User { Id = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5f0") };
            var competitionId = Guid.Parse("731bc7fd-1ab6-49ae-8056-92b507eef5e9");

            var rightsHelper = new Mock<IRightsHelper>();

            var rights = new UserCompetitionRights(competitionId, user, rightsHelper.Object);
            rights.Permissions.Add(WinShooterCompetitionPermissions.CreateCompetition);
            Assert.IsTrue(rights.HasPermission(WinShooterCompetitionPermissions.CreateCompetition));
            Assert.IsFalse(rights.HasPermission(WinShooterCompetitionPermissions.CreateUserCompetitionRole));
        }
    }
}
