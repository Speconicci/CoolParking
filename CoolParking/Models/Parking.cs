using System.Collections.Generic;

namespace CoolParking.Models
{
    public class Parking
    {
        public static Parking instance;
        private decimal balance;
        public List<Vehicle> vehicles;

        private Parking()
        {
            this.balance = Settings.initialParkingBalance;
            this.vehicles = new List<Vehicle>(Settings.parkingCapacity);
        }

        public decimal Balance
        {
            get => balance;
        }

        public void TopUp(decimal sum)
        {
            if (sum > 0)
            {
                this.balance += sum;
            }
        }

        public static Parking GetInstance()
        {
            if (instance == null)
            {
                instance = new Parking();
                return instance;
            }
            else
            {
                return instance;
            }
        }
    }
}
