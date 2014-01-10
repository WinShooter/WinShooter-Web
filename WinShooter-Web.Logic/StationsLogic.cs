﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StationsLogic.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
        /// The current user.
        /// </summary>
        private User currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="StationsLogic"/> class.
        /// </summary>
        /// <param name="stationRepository">
        /// The station Repository.
        /// </param>
        /// <param name="rightsHelper">
        /// The rights Helper.
        /// </param>
        public StationsLogic(IRepository<Station> stationRepository, IRightsHelper rightsHelper)
            : this()
        {
            this.stationRepository = stationRepository;
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
            if (
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
    }
}
