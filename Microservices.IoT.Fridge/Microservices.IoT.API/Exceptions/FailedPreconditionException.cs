using System;

namespace Microservices.IoT.API.Exceptions
{
    /// <summary>
    /// Signifies violation of a business logic rule
    /// <br>maps to HTTP Status code 422</br>
    /// </summary>
    [Serializable]
    public class FailedPreconditionException : Exception
    {
        public FailedPreconditionException()
        {
        }

        public FailedPreconditionException(string message)
            : base(message)
        {
        }

        public FailedPreconditionException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
