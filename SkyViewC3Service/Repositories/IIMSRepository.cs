using System.Threading.Tasks;
using RobotStoreEntitiesLib;
using RobotStoreContextLib;
using System.Collections.Generic;

namespace SkyViewC3Service.Repositories
{
    public interface IIMSRepository
    {
        Task<Rack> CreateRackAsync(string rackID, RackType type);
        Task<Box> CreateBoxAsync(string boxID, BoxType type);
        Task<Vial> CreateVialAsync(string vialID, VialType type);

        Task<Box> InputBoxInRackAsync(Box box, Rack toRack, int slot);
        Task<Vial> InputVialInBoxAsync(Vial vial, Box toBox, int position);

        Task<Box> OutputBoxInRackAsync(Box box, Rack fromRack);
        Task<Vial> OutputVialInBoxAsync(Vial vial, Box fromBox);

        Task<IEnumerable<Box>> RetrieveAllBoxFromRack(Rack rack);
        Task<IEnumerable<Vial>> RetrieveAllVialFromBox(Box box);
        Task<IEnumerable<Rack>> RetrieveAllRack();

        Task<Rack> RetrieveRack(string id);
        Task<Box> RetrieveBox(string id);
        Task<Vial> RetrieveVial(string id);


        Task<BoxHistory> AddBoxHistory(Box box, User byUser);
        Task<VialHistory> AddVialHistory(Vial vial, User byUser);


    }
}