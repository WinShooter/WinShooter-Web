﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AccountController.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Defines the RouteConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// The MVC controller for accounts.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Key used for storing the referrer in the session.
        /// </summary>
        private static readonly string SessionRefererKey = "AuthenticationReferrer";

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        public AccountController()
        {
            ViewBag.Referrer = "/";
        }

        /// <summary>
        /// GET: /Home/Index/{id}
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Login()
        {
            if (Request.UrlReferrer != null &&
                Request.Url != null &&
                Request.UrlReferrer.Host == Request.Url.Host)
            {
                this.Session[SessionRefererKey] = Request.UrlReferrer.AbsoluteUri;
            }

            if (Request.UrlReferrer == null &&
                this.Session[SessionRefererKey] != null)
            {
                // Very likely coming back from authentication in ServiceStack
                ViewBag.Referrer = this.Session[SessionRefererKey];
            }

            return this.View();
        }
    }
}