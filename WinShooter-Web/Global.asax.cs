// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Defines the Global type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter
{
    using System;
    using System.Configuration;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Optimization;
    using System.Web.Routing;

    using log4net;

    using WinShooter.Api;
    using WinShooter.Web.DatabaseMigrations;

    /// <summary>
    /// The global.
    /// </summary>
    public class Global : HttpApplication
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The win shooter API host.
        /// </summary>
        private WinShooterApiHost winShooterApiHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="Global"/> class.
        /// </summary>
        public Global()
        {
            this.log = LogManager.GetLogger(this.GetType());
        }

        /// <summary>
        /// Check if a path is a file path
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// True if it is a file, meaning it has an extension
        /// </returns>
        public static bool IsFile(string path)
        {
            var regex = new Regex("^.*\\.[\\w]*$");

            return regex.IsMatch(path);
        }

        /// <summary>
        /// Check if a path is a API call
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// True if it is a API call.
        /// </returns>
        public static bool IsApi(string path)
        {
            var regex = new Regex("^/api/", RegexOptions.IgnoreCase);

            return regex.IsMatch(path);
        }

        /// <summary>
        /// The application start, where everything is setup.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            this.winShooterApiHost = new WinShooterApiHost();
            this.winShooterApiHost.Init();

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var sqlDatabaseMigrator = new SqlDatabaseMigrator();
            sqlDatabaseMigrator.MigrateToLatest(ConfigurationManager.ConnectionStrings["WinShooterConnection"].ConnectionString);
        }

        /// <summary>
        /// Code that runs with unhandled exceptions.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Application_Error(object sender, EventArgs e)
        {
            // Get the exception object.
            var exc = Server.GetLastError();

            this.log.Error("Unexpected error: " + exc);
        }

        /// <summary>
        /// The application begin request.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // For each call, validate if it's an api or file call.
            // If it is, let the call through.
            // If it's a call to a path, it's supposed to go to
            // Angular, which is on the root so we rewrite the path
            // internally
            var path = HttpContext.Current.Request.Path;

            if (IsStrangeDebuggerCall(path))
            {
                return;
            }

            if (IsApi(path))
            {
                return;
            }

            if (IsFile(path))
            {
                return;
            }

            Context.RewritePath("/");
        }

        /// <summary>
        /// Check if a path is a strange debugger call.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool IsStrangeDebuggerCall(string path)
        {
            if (path.StartsWith("/__browser"))
            {
                return true;
            }

            return false;
        }
    }
}