using System.Collections.Generic;

namespace RobotStoreEntitiesLib
{
    public class Rack
    {
        public int RackID { get; set; }
        public string RackTypeName { get; set; }
        public RackType RackType { get; set; }
        public ICollection<Box> Boxes { get; set; }
    }
}