using System.ComponentModel.DataAnnotations;
namespace RobotStoreEntitiesLib
{
    public class Permission
    {
        [Key]
        public string Name { get; set; }
    }
}