namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Script.Serialization;

    using WinShooter.Web.DataValidation;

    public class GoogleToken : GenericToken
    {
        // Used for string parsing the Certificates from Google
        private const string BeginCert = "-----BEGIN CERTIFICATE-----";
        private const string EndCert = "-----END CERTIFICATE-----";

        private readonly string expectedAudience;

        private readonly JwtSecurityToken token;

        public GoogleToken(string jsonInput, string expectedAudience)
        {
            jsonInput.Require("jsonInput").NotNull();
            jsonInput.Require("expectedAudience").NotNull();

            var parts = jsonInput.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("The jsonInput must have three parts, separated by a dot. It is: " + jsonInput);
            }

            this.expectedAudience = expectedAudience;

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

        public IEnumerable<System.Security.Claims.Claim> Claims;

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

            var googleCertificates = this.GetGoogleCertificates();

            // TODO Implement
            return true;
        }

        /// <summary>
        /// Retrieves the certificates for Google and returns them as byte arrays.
        /// </summary>
        /// <returns>An array of byte arrays representing the Google certificates.</returns>
        private X509Certificate2[] GetGoogleCertificates()
        {
            // The request will be made to the authentication server.
            var request = WebRequest.Create("https://www.googleapis.com/oauth2/v1/certs");

            string responseFromServer;
            using (var reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                responseFromServer = reader.ReadToEnd();
            }

            var ser = new JavaScriptSerializer();
            var content = ser.Deserialize<Dictionary<string, string>>(responseFromServer);

            return
                content.Select(x => x.Value.Replace(BeginCert, string.Empty).Replace(EndCert, string.Empty))
                    .Select(Convert.FromBase64String)
                    .Select(certificateBytes => new X509Certificate2(certificateBytes))
                    .ToArray();
        }

        private string GetNamedClaim(string claimName)
        {
            var emailClaim = this.Claims.FirstOrDefault(claim => claim.Type == claimName);

            return emailClaim == null ? null : emailClaim.Value;
        }
    }
}
