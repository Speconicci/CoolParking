using System;
using System.Timers;
using CoolParking.Interfaces;

namespace CoolParking.Services
{
    public class TimerService : ITimerService
    {
        private Timer timer;

        public double Interval
        {
            get => timer.Interval;
            set
            {
                if (value > 0)
                {
                    timer.Interval = value;
                }
                else throw new ArgumentException();
            }
        }

        public TimerService(double interval, ElapsedEventHandler eventHandler)
        {
            this.timer = new Timer();
            this.Interval = interval;
            if (eventHandler != null) this.timer.Elapsed += eventHandler;
            else throw new ArgumentException();
        }

        public void Dispose()
        {
            this.timer.Dispose();
        }

        public void Start()
        {
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }
    }
}
