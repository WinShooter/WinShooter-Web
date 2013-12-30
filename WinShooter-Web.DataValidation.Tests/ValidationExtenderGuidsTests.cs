namespace WinShooter_Web.DataValidation.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WinShooter.Web.DataValidation;

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
