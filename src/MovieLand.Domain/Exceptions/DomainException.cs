using System;


namespace MovieLand.Domain.Exceptions
{
    public class DomainException : Exception
    {
        internal DomainException(string message)
            : base(message) { }


        internal DomainException(string message, Exception exception)
            : base(message, exception) { }
    }
}
