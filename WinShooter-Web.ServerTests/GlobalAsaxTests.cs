// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalAsaxTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Tests the <see cref="Global" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.ServerTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="Global"/> class.
    /// </summary>
    [TestClass]
    public class GlobalAsaxTests
    {
        /// <summary>
        /// Verify that file paths are detected as files.
        /// </summary>
        [TestMethod]
        public void TestFileRegexpForFiles()
        {
            Assert.IsTrue(Global.IsFile("/partials/stations.html"));
            Assert.IsTrue(Global.IsFile("/robots.txt"));
            Assert.IsTrue(Global.IsFile("/Scripts/App/AngularModules.js"));
        }

        /// <summary>
        /// Verify that directory paths are not detected as files.
        /// </summary>
        [TestMethod]
        public void TestFileRegexpForPaths()
        {
            Assert.IsFalse(Global.IsFile("/Home/Index"));
            Assert.IsFalse(Global.IsFile("/Account/Login"));
            Assert.IsFalse(Global.IsFile("/Account.Test/Login"));
        }

        /// <summary>
        /// Verify that directory and file paths are not detected as API.
        /// </summary>
        [TestMethod]
        public void TestApiRegexpForFiles()
        {
            Assert.IsFalse(Global.IsApi("/partials/stations.html"));
            Assert.IsFalse(Global.IsApi("/robots.txt"));
            Assert.IsFalse(Global.IsApi("/Scripts/App/AngularModules.js"));
            Assert.IsFalse(Global.IsApi("/Home/Index"));
            Assert.IsFalse(Global.IsApi("/Account/Login"));
        }

        /// <summary>
        /// Verify that API calls are detected as API.
        /// </summary>
        [TestMethod]
        public void TestApiRegexpForPaths()
        {
            Assert.IsTrue(Global.IsApi("/api/competitions"));
        }
    }
}
