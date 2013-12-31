namespace WinShooter.Logic.Authorization
{
    using System;

    public interface IRightsHelper
    {
        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="includePublic">
        /// If all public competitions should be included.
        /// </param>
        /// <returns>
        /// The <see cref="Guid"/> array.
        /// </returns>
        Guid[] GetCompetitionIdsTheUserHasRightsOn(Guid userId, bool includePublic);

        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        WinShooterCompetitionPermissions[] GetRightsForCompetitionIdAndTheUser(Guid userId, Guid competitionId);

        /// <summary>
        /// Get competition ids the user has rights on.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="competitionId">
        /// The competition Id.
        /// </param>
        /// <returns>
        /// The <see cref="WinShooterCompetitionPermissions"/> array.
        /// </returns>
        string[] GetRolesForCompetitionIdAndTheUser(Guid userId, Guid competitionId);
    }
}