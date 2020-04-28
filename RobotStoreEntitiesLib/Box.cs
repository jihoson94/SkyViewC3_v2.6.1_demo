namespace RobotStoreEntitiesLib
{
    public class Box
    {
        public string BoxID { get; set; }
        public int RackID { get; set; }
        public Rack Rack { get; set; }
        public int Slot { get; set; }
        public bool IsOut { get; set; }
        public BoxType Type { get; set; }
    }
}