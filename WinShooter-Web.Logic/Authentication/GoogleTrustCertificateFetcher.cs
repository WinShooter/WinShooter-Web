namespace WinShooter.Logic.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Cryptography.X509Certificates;
    using System.Web.Script.Serialization;

    public class GoogleTrustCertificateFetcher : IGoogleTrustCertificateFetcher
    {
        private const int MaxCertificateAgeHours = 6;

        // Used for string parsing the Certificates from Google
        private const string BeginCert = "-----BEGIN CERTIFICATE-----";

        private const string EndCert = "-----END CERTIFICATE-----";

        private static GoogleTrustCertificateFetcher instance;
        private static object instanceLocker = new object();

        private DateTime timeStamp = DateTime.MinValue;

        private X509Certificate2[] certificateCache;

        public static GoogleTrustCertificateFetcher GetInstance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }

                lock (instanceLocker)
                {
                    if (instance == null)
                    {
                        instance = new GoogleTrustCertificateFetcher();
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// Get the Google trust certificates. 
        /// If they are cached, the cache will be retrieved.
        /// </summary>
        /// <returns>An array of <see cref="X509Certificate2"/>.</returns>
        public X509Certificate2[] GetCertificates()
        {
            if (this.CacheIsUpdated())
            {
                return this.certificateCache;
            }

            lock (this)
            {
                if (this.CacheIsUpdated())
                {
                    return this.certificateCache;
                }

                this.certificateCache = this.GetGoogleCertificates();
                this.timeStamp = DateTime.Now;
            }

            return this.certificateCache;
        }

        /// <summary>
        /// Check if cache is updated.
        /// </summary>
        /// <returns>True if cache is updated</returns>
        private bool CacheIsUpdated()
        {
            return this.certificateCache != null
                   && ((DateTime.Now - this.timeStamp).TotalHours < MaxCertificateAgeHours);
        }

        /// <summary>
        /// Retrieves the certificates for Google and returns them as byte arrays.
        /// </summary>
        /// <returns>An array of byte arrays representing the Google certificates.</returns>
        private X509Certificate2[] GetGoogleCertificates()
        {
            // The request will be made to the authentication server.
            var request = WebRequest.Create("https://www.googleapis.com/oauth2/v1/certs");
            var requestStream = request.GetResponse().GetResponseStream();

            if (requestStream == null)
            {
                throw new NullReferenceException("Got null stream from HTTP request.");
            }

            string responseFromServer;
            using (var reader = new StreamReader(requestStream))
            {
                responseFromServer = reader.ReadToEnd();
            }

            var ser = new JavaScriptSerializer();
            var content = ser.Deserialize<Dictionary<string, string>>(responseFromServer);

            return
                content.Select(x => x.Value.Replace(BeginCert, String.Empty).Replace(EndCert, String.Empty))
                    .Select(Convert.FromBase64String)
                    .Select(certificateBytes => new X509Certificate2(certificateBytes))
                    .ToArray();
        }
    }
}
