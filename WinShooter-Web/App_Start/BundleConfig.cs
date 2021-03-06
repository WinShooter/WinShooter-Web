﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Configures the bundles.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter
{
    using System.Web.Optimization;

    /// <summary>
    /// Configures the bundles.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Configures the bundles.
        /// For more information on Bundling, visit <see cref="http://go.microsoft.com/fwlink/?LinkId=254725"/>.
        /// </summary>
        /// <param name="bundles">
        /// The bundles.
        /// </param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Enable CDN support
            bundles.UseCdn = true;
#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/jquery",
                    "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.1.min.js").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(
                new ScriptBundle(
                    "~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/Flat-ui/flatui-checkbox.js",
                "~/Scripts/Flat-ui/flatui-radio.js"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/angularjs",
                    "https://ajax.googleapis.com/ajax/libs/angularjs/1.2.8/angular.min.js")
                    .Include("~/Scripts/angular.js"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/angularjs-route",
                    "https://ajax.googleapis.com/ajax/libs/angularjs/1.2.8/angular-route.min.js").Include(
                        "~/Scripts/angular-route.js"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/angularjs-resource",
                    "https://ajax.googleapis.com/ajax/libs/angularjs/1.2.8/angular-resource.min.js").Include(
                        "~/Scripts/angular-resource.js"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/angularjs-sanitize").Include(
                    "~/Scripts/angular-sanitize.js"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/angulari18n/sv-se").Include(
                    "~/Scripts/i18n/angular-locale_sv-se.js"));

            bundles.Add(
                new ScriptBundle(
                    "~/bundles/angularjs-bootstrapui")
                    .Include("~/Scripts/angular-ui/ui-bootstrap.js")
                    .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/AppControllers")
                    .IncludeDirectory("~/Scripts/App/Controllers", "*.js"));

            bundles.Add(new StyleBundle("~/Content/styles").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-responsive.css",
                "~/Content/flat-ui/flat-ui.css",
                "~/Content/site.css"));
        }
    }
}