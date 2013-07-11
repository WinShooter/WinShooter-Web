// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WinShooterMembershipProvider.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
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
//   The WinShooter membership provider.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Security;

    using log4net;

    using WebMatrix.WebData;

    /// <summary>
    /// The WinShooter membership provider.
    /// </summary>
    public class WinShooterMembershipProvider : ExtendedMembershipProvider
    {
        /// <summary>
        /// The log.
        /// </summary>
        private readonly ILog log;

        /// <summary>
        /// Initializes a new instance of the <see cref="WinShooterMembershipProvider"/> class.
        /// </summary>
        public WinShooterMembershipProvider()
        {
            log4net.Config.XmlConfigurator.Configure();
            this.log = LogManager.GetLogger(this.GetType());
            this.log.Debug("WinShooterMembershipProvider created");
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
        /// </summary>
        /// <returns>
        /// true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
        /// </returns>
        public override bool EnablePasswordRetrieval
        {
            get
            {
                this.log.Debug("EnablePasswordRetrieval");
                return false;
            }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <returns>
        /// true if the membership provider supports password reset; otherwise, false. The default is true.
        /// </returns>
        public override bool EnablePasswordReset
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
        /// </summary>
        /// <returns>
        /// true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <returns>
        /// Returns the name of the application using the custom membership provider.
        /// </returns>
        public override string ApplicationName { get; set; }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </returns>
        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </summary>
        /// <returns>
        /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
        /// </returns>
        public override int PasswordAttemptWindow
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <returns>
        /// true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
        /// </returns>
        public override bool RequiresUniqueEmail
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <returns>
        /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"/> values indicating the format for storing passwords in the data store.
        /// </returns>
        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return MembershipPasswordFormat.Encrypted;
            }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <returns>
        /// The minimum length required for a password. 
        /// </returns>
        public override int MinRequiredPasswordLength
        {
            get
            {
                return 12;
            }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <returns>
        /// The minimum number of special characters that must be present in a valid password.
        /// </returns>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <returns>
        /// A regular expression used to evaluate a password.
        /// </returns>
        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the information for the newly created user.
        /// </returns>
        /// <param name="username">The user name for the new user. </param><param name="password">The password for the new user. </param><param name="email">The e-mail address for the new user.</param><param name="passwordQuestion">The password question for the new user.</param><param name="passwordAnswer">The password answer for the new user</param><param name="isApproved">Whether or not the new user is approved to be validated.</param><param name="providerUserKey">The unique identifier from the membership data source for the user.</param><param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"/> enumeration value indicating whether the user was created successfully.</param>
        public override MembershipUser CreateUser(
            string username,
            string password,
            string email,
            string passwordQuestion,
            string passwordAnswer,
            bool isApproved,
            object providerUserKey,
            out MembershipCreateStatus status)
        {
            // TODO Implement
            this.log.Debug("CreateUser");
            status = MembershipCreateStatus.Success;
            return new MembershipUser("google", "a", null, "a@a.com", "a", "a", true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
        }

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <returns>
        /// true if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        /// <param name="username">The user to change the password question and answer for. </param><param name="password">The password for the specified user. </param><param name="newPasswordQuestion">The new password question for the specified user. </param><param name="newPasswordAnswer">The new password answer for the specified user. </param>
        public override bool ChangePasswordQuestionAndAnswer(
            string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        /// <param name="username">The user to retrieve the password for. </param><param name="answer">The password answer for the user. </param>
        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        /// <param name="username">The user to update the password for. </param><param name="oldPassword">The current password for the specified user. </param><param name="newPassword">The new password for the specified user. </param>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <returns>
        /// The new password for the specified user.
        /// </returns>
        /// <param name="username">The user to reset the password for. </param><param name="answer">The password answer for the specified user. </param>
        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser"/> object that represents the user to update and the updated information for the user. </param>
        public override void UpdateUser(MembershipUser user)
        {
            // TODO Implement
            this.log.Debug("UpdateUser");
        }

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        /// <param name="username">The name of the user to validate. </param><param name="password">The password for the specified user. </param>
        public override bool ValidateUser(string username, string password)
        {
            return false;
        }

        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        /// <param name="userName">The membership user whose lock status you want to clear.</param>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param><param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            // TODO Implement
            this.log.Debug("GetUser");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        /// <param name="username">The name of the user to get information for. </param><param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user. </param>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            // TODO Implement
            this.log.Debug("GetUser");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <returns>
        /// The user name associated with the specified e-mail address. If no match is found, return null.
        /// </returns>
        /// <param name="email">The e-mail address to search for. </param>
        public override string GetUserNameByEmail(string email)
        {
            // TODO Implement
            this.log.Debug("GetUserNameByEmail");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a user from the membership data source. 
        /// </summary>
        /// <returns>
        /// true if the user was successfully deleted; otherwise, false.
        /// </returns>
        /// <param name="username">The name of the user to delete.</param><param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            // TODO Implement
            this.log.Debug("DeleteUser");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of all the users in the data source in pages of data.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            this.log.Debug("GetAllUsers");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the number of users currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of users currently accessing the application.
        /// </returns>
        public override int GetNumberOfUsersOnline()
        {
            this.log.Debug("GetNumberOfUsersOnline");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of membership users where the user name contains the specified user name to match.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="usernameToMatch">The user name to search for.</param><param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            this.log.Debug("FindUsersByName");
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        /// <param name="emailToMatch">The e-mail address to search for.</param><param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param><param name="pageSize">The size of the page of results to return.</param><param name="totalRecords">The total number of matched users.</param>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            this.log.Debug("FindUsersByEmail");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns all OAuthentication membership accounts associated with the specified user name.
        /// </summary>
        /// <returns>
        /// A list of all OAuthentication membership accounts associated with the specified user name.
        /// </returns>
        /// <param name="userName">The user name.</param>
        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            this.log.Debug("GetAccountsForUser");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, creates a new user profile and a new membership account.
        /// </summary>
        /// <returns>
        /// A token that can be sent to the user to confirm the user account.
        /// </returns>
        /// <param name="userName">The user name.</param><param name="password">The password.</param><param name="requireConfirmation">(Optional) true to specify that the user account must be confirmed; otherwise, false. The default is false.</param><param name="values">(Optional) A dictionary that contains additional user attributes to store in the user profile. The default is null.</param>
        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            // TODO Implement
            this.log.Debug("CreateUserAndAccount");
            return "sm0uda@gmail.com";
        }

        /// <summary>
        /// When overridden in a derived class, creates a new user account using the specified user name and password, optionally requiring that the new account must be confirmed before the account is available for use.
        /// </summary>
        /// <returns>
        /// A token that can be sent to the user to confirm the account.
        /// </returns>
        /// <param name="userName">The user name.</param><param name="password">The password.</param><param name="requireConfirmationToken">(Optional) true to specify that the account must be confirmed; otherwise, false. The default is false.</param>
        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            // TODO Implement
            this.log.Debug("CreateAccount");
            return "sm0uda@gmail.com";
        }

        /// <summary>
        /// Activates a pending membership account for the specified user.
        /// </summary>
        /// <returns>
        /// true if the account is confirmed; otherwise, false.
        /// </returns>
        /// <param name="userName">The user name.</param><param name="accountConfirmationToken">A confirmation token to pass to the authentication provider.</param>
        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            this.log.Debug("ConfirmAccount");
            return true;
            throw new NotImplementedException();
        }

        /// <summary>
        /// Activates a pending membership account.
        /// </summary>
        /// <returns>
        /// true if the account is confirmed; otherwise, false.
        /// </returns>
        /// <param name="accountConfirmationToken">A confirmation token to pass to the authentication provider.</param>
        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            this.log.Debug("ConfirmAccount");
            return true;
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, deletes the specified membership account.
        /// </summary>
        /// <returns>
        /// true if the user account was deleted; otherwise, false.
        /// </returns>
        /// <param name="userName">The user name.</param>
        public override bool DeleteAccount(string userName)
        {
            this.log.Debug("DeleteAccount");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, generates a password reset token that can be sent to a user in email.
        /// </summary>
        /// <returns>
        /// A token to send to the user.
        /// </returns>
        /// <param name="userName">The user name.</param><param name="tokenExpirationInMinutesFromNow">(Optional) The time, in minutes, until the password reset token expires. The default is 1440 (24 hours).</param>
        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            this.log.Debug("GeneratePasswordResetToken");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns an ID for a user based on a password reset token.
        /// </summary>
        /// <returns>
        /// The user ID.
        /// </returns>
        /// <param name="token">The password reset token.</param>
        public override int GetUserIdFromPasswordResetToken(string token)
        {
            this.log.Debug("GetUserIdFromPasswordResetToken");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns a value that indicates whether the user account has been confirmed by the provider.
        /// </summary>
        /// <returns>
        /// true if the user is confirmed; otherwise, false.
        /// </returns>
        /// <param name="userName">The user name.</param>
        public override bool IsConfirmed(string userName)
        {
            this.log.Debug("IsConfirmed");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, resets a password after verifying that the specified password reset token is valid.
        /// </summary>
        /// <returns>
        /// true if the password was changed; otherwise, false.
        /// </returns>
        /// <param name="token">A password reset token.</param><param name="newPassword">The new password.</param>
        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            this.log.Debug("ResetPasswordWithToken");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the number of times that the password for the specified user account was incorrectly entered since the most recent successful login or since the user account was created.
        /// </summary>
        /// <returns>
        /// The count of failed password attempts for the specified user account.
        /// </returns>
        /// <param name="userName">The user name of the account.</param>
        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            this.log.Debug("GetPasswordFailuresSinceLastSuccess");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the date and time when the specified user account was created.
        /// </summary>
        /// <returns>
        /// The date and time the account was created, or <see cref="F:System.DateTime.MinValue"/> if the account creation date is not available.
        /// </returns>
        /// <param name="userName">The user name of the account.</param>
        public override DateTime GetCreateDate(string userName)
        {
            this.log.Debug("GetCreateDate");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the date and time when the password was most recently changed for the specified membership account.
        /// </summary>
        /// <returns>
        /// The date and time when the password was more recently changed for membership account, or <see cref="F:System.DateTime.MinValue"/> if the password has never been changed for this user account.
        /// </returns>
        /// <param name="userName">The user name of the account.</param>
        public override DateTime GetPasswordChangedDate(string userName)
        {
            this.log.Debug("GetPasswordChangedDate");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, returns the date and time when an incorrect password was most recently entered for the specified user account.
        /// </summary>
        /// <returns>
        /// The date and time when an incorrect password was most recently entered for this user account, or <see cref="F:System.DateTime.MinValue"/> if an incorrect password has not been entered for this user account.
        /// </returns>
        /// <param name="userName">The user name of the account.</param>
        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            this.log.Debug("GetLastPasswordFailureDate");
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, creates a new OAuth membership account, or updates an existing OAuth Membership account.
        /// </summary>
        /// <param name="provider">The OAuth or OpenID provider.</param><param name="providerUserId">The OAuth or OpenID provider user ID. This is not the user ID of the user account, but the user ID on the OAuth or Open ID provider.</param><param name="userName">The user name.</param>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
        {
            // TODO Implement
            this.log.Debug("CreateOrUpdateOAuthAccount");
        }

        /// <summary>
        /// When overridden in a derived class, returns the user ID for the specified OAuth or OpenID provider and provider user ID.
        /// </summary>
        /// <param name="provider">
        /// The name of the OAuth or OpenID provider.
        /// </param>
        /// <param name="providerUserId">
        /// The OAuth or OpenID provider user ID. This is not the user ID of the user account, but the user ID on the OAuth or Open ID provider.
        /// </param>
        /// <returns>
        /// The UserId of the application.
        /// </returns>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
        public override int GetUserIdFromOAuth(string provider, string providerUserId)
        {
            // TODO Implement
            this.log.Debug("GetUserIdFromOAuth");
            if (provider == "google"
                && providerUserId == "https://www.google.com/accounts/o8/id?id=AItOawmsw82S-x8Vu3TlX4pW9DfF8e-6f0bwz_c")
            {
                // sm0uda@gmail.com
                return 42;
            }

            return -1;
        }

        /// <summary>
        /// Returns the user name that is associated with the specified user ID.
        /// </summary>
        /// <returns>
        /// The user name.
        /// </returns>
        /// <param name="userId">The user ID to get the name for.</param>
        public override string GetUserNameFromId(int userId)
        {           
            // TODO Implement
            this.log.Debug("GetUserNameFromId");

            if (userId == 42)
            {
                return "sm0uda@gmail.com";
            }

            return null;
        }
    }
}