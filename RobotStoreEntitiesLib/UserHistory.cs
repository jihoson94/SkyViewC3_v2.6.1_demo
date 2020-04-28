using System;

namespace RobotStoreEntitiesLib
{
    public class UserHistory
    {
        public int UserHistoryID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string GradeID { get; set; }
        public Grade Grade { get; set; }
        public bool IsDelete { get; set; }
        public DateTime AddDate { get; set; } = DateTime.Now;
        public User AddBy { get; set; }

    }
}