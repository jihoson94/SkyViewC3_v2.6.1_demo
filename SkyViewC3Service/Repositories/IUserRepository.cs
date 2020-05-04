using System.Collections.Generic;
using System.Threading.Tasks;
using RobotStoreEntitiesLib;
namespace SkyViewC3Service.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(string id, User user);
        Task<bool?> DeleteAsync(string id);
        Task<IEnumerable<User>> RetrieveAllAsync();
        Task<User> RetrieveAsync(string id);
        Task<IEnumerable<Permission>> RetrieveUserPermissionsAsync(string id);
        Task<bool> CheckPermissionAsync(string id, Permission permission);
        Task<UserPermission> AddUserPermissionAsync(string id, Permission permission);
        Task<bool?> RemoveUserPermissionAsync(string id, Permission permission);
        Task<User> UpdateGradeAsync(string id, Grade grade);
        Task<bool?> AddUserHistory(User userState, User byUser);
        Task<bool?> AddUserPemissionHistory(UserPermission pemissionState, User byUser, bool isDelete);
    }
}