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
    using WinShooter.Database;

    /// <summary>
    /// Tests the <see cref="CompetitionResponse"/> class.
    /// </summary>
    [TestClass]
    public class CompetitionResponseTests
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

            var response = new CompetitionResponse(dbcompetition);
            Assert.AreEqual(
                "{\"CompetitionId\":\"74ec4f92-4b72-4c40-927a-de308269e074\",\"CompetitionType\":\"Field\",\"IsPublic\":true,\"Name\":\"Name\",\"StartDate\":\"\\\"\\\\/Date(1391200860000-0000)\\\\/\\\"\",\"UseNorwegianCount\":true}",
                JsonSerializer.SerializeToString(response));
        }
    }
}
