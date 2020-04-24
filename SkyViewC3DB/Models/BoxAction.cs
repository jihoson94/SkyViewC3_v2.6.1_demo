using System;

namespace SkyViewC3DB.Models
{
    public class BoxAction
    {
        public int BoxActionID { get; set; }

        public string BoxID { get; set; }
        public Box Box { get; set; }

        public string UserID { get; set; }
        public User User { get; set; }

        public string RackID { get; set; }
        public Rack Rack { get; set; }

        public DateTime Time { get; set; }
        public int Position { get; set; }
        public string Action { get; set; }
    }


}