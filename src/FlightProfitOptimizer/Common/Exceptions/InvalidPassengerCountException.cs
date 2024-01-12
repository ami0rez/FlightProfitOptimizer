namespace FlightProfitOptimizer.Common.Exceptions
{
    /// <summary>
    /// The exception that is thrown when an invalid number of passengers is specified.
    /// </summary>
    /// <remarks>
    /// This exception is used within the passenger generation process to validate
    /// that the requested number of passengers is a positive, non-zero value.
    /// </remarks>
    public class InvalidPassengerCountException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPassengerCountException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidPassengerCountException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPassengerCountException"/> class
        /// with a specified error message and a reference to the inner exception that is
        /// the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception,
        /// or a null reference if no inner exception is specified.</param>
        public InvalidPassengerCountException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPassengerCountException"/> class.
        /// </summary>
        public InvalidPassengerCountException()
            : base()
        {
        }
    }
}
