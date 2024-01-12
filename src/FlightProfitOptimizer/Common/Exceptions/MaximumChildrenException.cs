using FlightProfitOptimizer.Common.Constants;

namespace FlightProfitOptimizer.Common.Exceptions
{
    /// <summary>
    /// Represents errors that occur when attempting to add more children to a family than the allowed maximum.
    /// </summary>
    /// <remarks>
    /// This exception is used to enforce business rules regarding the maximum number of children 
    /// permitted in a family. It ensures that family compositions remain within defined limits.
    /// </remarks>
    public class MaximumChildrenException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaximumChildrenException"/> class with a predefined error message.
        /// </summary>
        /// <remarks>
        /// The error message is defined in <see cref="ErrorConstants.MaxChildrenReached"/> and describes
        /// the violation of the maximum permitted number of children in a family.
        /// </remarks>
        public MaximumChildrenException()
            : base(ErrorConstants.MaxChildrenReached)
        {
        }

        // As with other custom exceptions, additional constructors can be implemented if the 
        // application logic requires handling custom messages or inner exceptions.
    }
}
