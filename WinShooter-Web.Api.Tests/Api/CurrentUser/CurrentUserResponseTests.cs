// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentUserResponseTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Tests.Api.CurrentUser
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ServiceStack.Text;

    using WinShooter.Api.Api.CurrentUser;

    /// <summary>
    /// Tests the <see cref="CurrentUserResponse"/> class.
    /// </summary>
    [TestClass]
    public class CurrentUserResponseTests
    {
        /// <summary>
        /// Verify serialization.
        /// </summary>
        [TestMethod]
        public void Serialize()
        {
            var response = new CurrentUserResponse
                               {
                                   CompetitionRights = new[] { "right1", "right2" },
                                   DisplayName = "My Display Name",
                                   Email = "email@example.com",
                                   IsLoggedIn = true
                               };

            Assert.AreEqual(
                "{\"IsLoggedIn\":true,\"DisplayName\":\"My Display Name\",\"Email\":\"email@example.com\",\"CompetitionRights\":[\"right1\",\"right2\"]}",
                JsonSerializer.SerializeToString(response));
        }
    }
}
