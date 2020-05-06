using System;

namespace SkyViewC3Service.Repositories.IMSRepositoryExceptions
{
    public class NotFoundBoxException : Exception
    {
        public NotFoundBoxException(string message) : base(message)
        {
        }
    }
}