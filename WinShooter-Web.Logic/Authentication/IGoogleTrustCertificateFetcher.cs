namespace WinShooter.Logic.Authentication
{
    using System.Security.Cryptography.X509Certificates;

    public interface IGoogleTrustCertificateFetcher
    {
        /// <summary>
        /// Get the Google trust certificates. 
        /// If they are cached, the cache will be retrieved.
        /// </summary>
        /// <returns>An array of <see cref="X509Certificate2"/>.</returns>
        X509Certificate2[] GetCertificates();
    }
}