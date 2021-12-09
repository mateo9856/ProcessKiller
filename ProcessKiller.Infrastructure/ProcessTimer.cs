using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace ProcessKiller.Infrastructure
{
    public class ProcessTimer : Timer
    {
        public void SetIntervalTime(double interval)
        {
            base.Interval = interval * 1000;
        }

        public void StartInterval()
        {
            base.AutoReset = true;
            base.Enabled = true;
            base.Start();
        }
    }
}
