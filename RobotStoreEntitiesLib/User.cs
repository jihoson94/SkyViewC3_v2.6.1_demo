using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RobotStoreEntitiesLib
{
    public class User
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  // encrypted pw
        public bool IsDelete { get; set; } = false;

        public string GradeID { get; set; }
        public Grade Grade { get; set; }
        public ICollection<UserPermission> Permissions { get; set; }



    }
}