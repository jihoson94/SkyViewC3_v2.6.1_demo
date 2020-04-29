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
        Task<UserPermission> RetrieveUserPermissionAsync(string id);
        Task<User> AddUserPermissionAsync(Permission permission);
        Task<bool?> RemoveUserPermissionAsync(Permission permission);
        Task<User> UpdateGradeAsync(Grade grade);
    }
}