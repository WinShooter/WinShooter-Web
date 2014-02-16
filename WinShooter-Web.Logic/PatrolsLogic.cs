// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatrolsLogic.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The patrols logic.
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
    /// The patrols logic.
    /// </summary>
    public class PatrolsLogic
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The competition repository.
        /// </summary>
        private readonly IRepository<Patrol> patrolRepository;

        /// <summary>
        /// The competition repository.
        /// </summary>
        private readonly IRepository<Competition> competitionRepository;

        /// <summary>
        /// The current user.
        /// </summary>
        private User currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="PatrolsLogic"/> class.
        /// </summary>
        /// <param name="patrolRepository">
        /// The patrol repository.
        /// </param>
        /// <param name="competitionRepository">
        /// The competition repository.
        /// </param>
        /// <param name="rightsHelper">
        /// The rights Helper.
        /// </param>
        public PatrolsLogic(IRepository<Patrol> patrolRepository, IRepository<Competition> competitionRepository, IRightsHelper rightsHelper)
            : this()
        {
            this.patrolRepository = patrolRepository;
            this.competitionRepository = competitionRepository;
            this.RightsHelper = rightsHelper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PatrolsLogic"/> class.
        /// </summary>
        /// <param name="session">
        /// The database Session.
        /// </param>
        public PatrolsLogic(ISession session)
            : this()
        {
            this.patrolRepository = new Repository<Patrol>(session);
            this.competitionRepository = new Repository<Competition>(session);
            this.RightsHelper = new RightsHelper(
                new Repository<UserRolesInfo>(session),
                new Repository<RoleRightsInfo>(session));
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PatrolsLogic"/> class from being created.
        /// </summary>
        private PatrolsLogic()
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
                (from patrol in this.patrolRepository.FilterBy(x => x.Competition.Id.Equals(competitionId))
                 select patrol).ToArray();
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
        public Patrol AddPatrol(Guid competitionId, Patrol patrol)
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

            var patrolCount = this.patrolRepository.FilterBy(x => x.Competition.Id.Equals(competitionId)).Count();

            patrol.Competition = competition;
            patrol.PatrolNumber = patrolCount + 1;

            using (var transaction = this.patrolRepository.StartTransaction())
            {
                if (this.patrolRepository.Add(patrol))
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
                this.patrolRepository.FilterBy(x => x.Competition.Id.Equals(competitionId) && x.Id.Equals(patrol.Id)).FirstOrDefault();

            if (databasePatrol == null)
            {
                throw new Exception(string.Format("There is no patrol for competition {0} with Id {1}", competitionId, patrol.Id));
            }

            databasePatrol.UpdateFromOther(patrol);

            using (var transaction = this.patrolRepository.StartTransaction())
            {
                if (this.patrolRepository.Update(databasePatrol))
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
                this.patrolRepository.FilterBy(x => x.Id.Equals(patrolId)).FirstOrDefault();

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

            using (var transaction = this.patrolRepository.StartTransaction())
            {
                if (this.patrolRepository.Delete(databasePatrol))
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
            using (var transaction = this.patrolRepository.StartTransaction())
            {
                var patrols =
                    this.patrolRepository.FilterBy(patrol => patrol.Competition.Id.Equals(competition.Id))
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
