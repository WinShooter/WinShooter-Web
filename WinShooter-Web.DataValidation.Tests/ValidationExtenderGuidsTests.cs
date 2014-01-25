// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationExtenderGuidsTests.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Defines the ValidationExtenderGuidsTests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Web.DataValidation.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the <see cref="ValidationExtenderGuids"/> class.
    /// </summary>
    [TestClass]
    public class ValidationExtenderGuidsTests
    {
        /// <summary>
        /// Check that not empty validation works on <see cref="Guid"/> that is not empty.
        /// </summary>
        [TestMethod]
        public void NotEmptyNotEmpty()
        {
            var guid = Guid.NewGuid();
            guid.Require("Test").NotEmpty();
        }

        /// <summary>
        /// Check that not empty validation works on <see cref="Guid"/> that is empty.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotEmptyEmpty()
        {
            var guid = Guid.Empty;
            guid.Require("Test").NotEmpty();
        }
    }
}
