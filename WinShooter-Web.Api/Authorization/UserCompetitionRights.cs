﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserCompetitionRights.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Gets the user rights on a certain competition.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using WinShooter.Database;

    /// <summary>
    /// Gets the user rights on a certain competition.
    /// </summary>
    internal class UserCompetitionRights
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCompetitionRights"/> class.
        /// </summary>
        /// <param name="competitionId">
        /// The competition id.
        /// </param>
        internal UserCompetitionRights(Guid competitionId)
        {
            this.CompetitionId = competitionId;
            this.Permissions = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCompetitionRights"/> class.
        /// </summary>
        /// <param name="competitionId">
        /// The competition id.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        internal UserCompetitionRights(Guid competitionId, User user)
        {
            this.CompetitionId = competitionId;
            this.Permissions = new List<string>();

            using (var dbsession = NHibernateHelper.OpenSession())
            {
                // TODO Implement fetching rights from database
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCompetitionRights"/> class.
        /// </summary>
        /// <param name="competitionId">
        /// The competition id.
        /// </param>
        /// <param name="permissions">
        /// The permissions.
        /// </param>
        internal UserCompetitionRights(Guid competitionId, List<string> permissions)
        {
            this.CompetitionId = competitionId;
            this.Permissions = permissions;
        }

        /// <summary>
        /// Gets the permissions.
        /// </summary>
        internal List<string> Permissions { get; private set; }

        internal Guid CompetitionId { get; private set; }

        /// <summary>
        /// Check if a user has a certain permission.
        /// </summary>
        /// <param name="requestedPermission">
        /// The permission.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        internal bool HasPermission(string requestedPermission)
        {
            requestedPermission = requestedPermission.ToUpperInvariant();

            return (from permision in this.Permissions
                where permision.ToUpperInvariant() == requestedPermission
                select permision.ToUpperInvariant()).Any();
        }
    }
}
