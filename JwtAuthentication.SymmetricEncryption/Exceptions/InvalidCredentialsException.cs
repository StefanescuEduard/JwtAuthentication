using System;

namespace JwtAuthentication.SymmetricEncryption.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
        {
        }

        public InvalidCredentialsException(string message) : base(message)
        {
        }

        public InvalidCredentialsException(string message,
            Exception exception) : base(message, exception)
        {
        }
    }
}