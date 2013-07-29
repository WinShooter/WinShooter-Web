// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinShooterApiHost.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The hello app host.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api
{
    using Funq;
    using ServiceStack.Authentication.OpenId;
    using ServiceStack.CacheAccess;
    using ServiceStack.CacheAccess.Providers;
    using ServiceStack.Configuration;
    using ServiceStack.ServiceInterface;
    using ServiceStack.ServiceInterface.Auth;
    using ServiceStack.WebHost.Endpoints;

    using WinShooter.Api.Api;
    using WinShooter.Api.Authentication;

    /// <summary>
    /// The hello app host.
    /// </summary>
    public class WinShooterApiHost : AppHostBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WinShooterApiHost"/> class.
        /// Tell Service Stack the name of your application and 
        /// where to find your web services.
        /// </summary>
        public WinShooterApiHost()
            : base("WinShooter Web Services", typeof(Competitions).Assembly)
        {
        }

        /// <summary>
        /// Gets the app config.
        /// </summary>
        internal static AppConfig AppConfig { get; private set; }

        /// <summary>
        /// Configure the given container with the 
        /// registrations provided by the service.
        /// </summary>
        /// <param name="container">Container to register.</param>
        public override void Configure(Container container)
        {
            // Access Web.Config AppSettings
            var appSettings = new AppSettings();
            container.Register(appSettings);

            AppConfig = new AppConfig(appSettings);

            // register REST-ful urls
            this.ConfigureRoutes();

            // Adds caching
            container.Register<ICacheClient>(new MemoryCacheClient());

            // Adds persistent user repository
            var userRep = new InMemoryAuthRepository();
            container.Register<IUserAuthRepository>(userRep);

            // Add all the Auth Providers to allow registration with
            this.ConfigureAuth();
        }

        /// <summary>
        /// Configures the routes.
        /// </summary>
        private void ConfigureRoutes()
        {
            this.Routes
              .Add<Competitions>("/competitions")
              .Add<Competition>("/competition/{Guid}");
        }

        /// <summary>
        /// Configures authentication options.
        /// </summary>
        private void ConfigureAuth()
        {
            // Access Web.Config AppSettings
            var appSettings = new AppSettings();

            var authProviders = new IAuthProvider[]
                                    {
                                        // Sign-in with Twitter
                                        new TwitterAuthProvider(appSettings), 

                                        // Sign-in with Facebook
                                        new FacebookAuthProvider(appSettings),

                                        // Register new OpenId providers you want to allow authentication with
                                        new GoogleOpenIdOAuthProvider(appSettings),

                                        // Sign-in with Goolge OpenId
                                        new YahooOpenIdOAuthProvider(appSettings),

                                        // Sign-in with Yahoo OpenId
                                        new OpenIdOAuthProvider(appSettings)
                                    };

            Plugins.Add(new AuthFeature(() => new CustomUserSession(), authProviders));
        }
    }
}