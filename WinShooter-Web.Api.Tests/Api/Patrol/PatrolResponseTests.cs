// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatrolResponseTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Tests.Api.Patrol
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ServiceStack.Text;

    using WinShooter.Api.Api.Patrols;
    using WinShooter.Database;

    /// <summary>
    /// Tests the <see cref="PatrolResponse"/> class.
    /// </summary>
    [TestClass]
    public class PatrolResponseTests
    {
        /// <summary>
        /// Verify serialization.
        /// </summary>
        [TestMethod]
        public void Serialize()
        {
            var dbcompetition = new Competition
            {
                Id = Guid.Parse("74ec4f92-4b72-4c40-927a-de308269e074"),
                CompetitionType = CompetitionType.Field,
                IsPublic = true,
                Name = "Name",
                StartDate = DateTime.Parse("2014-01-31 21:41:00"),
                UseNorwegianCount = true
            };

            var dbstation = new Patrol
            {
                Id = Guid.Parse("74ec4f92-4b72-4c40-927a-de308269e074"),
                Competition = dbcompetition,
                PatrolNumber = 2,
                PatrolClass = PatrolClassEnum.B,
                StartTime = DateTime.Parse("2014-02-16 11:27:00")
            };

            var response = new PatrolResponse(dbstation);
            Assert.AreEqual(
                "{\"CompetitionId\":\"74ec4f924b724c40927ade308269e074\",\"PatrolId\":\"74ec4f92-4b72-4c40-927a-de308269e074\",\"PatrolNumber\":2,\"StartTime\":\"2014-02-16T10:27:00.000Z\",\"PatrolClass\":2}",
                JsonSerializer.SerializeToString(response));
        }
    }
}
