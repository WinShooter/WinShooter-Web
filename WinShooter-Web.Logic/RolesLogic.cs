// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RolesLogic.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The roles logic.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Logic
{
    using System.Configuration;
    using System.Linq;

    using NHibernate.Linq;

    using WinShooter.Database;

    /// <summary>
    /// The roles logic.
    /// </summary>
    public class RolesLogic
    {
        /// <summary>
        /// Gets the default owner role name.
        /// </summary>
        public string DefaultOwnerRoleName
        {
            get
            {
                var name = ConfigurationManager.AppSettings["DefaultCompetitionOwnerRole"];

                if (name == null)
                {
                    name = "CompetitionOwner";
                }

                return name;
            }
        }

        /// <summary>
        /// Gets the default owner role name.
        /// </summary>
        public Role DefaultOwnerRole
        {
            get
            {
                return this.GetRole(this.DefaultOwnerRoleName);
            }
        }

        /// <summary>
        /// Get a certain role.
        /// </summary>
        /// <param name="roleName">
        /// The name of the role we searching for.
        /// </param>
        /// <returns>
        /// The <see cref="Competition"/>.
        /// </returns>
        public Role GetRole(string roleName)
        {
            using (var databaseSession = NHibernateHelper.OpenSession())
            {
                var roles = from role in databaseSession.Query<Role>()
                                   where role.RoleName == roleName
                                   select role;

                return roles.FirstOrDefault();
            }
        }
    }
}
