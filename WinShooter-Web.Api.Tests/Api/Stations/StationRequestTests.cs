// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationRequestTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Tests.Api.Stations
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ServiceStack.Text;

    using WinShooter.Api.Api.Stations;

    /// <summary>
    /// Tests the <see cref="StationRequest"/> class.
    /// </summary>
    [TestClass]
    public class StationRequestTests
    {
        /// <summary>
        /// Verify serialization.
        /// </summary>
        [TestMethod]
        public void Deserialize()
        {
            var stationRequest =
            JsonSerializer.DeserializeFromString<StationRequest>(
                "{\"CompetitionId\":\"9cfc716f-f218-44e2-a72b-a2c40109a2eb\",\"Distinguish\":false,\"NumberOfShots\":\"1\",\"NumberOfTargets\": \"2\",\"Points\":\"true\",\"StationId\":\"9cfc716f-f218-44e2-a72b-a2c40109a200\", \"StationNumber\":\"5\"}");

            Assert.IsNotNull(stationRequest);

            Assert.AreEqual(Guid.Parse("9cfc716f-f218-44e2-a72b-a2c40109a2eb"), stationRequest.CompetitionId);
            Assert.AreEqual(false, stationRequest.Distinguish);
            Assert.AreEqual(1, stationRequest.NumberOfShots);
            Assert.AreEqual(2, stationRequest.NumberOfTargets);
            Assert.AreEqual(true, stationRequest.Points);
            Assert.AreEqual("9cfc716f-f218-44e2-a72b-a2c40109a200", stationRequest.StationId);
            Assert.AreEqual(5, stationRequest.StationNumber);
        }

        /// <summary>
        /// Check parsing the station ID.
        /// </summary>
        [TestMethod]
        public void CheckParsingStationId()
        {
            var stationRequest = new StationRequest { StationId = "9cfc716f-f218-44e2-a72b-a2c40109a200" };
            Assert.AreEqual(Guid.Parse("9cfc716f-f218-44e2-a72b-a2c40109a200"), stationRequest.ParseStationId());

            stationRequest.StationId = null;
            Assert.AreEqual(Guid.Empty, stationRequest.ParseStationId());

            stationRequest.StationId = string.Empty;
            Assert.AreEqual(Guid.Empty, stationRequest.ParseStationId());
        }
    }
}
