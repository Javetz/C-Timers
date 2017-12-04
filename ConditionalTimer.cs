using System;
using System.Timers;

namespace.MyTimers.Utils.Timer
{
    public class ConditionalTimer
    {
        private System.Timers.Timer internalTimer;
        private readonly Action action;
        private readonly Func<bool> stopCondition;
        private readonly int interval;

        public ConditionalTimer(Action action, Func<bool> stopCondition, int interval)
        {
            this.action = action;
            this.stopCondition = stopCondition;
            this.interval = interval;

            SetUpTimer();
        }

        private void SetUpTimer()
        {
            internalTimer = new System.Timers.Timer(interval) {AutoReset = false};
            internalTimer.Elapsed += new ElapsedEventHandler(Target);
        }

        public void ExecuteTimer()
        {
            internalTimer.Start();
        }

        private void Target(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            internalTimer.Stop();
            action();
            var shouldContinue = !stopCondition();
            if (shouldContinue)
            {
                internalTimer.Start();
            }
            else
            {
                internalTimer.Dispose();
            }
        }

    }
}
