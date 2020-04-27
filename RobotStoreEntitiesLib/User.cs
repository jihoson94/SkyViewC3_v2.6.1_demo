using System.Collections.Generic;
namespace RobotStoreEntitiesLib
{
    public class User
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }  // encrypted pw

        public bool IsDelete { get; set; }

        public Grade Grade { get; set; }

        public ICollection<UserPermission> Permissions { get; set; }
    }
}