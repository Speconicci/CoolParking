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
            parkingService.AddVehicle(new Vehicle(VehicleType.PassengerCar, 10));
            parkingService.AddVehicle(new Vehicle(VehicleType.Bus, 10));
            Console.ReadKey();
        }
    }
}
