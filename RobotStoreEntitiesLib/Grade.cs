using System.ComponentModel.DataAnnotations;

namespace RobotStoreEntitiesLib
{
    public class Grade
    {
        [Key]
        public string Name { get; set; }
    }
}