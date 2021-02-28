using System.Timers;

namespace CoolParking.Interfaces
{
    public interface ITimerService
    {
        double Interval { get; set; }
        void Start();
        void Stop();
        void Dispose();
    }
}
