// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CurrentUserService.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The competition service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api.CurrentUser
{
    using System;
    using System.Linq;

    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authentication;
    using WinShooter.Database;
    using WinShooter.Logic.Authorization;

    /// <summary>
    /// The competition service.
    /// </summary>
    public class CurrentUserService : Service
    {

        /// <summary>
        /// The database session.
        /// </summary>
        private readonly NHibernate.ISession databaseSession;

        private readonly IRightsHelper rightsHelper;

        public CurrentUserService()
        {
            this.databaseSession = NHibernateHelper.OpenSession();
            this.rightsHelper = new RightsHelper(new Repository<UserRolesInfo>(this.databaseSession), new Repository<RoleRightsInfo>(this.databaseSession));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public sealed override void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The any.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="CurrentUserResponse"/>.
        /// </returns>
        public CurrentUserResponse Get(CurrentUserRequest request)
        {
            var session = this.GetSession() as CustomUserSession;

            if (session == null || session.User == null)
            {
                return new CurrentUserResponse { IsLoggedIn = false };
            }

            this.rightsHelper.CurrentUser = session.User;
            var competitionRights = new string[0];
            if (!string.IsNullOrEmpty(request.CompetitionId))
            {
                competitionRights =
                    (from right in
                         this.rightsHelper.GetRightsForCompetitionIdAndTheUser(Guid.Parse(request.CompetitionId))
                     select right.ToString()).ToArray();
            }

            return new CurrentUserResponse 
                       { 
                           IsLoggedIn = true,
                           DisplayName = session.User.DisplayName,
                           Email = session.User.Email,
                           CompetitionRights = competitionRights
                       };
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.databaseSession.Dispose();

                base.Dispose();
            }
        }
    }
}
