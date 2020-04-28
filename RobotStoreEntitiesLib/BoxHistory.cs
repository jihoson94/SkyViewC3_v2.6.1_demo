using System;

namespace RobotStoreEntitiesLib
{
    public class BoxHistory
    {
        public int BoxHistoryID { get; set; }
        public string BoxID { get; set; }
        public Box Box { get; set; }
        public int RackID { get; set; }
        public Rack Rack { get; set; }
        public int Slot { get; set; }
        public bool IsOut { get; set; }
        public string BoxTypeName { get; set; }
        public BoxType BoxType { get; set; }
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
    }
}