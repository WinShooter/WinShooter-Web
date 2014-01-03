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
    /// The competitions logic.
    /// </summary>
    public class CompetitionsLogic
    {
        /// <summary>
        /// The competition repository.
        /// </summary>
        private readonly IRepository<Competition> competitionRepository;

        /// <summary>
        /// The user roles info repository.
        /// </summary>
        private readonly IRepository<UserRolesInfo> userRolesInfoRepository;

        /// <summary>
        /// The rights helper.
        /// </summary>
        private readonly IRightsHelper rightsHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsLogic"/> class.
        /// </summary>
        /// <param name="competitionRepository">
        /// The competition repository.
        /// </param>
        /// <param name="userRolesInfoRepository">
        /// The <see cref="UserRolesInfo"/> repository.
        /// </param>
        /// <param name="rightsHelper">
        /// The rights helper implementation.
        /// </param>
        public CompetitionsLogic(IRepository<Competition> competitionRepository, IRepository<UserRolesInfo> userRolesInfoRepository, IRightsHelper rightsHelper)
        {
            this.competitionRepository = competitionRepository;
            this.userRolesInfoRepository = userRolesInfoRepository;
            this.rightsHelper = rightsHelper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitionsLogic"/> class.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        public CompetitionsLogic(NHibernate.ISession session)
            : this(
            new Repository<Competition>(session), 
            new Repository<UserRolesInfo>(session), 
            new RightsHelper(new Repository<UserRolesInfo>(session), new Repository<RoleRightsInfo>(session)))
        {
        }

        /// <summary>
        /// Get all public competitions.
        /// </summary>
        /// <returns>
        /// The <see cref="Competition"/> array.
        /// </returns>
        public Competition[] GetPublicCompetitions()
        {
            return this.competitionRepository.FilterBy(x => x.IsPublic).ToArray();
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

            var competitionIdsForUser = this.rightsHelper.GetCompetitionIdsTheUserHasRightsOn(userId, false);

            var competitions = from competition in this.competitionRepository.FilterBy(x => !x.IsPublic)
                               where competitionIdsForUser.Contains(competition.Id)
                               select competition;

            return competitions.ToArray();
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
        /// Adds a new competition.
        /// </summary>
        /// <param name="user">
        /// The user trying to add or update a competition.
        /// </param>
        /// <param name="competition">
        /// The competition to add or update.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/> of the new competition.
        /// </returns>
        public Competition AddCompetition(User user, Competition competition)
        {
            competition.Id = Guid.NewGuid();

            competition.VerifyDataContent();

            var rolesLogic = new RolesLogic();
            var role = rolesLogic.DefaultOwnerRole;

            if (role == null)
            {
                // This should never happen
                throw new NullReferenceException(string.Format("Default Owner Role \"{0}\" is not present.", rolesLogic.DefaultOwnerRoleName));
            }

            var userRolesInfo = new UserRolesInfo
                                     {
                                         Competition = competition,
                                         Role = role,
                                         User = user
                                     };

            using (var transaction = this.competitionRepository.StartTransaction())
            {
                // First add the new competition to database
                this.competitionRepository.Add(competition);

                // Then add the user as admin
                this.userRolesInfoRepository.Add(userRolesInfo);

                // Commit transaction
                transaction.Commit();
            }

            return competition;
        }

        /// <summary>
        /// Updates a competition.
        /// </summary>
        /// <param name="userId">
        /// The user trying to add or update a competition.
        /// </param>
        /// <param name="competition">
        /// The competition to add or update.
        /// </param>
        public void UpdateCompetition(Guid userId, Competition competition)
        {
            throw new NotImplementedException();
        }
    }
}
