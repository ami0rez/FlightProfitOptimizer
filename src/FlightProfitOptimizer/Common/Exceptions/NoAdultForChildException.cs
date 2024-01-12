using FlightProfitOptimizer.Common.Constants;

namespace FlightProfitOptimizer.Common.Exceptions
{
    /// <summary>
    /// Represents errors that occur when a child is added to a family without an accompanying adult.
    /// </summary>
    /// <remarks>
    /// This exception is thrown in scenarios where business logic mandates that every child in a family
    /// must be accompanied by at least one adult. It helps ensure that the family composition adheres to
    /// the established rules and safety guidelines.
    /// </remarks>
    public class NoAdultForChildException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoAdultForChildException"/> class with the standard error message.
        /// </summary>
        /// <remarks>
        /// The message used is defined in <see cref="ErrorConstants.ChildNeedsAdult"/>.
        /// </remarks>
        public NoAdultForChildException()
            : base(ErrorConstants.ChildNeedsAdult)
        {
        }

        // Additional constructors could be added here, such as one accepting a custom message or an inner exception,
        // if the use case of the exception within the application requires them.
    }
}
