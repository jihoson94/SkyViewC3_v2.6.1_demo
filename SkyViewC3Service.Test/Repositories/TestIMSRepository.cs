using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RobotStoreContextLib;
using RobotStoreEntitiesLib;
using SkyViewC3Service.Repositories;
using SkyViewC3Service.Repositories.IMSRepositoryExceptions;
using Xunit;

namespace SkyViewC3Service.Test.Repositories
{
    public class TestIMSRepository : IDisposable
    {
        private IIMSRepository repository;
        private SqliteConnection connection;
        private RobotStoreContext context;
        // Setup
        public TestIMSRepository()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<RobotStoreContext>()
                                    .UseSqlite(connection).Options;
            context = new RobotStoreContext(options);
            context.Database.EnsureCreated();
            repository = new IMSRepository(context);
        }

        // TearDown
        public void Dispose()
        {
            connection.Close();
        }

        [Fact]

        public async Task TestCreateRackAsync()
        {
            var newRackType = new RackType()
            {
                Name = "TestRackType",
                Capacity = 10
            };
            context.RackType.Add(newRackType);
            context.SaveChanges();
            var createdRack = await repository.CreateRackAsync(rackID: "TestRack", type: newRackType);
            Assert.NotNull(createdRack);
            Assert.True(createdRack.RackID == "TestRack");
            Assert.True(createdRack.RackTypeName == newRackType.Name);
            Assert.Null(createdRack.Boxes);

            // create id duplicate rack.
            createdRack = await repository.CreateRackAsync(rackID: "TestRack", type: newRackType);
            Assert.Null(createdRack);
        }

        [Fact]
        public async Task TestCreateBoxAsync()
        {
            var newBoxType = new BoxType()
            {
                Name = "TestBoxType",
                Capacity = 12
            };
            context.BoxTypes.Add(newBoxType);
            context.SaveChanges();
            var createdBox = await repository.CreateBoxAsync(boxID: "TestVialBox", type: newBoxType);
            Assert.NotNull(createdBox);
            Assert.True(createdBox.BoxID == "TestVialBox");
            Assert.True(createdBox.BoxTypeName == newBoxType.Name);
            Assert.Null(createdBox.Vials);

            createdBox = await repository.CreateBoxAsync(boxID: "TestVialBox", type: newBoxType);
            Assert.Null(createdBox);
        }

        [Fact]
        public async Task TestCreateVialAsync()
        {
            var newVialType = new VialType()
            {
                Name = "TestVialType",
            };
            context.VialTypes.Add(newVialType);
            context.SaveChanges();
            var createdVial = await repository.CreateVialAsync(vialID: "TestVial", type: newVialType);
            Assert.NotNull(createdVial);
            Assert.True(createdVial.VialID == "TestVial");
            Assert.True(createdVial.VialTypeName == newVialType.Name);

            createdVial = await repository.CreateVialAsync(vialID: "TestVial", type: newVialType);
            Assert.Null(createdVial);

        }

        [Fact]
        public async Task TestInputBoxInRackAsync()
        {
            var newRackType = new RackType()
            {
                Name = "TestRackType",
                Capacity = 10
            };
            var newBoxType = new BoxType() { Name = "TestBoxType", Capacity = 12 };
            var createdRack = await repository.CreateRackAsync(rackID: "TestRack", type: newRackType);
            var createdBox = await repository.CreateBoxAsync(boxID: "TestBox", type: newBoxType);

            var inputedBox = await repository.InputBoxInRackAsync(box: createdBox, toRack: createdRack, slot: 1);
            Assert.NotNull(inputedBox);
            Assert.True(inputedBox.IsOut == false);
            Assert.True(inputedBox.RackID == createdRack.RackID);
            Assert.True(inputedBox.Slot == 1);

            // with not Existing Rack.
            try
            {
                var notExistedRack = new Rack() { RackID = "noExistedRack" };
                inputedBox = await repository.InputBoxInRackAsync(box: createdBox, toRack: notExistedRack, slot: 1);
            }
            catch (Exception e)
            {
                Assert.True(e is NotFoundRackException);
            }

            // with box in tank.
            try
            {
                // Already Box is Existed in other rack.
                inputedBox = await repository.InputBoxInRackAsync(box: createdBox, toRack: createdRack, slot: 1);
            }
            catch (Exception e)
            {
                Assert.True(e is NotMoveBoxException);
            }

            // with not existing box
            try
            {
                var notExistedBox = new Box() { BoxID = "noExistedBox" };
                inputedBox = await repository.InputBoxInRackAsync(box: notExistedBox, toRack: createdRack, slot: 1);

            }
            catch (Exception e)
            {
                Assert.True(e is NotFoundBoxException);
            }

            // this slot is occupied!
            try
            {
                var newBox = await repository.CreateBoxAsync(boxID: "ExistedBox001", type: newBoxType);
                inputedBox = await repository.InputBoxInRackAsync(box: newBox, toRack: createdRack, slot: 1);
            }
            catch (Exception e)
            {
                Assert.True(e is AlreadyOccupiedInSlotException);
            }
        }
    }
}