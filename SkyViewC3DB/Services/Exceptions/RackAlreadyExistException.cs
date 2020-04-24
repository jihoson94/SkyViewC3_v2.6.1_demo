using System;

namespace SkyViewC3DB.Services.Exceptions
{
    public class RackAlreadyExistException : Exception
    {
        public RackAlreadyExistException()
        {
        }
        public RackAlreadyExistException(string message) : base(message)
        {

        }
    }
}