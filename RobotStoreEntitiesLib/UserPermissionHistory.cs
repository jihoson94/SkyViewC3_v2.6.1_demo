using System;
using System.ComponentModel.DataAnnotations;

namespace RobotStoreEntitiesLib
{
    public class UserPermissionHistory
    {
        [Key]
        public int UserPermissionHistoryID { get; set; }
        public User User { get; set; }
        public Permission Permission { get; set; }
        public string Action { get; set; } // Add or Remove
        public User AddBy { get; set; }
        public DateTime AddDate { get; set; }
    }
}