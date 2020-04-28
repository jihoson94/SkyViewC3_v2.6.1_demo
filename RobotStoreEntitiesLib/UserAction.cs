namespace RobotStoreEntitiesLib
{
    // Reference about User Actions
    // https://docs.google.com/document/d/1wcRvBzhEZUeMB1GBsFsWnqpeVaXfGVfRETae3-xoU4g/edit
    // Writer: Raphael Kim
    public class UserAction
    {
        public int UserActionID { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public string Section { get; set; }
        public string Subsection { get; set; }
        public string Message { get; set; }
    }
}
