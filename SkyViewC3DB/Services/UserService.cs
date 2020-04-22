
using SkyViewC3DB.Contexts;
using SkyViewC3DB.Models;
using System;
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

        public void AddUser(string name, string password, string email)
        {
            var newUser = new User();
            newUser.Password = password;
            newUser.Email = email;
            newUser.Name = name;
            newUser.Created = DateTime.Today;
            newUser.Grade = new Grade()
            {
                GradeType = new GradeType()
                {
                    GradeTypeName = "Developer"
                }
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public bool Login(string id, string password)
        {
            return false;
        }

        public List<User> GetAllUsers()
        {
            var query = from u in _context.Users orderby u.Name select u;
            return query.ToList();
        }
    }
}