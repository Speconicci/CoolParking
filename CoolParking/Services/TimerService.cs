using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using CoolParking.Interfaces;

namespace CoolParking.Services
{
    public class TimerService : ITimerService
    {
        private Timer timer;
        private double interval;

        public double Interval
        {
            get => interval;
            set
            {
                if (value > 0)
                {
                    this.interval = value;
                    this.timer.Interval = value;
                }
            }
        }

        public TimerService(double interval, ElapsedEventHandler eventHandler)
        {
            this.interval = interval;
            this.timer = new Timer(interval);
            this.timer.Elapsed += eventHandler;
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
