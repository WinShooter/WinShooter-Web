// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExtenderClassTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Tests the <see cref="ValidationExtenderClass" /> class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WinShooter.Web.DataValidation;

    /// <summary>
    /// Tests the <see cref="ValidationExtenderClass"/> class.
    /// </summary>
    [TestClass]
    public class ValidationExtenderClassTests
    {
        /// <summary>
        /// Tests the NotNull with a not null value.
        /// </summary>
        [TestMethod]
        public void NotNullIsNotNull()
        {
            string str = string.Empty;
            str.Require("str").NotNull();
        }

        /// <summary>
        /// Tests the NotNull requirement with a null value.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNullIsNull()
        {
            string str = null;
            str.Require("str").NotNull();
        }
    }
}
