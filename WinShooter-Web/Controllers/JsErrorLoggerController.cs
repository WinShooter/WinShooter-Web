// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsErrorLoggerController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   Defines the JsErrorLoggerController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Controllers
{
    using System.Web.Mvc;

    using log4net;

    /// <summary>
    /// The JS error logger controller.
    /// </summary>
    public class JsErrorLoggerController : Controller
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log = LogManager.GetLogger(typeof(JsErrorLoggerController));

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="line">
        /// The line.
        /// </param>
        /// <param name="column">
        /// The column.
        /// </param>
        /// <param name="stacktrace">
        /// The stack trace.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(string error, string url, string line, string column, string stacktrace)
        {
            this.log.ErrorFormat(
                "Error in client side js. Error: {0}, Url: {1}, Line: {2}, Column: {3}, Stacktrace: {4}",
                error,
                url,
                line,
                column,
                stacktrace);
            return this.Json("Ok");
        }
    }
}