// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserService.cs" company="Copyright ©2014 John Allberg & Jonas Fredriksson">
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
//   The competition service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Api.User
{
    using ServiceStack.ServiceInterface;

    using WinShooter.Api.Api.Competition;
    using WinShooter.Api.Authentication;

    /// <summary>
    /// The competition service.
    /// </summary>
    public class UserService : Service
    {
        /// <summary>
        /// The any.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The <see cref="CompetitionResponse"/>.
        /// </returns>
        public UserResponse Get(UserRequest request)
        {
            var session = this.GetSession() as CustomUserSession;

            if (session != null && session.User != null)
            {
                return new UserResponse 
                { 
                    IsLoggedIn = true,
                    DisplayName = session.User.DisplayName,
                    Email = session.User.Email 
                };
            }

            return new UserResponse { IsLoggedIn = false };
        }
    }
}
