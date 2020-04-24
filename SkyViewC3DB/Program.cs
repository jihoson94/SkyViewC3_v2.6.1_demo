using System;
using System.Linq;
using SkyViewC3DB.Contexts;
using SkyViewC3DB.Models;
using SkyViewC3DB.Services;
using SkyViewC3DB.Services.Exceptions;

namespace SkyViewC3DB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctx = new IMSContext())
            {
                var IMSService = new IMSService(ctx);

                #region Add User
                var user = new User();
                user.UserID = "testID";
                user.Name = "tester";
                user.Password = "123";
                user.IsDelete = false;
                user.Email = "test@rnd.re.kr";
                #endregion

                #region Add Racks
                for (var i = 0; i < 12; i++)
                {
                    try
                    {
                        IMSService.AddRack($"{i}", "Normal");
                        Console.WriteLine("Add Rack");
                    }
                    catch (RackAlreadyExistException)
                    {
                        Console.WriteLine($"Warning - {i} Rack is existed.");
                        continue;
                    }
                }
                #endregion

                #region Add Boxes to Rack 1
                Rack rack = IMSService.GetRack("1");

                for (var i = 0; i < 12; i++)
                {
                    var box = new Box();
                    box.BoxID = i.ToString();
                    box.Name = "TEST";
                    box.Type = "10x10";
                    box.IsOut = false;
                    IMSService.InputBox(rack, box, user, i);
                    Console.WriteLine($"Insert Box(ID:{box.BoxID})");
                }
                #endregion


            }
        }

    }
}