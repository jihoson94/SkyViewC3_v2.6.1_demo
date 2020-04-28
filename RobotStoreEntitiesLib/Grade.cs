using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RobotStoreEntitiesLib
{
    public class Grade
    {
        public string GradeID { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<GradeInitialPermission> Permissions { get; set; }
    }
}