using System;

namespace RobotStoreEntitiesLib
{
    public class TopTempCalibrationHistory
    {
        public int TopTempCalibrationHistoryID { get; set; }
        public double Reference { get; set; }
        public double Value { get; set; }
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
        public string Action { get; set; }
    }
}