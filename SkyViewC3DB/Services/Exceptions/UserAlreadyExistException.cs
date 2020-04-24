using System;

namespace SkyViewC3DB.Services.Exceptions
{
    public class UserAlreadyExistException : Exception
    {
        public UserAlreadyExistException()
        {
        }
        public UserAlreadyExistException(string message) : base(message)
        {
        }
    }
}