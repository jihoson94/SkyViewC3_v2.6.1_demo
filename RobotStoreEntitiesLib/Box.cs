using System.Collections.Generic;

namespace RobotStoreEntitiesLib
{
    public class Box
    {
        public string BoxID { get; set; }
        public string RackID { get; set; }
        public Rack Rack { get; set; }
        public int? Slot { get; set; }
        public bool IsOut { get; set; }
        public string BoxTypeName { get; set; }
        public BoxType BoxType { get; set; }

        public ICollection<Vial> Vials { get; set; }
    }
}