// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionResponseTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Tests the <see cref="CompetitionResponse" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Tests.Api.Competitions
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ServiceStack.Text;

    using WinShooter.Api.Api.Competition;

    /// <summary>
    /// Tests the <see cref="CompetitionRequest"/> class.
    /// </summary>
    [TestClass]
    public class CompetitionRequestTests
    {
        /// <summary>
        /// Verify serialization.
        /// </summary>
        [TestMethod]
        public void Deserialize()
        {
            var competitionRequest = 
            JsonSerializer.DeserializeFromString<CompetitionRequest>(
                "{\"CompetitionId\":\"9cfc716f-f218-44e2-a72b-a2c40109a2eb\",\"CompetitionType\":\"Field\",\"IsPublic\":true,\"Name\":\"aaaaaaaaaaaaaaaaaa\",\"StartDate\":\"2014-01-31T12:00:00.000Z\",\"UseNorwegianCount\":true}");

            Assert.IsNotNull(competitionRequest);

            Assert.AreEqual("9cfc716f-f218-44e2-a72b-a2c40109a2eb", competitionRequest.CompetitionId);
            Assert.AreEqual("Field", competitionRequest.CompetitionType);
            Assert.AreEqual(true, competitionRequest.IsPublic);
            Assert.AreEqual("aaaaaaaaaaaaaaaaaa", competitionRequest.Name);
            Assert.AreEqual("2014-01-31T12:00:00.000Z", competitionRequest.StartDate);
            Assert.AreEqual(true, competitionRequest.UseNorwegianCount);
            Assert.AreEqual(DateTime.Parse("2014-01-31 13:00:00.000"), competitionRequest.ParseStartDate());
        }
    }
}
