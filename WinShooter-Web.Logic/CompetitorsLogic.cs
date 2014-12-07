// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompetitorsLogic.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The competitors logic.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic
{
    using System;
    using System.Linq;

    using log4net;

    using NHibernate;

    using WinShooter.Database;
    using WinShooter.Logic.Authorization;

    /// <summary>
    /// The competitors logic.
    /// </summary>
    public class CompetitorsLogic
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The competition repository.
        /// </summary>
        private readonly IRepository<Competitor> competitorRepository;

        /// <summary>
        /// The competition repository.
        /// </summary>
        private readonly IRepository<Competition> competitionRepository;

        /// <summary>
        /// The current user.
        /// </summary>
        private User currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitorsLogic"/> class.
        /// </summary>
        /// <param name="competitorRepository">
        /// The patrol repository.
        /// </param>
        /// <param name="competitionRepository">
        /// The competition repository.
        /// </param>
        /// <param name="rightsHelper">
        /// The rights Helper.
        /// </param>
        public CompetitorsLogic(IRepository<Competitor> competitorRepository, IRepository<Competition> competitionRepository, IRightsHelper rightsHelper)
            : this()
        {
            this.competitorRepository = competitorRepository;
            this.competitionRepository = competitionRepository;
            this.RightsHelper = rightsHelper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompetitorsLogic"/> class.
        /// </summary>
        /// <param name="session">
        /// The database Session.
        /// </param>
        public CompetitorsLogic(ISession session)
            : this()
        {
            this.competitorRepository = new Repository<Competitor>(session);
            this.competitionRepository = new Repository<Competition>(session);
            this.RightsHelper = new RightsHelper(
                new Repository<UserRolesInfo>(session),
                new Repository<RoleRightsInfo>(session));
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="CompetitorsLogic"/> class from being created.
        /// </summary>
        private CompetitorsLogic()
        {
            this.log = LogManager.GetLogger(this.GetType());
        }

        /// <summary>
        /// Gets or sets the rights helper.
        /// </summary>
        public IRightsHelper RightsHelper { get; set; }

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
        /// Get patrols for the competition.
        /// </summary>
        /// <param name="competitionId">
        /// The competition id.
        /// </param>
        /// <returns>
        /// The <see cref="Patrol"/>s.
        /// </returns>
        public Patrol[] GetPatrols(Guid competitionId)
        {
            var competition = this.competitionRepository.FilterBy(x => x.Id.Equals(competitionId)).FirstOrDefault();

            if (competition == null)
            {
                throw new Exception("There are no competition with ID " + competitionId);
            }

            if (!competition.IsPublic && 
                !this.RightsHelper.GetRightsForCompetitionIdAndTheUser(competitionId)
                .Contains(WinShooterCompetitionPermissions.ReadPatrol))
            {
                this.log.ErrorFormat(
                    "User {0} does not have enough rights to read patrols for competition {1}",
                    this.currentUser,
                    competitionId);
                throw new NotEnoughRightsException("You don't have rights to patrols for this competition");
            }

            return
                (from patrol in this.competitorRepository.FilterBy(x => x.Competition.Id.Equals(competitionId))
                 select patrol).ToArray();
        }

        /// <summary>
        /// The update competition.
        /// </summary>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <returns>
        /// The <see cref="Patrol"/>.
        /// </returns>
        public Patrol AddPatrol(Guid competitionId)
        {
            var competition = this.competitionRepository.FilterBy(x => x.Id.Equals(competitionId)).FirstOrDefault();

            if (competition == null)
            {
                throw new Exception("There are no competition with ID " + competitionId);
            }

            if (!this.RightsHelper.GetRightsForCompetitionIdAndTheUser(competitionId)
                .Contains(WinShooterCompetitionPermissions.CreatePatrol))
            {
                this.log.ErrorFormat(
                    "User {0} does not have enough rights to add patrol for competition {1}",
                    this.currentUser,
                    competitionId);
                throw new NotEnoughRightsException("You don't have rights to add patrol for this competition");
            }

            var currentPatrols =
                this.competitorRepository.FilterBy(x => x.Competition.Id.Equals(competitionId))
                    .OrderBy(x => x.PatrolNumber)
                    .ToList();

            var patrol = new Patrol { Competition = competition, PatrolNumber = currentPatrols.Count + 1 };
            if (currentPatrols.Count > 0)
            {
                // At least one patrol already exist
                var lastPatrol = currentPatrols[currentPatrols.Count - 1];

                patrol.StartTime = lastPatrol.StartTime.AddMinutes(competition.GetIntParameter(CompetitionParamType.MinutesBetweenPatrols));
            }
            else
            {
                // No patrol exist
                patrol.StartTime = competition.StartDate;
            }

            using (var transaction = this.competitorRepository.StartTransaction())
            {
                if (this.competitorRepository.Add(patrol))
                {
                    transaction.Commit();
                }
            }

            return patrol;
        }

        /// <summary>
        /// The update competition.
        /// </summary>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <param name="patrol">
        /// The patrol.
        /// </param>
        /// <returns>
        /// The <see cref="Patrol"/>.
        /// </returns>
        public Patrol UpdatePatrol(Guid competitionId, Patrol patrol)
        {
            var competition = this.competitionRepository.FilterBy(x => x.Id.Equals(competitionId)).FirstOrDefault();

            if (competition == null)
            {
                throw new Exception("There are no competition with ID " + competitionId);
            }

            if (!this.RightsHelper.GetRightsForCompetitionIdAndTheUser(competitionId)
                .Contains(WinShooterCompetitionPermissions.UpdatePatrol))
            {
                this.log.ErrorFormat(
                    "User {0} does not have enough rights to update patrols for competition {1}",
                    this.currentUser,
                    competitionId);
                throw new NotEnoughRightsException("You don't have rights to update patrols for this competition");
            }

            var databasePatrol =
                this.competitorRepository.FilterBy(x => x.Competition.Id.Equals(competitionId) && x.Id.Equals(patrol.Id)).FirstOrDefault();

            if (databasePatrol == null)
            {
                throw new Exception(string.Format("There is no patrol for competition {0} with Id {1}", competitionId, patrol.Id));
            }

            databasePatrol.UpdateFromOther(patrol);

            using (var transaction = this.competitorRepository.StartTransaction())
            {
                if (this.competitorRepository.Update(databasePatrol))
                {
                    transaction.Commit();
                }
            }

            return databasePatrol;
        }

        /// <summary>
        /// The update competition.
        /// </summary>
        /// <param name="patrolId">
        /// The patrol ID.
        /// </param>
        public void DeletePatrol(Guid patrolId)
        {
            var databasePatrol =
                this.competitorRepository.FilterBy(x => x.Id.Equals(patrolId)).FirstOrDefault();

            if (databasePatrol == null)
            {
                throw new Exception("There are no patrol with ID " + patrolId);
            }

            if (!this.RightsHelper.GetRightsForCompetitionIdAndTheUser(databasePatrol.Competition.Id)
                .Contains(WinShooterCompetitionPermissions.DeletePatrol))
            {
                this.log.ErrorFormat(
                    "User {0} does not have enough rights to delete patrols for competition {1}",
                    this.currentUser,
                    databasePatrol.Competition.Id);
                throw new NotEnoughRightsException("You don't have rights to delete patrols for this competition");
            }

            using (var transaction = this.competitorRepository.StartTransaction())
            {
                if (this.competitorRepository.Delete(databasePatrol))
                {
                    transaction.Commit();
                }
            }

            this.RenumberPatrols(databasePatrol.Competition);
        }

        /// <summary>
        /// Renumbers the patrols in a competition.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        private void RenumberPatrols(Competition competition)
        {
            using (var transaction = this.competitorRepository.StartTransaction())
            {
                var patrols =
                    this.competitorRepository.FilterBy(patrol => patrol.Competition.Id.Equals(competition.Id))
                        .OrderBy(patrol => patrol.PatrolNumber);

                var patrolNumber = 0;
                foreach (var patrol in patrols)
                {
                    patrolNumber++;
                    patrol.PatrolNumber = patrolNumber;
                }

                transaction.Commit();
            }
        }
    }
}
