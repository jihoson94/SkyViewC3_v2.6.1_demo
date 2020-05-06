using System;

namespace SkyViewC3Service.Repositories.IMSRepositoryExceptions
{
    public class NotFoundRackException : Exception
    {
        public NotFoundRackException(string message) : base(message)
        {
        }
    }
}