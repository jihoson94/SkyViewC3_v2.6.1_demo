using System;

namespace SkyViewC3Service.Repositories.IMSRepositoryExceptions
{
    public class NotFouncVialException : Exception
    {
        public NotFouncVialException(string message) : base(message)
        {
        }
    }
}