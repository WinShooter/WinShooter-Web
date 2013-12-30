namespace WinShooter_Web.DataValidation.Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using WinShooter.Web.DataValidation;

    [TestClass]
    public class ValidationExtenderStringsTests
    {
        /// <summary>
        /// Test the shorter than requirements positive.
        /// </summary>
        [TestMethod]
        public void ShorterThanShorterThan()
        {
            var str = new String('a', 100);
            str.Require("str").ShorterThan(101);
        }

        /// <summary>
        /// Test the shorter than requirement negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ShorterThanLongerThan()
        {
            var str = new String('a', 100);
            str.Require("str").ShorterThan(99);
        }

        /// <summary>
        /// Test the longer than requirements negative.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LongerThanShorterThan()
        {
            var str = new String('a', 100);
            str.Require("str").LongerThan(101);
        }

        /// <summary>
        /// Test the longer than requirement postitive.
        /// </summary>
        [TestMethod]
        public void LongerThanLongerThan()
        {
            var str = new String('a', 100);
            str.Require("str").LongerThan(99);
        }
    }
}
