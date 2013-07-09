// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The home controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Controllers
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// GET: /Home/Index/{id}
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return this.View();
            }

            // TODO Get competition from database
            // TODO Check user rights on database
            this.ViewBag.CompetitionName = "Höstsaluten";
            this.ViewBag.CompetitionGuid = id;

            return this.View();
        }

        /// <summary>
        /// The competition info.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Competition()
        {
            return this.View();
        }

        /// <summary>
        /// The clubs.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Clubs()
        {
            return this.View();
        }

        /// <summary>
        /// The weapons.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Weapons()
        {
            return this.View();
        }

        /// <summary>
        /// The stations.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Stations()
        {
            return this.View();
        }

        /// <summary>
        /// The competitors.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Competitors()
        {
            return this.View();
        }

        /// <summary>
        /// The enter results.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult EnterResults()
        {
            return this.View();
        }

        /// <summary>
        /// The results.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Results()
        {
            return this.View();
        }
    }
}
