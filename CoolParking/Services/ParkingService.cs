using System;
using System.Timers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using CoolParking.Interfaces;
using CoolParking.Models;

namespace CoolParking.Services
{
    public class ParkingService
    {
        private Parking parking;
        private TimerService withdrawTimer;
        private TimerService logTimer;
        private LogService logService;
        List<TransactionInfo> currentTransactionInfo;

        public ParkingService()
        {
            Parking.GetInstance();
            this.parking = Parking.instance;
            this.logService = new LogService(Settings.logFilePath);
            this.withdrawTimer = new TimerService(Settings.withdrawIntervalInMillis, RegularWithdraw);
            this.logTimer = new TimerService(Settings.logIntervalInMillis, LogCurrentTransactions);
            this.currentTransactionInfo = new List<TransactionInfo>();
            withdrawTimer.Start();
            logTimer.Start();
        }

        public void AddVehicle(Vehicle vehicle)
        {
            foreach (Vehicle v in parking.vehicles)
            {
                if (v.Id == vehicle.Id) throw new ArgumentException();
            }
            parking.vehicles?.Add(vehicle);
        }

        public void Dispose()
        {
            withdrawTimer.Dispose();
            logTimer.Dispose();
        }

        public decimal GetBalance()
        {
            return parking.Balance;
        }

        public int GetCapacity()
        {
            return parking.vehicles.Capacity;
        }

        public int GetFreePlaces()
        {
            return GetCapacity() - parking.vehicles.Count;
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return currentTransactionInfo?.ToArray();
        }

        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            ReadOnlyCollection<Vehicle> vehicles = new ReadOnlyCollection<Vehicle>(parking.vehicles);
            return vehicles;
        }

        public string ReadFromLog()
        {
            StreamReader streamReader = new StreamReader(logService.LogPath);
            return streamReader.ReadToEnd();
        }

        public void RemoveVehicle(string vehicleId)
        {
            parking.vehicles.Remove(FindVehicleById(vehicleId));
        }

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            FindVehicleById(vehicleId).Balance += sum;
        }

        private void RegularWithdraw(object sender, ElapsedEventArgs e)
        {
            foreach (Vehicle v in parking.vehicles)
            {
                decimal withdrawnSum = CalculateWithdrawnSum(v);
                v.Balance -= withdrawnSum;
                currentTransactionInfo.Add(ComposeTransactionInfo(v, withdrawnSum));
                Console.WriteLine("{1} was withdrawn from the vehicle with id {0}. Vehicle balance is {2}", v.Id, withdrawnSum, v.Balance); // debug
            }
            Console.WriteLine("\n");
            TransactionInfo ComposeTransactionInfo(Vehicle vehicle, decimal withdrawnSum)
            {
                TransactionInfo transactionInfo = new TransactionInfo(
                    DateTime.Now.ToShortTimeString() + ' ' + DateTime.Now.ToShortDateString(),
                    vehicle.Id,
                    vehicle.Balance,
                    withdrawnSum
                    );
                return transactionInfo;
            }

            decimal CalculateWithdrawnSum(Vehicle vehicle)
            {
                decimal withdrawnSum;
                Settings.vehicleParkingTariff.TryGetValue(vehicle.VehicleType, out withdrawnSum);
                if (vehicle.Balance >= withdrawnSum)
                {
                    return withdrawnSum;
                }
                else if (vehicle.Balance == 0)
                {
                    return withdrawnSum * Settings.penaltyMultiplier;
                }
                else if (vehicle.Balance < withdrawnSum && vehicle.Balance > 0)
                {
                    return (withdrawnSum - vehicle.Balance) * Settings.penaltyMultiplier + (withdrawnSum - vehicle.Balance);
                }
                else
                {
                    return Math.Abs(vehicle.Balance) * Settings.penaltyMultiplier;
                }
            }
        }

        private void LogCurrentTransactions(object sender, ElapsedEventArgs e)
        {
            logService.Write(FormateTransactionInfo());

            string FormateTransactionInfo()
            {
                TransactionInfo[] currentTransactionInfo = GetLastParkingTransactions();
                string logFileInfo = "";
                foreach (TransactionInfo ti in currentTransactionInfo)
                {
                    logFileInfo += "\n" + ti.TransactionTime + "\n";
                    logFileInfo += "Id: " + ti.VehicleId + "\n";
                    logFileInfo += "Balance: " + ti.VehicleBalance + "\n";
                    logFileInfo += "Withdrawn sum: " + ti.WithdrawSum + "\n";
                }
                return logFileInfo;
            }
        }

        private Vehicle FindVehicleById(string id)
        {
            foreach (Vehicle v in parking.vehicles)
            {
                if (v.Id == id)
                {
                    return v;
                }
            }
            throw new ArgumentException();
        }
    }
}
