// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppConfigTests.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Tests the <see cref="AppConfig" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Tests
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using ServiceStack.Configuration;

    using WinShooter.Api;

    /// <summary>
    /// Tests the <see cref="AppConfig"/> class.
    /// </summary>
    [TestClass]
    public class AppConfigTests
    {
        /// <summary>
        /// The test 1.
        /// </summary>
        [TestMethod]
        public void Test1()
        {
            var moq = new Moq.Mock<IResourceManager>();
            moq.Setup(x => x.GetList("AdminEmailAddresses")).Returns(new List<string> { "john.smith@example.com", " john.doe@example.com " });
            var appConfig = new AppConfig(moq.Object);

            Assert.AreEqual(2, appConfig.AdminEmailAddresses.Count);
            Assert.AreEqual("JOHN.SMITH@EXAMPLE.COM", appConfig.AdminEmailAddresses[0]);
            Assert.AreEqual("JOHN.DOE@EXAMPLE.COM", appConfig.AdminEmailAddresses[1]);

            Assert.IsTrue(appConfig.IsAdminUser("john.smith@example.com"));
            Assert.IsTrue(appConfig.IsAdminUser(" john.smith@example.com "));
            Assert.IsTrue(appConfig.IsAdminUser("john.doe@example.com"));
            Assert.IsTrue(appConfig.IsAdminUser(" john.doe@example.com "));

            Assert.IsFalse(appConfig.IsAdminUser("jane.doe@example.com "));
            Assert.IsFalse(appConfig.IsAdminUser(null));
        }
    }
}
