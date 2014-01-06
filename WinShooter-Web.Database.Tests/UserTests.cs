// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserTests.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Tests the <see cref="User" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Database.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="User"/> class.
    /// </summary>
    [TestClass]
    public class UserTests
    {
        /// <summary>
        /// Test the string method.
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            var user = new User { Id = Guid.Parse("74ec4f92-4b72-4c40-927a-de308269e074") };

            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"\", Surname=\"\", CardNumber=\"\", Email=\"\", LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.CardNumber = "123";
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"\", Surname=\"\", CardNumber=\"123\", Email=\"\", LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.ClubId = Guid.Parse("74ec4f92-4b72-4c40-927a-de308269e123");
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"\", Surname=\"\", CardNumber=\"123\", Email=\"\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.Email = "mail@example.com";
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"\", Surname=\"\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.Givenname = "Givenname";
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"Givenname\", Surname=\"\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.LastLogin = DateTime.Parse("2014-01-06 21:29:12");
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"Givenname\", Surname=\"\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=2014-01-06 21:29:12, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.LastUpdated = DateTime.Parse("2014-01-06 21:30:45");
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"Givenname\", Surname=\"\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=2014-01-06 21:29:12, LastUpdated=2014-01-06 21:30:45 ]", user.ToString());

            user.Surname = "Surname";
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"Givenname\", Surname=\"Surname\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=2014-01-06 21:29:12, LastUpdated=2014-01-06 21:30:45 ]", user.ToString());
        }

        /// <summary>
        /// Test the string method.
        /// </summary>
        [TestMethod]
        public void ToStringWithNulls()
        {
            var user = new User { Id = Guid.Parse("74ec4f92-4b72-4c40-927a-de308269e074"), CardNumber = null, ClubId = Guid.Empty, Email = null, Givenname = null, Surname = null };

            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.CardNumber = "123";
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, CardNumber=\"123\", LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.ClubId = Guid.Parse("74ec4f92-4b72-4c40-927a-de308269e123");
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, CardNumber=\"123\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.Email = "mail@example.com";
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.Givenname = "Givenname";
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"Givenname\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=0001-01-01 00:00:00, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.LastLogin = DateTime.Parse("2014-01-06 21:29:12");
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"Givenname\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=2014-01-06 21:29:12, LastUpdated=0001-01-01 00:00:00 ]", user.ToString());

            user.LastUpdated = DateTime.Parse("2014-01-06 21:30:45");
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"Givenname\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=2014-01-06 21:29:12, LastUpdated=2014-01-06 21:30:45 ]", user.ToString());

            user.Surname = "Surname";
            Assert.AreEqual("User [ Id=74ec4f92-4b72-4c40-927a-de308269e074, Givenname=\"Givenname\", Surname=\"Surname\", CardNumber=\"123\", Email=\"mail@example.com\", ClubId=74ec4f92-4b72-4c40-927a-de308269e123, LastLogin=2014-01-06 21:29:12, LastUpdated=2014-01-06 21:30:45 ]", user.ToString());
        }

        /// <summary>
        /// Test the display name method.
        /// </summary>
        [TestMethod]
        public void DisplayName()
        {
            var user = new User() { Email = "email@example.com" };

            Assert.AreEqual("email@example.com", user.DisplayName);

            user.Surname = "Surname";
            Assert.AreEqual("email@example.com", user.DisplayName);

            user.Givenname = "GivenName";
            Assert.AreEqual("GivenName Surname", user.DisplayName);
        }
    }
}
