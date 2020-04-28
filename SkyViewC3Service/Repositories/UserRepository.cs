using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using RobotStoreEntitiesLib;
using RobotStoreContextLib;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SkyViewC3Service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private RobotStoreContext db;
        private static ConcurrentDictionary<string, User> userCache;
        public UserRepository(RobotStoreContext db)
        {
            this.db = db;

            if (userCache == null)
            {
                userCache = new ConcurrentDictionary<string, User>(db.Users.ToDictionary(c => c.UserID));
            }
        }

        private User UpdateCache(string id, User user)
        {
            User old;
            if (userCache.TryGetValue(id, out old))
            {
                if (userCache.TryUpdate(id, user, old))
                {
                    return user;
                }
            }
            return null;
        }

        public async Task<User> CreateAsync(User newUser)
        {
            newUser.UserID = newUser.UserID.ToUpper();
            EntityEntry<User> added = await db.Users.AddAsync(newUser);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return userCache.AddOrUpdate(newUser.UserID, newUser, UpdateCache);
            }
            else
            {
                return null;
            }
        }

        public Task<bool?> DeleteAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<User>> RetrieveAllSync()
        {
            throw new System.NotImplementedException();
        }

        public Task<User> RetrieveAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> UpdateAsync(string id, User user)
        {
            throw new System.NotImplementedException();
        }
    }
}