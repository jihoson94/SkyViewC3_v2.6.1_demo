using System;

namespace RobotStoreEntitiesLib
{
    public class VialHistory
    {
        public int VialHistoryID { get; set; }
        public string VialID { get; set; }
        public Vial Vial { get; set; }
        public string BoxID { get; set; }
        public Box Box { get; set; }
        public int posisiton { get; set; }
        public bool IsOut { get; set; }
        public VialType Type { get; set; }
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
    }
}