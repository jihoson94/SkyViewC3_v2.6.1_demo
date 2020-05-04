namespace RobotStoreEntitiesLib
{
    public class Vial
    {
        public string VialID { get; set; }
        public string BoxID { get; set; }
        public Box Box { get; set; }
        public int? Position { get; set; }
        public bool IsOut { get; set; }
        public string VialTypeName { get; set; }
        public VialType VialType { get; set; }
    }
}