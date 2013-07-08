// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AuthConfig.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   Configures the external authentication providers.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.App_Start
{
    using Microsoft.Web.WebPages.OAuth;

    /// <summary>
    /// Configures the external authentication providers.
    /// </summary>
    public static class AuthConfig
    {
        /// <summary>
        /// Configures the external authentication providers.
        /// </summary>
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166
            // OAuthWebSecurity.RegisterMicrosoftClient(
            // clientId: "",
            // clientSecret: "");
            // OAuthWebSecurity.RegisterTwitterClient(
            // consumerKey: "",
            // consumerSecret: "");
            // OAuthWebSecurity.RegisterFacebookClient(
            // appId: "",
            // appSecret: "");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}