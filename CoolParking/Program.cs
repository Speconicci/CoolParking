using System;
using CoolParking.Models;
using CoolParking.Services;

namespace CoolParking
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingService parkingService = new ParkingService();
            parkingService.AddVehicle(new Vehicle(VehicleType.Bus, 300m));
            parkingService.AddVehicle(new Vehicle(VehicleType.Truck, 500m));
            Console.ReadKey();
        }
    }
}
