namespace SkyViewC3DB.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }

        public int PermissionTypeId { get; set; }
        public PermissionType PermissionType { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}