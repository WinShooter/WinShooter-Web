// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UsersLogic.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The users logic.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic
{
    using System;
    using System.Linq;

    using log4net;

    using WinShooter.Database;
    using WinShooter.Logic.Authentication;
    using WinShooter.Logic.Authorization;

    /// <summary>
    /// The users logic.
    /// </summary>
    public class UsersLogic
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// The competition repository.
        /// </summary>
        private readonly IRepository<User> userRepository;

        /// <summary>
        /// The current user.
        /// </summary>
        private CustomPrincipal currentUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersLogic"/> class.
        /// </summary>
        /// <param name="userRepository">
        /// The user repository.
        /// </param>
        /// <param name="rightsHelper">
        /// The rights helper implementation.
        /// </param>
        public UsersLogic(IRepository<User> userRepository, IRightsHelper rightsHelper) : this()
        {
            this.userRepository = userRepository;
            this.RightsHelper = rightsHelper;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersLogic"/> class.
        /// </summary>
        /// <param name="session">
        /// The session.
        /// </param>
        public UsersLogic(NHibernate.ISession session)
            : this(
            new Repository<User>(session), 
            new RightsHelper(new Repository<UserRolesInfo>(session), new Repository<RoleRightsInfo>(session)))
        {
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="UsersLogic"/> class from being created.
        /// </summary>
        private UsersLogic()
        {
            this.log = LogManager.GetLogger(this.GetType());
        }

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
        /// Gets the rights helper.
        /// </summary>
        public IRightsHelper RightsHelper { get; private set; }

        /// <summary>
        /// Updates a competition.
        /// </summary>
        /// <param name="user">
        /// The user to add or update.
        /// </param>
        public void UpdateUser(User user)
        {
            if (!this.CurrentUser.Id.Equals(user.Id))
            {
                this.log.WarnFormat("User {0} is trying to delete user {1}", this.CurrentUser, user);
                throw new Exception("You can only update yourself.");
            }

            var databaseUser = this.userRepository.FilterBy(x => x.Id.Equals(user.Id)).First();
            databaseUser.UpdateFromOther(user);

            using (var transaction = this.userRepository.StartTransaction())
            {
                this.userRepository.Update(databaseUser);
                transaction.Commit();
            }
        }

        /// <summary>
        /// The delete competition.
        /// </summary>
        /// <param name="userId">
        /// The user ID.
        /// </param>
        public void DeleteUser(Guid userId)
        {
            if (!this.CurrentUser.Id.Equals(userId))
            {
                this.log.WarnFormat("User {0} is trying to delete user {1}", this.CurrentUser, userId);
                throw new Exception("You can only delete yourself.");
            }

            var user = this.userRepository.FilterBy(x => x.Id.Equals(userId));

            using (var transaction = this.userRepository.StartTransaction())
            {
                if (this.userRepository.Delete(user))
                {
                    transaction.Commit();
                }
            }
        }
    }
}
