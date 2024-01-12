namespace FlightProfitOptimizer.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when the size of a family exceeds the maximum limit.
    /// </summary>
    public class InvalidFamilySizeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFamilySizeException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidFamilySizeException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFamilySizeException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public InvalidFamilySizeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFamilySizeException"/> class.
        /// </summary>
        public InvalidFamilySizeException() : base()
        {
        }
    }
}
