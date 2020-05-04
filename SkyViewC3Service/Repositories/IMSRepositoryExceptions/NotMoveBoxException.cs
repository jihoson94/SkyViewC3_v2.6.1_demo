using System;

namespace SkyViewC3Service.Repositories.IMSRepositoryExceptions
{
    public class NotMoveBoxException : Exception
    {
        public NotMoveBoxException(string message) : base(message)
        {
        }
    }
}