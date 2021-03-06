using System;

namespace RobotStoreEntitiesLib
{
    public class LN2LevelCalibratoinHistory
    {
        public int LN2LevelCalibratoinHistoryID { get; set; }
        public double Reference { get; set; }
        public double Value { get; set; }
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string Action { get; set; } // add, remove
    }
}