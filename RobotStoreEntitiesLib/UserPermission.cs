namespace RobotStoreEntitiesLib
{
    public class UserPermission
    {
        public int UserPermissionID { get; set; }
        public User User { get; set; }
        public Permission Permission { get; set; }
    }
}