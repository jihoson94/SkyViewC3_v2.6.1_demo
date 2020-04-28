namespace RobotStoreEntitiesLib
{
    public class GradeInitialPermission
    {
        public string GradeID { get; set; }
        public Grade Grade { get; set; }
        public string PermissionID { get; set; }
        public Permission Permission { get; set; }
    }
}
