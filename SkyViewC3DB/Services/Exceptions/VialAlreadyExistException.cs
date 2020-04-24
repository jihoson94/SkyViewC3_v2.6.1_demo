using System;

namespace SkyViewC3DB.Services.Exceptions
{
    public class VialAlreadyExistException : Exception
    {
        public VialAlreadyExistException()
        {
        }

        public VialAlreadyExistException(string message) : base(message)
        {
        }
    }

}