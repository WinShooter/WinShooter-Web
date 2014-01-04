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
        /// The current user.
        /// </summary>
        private User currentUser;

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
            this.RightsHelper = rightsHelper;
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
        /// Gets or sets the current user.
        /// </summary>
        public User CurrentUser
        {
            get
            {
                return this.currentUser;
            }

            set
            {
                this.RightsHelper.CurrentUser = value;
                this.currentUser = value;
            }
        } 
        
        /// <summary>
        /// Gets the rights helper.
        /// </summary>
        public IRightsHelper RightsHelper { get; private set; }

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
        /// <returns>
        /// The <see cref="Competition"/> array.
        /// </returns>
        public Competition[] GetPrivateCompetitions()
        {
            if (this.CurrentUser == null || this.CurrentUser.Id.Equals(Guid.Empty))
            {
                // Quick bailout if anonymous
                return new Competition[0];
            }

            var competitionIdsForUser = this.RightsHelper.GetCompetitionIdsTheUserHasRightsOn(false);

            var competitions = from competition in this.competitionRepository.FilterBy(x => !x.IsPublic)
                               where competitionIdsForUser.Contains(competition.Id)
                               select competition;

            return competitions.ToArray();
        }

        /// <summary>
        /// Get all competitions for currently logged in user.
        /// </summary>
        /// <returns>
        /// The <see cref="Competition"/> array.
        /// </returns>
        public Competition[] GetCompetitions()
        {
            var toReturn = new List<Competition>();
            toReturn.AddRange(this.GetPublicCompetitions());
            toReturn.AddRange(this.GetPrivateCompetitions());

            return toReturn.ToArray();
        }

        /// <summary>
        /// Get a certain competition.
        /// </summary>
        /// <param name="competitionId">
        /// The competition <see cref="Guid"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Competition GetCompetition(Guid competitionId)
        {
            var competitions = this.GetCompetitions();

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

        /// <summary>
        /// The delete competition.
        /// </summary>
        /// <param name="competitionId">
        /// The competition.
        /// </param>
        public void DeleteCompetition(Guid competitionId)
        {
            if (
                !this.RightsHelper.GetRightsForCompetitionIdAndTheUser(competitionId)
                     .Contains(WinShooterCompetitionPermissions.DeleteCompetition))
            {
                throw new Exception("You don't have rights to delete this competition");
            }

            throw new NotImplementedException();
        }
    }
}
