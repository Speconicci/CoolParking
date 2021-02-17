﻿using System.Timers;

namespace CoolParking.Interfaces
{
    public interface ITimerService
    {
        event ElapsedEventHandler Elapsed;
        double Interval { get; set; }
        void Start();
        void Stop();
        void Dispose();
    }
}
