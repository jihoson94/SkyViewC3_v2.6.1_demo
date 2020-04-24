using System;
using System.Collections.Generic;

namespace SkyViewC3DB.Models
{
    public class User
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool IsDelete { get; set; }
    }
}