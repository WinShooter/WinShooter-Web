namespace WinShooter.Web.DataValidation
{
    using System;

    /// <summary>
    /// Internal helper for the validation classes.
    /// Performance is much higher for them when the exception throwing is done outside of that class.
    /// </summary>
    internal static class ThrowHelper
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="argumentName">
        /// The argument name.
        /// </param>
        internal static void ThrowArgumentNullException(string argumentName)
        {
            throw new ArgumentNullException(argumentName);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        internal static void ThrowArgumentException(string message)
        {
            throw new ArgumentException(message);
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="paramName">
        /// The parameter name.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        internal static void ThrowArgumentOutOfRangeException(string paramName, string message)
        {
            throw new ArgumentOutOfRangeException(paramName, message);
        }
    }
}
