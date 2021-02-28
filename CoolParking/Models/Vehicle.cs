using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CoolParking.Models
{
    public class Vehicle
    {
        private string id;
        private readonly string idPattern = @"[A-Z]{2}[-][0-9]{4}[-][A-Z]{2}";
        private VehicleType vehicleType;
        private decimal balance;
        
        public string Id
        {
            get => id;
            private set
            {
                if (Regex.IsMatch(value, idPattern))
                {
                    id = value;
                }
                else throw new ArgumentException();
            }
        }

        public VehicleType VehicleType
        {
            get => vehicleType;
            private set => vehicleType = value;
        }

        public decimal Balance
        {
            get => balance;
            private set
            {
                if (value > 0) balance = value;
                else throw new ArgumentException();
            }
        }

        public Vehicle(string id, VehicleType vehicleType, decimal balance)
        {
            this.Id = id;
            this.VehicleType = vehicleType;
            this.Balance = balance;
        }

        public Vehicle(VehicleType vehicleType, decimal balance)
        {
            this.Id = GenerateRandomVehicleId();
            this.VehicleType = vehicleType;
            this.Balance = balance;
        }

        public void Withdraw(decimal sum)
        {
            if (sum > 0) this.balance -= sum;
            else throw new ArgumentException();
        }

        public void TopUp(decimal sum)
        {
            if (sum <= 0) this.balance += sum;
            else throw new ArgumentException();
        }

        static string GenerateRandomVehicleId()
        {
            StringBuilder sb = new StringBuilder(10);
            Random random = new Random();

            sb.AppendFormat(GenerateRandomPrefix());
            sb.AppendFormat(AddSeparator());
            sb.AppendFormat(GenerateRandomCode());
            sb.AppendFormat(AddSeparator());
            sb.AppendFormat(GenerateRandomPrefix());

            return sb.ToString();

            string GenerateRandomPrefix()
            {
                string prefix = "";
                prefix += (char)random.Next('A', 'Z');
                prefix += (char)random.Next('A', 'Z');
                return prefix;
            }

            string GenerateRandomCode()
            {
                string code = "";
                for (byte i = 0; i < 4; i++)
                {
                    code += random.Next(0, 9);
                }
                return code;
            }

            string AddSeparator()
            {
                return "-";
            }
        }
    }
}
