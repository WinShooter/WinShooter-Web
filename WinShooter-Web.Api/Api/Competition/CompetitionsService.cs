// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionsService.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The competitions service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api.Competition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Authentication;
    using WinShooter.Database;
    using WinShooter.Logic;

    /// <summary>
    /// The competitions service.
    /// </summary>
    public class CompetitionsService : Service
    {
        /// <summary>
        /// The database session.
        /// </summary>
        private readonly NHibernate.ISession databaseSession;

        /// <summary>
        /// The logic.
        /// </summary>
        private readonly CompetitionsLogic logic;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsService"/> class.
        /// </summary>
        public CompetitionsService()
        {
            this.databaseSession = NHibernateHelper.OpenSession();
            this.logic = new CompetitionsLogic(this.databaseSession);
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
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        public List<CompetitionResponse> Get(CompetitionsRequest request)
        {
            var authSession = this.GetSession() as CustomUserSession;

            this.logic.CurrentUser = authSession == null ? null : authSession.User;

            var competitions = this.logic.GetCompetitions();

            var responses = (from dbcompetition in competitions 
                 select new CompetitionResponse(dbcompetition)).ToList();

            return responses;
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }

            this.databaseSession.Dispose();

            base.Dispose();
        }
    }
}
