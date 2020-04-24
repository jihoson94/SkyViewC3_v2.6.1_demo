
using SkyViewC3DB.Contexts;
using SkyViewC3DB.Models;
using SkyViewC3DB.Services.Exceptions;
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

        public bool IsUserExist(string UserID)
        {
            var user = _context.Users.Find(UserID);
            if (user == null)
            {
                return false;
            }
            return true;
        }

        public void AddUser(
            string UserId,
            string Password,
            string Email,
            string Name
            )
        {
            if (IsUserExist(UserId))
            {
                throw new UserAlreadyExistException(nameof(AddUser));
            }

            var user = new User();
            user.UserID = UserId;
            user.Password = Password;
            user.Email = Email;
            user.Name = Name;
            user.IsDelete = false;

            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}