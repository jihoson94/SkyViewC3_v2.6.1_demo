using System;

namespace SkyViewC3DB.Services.Exceptions
{
    public class BoxAlreadyExistException : Exception
    {
        public BoxAlreadyExistException()
        {
        }
        public BoxAlreadyExistException(string message) : base(message)
        {
        }
    }
}