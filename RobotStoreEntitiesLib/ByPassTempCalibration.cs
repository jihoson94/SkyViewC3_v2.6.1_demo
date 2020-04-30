namespace RobotStoreEntitiesLib
{
    public class ByPassTempCalibration : Calibration
    {
        public override double Reference { get; set; }
        public override double Value { get; set; }
    }
}