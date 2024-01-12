using FlightProfitOptimizer.Common.Constants;

namespace FlightProfitOptimizer.Common.Exceptions
{
    /// <summary>
    /// Represents errors that occur when the maximum allowed number of adults in a family is exceeded.
    /// </summary>
    /// <remarks>
    /// This exception should be thrown during the family member addition process when trying to
    /// add an adult to a family that has already reached its limit of adult members.
    /// Utilizing this exception helps to enforce business rules regarding family compositions.
    /// </remarks>
    public class MaximumAdultsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaximumAdultsException"/> class with the default error message.
        /// </summary>
        public MaximumAdultsException()
            : base(ErrorConstants.MaxAdultsReached)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaximumAdultsException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MaximumAdultsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MaximumAdultsException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public MaximumAdultsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
