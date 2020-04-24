using System;

namespace SkyViewC3DB.Models
{
    public class VialAction
    {
        public int VialActionID { get; set; }
        public string VialID { get; set; }
        public Vial Vial { get; set; }
        public string BoxID { get; set; }
        public Box Box { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
        public DateTime Time { get; set; }

        public int Position { get; set; }
        public string Action { get; set; }
    }
}