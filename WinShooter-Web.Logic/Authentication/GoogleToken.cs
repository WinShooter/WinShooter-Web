namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.Linq;
    using System.Security.Claims;

    using WinShooter.Web.DataValidation;

    public class GoogleToken : GenericToken
    {
        private readonly string expectedAudience;

        private readonly IGoogleTrustCertificateFetcher certificateFetcher;

        private readonly JwtSecurityToken token;

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
