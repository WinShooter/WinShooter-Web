namespace WinShooter.Logic.Tests.Authentication
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using WinShooter.Logic.Authentication;

    /// <summary>
    /// Tests the <see cref="GoogleToken"/> class.
    /// </summary>
    [TestClass]
    public class GoogleTokenTests
    {
        /// <summary>
        /// Test parsing the token.
        /// </summary>
        [TestMethod]
        public void TestParsing()
        {
            var mockGoogleCerts = new Mock<IGoogleTrustCertificateFetcher>();
            const string TokenString = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc0YmUxYTA5Y2I4NWRkYjZlZjFjMjQ1ZTgxNjU1MWQ2Mzk2MDQyMjQifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwic3ViIjoiMTAzMzIxMTA1MzE3NDM1Njk5MDE4IiwiYXpwIjoiODQ1NDQzNTg2MjE1LTB1OHBtdTMzYnFkanRvNnMzbnV2ZjE5Y2p2bWJpa3RhLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiZW1haWwiOiJzbTB1ZGFAZ21haWwuY29tIiwiYXRfaGFzaCI6InkwbFdJWEtJdURSNmlnUnpwUUVhemciLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiYXVkIjoiODQ1NDQzNTg2MjE1LTB1OHBtdTMzYnFkanRvNnMzbnV2ZjE5Y2p2bWJpa3RhLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiaWF0IjoxNDE5OTE3NDQyLCJleHAiOjE0MTk5MjEzNDJ9.YyxHr60XdDxwuTWiIqCdlrHh5AYYtYeI7sq3gEqpe5wMSEYKM_pwe7btIIe1zMPdeQXrSCQT90XTyEikGa8RsnZJ7QgOskeV0qfxp1V29hf7nbZAQUnzWiybw2Tv9lWH2GlllVgxqTHzMD2TFWE407uCNCqzdlTwTDpkdR1P948";
            var token = new GoogleToken(TokenString, "845443586215-0u8pmu33bqdjto6s3nuvf19cjvmbikta.apps.googleusercontent.com", mockGoogleCerts.Object);

            Assert.AreEqual(9, token.Claims.Count());
            Assert.AreEqual("103321105317435699018@google", token.IdentityProviderId);
            Assert.AreEqual("sm0uda@gmail.com", token.Email);
            Assert.IsNull(token.Name);
        }

        /// <summary>
        /// Test validating the token.
        /// </summary>
        [TestMethod]
        public void ValidatingNotValidAfter()
        {
            var mockGoogleCerts = new Mock<IGoogleTrustCertificateFetcher>();
            const string TokenString = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc0YmUxYTA5Y2I4NWRkYjZlZjFjMjQ1ZTgxNjU1MWQ2Mzk2MDQyMjQifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwic3ViIjoiMTAzMzIxMTA1MzE3NDM1Njk5MDE4IiwiYXpwIjoiODQ1NDQzNTg2MjE1LTB1OHBtdTMzYnFkanRvNnMzbnV2ZjE5Y2p2bWJpa3RhLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiZW1haWwiOiJzbTB1ZGFAZ21haWwuY29tIiwiYXRfaGFzaCI6InkwbFdJWEtJdURSNmlnUnpwUUVhemciLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiYXVkIjoiODQ1NDQzNTg2MjE1LTB1OHBtdTMzYnFkanRvNnMzbnV2ZjE5Y2p2bWJpa3RhLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiaWF0IjoxNDE5OTE3NDQyLCJleHAiOjE0MTk5MjEzNDJ9.YyxHr60XdDxwuTWiIqCdlrHh5AYYtYeI7sq3gEqpe5wMSEYKM_pwe7btIIe1zMPdeQXrSCQT90XTyEikGa8RsnZJ7QgOskeV0qfxp1V29hf7nbZAQUnzWiybw2Tv9lWH2GlllVgxqTHzMD2TFWE407uCNCqzdlTwTDpkdR1P948";
            var token = new GoogleToken(TokenString, "845443586215-0u8pmu33bqdjto6s3nuvf19cjvmbikta.apps.googleusercontent.com", mockGoogleCerts.Object);

            Assert.IsTrue(token.IsValid(DateTime.Parse("2014-12-30 06:00")));
            Assert.IsFalse(token.IsValid(DateTime.Parse("2014-12-30 21:00")));
        }

        /// <summary>
        /// Test validating the token.
        /// </summary>
        [TestMethod]
        public void Validating()
        {
            var mockGoogleCerts = new Mock<IGoogleTrustCertificateFetcher>();
            const string TokenString = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ijc0YmUxYTA5Y2I4NWRkYjZlZjFjMjQ1ZTgxNjU1MWQ2Mzk2MDQyMjQifQ.eyJpc3MiOiJhY2NvdW50cy5nb29nbGUuY29tIiwic3ViIjoiMTAzMzIxMTA1MzE3NDM1Njk5MDE4IiwiYXpwIjoiODQ1NDQzNTg2MjE1LTB1OHBtdTMzYnFkanRvNnMzbnV2ZjE5Y2p2bWJpa3RhLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiZW1haWwiOiJzbTB1ZGFAZ21haWwuY29tIiwiYXRfaGFzaCI6InkwbFdJWEtJdURSNmlnUnpwUUVhemciLCJlbWFpbF92ZXJpZmllZCI6dHJ1ZSwiYXVkIjoiODQ1NDQzNTg2MjE1LTB1OHBtdTMzYnFkanRvNnMzbnV2ZjE5Y2p2bWJpa3RhLmFwcHMuZ29vZ2xldXNlcmNvbnRlbnQuY29tIiwiaWF0IjoxNDE5OTE3NDQyLCJleHAiOjE0MTk5MjEzNDJ9.YyxHr60XdDxwuTWiIqCdlrHh5AYYtYeI7sq3gEqpe5wMSEYKM_pwe7btIIe1zMPdeQXrSCQT90XTyEikGa8RsnZJ7QgOskeV0qfxp1V29hf7nbZAQUnzWiybw2Tv9lWH2GlllVgxqTHzMD2TFWE407uCNCqzdlTwTDpkdR1P948";
            var token = new GoogleToken(TokenString, "845443586215-0u8pmu33bqdjto6s3nuvf19cjvmbikta.apps.googleusercontent.com", mockGoogleCerts.Object);

            Assert.IsTrue(token.IsValid());
        }
    }
}
