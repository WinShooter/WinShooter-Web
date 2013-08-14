// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RequiredWinShooterPermissionAttribute.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The authorization filter.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Authorization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using ServiceStack;
    using ServiceStack.ServiceHost;
    using ServiceStack.ServiceInterface;
    using ServiceStack.ServiceInterface.Auth;

    using WinShooter.Api.Authentication;

    /// <summary>
    /// The authorization filter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class RequiredWinShooterCompetitionPermissionAttribute : AuthenticateAttribute
    {
        /// <summary>
        /// The rights.
        /// </summary>
        private UserCompetitionRights rights;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredWinShooterCompetitionPermissionAttribute"/> class.
        /// </summary>
        /// <param name="permissions">
        /// The permissions.
        /// </param>
        public RequiredWinShooterCompetitionPermissionAttribute(params string[] permissions)
            : this(ApplyTo.All, permissions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredWinShooterCompetitionPermissionAttribute"/> class.
        /// </summary>
        /// <param name="applyTo">
        /// The apply to.
        /// </param>
        /// <param name="permissions">
        /// The permissions.
        /// </param>
        public RequiredWinShooterCompetitionPermissionAttribute(ApplyTo applyTo, params string[] permissions)
            : this(applyTo, null, permissions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredWinShooterCompetitionPermissionAttribute"/> class.
        /// </summary>
        /// <param name="applyTo">
        /// The apply to.
        /// </param>
        /// <param name="rights">
        /// The rights for the user for the current competition.
        /// </param>
        /// <param name="permissions">
        /// The permissions.
        /// </param>
        internal RequiredWinShooterCompetitionPermissionAttribute(ApplyTo applyTo, UserCompetitionRights rights, params string[] permissions)
        {
            this.rights = rights;
            this.RequiredPermissions = permissions.ToList();
            this.ApplyTo = applyTo;
            this.Priority = (int)RequestFilterPriority.RequiredPermission;
        }

        /// <summary>
        /// Gets or sets the required permissions.
        /// </summary>
        public List<string> RequiredPermissions { get; set; }

        /// <summary>
        /// Gets the competitionID from request URL.
        /// </summary>
        /// <param name="relativeUrl">The URL</param>
        /// <returns>The <see cref="Guid"/> for the competition or null of none is found</returns>
        public static Guid GetCompetitionIdFromUrl(string relativeUrl)
        {
            const string CompetitionString = "/COMPETITION/";

            try
            {
                relativeUrl = relativeUrl.ToUpperInvariant();
                var competitionStart = relativeUrl.IndexOf(CompetitionString, StringComparison.Ordinal);

                if (competitionStart == -1)
                {
                    return Guid.Empty;
                }

                relativeUrl = relativeUrl.Substring(competitionStart + CompetitionString.Length);

                var lastSlash = relativeUrl.IndexOf('/');
                if (lastSlash > -1)
                {
                    relativeUrl = relativeUrl.Substring(0, lastSlash);
                }

                return Guid.Parse(relativeUrl);
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="httpRequest">
        /// The http request.
        /// </param>
        /// <param name="httpResponse">
        /// The http response.
        /// </param>
        /// <param name="requestDto">
        /// The request DTO.
        /// </param>
        public override void Execute(IHttpRequest httpRequest, IHttpResponse httpResponse, object requestDto)
        {
            base.Execute(httpRequest, httpResponse, requestDto); // first check if session is authenticated
            if (httpResponse.IsClosed)
            {
                // AuthenticateAttribute already closed the request (ie auth failed)
                return;
            }

            var competitionId = GetCompetitionIdFromUrl(httpRequest.PathInfo);
            var session = httpRequest.GetSession() as CustomUserSession;

            if (this.rights == null || this.rights.CompetitionId != competitionId)
            {
                this.rights = session == null ? new UserCompetitionRights(competitionId) : new UserCompetitionRights(competitionId, session.User);
            }

            if (session != null)
            {
                session.UserCompetitionRights = this.rights;
            }

            if (this.HasAllPermissions(httpRequest, session))
            {
                return;
            }

            if (this.DoHtmlRedirectIfConfigured(httpRequest, httpResponse))
            {
                return;
            }

            httpResponse.StatusCode = (int)HttpStatusCode.Forbidden;
            httpResponse.StatusDescription = "Invalid Permission";
            httpResponse.EndRequest();
        }

        /// <summary>
        /// Check that the user has all permissions.
        /// </summary>
        /// <param name="httpRequest">
        /// The request.
        /// </param>
        /// <param name="session">
        /// The session.
        /// </param>
        /// <param name="userAuthRepo">
        /// The user authentication repository.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool HasAllPermissions(IHttpRequest httpRequest, IAuthSession session, IUserAuthRepository userAuthRepo = null)
        {
            if (session.HasRole(RoleNames.Admin))
            {
                return true;
            }

            var competitionId = GetCompetitionIdFromUrl(httpRequest.PathInfo);

            var customUserSession = session as CustomUserSession;
            if (customUserSession == null || customUserSession.User == null || (Guid.Empty == competitionId))
            {
                // Anonymous
                // TODO Implement
                return false;
            }
            else
            {
                if (this.rights == null)
                {
                    this.rights = new UserCompetitionRights(competitionId, customUserSession.User);
                }

                var missingRights =
                    (from requiredRight in this.RequiredPermissions
                        where !this.rights.HasPermission(requiredRight)
                        select requiredRight).Any();

                return !missingRights;
            }
        }
    }
}
