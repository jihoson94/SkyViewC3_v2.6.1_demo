using System;

namespace RobotStoreEntitiesLib
{
    public class AlarmLog
    {
        public int AlarmLogID { get; set; }
        public DateTime Created { get; set; }
        public string AlarmCode { get; set; }
    }
}