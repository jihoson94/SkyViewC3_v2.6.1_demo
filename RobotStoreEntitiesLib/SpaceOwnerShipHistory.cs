using System;

namespace RobotStoreEntitiesLib
{
    public class SpaceOwnerShipHistory
    {
        public int SpaceOwnerShipHistoryID { get; set; }
        public int Slot { get; set; }
        public int RackID { get; set; }
        public Rack Rack { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string Action { get; set; } // Add or Remove
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
    }
}