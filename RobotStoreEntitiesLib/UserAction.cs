namespace RobotStoreEntitiesLib
{
    public class UserAction
    {
        public int UserActionID { get; set; }
        public User User { get; set; }
        public string Section { get; set; }
        public string Subsection { get; set; }
        public string Message { get; set; }
    }
}
