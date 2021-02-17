using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CoolParking.Models
{
    class Vehicle
    {
        private string id;
        private readonly string idPattern = @"[A-Z]{2}[-][0-9]{4}[-][A-Z]{2}";
        private VehicleType vehicleType;
        private decimal balance;
        
        public string Id
        {
            get => id;
        }

        public VehicleType VehicleType
        {
            get => vehicleType;
        }

        public decimal Balance
        {
            get => balance;
            set => balance = value;
        }

        public Vehicle(string id, VehicleType vehicleType, decimal balance)
        {
            if (Regex.IsMatch(id, idPattern))
            {
                this.id = id;
            }
            this.vehicleType = vehicleType;
            if (balance > 0) this.Balance = balance;
        }

        public Vehicle(VehicleType vehicleType, decimal balance)
        {
            string id = GenerateRandomVehicleId();
            if (Regex.IsMatch(id, idPattern))
            {
                this.id = id;
            }
            this.vehicleType = vehicleType;
            if (balance > 0) this.Balance = balance;
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
