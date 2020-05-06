using System;

namespace SkyViewC3Service.Repositories.IMSRepositoryExceptions
{
    public class AlreadyOccupiedInSlotException : Exception
    {
        public AlreadyOccupiedInSlotException(string message) : base(message)
        {
        }
    }
}