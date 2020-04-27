using System;

namespace RobotStoreEntitiesLib
{
    public class UserPermissionHistory
    {
        public int UserPermissionHistoryID { get; set; }
        public User User { get; set; }
        public Permission Permission { get; set; }
        public string Action { get; set; } // Add or Remove
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
    }
}