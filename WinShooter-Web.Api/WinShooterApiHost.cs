// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelloAppHost.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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

namespace WinShooter.Api.Configuration
{
    using Funq;

    using ServiceStack.WebHost.Endpoints;

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
            : base("WinShooter Web Services", typeof(HelloService).Assembly)
        {
        }

        /// <summary>
        /// Configure the given container with the 
        /// registrations provided by the service.
        /// </summary>
        /// <param name="container">Container to register.</param>
        public override void Configure(Container container)
        {
            // register user-defined REST-ful urls
            Routes
              .Add<Hello>("/api/hello")
              .Add<Hello>("/api/hello/{Name}");
        }
    }
}