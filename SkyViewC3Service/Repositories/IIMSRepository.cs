using System.Threading.Tasks;
using RobotStoreEntitiesLib;
using RobotStoreContextLib;
using System.Collections.Generic;

namespace SkyViewC3Service.Repositories
{
    public interface IIMSRepository
    {
        Task<Rack> CreateRackAsync(int rackID);
        Task<Box> CreateBoxAsync(string boxID, BoxType type);
        Task<Vial> CreateVialAsync(string VialID, VialType type);

        Task<Box> InputBoxInRackAsync(Box box, Rack toRack, int slot);
        Task<Vial> InputVialInBoxAsync(Vial vial, Box toBox, int position);

        Task<Box> OutputBoxInRackAsync(Box box, Rack fromRack);
        Task<Vial> OutputVialInBoxAsync(Vial vial, Box fromBox);

        Task<IEnumerable<Box>> RetrieveAllBoxFromRack(Rack rack);
        Task<IEnumerable<Vial>> RetrieveAllVialFromBox(Box box);
        Task<IEnumerable<Rack>> RetrieveAllRack();


    }
}