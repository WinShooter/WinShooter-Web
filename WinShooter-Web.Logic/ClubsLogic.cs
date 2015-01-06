// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClubsLogic.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
    using WinShooter.Logic.Authentication;
    using WinShooter.Logic.Authorization;

    public class ClubsLogic
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The club repository.
        /// </summary>
        private readonly IRepository<Club> clubRepository;

        /// <summary>
        /// The shooter repository.
        /// </summary>
        private readonly IRepository<Shooter> shooterRepository;

        /// <summary>
        /// The current user.
        /// </summary>
        private CustomPrincipal currentUser;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PatrolsLogic"/> class.
        /// </summary>
        /// <param name="clubRepository">
        ///     The club repository.
        /// </param>
        /// <param name="shooterRepository">
        ///     The shooter repository.
        /// </param>
        /// <param name="rightsHelper">
        ///     The rights Helper.
        /// </param>
        public ClubsLogic(
            IRepository<Club> clubRepository, 
            IRepository<Shooter> shooterRepository, 
            IRightsHelper rightsHelper)
            : this()
        {
            this.clubRepository = clubRepository;
            this.shooterRepository = shooterRepository;
            this.RightsHelper = rightsHelper;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PatrolsLogic"/> class.
        /// </summary>
        /// <param name="session">
        /// The database Session.
        /// </param>
        public ClubsLogic(ISession session)
            : this()
        {
            this.clubRepository = new Repository<Club>(session);
            this.shooterRepository = new Repository<Shooter>(session);
            this.RightsHelper = new RightsHelper(
                new Repository<UserRolesInfo>(session),
                new Repository<RoleRightsInfo>(session));
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="PatrolsLogic"/> class from being created.
        /// </summary>
        private ClubsLogic()
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
        public CustomPrincipal CurrentUser
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
        /// Get all <see cref="Club"/>.
        /// </summary>
        /// <returns>An array of clubs.</returns>
        public Club[] GetClubs()
        {
            this.log.Debug("Returning all clubs.");
            return this.clubRepository.All().ToArray();
        }

        /// <summary>
        /// Returns the club with a certain ID.
        /// </summary>
        /// <param name="clubId">The club ID</param>
        /// <returns>The club</returns>
        public Club GetClub(string clubId)
        {
            return this.clubRepository
                .FilterBy(club => club.ClubId == clubId)
                .FirstOrDefault();
        }

        /// <summary>
        /// Add a new club.
        /// </summary>
        /// <param name="newClub"></param>
        /// <returns></returns>
        public Club AddClub(Club newClub)
        {
            // TODO: Check user has add right
            this.clubRepository.Add(newClub);
            return newClub;
        }

        /// <summary>
        /// Updates a club.
        /// </summary>
        /// <param name="club">The club to update.</param>
        /// <returns>The updated club.</returns>
        public Club UpdateClub(Club club)
        {
            // TODO: Check user has update right
            this.clubRepository.Update(club);
            return club;
        }

        /// <summary>
        /// Deletes a club.
        /// </summary>
        /// <param name="clubGuid">The GUID to delete.</param>
        public void DeleteClub(Guid clubGuid)
        {
            // TODO: Check user has delete right
            // First check if there is any shooters in this club.
            if (this.shooterRepository.FilterBy(shooter => shooter.Club.Id.Equals(clubGuid)).Any())
            {
                throw new DependencysExistException(
                    DependencysExistException.ConflictingDepencencyEnum.Shooter);
            }

            var clubToDelete = this.clubRepository.FindBy(club => club.Id.Equals(clubGuid));
            this.clubRepository.Delete(clubToDelete);
        }
    }
}
