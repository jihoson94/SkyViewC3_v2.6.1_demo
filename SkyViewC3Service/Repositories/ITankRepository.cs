using System.Collections.Generic;
using System.Threading.Tasks;
using RobotStoreEntitiesLib;
namespace SkyViewC3Service.Repositories
{
    public interface ITankRepository
    {
        Task<bool> CreateTankConfigAsync(TankConfig tankConfig);
        Task<bool> UpdateTankConfigAsync(TankConfig tankConfig);

        Task<TankConfig> RetrieveTankConfig(string configName);

        Task<IEnumerable<TankConfig>> RetrieveAllTankConfigs();

        Task<LN2LevelCalibration> AddLN2LevelCalibrationAsync(LN2LevelCalibration calibration);

        Task<LN2LevelCalibration> RemoveLN2LevelCalibration(double reference);

        Task<IEnumerable<LN2LevelCalibration>> RetrieveAllLN2LevelCalibrations();

        Task<TopTempCalibration> AddTopTempCalibrationAsync(TopTempCalibration calibration);

        Task<TopTempCalibration> RemoveTopTempCalibration(double reference);

        Task<IEnumerable<TopTempCalibration>> RetrieveAllTopTempCalibrations();

        Task<BottomTempCalibration> AddBottomTempCalibrationAsync(BottomTempCalibration calibration);

        Task<BottomTempCalibration> RemoveBottomTempCalibration(double reference);

        Task<IEnumerable<BottomTempCalibration>> RetrieveAllBottomTempCalibrations();

        Task<ByPassTempCalibration> AddByPassTempCalibrationAsync(ByPassTempCalibration calibration);

        Task<ByPassTempCalibration> RemoveByPassTempCalibration(double reference);

        Task<IEnumerable<ByPassTempCalibration>> RetrieveAllByPassTempCalibrations();
    }
}