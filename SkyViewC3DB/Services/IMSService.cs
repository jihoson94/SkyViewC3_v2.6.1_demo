using System;
using SkyViewC3DB.Contexts;
using SkyViewC3DB.Models;
using SkyViewC3DB.Services.Exceptions;

using System.Linq;
using System.Collections.Generic;

namespace SkyViewC3DB.Services
{
    public class IMSService
    {
        private IMSContext _context;
        public IMSService(IMSContext context)
        {
            _context = context;
        }

        public List<Vial> GetAllVialsFromBox(Box box)
        {
            //  (BoxID, Position, Time) of Latest BoxAction
            var lastestVialActions = from va in _context.VialActions
                                     group va by new { va.BoxID, va.Position } into g
                                     select new { BoxID = g.Key.BoxID, Position = g.Key.Position, Time = g.Max(va => va.Time) };

            // lastest Vial 
            var vials = from va in _context.VialActions
                        join v in _context.Vials on va.VialID equals v.VialID
                        where lastestVialActions.Contains(new { va.BoxID, va.Position, va.Time })
                        select v;

            return vials.ToList();
        }

        public Rack GetRack(string rackID)
        {
            return _context.Racks.Find(rackID);
        }
        public void AddRack(string rackID, string type)
        {
            if (IsRackExist(rackID))
            {
                throw new RackAlreadyExistException();
            }
            var rack = new Rack();
            rack.RackID = rackID;
            rack.Type = type;
            _context.Racks.Add(rack);
            _context.SaveChanges();
        }
        public bool IsRackExist(string rackID)
        {
            var rack = _context.Racks.Find(rackID);
            if (rack == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void InputBox(Rack rack, Box box, User user, int Position)
        {
            var boxAction = new BoxAction();
            boxAction.Box = box;
            boxAction.Rack = rack;
            boxAction.User = user;
            boxAction.Position = Position;
            boxAction.Time = DateTime.Now;
            boxAction.Action = "in";
            _context.BoxActions.Add(boxAction);
            _context.SaveChanges();

            var vials = GetAllVialsFromBox(box);
            foreach (var vial in vials)
            {
                vial.IsOut = false;
            }
            _context.SaveChanges();

        }
        public void OutputBox(Rack rack, Box box, User user, int Position)
        {

            var boxAction = new BoxAction();
            boxAction.Box = box;
            boxAction.Rack = rack;
            boxAction.User = user;
            boxAction.Position = Position;
            boxAction.Time = DateTime.Now;
            boxAction.Action = "out";
            _context.BoxActions.Add(boxAction);
            _context.SaveChanges();
        }

        public Box GetBox(string BoxID)
        {
            return _context.Boxes.Find(BoxID);
        }
        private void AddBox(string BoxID, string Type, string Name)
        {
            if (IsBoxExist(BoxID))
            {
                throw new BoxAlreadyExistException();
            }
            var newBox = new Box();
            newBox.BoxID = BoxID;
            newBox.Type = Type;
            newBox.Name = Name;
            newBox.IsOut = true;

            _context.Boxes.Add(newBox);
            _context.SaveChanges();
        }
        public bool IsBoxExist(string BoxID)
        {
            var box = _context.Boxes.Find(BoxID);
            if (box == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void AddVial(string vialID, string name, string type)
        {
            if (IsVialExist(vialID))
            {
                throw new VialAlreadyExistException();
            }
            var newVial = new Vial();
            newVial.VialID = vialID;
            newVial.Name = name;
            newVial.Type = type;
            newVial.IsOut = true;
            _context.Vials.Add(newVial);
            _context.SaveChanges();
        }
        public bool IsVialExist(string vialID)
        {
            var vial = _context.Vials.Find(vialID);
            if (vial == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void InputVial(Box box, Vial vial, User user, int position)
        {
            var vialAction = new VialAction();
            vialAction.Box = box;
            vialAction.Vial = vial;
            vialAction.User = user;
            vialAction.Position = position;
            vialAction.Time = DateTime.Now;
            vialAction.Action = "in";
            _context.VialActions.Add(vialAction);
            _context.SaveChanges();
        }
        public void OutputVial(Box box, Vial vial, User user, int position)
        {
            var vialAction = new VialAction();
            vialAction.Box = box;
            vialAction.Vial = vial;
            vialAction.User = user;
            vialAction.Position = position;
            vialAction.Time = DateTime.Now;
            vialAction.Action = "out";
            _context.VialActions.Add(vialAction);
            _context.SaveChanges();
        }
    }
}