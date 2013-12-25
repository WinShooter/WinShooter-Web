// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitionsLogic.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The competitions.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NHibernate.Linq;

    using WinShooter.Database;
    using WinShooter.Logic.Authorization;

    /// <summary>
    /// The competitions.
    /// </summary>
    public class CompetitionsLogic
    {
        /// <summary>
        /// Get all public competitions.
        /// </summary>
        /// <returns>
        /// The <see cref="Competition"/> array.
        /// </returns>
        public Competition[] GetPublicCompetitions()
        {
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitions = from competition in databaseSession.Query<Competition>()
                                   where competition.IsPublic
                                   select competition;

                return competitions.ToArray();
            }
        }

        /// <summary>
        /// Get all public competitions.
        /// </summary>
        /// <param name="userId">
        /// The user <see cref="Guid"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/> array.
        /// </returns>
        public Competition[] GetPrivateCompetitions(Guid userId)
        {
            if (userId.Equals(Guid.Empty))
            {
                // Quick bailout if anonymous
                return new Competition[0];
            }

            var competitionIdsForUser = RightsHelper.GetCompetitionIdsTheUserHasRightsOn(userId, false);
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var competitions = from competition in databaseSession.Query<Competition>()
                                   where !competition.IsPublic &&
                                   competitionIdsForUser.Contains(competition.Id)
                                   select competition;

                return competitions.ToArray();
            }
        }

        /// <summary>
        /// Get all competitions for currently logged in user.
        /// </summary>
        /// <param name="userId">
        /// The user <see cref="Guid"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/> array.
        /// </returns>
        public Competition[] GetCompetitions(Guid userId)
        {
            var toReturn = new List<Competition>();
            toReturn.AddRange(this.GetPublicCompetitions());
            toReturn.AddRange(this.GetPrivateCompetitions(userId));

            return toReturn.ToArray();
        }

        /// <summary>
        /// Get a certain competition.
        /// </summary>
        /// <param name="userId">
        /// The user <see cref="Guid"/>.
        /// </param>
        /// <param name="competitionId">
        /// The competition <see cref="Guid"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition GetCompetition(Guid userId, Guid competitionId)
        {
            var competitions = this.GetCompetitions(userId);

            return
                (from competition in competitions 
                 where competition.Id.Equals(competitionId) 
                 select competition)
                    .FirstOrDefault();
        }

        /// <summary>
        /// Adds or updates a competition.
        /// </summary>
        /// <param name="userId">
        /// The user trying to add or update a competition.
        /// </param>
        /// <param name="competition">
        /// The competition to add or update.
        /// </param>
        public void AddOrUpdateCompetition(Guid userId, Competition competition)
        {
            throw new NotImplementedException();
        }
    }
}
