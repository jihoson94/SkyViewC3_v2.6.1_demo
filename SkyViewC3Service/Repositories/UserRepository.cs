using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using RobotStoreEntitiesLib;
using RobotStoreContextLib;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using Microsoft.EntityFrameworkCore;

namespace SkyViewC3Service.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly RobotStoreContext db;
        private static ConcurrentDictionary<string, User> userCache;
        public UserRepository(RobotStoreContext db)
        {
            this.db = db;

            if (userCache == null)
            {
                userCache = new ConcurrentDictionary<string, User>(
                    db.Users
                    .Where(u => u.IsDelete == false)
                    .Include(u => u.Grade)
                    .Include(u => u.Permissions)
                    .ToDictionary(c => c.UserID)
                    );
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

        public Task<IEnumerable<User>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<User>>(() => userCache.Values);
        }

        public Task<User> RetrieveAsync(string id)
        {
            return Task.Run<User>(() =>
            {
                id = id.ToUpper();
                User user;
                if (userCache.TryGetValue(id, out user))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            });
        }

        public async Task<bool?> DeleteAsync(string id)
        {
            id = id.ToUpper();
            User u = db.Users.Find(id);
            db.Users.Remove(u);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return userCache.TryRemove(id, out u);
            }
            else
            {
                return null;
            }

        }

        public async Task<User> UpdateAsync(string id, User user)
        {
            id = id.ToUpper();
            user.UserID = user.UserID.ToUpper();
            db.Users.Update(user);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return UpdateCache(id, user);
            }

            return null;
        }

        public Task<IEnumerable<Permission>> RetrieveUserPermissionsAsync(string id)
        {
            return Task.Run<IEnumerable<Permission>>(
                () => db.UserPermissions
                .Where(up => up.UserID == id)
                .Include(up => up.Permission)
                .Select(up => up.Permission)
                );
        }

        public async Task<bool> CheckPermissionAsync(string id, Permission permission)
        {

            var result = await db.UserPermissions.FirstOrDefaultAsync(up => up.UserID == id && up.PermissionID == permission.PermissionID);
            if (result == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task<UserPermission> AddUserPermissionAsync(string userID, Permission permission)
        {
            var result = await db.UserPermissions.FirstOrDefaultAsync(
                up => up.UserID == userID && up.PermissionID == permission.PermissionID);
            if (result != null)
            {
                return null;
            }
            var newUserPermission = new UserPermission();
            newUserPermission.UserID = userID;
            newUserPermission.Permission = permission;
            db.UserPermissions.Add(newUserPermission);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                var user = db.Users.Where(u => u.UserID == userID).Include(u => u.Permissions).Include(u => u.Grade).FirstOrDefault();
                UpdateCache(userID, user);
                return newUserPermission;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool?> RemoveUserPermissionAsync(string userID, Permission permission)
        {
            var willRemovedPermission = await db.UserPermissions.FirstOrDefaultAsync(
                up => up.UserID == userID && up.PermissionID == permission.PermissionID);
            if (willRemovedPermission == null)
            {
                return false;
            }
            db.UserPermissions.Remove(willRemovedPermission);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                var user = db.Users
                    .Where(u => u.UserID == userID)
                    .Include(u => u.Permissions)
                    .Include(u => u.Grade)
                    .FirstOrDefault();

                UpdateCache(userID, user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<User> UpdateGradeAsync(string userID, Grade grade)
        {
            var user = await db.Users.FindAsync(userID);
            if (user == null)
            {
                return null;
            }

            user.Grade = grade;
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                user = db.Users
                    .Where(u => u.UserID == userID)
                    .Include(u => u.Permissions)
                    .Include(u => u.Grade)
                    .FirstOrDefault();

                return UpdateCache(userID, user);
            }
            else
            {
                return null;
            }
        }

        public async Task<UserHistory> AddUserHistoryAsync(User userState, User byUser)
        {

            var userHistory = new UserHistory()
            {
                UserID = userState.UserID,
                Name = userState.Name,
                Email = userState.Email,
                Password = userState.Password,
                GradeID = userState.GradeID,
                IsDelete = userState.IsDelete,
                AddDate = DateTime.Now,
                AddBy = byUser
            };
            db.UserHistories.Add(userHistory);
            var affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return userHistory;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserPermissionHistory> AddUserPemissionHistoryAsync(UserPermission pemissionState, User byUser, bool isDelete)
        {
            var permissionHistory = new UserPermissionHistory()
            {
                UserID = pemissionState.UserID,
                PermissionID = pemissionState.PermissionID,
                Action = isDelete ? "remove" : "add",
                AddBy = byUser,
                AddDate = DateTime.Now
            };
            db.UserPermissionHistories.Add(permissionHistory);
            int affected = await db.SaveChangesAsync();
            if (affected == 1)
            {
                return permissionHistory;
            }
            else
            {
                return null;
            }
        }

    }
}