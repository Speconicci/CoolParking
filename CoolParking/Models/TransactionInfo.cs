using System;

namespace CoolParking.Models
{
    public struct TransactionInfo
    {
        private string transactionTime;
        private string vehicleId;
        private decimal vehicleBalance;
        private decimal withdrawSum;

        public TransactionInfo(string transactionTime, string vehicleId, decimal vehiclebalance, decimal withdrawSum)
        {
            this.transactionTime = transactionTime;
            this.vehicleId = vehicleId;
            this.vehicleBalance = vehiclebalance;
            this.withdrawSum = withdrawSum;
        }

        public string TransactionTime { get => transactionTime; }
        public string VehicleId { get => vehicleId; }
        public decimal VehicleBalance { get => vehicleBalance; }
        public decimal WithdrawSum { get => withdrawSum; }
    }
}
