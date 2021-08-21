using System;


namespace MovieLand.Infrastructure.Exceptions
{
    public class InfrastructureException : Exception
    {
        internal InfrastructureException(string message)
               : base(message) { }

        internal InfrastructureException(string message, Exception exception)
            : base(message, exception) { }
    }
}
