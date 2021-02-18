using System.Collections.Generic;
namespace CoolParking.Models
{
    public static class Settings
    {
        public static decimal initialParkingBalance = 0;
        public static int parkingCapacity = 10;
        public static float withdrawIntervalInMillis = 5000f;
        public static float logIntervalInMillis = 12000f;
        public static decimal penaltyMultiplier = 2.5m;
        public static string logFilePath = @"C:\Logs\log.txt";

        public static Dictionary<VehicleType, decimal> vehicleParkingTariff = new Dictionary<VehicleType, decimal>()
        {
            {VehicleType.PassengerCar, 2m},
            {VehicleType.Truck, 5m},
            {VehicleType.Bus, 3.5m},
            {VehicleType.Motorcycle, 1m}
        };
    }
}
