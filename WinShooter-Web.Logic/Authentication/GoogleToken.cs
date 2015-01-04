// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GoogleToken.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Represents a Google token.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;

    using WinShooter.Web.DataValidation;

    /// <summary>
    /// Represents a Google token.
    /// </summary>
    public class GoogleToken : GenericToken
    {
        /// <summary>
        /// The expected audience.
        /// </summary>
        private readonly string expectedAudience;

        /// <summary>
        /// The fetcher for Google trust certificates.
        /// </summary>
        private readonly IGoogleTrustCertificateFetcher certificateFetcher;

        /// <summary>
        /// The JWT security token.
        /// </summary>
        private readonly JwtSecurityToken token;

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleToken" /> class.
        /// </summary>
        public GoogleToken(string jsonInput, string expectedAudience, IGoogleTrustCertificateFetcher certificateFetcher)
        {
            jsonInput.Require("jsonInput").NotNull();
            jsonInput.Require("expectedAudience").NotNull();

            var parts = jsonInput.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("The jsonInput must have three parts, separated by a dot. It is: " + jsonInput);
            }

            this.expectedAudience = expectedAudience;
            this.certificateFetcher = certificateFetcher;

            this.token = new JwtSecurityToken(jsonInput);
            if (this.token.Audiences.FirstOrDefault() != expectedAudience)
            {
                throw new FormatException(
                    string.Format("Expected audience \"{0}\" but was \"{1}\".",
                        expectedAudience,
                        this.token.Audiences.FirstOrDefault()));
            }

            this.Claims = this.token.Claims;
        }

        public IEnumerable<Claim> Claims;

        public string IdentityProviderId
        {
            get
            {
                return this.GetNamedClaim("sub");
            }
        }

        public string Email
        {
            get
            {
                return this.GetNamedClaim("email");
            }
        }

        public string Name
        {
            get
            {
                return this.GetNamedClaim("name");
            }
        }

        public string Audience
        {
            get
            {
                return this.GetNamedClaim("aud");
            }
        }

        public DateTime NotValidAfter
        {
            get
            {
                var secondsString = this.GetNamedClaim("exp");
                if (secondsString == null)
                {
                    return DateTime.MinValue;
                }

                var seconds = int.Parse(secondsString);

                var toReturn = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                return toReturn.AddSeconds(seconds);
            }
        }

        public bool IsValid()
        {
            return this.IsValid(DateTime.Now);
        }

        public bool IsValid(DateTime timeOfVerification)
        {
            //if (this.NotValidAfter < timeOfVerification)
            //{
            //    return false;
            //}

            var googleCertificates = this.certificateFetcher.GetCertificates();

            // TODO Implement
            return true;
        }

        private string GetNamedClaim(string claimName)
        {
            var emailClaim = this.Claims.FirstOrDefault(claim => claim.Type == claimName);

            return emailClaim == null ? null : emailClaim.Value;
        }
    }
}
