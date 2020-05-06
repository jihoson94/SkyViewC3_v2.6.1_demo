using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RobotStoreEntitiesLib
{
    public class Grade
    {
        public string GradeID { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<GradeInitialPermission> Permissions { get; set; }
        // override object.Equals
        public override bool Equals(object obj)
        {
            //
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return (obj as Grade).GradeID == GradeID;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}