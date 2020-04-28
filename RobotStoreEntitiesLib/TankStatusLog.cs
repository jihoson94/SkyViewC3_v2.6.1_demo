using System;

namespace RobotStoreEntitiesLib
{
    public class TankStatusLog
    {
        public int TankStatusLogID { get; set; }
        public DateTime Created { get; set; }
        public double TopTemperature { get; set; }
        public double BottomTemperature { get; set; }
        public double BypassTemperature { get; set; }
        public double LN2Level { get; set; }
        public double LN2Usage { get; set; }
    }
}