namespace RobotStoreEntitiesLib
{
    public class UserPermission
    {
        // MultiKey (UserID, PemissionID)
        public string UserID { get; set; }
        public User User { get; set; }
        public string PermissionID { get; set; }
        public Permission Permission { get; set; }
    }
}