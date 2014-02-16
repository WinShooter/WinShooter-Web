// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatrolRequestTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Tests the <see cref="CompetitionRequest" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Tests.Api.Patrol
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ServiceStack.Text;

    using WinShooter.Api.Api.Patrols;

    /// <summary>
    /// Tests the <see cref="PatrolRequest"/> class.
    /// </summary>
    [TestClass]
    public class PatrolRequestTests
    {
        /// <summary>
        /// Verify serialization.
        /// </summary>
        [TestMethod]
        public void Deserialize()
        {
            var stationRequest =
            JsonSerializer.DeserializeFromString<PatrolRequest>(
                "{\"CompetitionId\":\"9cfc716f-f218-44e2-a72b-a2c40109a2eb\",\"PatrolId\":\"9cfc716f-f218-44e2-a72b-a2c40109a200\",\"PatrolNumber\":\"5\",\"PatrolClass\":\"1\",\"StartTime\":\"2014-02-16T13:02:00.000Z\"}");

            Assert.IsNotNull(stationRequest);

            Assert.AreEqual(Guid.Parse("9cfc716f-f218-44e2-a72b-a2c40109a2eb"), stationRequest.CompetitionId);
            Assert.AreEqual(1, stationRequest.PatrolClass);
            Assert.AreEqual("9cfc716f-f218-44e2-a72b-a2c40109a200", stationRequest.PatrolId);
            Assert.AreEqual(5, stationRequest.PatrolNumber);
            Assert.AreEqual("2014-02-16T13:02:00.000Z", stationRequest.StartTime);
            Assert.AreEqual(DateTime.Parse("2014-02-16 14:02:00"), stationRequest.ParseStartTime());

            Assert.AreEqual("PatrolRequest [PatrolId: 9cfc716f-f218-44e2-a72b-a2c40109a200, CompetitionId: 9cfc716f-f218-44e2-a72b-a2c40109a2eb, PatrolNumber: 5, StartTime: 2014-02-16T13:02:00.000Z, PatrolClass: 1 ]", stationRequest.ToString());
        }

        /// <summary>
        /// Check parsing the station ID.
        /// </summary>
        [TestMethod]
        public void CheckParsingStationId()
        {
            var stationRequest = new PatrolRequest { PatrolId = "9cfc716f-f218-44e2-a72b-a2c40109a200" };
            Assert.AreEqual(Guid.Parse("9cfc716f-f218-44e2-a72b-a2c40109a200"), stationRequest.ParsePatrolId());

            stationRequest.PatrolId = null;
            Assert.AreEqual(Guid.Empty, stationRequest.ParsePatrolId());

            stationRequest.PatrolId = string.Empty;
            Assert.AreEqual(Guid.Empty, stationRequest.ParsePatrolId());
        }
    }
}
