

using SkyViewC3Service.Repositories;
using RobotStoreContextLib;
using RobotStoreEntitiesLib;

using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SkyViewC3Service.Test.Repositories
{
    public class TestTankRepository : IDisposable
    {
        private TankRepository repository;
        private SqliteConnection connection;
        private RobotStoreContext context;
        // Setup
        public TestTankRepository()
        {
            connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<RobotStoreContext>()
                                    .UseSqlite(connection).Options;
            context = new RobotStoreContext(options);
            context.Database.EnsureCreated();
            repository = new TankRepository(context);
        }

        // TearDown
        public void Dispose()
        {
            connection.Close();
        }

        [Fact]
        public async Task TestCreateTankConfigAsync()
        {
            var newTankConfig1 = new TankConfig()
            {
                ConfigName = "Bypass Time",
                ConfigValue = "30"
            };
            var newTankConfig2 = new TankConfig()
            {
                ConfigName = "Supply Timeout",
                ConfigValue = "60"
            };

            await repository.CreateTankConfigAsync(newTankConfig1);
            var tankConfigs = await repository.RetrieveAllTankConfigs();

            Assert.True(tankConfigs.Count() == 1);

            await repository.CreateTankConfigAsync(newTankConfig2);
            tankConfigs = await repository.RetrieveAllTankConfigs();

            Assert.True(tankConfigs.Count() == 2);
        }

        [Fact]
        public async Task TestUpdateTankConfigAsync()
        {
            var newTankConfig1 = new TankConfig()
            {
                ConfigName = "Bypass Time",
                ConfigValue = "30"
            };
            var newTankConfig2 = new TankConfig()
            {
                ConfigName = "Supply Timeout",
                ConfigValue = "60"
            };
            var newTankConfig3 = new TankConfig()
            {
                ConfigName = "Supply Timeout",
                ConfigValue = "30"
            };

            await repository.UpdateTankConfigAsync(newTankConfig1);
            var tankConfigs = await repository.RetrieveAllTankConfigs();

            Assert.True(tankConfigs.Count() == 1);

            await repository.UpdateTankConfigAsync(newTankConfig2);
            tankConfigs = await repository.RetrieveAllTankConfigs();

            Assert.True(tankConfigs.Count() == 2);

            await repository.UpdateTankConfigAsync(newTankConfig3);
            tankConfigs = await repository.RetrieveAllTankConfigs();
            var foundConfig = tankConfigs.FirstOrDefault(config => config.ConfigName == newTankConfig3.ConfigName);

            Assert.True(tankConfigs.Count() == 2);
            Assert.True(foundConfig != null);
            Assert.True(foundConfig.ConfigName == newTankConfig3.ConfigName);
            Assert.True(foundConfig.ConfigValue == newTankConfig3.ConfigValue);
        }

        [Fact]
        public async Task TestAddDeleteCalibrationAsync()
        {
            var newCalReference1 = 30;
            var newCalReference2 = 40;

            var newLN2Calibration1 = new LN2LevelCalibration()
            {
                Reference = newCalReference1,
                Value = 30
            };
            var newLN2Calibration2 = new LN2LevelCalibration()
            {
                Reference = newCalReference2,
                Value = 40
            };
            var newTopTempCalibration1 = new TopTempCalibration()
            {
                Reference = newCalReference1,
                Value = 30
            };
            var newTopTempCalibration2 = new TopTempCalibration()
            {
                Reference = newCalReference2,
                Value = 40
            };
            var newBotTempCalibration1 = new BottomTempCalibration()
            {
                Reference = newCalReference1,
                Value = 30
            };
            var newBotTempCalibration2 = new BottomTempCalibration()
            {
                Reference = newCalReference2,
                Value = 40
            };
            var newByPassTempCalibration1 = new ByPassTempCalibration()
            {
                Reference = newCalReference1,
                Value = 30
            };
            var newByPassTempCalibration2 = new ByPassTempCalibration()
            {
                Reference = newCalReference2,
                Value = 40
            };

            await repository.AddLN2LevelCalibrationAsync(newLN2Calibration1);
            await repository.AddTopTempCalibrationAsync(newTopTempCalibration1);
            await repository.AddBottomTempCalibrationAsync(newBotTempCalibration1);
            await repository.AddByPassTempCalibrationAsync(newByPassTempCalibration1);
            var LN2Calibrations = await repository.RetrieveAllLN2LevelCalibrations();
            var topTempCalibrations = await repository.RetrieveAllTopTempCalibrations();
            var botTempCalibrations = await repository.RetrieveAllBottomTempCalibrations();
            var byPassTempCalibrations = await repository.RetrieveAllByPassTempCalibrations();

            Assert.True(LN2Calibrations.Count() == 1);
            Assert.True(topTempCalibrations.Count() == 1);
            Assert.True(botTempCalibrations.Count() == 1);
            Assert.True(byPassTempCalibrations.Count() == 1);

            await repository.AddLN2LevelCalibrationAsync(newLN2Calibration2);
            await repository.AddTopTempCalibrationAsync(newTopTempCalibration2);
            await repository.AddBottomTempCalibrationAsync(newBotTempCalibration2);
            await repository.AddByPassTempCalibrationAsync(newByPassTempCalibration2);
            LN2Calibrations = await repository.RetrieveAllLN2LevelCalibrations();
            topTempCalibrations = await repository.RetrieveAllTopTempCalibrations();
            botTempCalibrations = await repository.RetrieveAllBottomTempCalibrations();
            byPassTempCalibrations = await repository.RetrieveAllByPassTempCalibrations();

            Assert.True(LN2Calibrations.Count() == 2);
            Assert.True(topTempCalibrations.Count() == 2);
            Assert.True(botTempCalibrations.Count() == 2);
            Assert.True(byPassTempCalibrations.Count() == 2);

            await repository.RemoveLN2LevelCalibration(newCalReference1);
            await repository.RemoveTopTempCalibration(newCalReference1);
            await repository.RemoveBottomTempCalibration(newCalReference1);
            await repository.RemoveByPassTempCalibration(newCalReference1);
            LN2Calibrations = await repository.RetrieveAllLN2LevelCalibrations();
            topTempCalibrations = await repository.RetrieveAllTopTempCalibrations();
            botTempCalibrations = await repository.RetrieveAllBottomTempCalibrations();
            byPassTempCalibrations = await repository.RetrieveAllByPassTempCalibrations();

            Assert.True(LN2Calibrations.Count() == 1);
            Assert.True(topTempCalibrations.Count() == 1);
            Assert.True(botTempCalibrations.Count() == 1);
            Assert.True(byPassTempCalibrations.Count() == 1);

            await repository.RemoveLN2LevelCalibration(newCalReference1);
            await repository.RemoveTopTempCalibration(newCalReference1);
            await repository.RemoveBottomTempCalibration(newCalReference1);
            await repository.RemoveByPassTempCalibration(newCalReference1);
            LN2Calibrations = await repository.RetrieveAllLN2LevelCalibrations();
            topTempCalibrations = await repository.RetrieveAllTopTempCalibrations();
            botTempCalibrations = await repository.RetrieveAllBottomTempCalibrations();
            byPassTempCalibrations = await repository.RetrieveAllByPassTempCalibrations();

            Assert.True(LN2Calibrations.Count() == 1);
            Assert.True(topTempCalibrations.Count() == 1);
            Assert.True(botTempCalibrations.Count() == 1);
            Assert.True(byPassTempCalibrations.Count() == 1);

            await repository.RemoveLN2LevelCalibration(newCalReference2);
            await repository.RemoveTopTempCalibration(newCalReference2);
            await repository.RemoveBottomTempCalibration(newCalReference2);
            await repository.RemoveByPassTempCalibration(newCalReference2);
            LN2Calibrations = await repository.RetrieveAllLN2LevelCalibrations();
            topTempCalibrations = await repository.RetrieveAllTopTempCalibrations();
            botTempCalibrations = await repository.RetrieveAllBottomTempCalibrations();
            byPassTempCalibrations = await repository.RetrieveAllByPassTempCalibrations();

            Assert.True(LN2Calibrations.Count() == 0);
            Assert.True(topTempCalibrations.Count() == 0);
            Assert.True(botTempCalibrations.Count() == 0);
            Assert.True(byPassTempCalibrations.Count() == 0);
        }
    }
}