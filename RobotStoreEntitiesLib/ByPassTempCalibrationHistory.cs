using System;

namespace RobotStoreEntitiesLib
{
    public class ByPassTempCalibrationHistory
    {
        public int ByPassTempCalibrationHistoryID { get; set; }
        public double Reference { get; set; }
        public double Value { get; set; }
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string Action { get; set; } // add, remove

    }
}