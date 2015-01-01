// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinShooterApiHost.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
    using System;
    using System.Collections.Generic;

    using Funq;
    using ServiceStack.Authentication.OpenId;
    using ServiceStack.CacheAccess;
    using ServiceStack.CacheAccess.Providers;
    using ServiceStack.Common.Web;
    using ServiceStack.Configuration;
    using ServiceStack.ServiceInterface;
    using ServiceStack.ServiceInterface.Auth;
    using ServiceStack.WebHost.Endpoints;

    using WinShooter.Api.Api.Competition;
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
            : base("WinShooter Web Services", typeof(CompetitionsRequest).Assembly)
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
            this.SetConfig(new EndpointHostConfig { ServiceStackHandlerFactoryPath = "api" });

            // Access Web.Config AppSettings
            var appSettings = new AppSettings();
            container.Register(appSettings);

            AppConfig = new AppConfig(appSettings);

            // Add all the Auth Providers to allow registration with
            this.ConfigureAuth();

            // register REST-ful urls
            this.ConfigureRoutes();

            // register Response Filters
            this.ConfigureResponseFilters();

            // Adds caching
            container.Register<ICacheClient>(new MemoryCacheClient());

            // Adds persistent user repository
            var userRep = new InMemoryAuthRepository();
            container.Register<IUserAuthRepository>(userRep);
        }

        /// <summary>
        /// Configures the routes.
        /// </summary>
        private void ConfigureRoutes()
        {
            this.Routes.AddFromAssembly(this.GetType().Assembly);
        }

        /// <summary>
        /// The configure response filters.
        /// </summary>
        private void ConfigureResponseFilters()
        {
            // Add a response filter to add a 'Cache-Control' header so browsers won't cache
            this.ResponseFilters.Add((req, res, dto) =>
            {
                if (req.ResponseContentType == ContentType.Json)
                {
                    res.AddHeader(HttpHeaders.CacheControl, "no-cache, no-store, must-revalidate");
                    res.AddHeader("Pragma", "no-cache");
                    res.AddHeader("Expires", "0");
                }
            });
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
                                        // Sign-in with Facebook
                                        new FacebookAuthProvider(appSettings),

                                        // Sign-in with Google
                                        new GoogleOpenIdOAuthProvider(appSettings)
                                    };
            var serviceRoutes = new Dictionary<Type, string[]>
                                    {
                                        {
                                            typeof(AuthService),
                                            new[] { "/auth", "/auth/{provider}" }
                                        },
                                        {
                                            typeof(AssignRolesService),
                                            new[] { "/assignroles" }
                                        },
                                        {
                                            typeof(UnAssignRolesService),
                                            new[] { "/unassignroles" }
                                        }
                                    };

            this.Plugins.Add(new AuthFeature(() => new CustomUserSession(), authProviders) { ServiceRoutes = serviceRoutes });
        }
    }
}