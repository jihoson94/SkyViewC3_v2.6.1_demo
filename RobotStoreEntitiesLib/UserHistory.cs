using System;

namespace RobotStoreEntitiesLib
{
    public class UserHistory
    {
        public int UserHistoryID { get; set; }
        public User User;
        public string Name;
        public string Email;
        public string Password;
        public Grade Grade;
        public bool IsDelete;
        public DateTime AddDate;
        public User AddBy;

    }
}