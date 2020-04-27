namespace RobotStoreEntitiesLib
{
    public class GradeInitialPermission
    {
        // Multi PK (Grade, Permission)
        public Grade Grade { get; set; }
        public Permission Permission { get; set; }
    }
}