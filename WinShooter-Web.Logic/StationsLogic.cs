// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationsLogic.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The stations logic.
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
    /// The stations logic.
    /// </summary>
    public class StationsLogic
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The competition repository.
        /// </summary>
        private readonly IRepository<Station> stationRepository;

        /// <summary>
        /// The competition repository.
        /// </summary>
        private readonly IRepository<Competition> competitionRepository;

        /// <summary>
        /// The current user.
        /// </summary>
        private User currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="StationsLogic"/> class.
        /// </summary>
        /// <param name="stationRepository">
        /// The station repository.
        /// </param>
        /// <param name="competitionRepository">
        /// The competition repository.
        /// </param>
        /// <param name="rightsHelper">
        /// The rights Helper.
        /// </param>
        public StationsLogic(IRepository<Station> stationRepository, IRepository<Competition> competitionRepository, IRightsHelper rightsHelper)
            : this()
        {
            this.stationRepository = stationRepository;
            this.competitionRepository = competitionRepository;
            this.RightsHelper = rightsHelper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StationsLogic"/> class.
        /// </summary>
        /// <param name="session">
        /// The database Session.
        /// </param>
        public StationsLogic(ISession session)
            : this()
        {
            this.stationRepository = new Repository<Station>(session);
            this.competitionRepository = new Repository<Competition>(session);
            this.RightsHelper = new RightsHelper(
                new Repository<UserRolesInfo>(session),
                new Repository<RoleRightsInfo>(session));
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="StationsLogic"/> class from being created.
        /// </summary>
        private StationsLogic()
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
        /// Get stations for the competition.
        /// </summary>
        /// <param name="competitionId">
        /// The competition id.
        /// </param>
        /// <returns>
        /// The <see cref="Station"/>s.
        /// </returns>
        public Station[] GetStations(Guid competitionId)
        {
            var competition = this.competitionRepository.FilterBy(x => x.Id.Equals(competitionId)).FirstOrDefault();

            if (competition == null)
            {
                throw new Exception("There are no competition with ID " + competitionId);
            }

            if (!competition.IsPublic && 
                !this.RightsHelper.GetRightsForCompetitionIdAndTheUser(competitionId)
                .Contains(WinShooterCompetitionPermissions.ReadStation))
            {
                this.log.ErrorFormat(
                    "User {0} does not have enough rights to read stations for competition {1}",
                    this.currentUser,
                    competitionId);
                throw new NotEnoughRightsException("You don't have rights to read stations for this competition");
            }

            return
                (from station in this.stationRepository.FilterBy(x => x.Competition.Id.Equals(competitionId))
                 select station).ToArray();
        }

        /// <summary>
        /// The update competition.
        /// </summary>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <param name="station">
        /// The station.
        /// </param>
        /// <returns>
        /// The <see cref="Station"/>.
        /// </returns>
        public Station AddStation(Guid competitionId, Station station)
        {
            var competition = this.competitionRepository.FilterBy(x => x.Id.Equals(competitionId)).FirstOrDefault();

            if (competition == null)
            {
                throw new Exception("There are no competition with ID " + competitionId);
            }

            if (!this.RightsHelper.GetRightsForCompetitionIdAndTheUser(competitionId)
                .Contains(WinShooterCompetitionPermissions.CreateStation))
            {
                this.log.ErrorFormat(
                    "User {0} does not have enough rights to read stations for competition {1}",
                    this.currentUser,
                    competitionId);
                throw new NotEnoughRightsException("You don't have rights to read stations for this competition");
            }

            var stationCount = this.stationRepository.FilterBy(x => x.Competition.Id.Equals(competitionId)).Count();

            station.Competition = competition;
            station.StationNumber = stationCount + 1;

            using (var transaction = this.stationRepository.StartTransaction())
            {
                if (this.stationRepository.Add(station))
                {
                    transaction.Commit();
                }
            }

            return station;
        }

        /// <summary>
        /// The update competition.
        /// </summary>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <param name="station">
        /// The station.
        /// </param>
        /// <returns>
        /// The <see cref="Station"/>.
        /// </returns>
        public Station UpdateStation(Guid competitionId, Station station)
        {
            var competition = this.competitionRepository.FilterBy(x => x.Id.Equals(competitionId)).FirstOrDefault();

            if (competition == null)
            {
                throw new Exception("There are no competition with ID " + competitionId);
            }

            if (!this.RightsHelper.GetRightsForCompetitionIdAndTheUser(competitionId)
                .Contains(WinShooterCompetitionPermissions.UpdateStation))
            {
                this.log.ErrorFormat(
                    "User {0} does not have enough rights to read stations for competition {1}",
                    this.currentUser,
                    competitionId);
                throw new NotEnoughRightsException("You don't have rights to update stations for this competition");
            }

            var databaseStation =
                this.stationRepository.FilterBy(x => x.Competition.Id.Equals(competitionId) && x.Id.Equals(station.Id)).FirstOrDefault();

            if (databaseStation == null)
            {
                throw new Exception(string.Format("There is no station for competition {0} with Id {1}", competitionId, station.Id));
            }

            databaseStation.UpdateFromOther(station);

            using (var transaction = this.stationRepository.StartTransaction())
            {
                if (this.stationRepository.Update(databaseStation))
                {
                    transaction.Commit();
                }
            }

            return databaseStation;
        }

        /// <summary>
        /// The update competition.
        /// </summary>
        /// <param name="stationId">
        /// The station ID.
        /// </param>
        public void DeleteStation(Guid stationId)
        {
            var databaseStation =
                this.stationRepository.FilterBy(x => x.Id.Equals(stationId)).FirstOrDefault();

            if (databaseStation == null)
            {
                throw new Exception("There are no station with ID " + stationId);
            }

            if (!this.RightsHelper.GetRightsForCompetitionIdAndTheUser(databaseStation.Competition.Id)
                .Contains(WinShooterCompetitionPermissions.DeleteStation))
            {
                this.log.ErrorFormat(
                    "User {0} does not have enough rights to read stations for competition {1}",
                    this.currentUser,
                    databaseStation.Competition.Id);
                throw new NotEnoughRightsException("You don't have rights to delete stations for this competition");
            }

            using (var transaction = this.stationRepository.StartTransaction())
            {
                if (this.stationRepository.Delete(databaseStation))
                {
                    transaction.Commit();
                }
            }

            this.RenumberStations(databaseStation.Competition);
        }

        /// <summary>
        /// Renumbers the stations in a competition.
        /// </summary>
        /// <param name="competition">
        /// The competition.
        /// </param>
        private void RenumberStations(Competition competition)
        {
            // TODO: Implement
        }
    }
}
