using System.Collections.Generic;
using System.Threading.Tasks;
using RobotStoreContextLib;
using RobotStoreEntitiesLib;

using System.Linq;
using Microsoft.EntityFrameworkCore;
using SkyViewC3Service.Repositories.IMSRepositoryExceptions;

namespace SkyViewC3Service.Repositories
{

    public class IMSRepository : IIMSRepository
    {
        private RobotStoreContext db;
        public IMSRepository(RobotStoreContext db)
        {
            this.db = db;
        }
        public async Task<Rack> CreateRackAsync(string rackID, RackType type)
        {
            // TODO:is it right here where validation is that check existing rack ??? 

            var rack = await db.Racks.FindAsync(rackID);
            if (rack != null)
                return null;

            rack = new Rack();
            rack.RackID = rackID;
            rack.RackType = type;
            db.Racks.Add(rack);

            var affected = await db.SaveChangesAsync();
            if (affected != 0)
            {
                return rack;
            }

            return null;
        }

        public async Task<Box> CreateBoxAsync(string boxID, BoxType type)
        {
            // TODO:is it right here where validation is that check existing box ??? 

            var box = await db.Boxes.FindAsync(boxID);
            if (box != null)
            {
                return null;
            }
            box = new Box()
            {
                BoxID = boxID,
                RackID = null,
                IsOut = true,
                BoxType = type,
                Slot = null
            };
            db.Boxes.Add(box);

            var affected = await db.SaveChangesAsync();
            if (affected != 0)
            {
                return box;
            }
            return null;
        }

        public async Task<Vial> CreateVialAsync(string vialID, VialType type)
        {
            // TODO:is it right here where validation is that check existing vial ??? 
            var vial = await db.Vials.FindAsync(vialID);
            if (vial != null)
            {
                return null;
            }

            vial = new Vial()
            {
                VialID = vialID,
                BoxID = null,
                IsOut = true,
                VialType = type,
                Position = null
            };
            db.Vials.Add(vial);
            var affected = await db.SaveChangesAsync();
            if (affected != 0)
            {
                return vial;
            }
            return null;

        }

        public async Task<Box> InputBoxInRackAsync(Box inputedBox, Rack toRack, int slot)
        {
            var rack = await db.Racks.Include(r => r.Boxes).SingleOrDefaultAsync(r => r.RackID == toRack.RackID);
            if (rack == null)
            {
                throw new NotFoundRackException($"Not Found Rack ID : {toRack.RackID}");
            }

            var box = await db.Boxes.FindAsync(inputedBox.BoxID);
            if (box == null)
            {
                throw new NotFoundBoxException($"Not Found Box ID : {inputedBox.BoxID}");
            }
            if (box.IsOut == false)
            {
                throw new NotMoveBoxException($"Box is already existed in Tank.");
            }

            var existedBoxinSameSlot = db.Boxes.Where(b => b.RackID == rack.RackID && b.Slot == slot).FirstOrDefault();
            if (existedBoxinSameSlot != null)
            {
                throw new AlreadyOccupiedInSlotException($"Another Box is already existed in {slot} slot of {rack.RackID}.");
            }

            box.RackID = toRack.RackID;
            box.Slot = slot;
            box.IsOut = false;
            toRack.Boxes.Add(box);

            int affected = await db.SaveChangesAsync();
            if (affected != 0)
            {
                return box;
            }
            else
            {
                return null;
            }
        }

        public Task<Vial> InputVialInBoxAsync(Vial vial, Box toBox, int position)
        {
            throw new System.NotImplementedException();

        }

        public Task<Box> OutputBoxInRackAsync(Box box, Rack fromRack)
        {
            throw new System.NotImplementedException();
        }

        public Task<Vial> OutputVialInBoxAsync(Vial vial, Box fromBox)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Box>> RetrieveAllBoxFromRack(Rack rack)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Rack>> RetrieveAllRack()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Vial>> RetrieveAllVialFromBox(Box box)
        {
            throw new System.NotImplementedException();
        }

        public Task<BoxHistory> AddBoxHistory(Box box, User byUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<VialHistory> AddVialHistory(Vial vial, User byUser)
        {
            throw new System.NotImplementedException();
        }

        public Task<Rack> RetrieveRack(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Box> RetrieveBox(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Vial> RetrieveVial(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}