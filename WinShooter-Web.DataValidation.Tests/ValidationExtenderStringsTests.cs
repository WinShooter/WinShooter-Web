// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExtenderStringsTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Tests the <see cref="ValidationExtenderStrings" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ValidationExtenderStrings"/> class.
    /// </summary>
    [TestClass]
    public class ValidationExtenderStringsTests
    {
        /// <summary>
        /// Test the shorter than requirements positive.
        /// </summary>
        [TestMethod]
        public void ShorterThanShorterThan()
        {
            var str = new string('a', 100);
            str.Require("str").ShorterThan(101);
        }

        /// <summary>
        /// Test the shorter than requirement negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShorterThanLongerThan()
        {
            var str = new string('a', 100);
            str.Require("str").ShorterThan(99);
        }

        /// <summary>
        /// Test the longer than requirements negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LongerThanShorterThan()
        {
            var str = new string('a', 100);
            str.Require("str").LongerThan(101);
        }

        /// <summary>
        /// Test the longer than requirement positive.
        /// </summary>
        [TestMethod]
        public void LongerThanLongerThan()
        {
            var str = new string('a', 100);
            str.Require("str").LongerThan(99);
        }

        /// <summary>
        /// Test the valid email address requirement positive.
        /// </summary>
        [TestMethod]
        public void ValidEmailAddressValid()
        {
            "john@allberg.se".Require("Email").ValidEmailAddress();
            "asdf_asdf@hotmail.com".Require("Email").ValidEmailAddress();
        }

        /// <summary>
        /// Test the valid email address requirement negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidEmailAddressInvalid1()
        {
            "www.allberg.se".Require("Email").ValidEmailAddress();
        }
    }
}
