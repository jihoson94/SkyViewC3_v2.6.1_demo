using System.Collections.Generic;
using System.Threading.Tasks;
using RobotStoreEntitiesLib;
namespace SkyViewC3Service.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> RetrieveAllSync();

        Task<User> RetrieveAsync(string id);
        Task<User> UpdateAsync(string id, User user);

        Task<bool?> DeleteAsync(string id);
    }
}