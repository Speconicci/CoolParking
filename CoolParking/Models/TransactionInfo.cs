using System;

namespace CoolParking.Models
{
    struct TransactionInfo
    {
        private string transactionTime;
        private string vehicleId;
        private decimal vehicleBalance;
        private decimal withdrawSum;

        public string TransactionTime { get => transactionTime; }
        public string VehicleId { get => vehicleId; }
        public decimal VehicleBalance { get => vehicleBalance; }
        public decimal WithdrawSum { get => withdrawSum; }
    }
}
