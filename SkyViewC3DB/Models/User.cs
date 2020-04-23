using System;
using System.Collections.Generic;

namespace SkyViewC3DB.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public Nullable<int> GradeId { get; set; }
        public Grade Grade { get; set; }

        public ICollection<Permission> Permissions { get; set; }
    }
}