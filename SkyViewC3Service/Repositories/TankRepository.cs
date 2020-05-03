using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using RobotStoreEntitiesLib;
using RobotStoreContextLib;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace SkyViewC3Service.Repositories
{
    public class TankRepository : ITankRepository
    {
        private RobotStoreContext db;
        public TankRepository(RobotStoreContext db)
        {
            this.db = db;
        }

        #region TankConfig
        public async Task<TankConfig> CreateTankConfigAsync(TankConfig tankConfig)
        {
            // TODO: Do we need Create?
            await UpdateTankConfigAsync(tankConfig);

            return null;
        }

        public async Task<TankConfig> UpdateTankConfigAsync(TankConfig tankConfig)
        {
            TankConfig[] tankConfigs = db.TankConfigs
                .Where(tc => tc.ConfigName == tankConfig.ConfigName).ToArray();

            if (tankConfigs.Length > 0)
                tankConfigs[0].ConfigValue =  tankConfig.ConfigValue;
            else
                await db.TankConfigs.AddAsync(tankConfig);

            await db.SaveChangesAsync();

            return null;
        }

        public Task<IEnumerable<TankConfig>> RetrieveAllTankConfigs()
        {
            return Task.Run<IEnumerable<TankConfig>>(() => db.TankConfigs.ToArray());
        }
        #endregion

        #region LN2LevelCalibration
        public async Task<LN2LevelCalibration> AddLN2LevelCalibrationAsync(LN2LevelCalibration calibration)
        {
            await AddCalibration(db.LN2LevelCalibrations, calibration);

            return null;
        }

        public async Task<LN2LevelCalibration> RemoveLN2LevelCalibration(double reference)
        {
            await RemoveCalibration(db.LN2LevelCalibrations, reference);

            return null;
        }

        public Task<IEnumerable<LN2LevelCalibration>> RetrieveAllLN2LevelCalibrations()
        {
            return Task.Run<IEnumerable<LN2LevelCalibration>>(() => db.LN2LevelCalibrations.ToArray());
        }
        #endregion

        #region TopTempCalibration
        public async Task<TopTempCalibration> AddTopTempCalibrationAsync(TopTempCalibration calibration)
        {
            await AddCalibration(db.TopTempCalibrations, calibration);

            return null;
        }

        public async Task<TopTempCalibration> RemoveTopTempCalibration(double reference)
        {
            await RemoveCalibration(db.TopTempCalibrations, reference);

            return null;
        }

        public Task<IEnumerable<TopTempCalibration>> RetrieveAllTopTempCalibrations()
        {
            return Task.Run<IEnumerable<TopTempCalibration>>(() => db.TopTempCalibrations.ToArray());
        }
        #endregion

        #region BottomTempCalibration
        public async Task<BottomTempCalibration> AddBottomTempCalibrationAsync(BottomTempCalibration calibration)
        {
            await AddCalibration(db.BottomTempCalibrations, calibration);

            return null;
        }

        public async Task<BottomTempCalibration> RemoveBottomTempCalibration(double reference)
        {
            await RemoveCalibration(db.BottomTempCalibrations, reference);

            return null;
        }

        public Task<IEnumerable<BottomTempCalibration>> RetrieveAllBottomTempCalibrations()
        {
            return Task.Run<IEnumerable<BottomTempCalibration>>(() => db.BottomTempCalibrations.ToArray());
        }
        #endregion

        #region ByPassTempCalibration
        public async Task<ByPassTempCalibration> AddByPassTempCalibrationAsync(ByPassTempCalibration calibration)
        {
            await AddCalibration(db.ByPassTempCalibrations, calibration);

            return null;
        }

        public async Task<ByPassTempCalibration> RemoveByPassTempCalibration(double reference)
        {
            await RemoveCalibration(db.ByPassTempCalibrations, reference);

            return null;
        }

        public Task<IEnumerable<ByPassTempCalibration>> RetrieveAllByPassTempCalibrations()
        {
            return Task.Run<IEnumerable<ByPassTempCalibration>>(() => db.ByPassTempCalibrations.ToArray());
        }
        #endregion

        #region Helper Functions
        private async Task<T> AddCalibration<T>(DbSet<T> dbSet, T calibration) where T : Calibration
        {
            T[] calibrations = dbSet.Where(cal => cal.Reference == calibration.Reference).ToArray();
            
            if (calibrations.Length > 0)
                calibrations[0].Value = calibration.Value;
            else
                await dbSet.AddAsync(calibration);
            
            await db.SaveChangesAsync();

            return null;
        }

        private async Task<T> RemoveCalibration<T>(DbSet<T> dbSet, double reference) where T : Calibration
        {
            T[] calibrations = dbSet.Where(cal => cal.Reference == reference).ToArray();
            
            if (calibrations.Length > 0)
                dbSet.Remove(calibrations[0]);
                await db.SaveChangesAsync();

            return null;
        }
        #endregion
    }
}