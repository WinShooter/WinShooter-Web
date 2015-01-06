// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseApiController.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The base for all API controllers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api
{
    using System;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Http;

    using NHibernate;

    using WinShooter.Database;
    using WinShooter.Logic.Authentication;

    /// <summary>
    /// The base for all API controllers.
    /// </summary>
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        protected BaseApiController()
        {
            this.DatabaseSession = NHibernateHelper.OpenSession();
        }

        /// <summary>
        /// Gets or sets the active database session.
        /// </summary>
        protected ISession DatabaseSession { get; set; }

        /// <summary>
        /// Gets the current user principal.
        /// </summary>
        protected virtual CustomPrincipal Principal
        {
            get { return HttpContext.Current.User as CustomPrincipal; }
        }

        /// <summary>
        /// Get the user from the <see cref="Principal"/>.
        /// </summary>
        /// <returns>The user if one exist, otherwise null.</returns>
        protected virtual User GetUser()
        {
            if (this.Principal == null)
            {
                return null;
            }

            var userRepository = new Repository<User>(this.DatabaseSession);

            return userRepository.FilterBy(user => user.Id == this.Principal.UserId).FirstOrDefault();
        }

        protected string GetApplicationPath()
        {
            return this.Url == null ? null : this.Url.Content("/").TrimEnd('/') + "/";
        }

        /// <summary>
        /// Dispose the current object.
        /// </summary>
        /// <param name="disposing">
        /// If we are disposing.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            this.DatabaseSession.Dispose();

            base.Dispose(true);
        }
    }
}
