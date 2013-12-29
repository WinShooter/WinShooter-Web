namespace WinShooter.Web.DataValidation
{
    /// <summary>
    /// The validation.
    /// </summary>
    /// <typeparam name="T">
    /// The type to validate.
    /// </typeparam>
    public class Validation<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Validation{T}"/> class.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="argName">
        /// The argument name.
        /// </param>
        public Validation(T value, string argName)
        {
            this.Value = value;
            this.ArgName = argName;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Gets the argument name.
        /// </summary>
        public string ArgName { get; private set; }
    }
}
