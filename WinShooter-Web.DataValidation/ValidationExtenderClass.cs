namespace WinShooter.Web.DataValidation
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The validation extender with the generic validation.
    /// </summary>
    public static class ValidationExtenderClass
    {
        /// <summary>
        /// The not null.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <typeparam name="T">
        /// The type to be checked.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Validation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// When the item is null.
        /// </exception>
        [DebuggerHidden]
        public static Validation<T> NotNull<T>(this Validation<T> item) where T : class
        {
            if (item.Value == null)
            {
                ThrowHelper.ThrowArgumentNullException(item.ArgName);
            }

            return item;
        }
    }
}