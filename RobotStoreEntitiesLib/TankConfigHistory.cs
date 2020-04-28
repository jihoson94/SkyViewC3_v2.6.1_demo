using System;

namespace RobotStoreEntitiesLib
{
    public class TankConfigHistory
    {
        public int TankConfigHistoryID { get; set; }
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
    }
}