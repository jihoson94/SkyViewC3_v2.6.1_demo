namespace RobotStoreEntitiesLib
{
    public class SpaceOwnership
    {
        public int Slot { get; set; }
        public int RackID { get; set; }
        public Rack Rack { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
    }
}