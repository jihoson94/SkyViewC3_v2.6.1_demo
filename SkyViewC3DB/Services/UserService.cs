
using SkyViewC3DB.Contexts;
using SkyViewC3DB.Models;
using System.Collections.Generic;
using System.Linq;

namespace SkyViewC3DB.Services
{
    public class UserService
    {
        private IMSContext _context;

        public UserService(IMSContext context)
        {
            _context = context;
        }

        public void AddUser(string Name)
        {
            var newUesr = new User();
            newUesr.Name = Name;
            newUesr.Grade = new Grade()
            {
                GradeType = new GradeType()
                {
                    GradeTypeName = "Developer"
                }
            };
            _context.Users.Add(newUesr);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            var query = from u in _context.Users orderby u.Name select u;
            return query.ToList();
        }
    }
}