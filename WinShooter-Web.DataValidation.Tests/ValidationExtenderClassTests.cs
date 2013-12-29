namespace WinShooter_Web.DataValidation.Tests
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
        [TestMethod]
        public void NotNullIsNotNull()
        {
            string str = string.Empty;
            str.Require("str").NotNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNullIsNull()
        {
            string str = null;
            str.Require("str").NotNull();
        }
    }
}
