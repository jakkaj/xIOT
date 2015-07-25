using System;
using System.Diagnostics;

namespace XIOTCore.Portable.Util.XamlingCore
{
    public static class StopwatchDelay
    {
        public static void Delay(int ms)
        {
            var sw = new Stopwatch();

            sw.Start();

            while (sw.ElapsedMilliseconds < ms)
            {
                continue;
            }
        }

        public static void DelayMicroSeconds(int ms)
        {
            var sw = new Stopwatch();

            sw.Start();

            while (sw.ElapsedMicroseconds() < ms)
            {
                continue;
            }
        }

        public static double ElapsedMilliseconds(this Stopwatch stopwatch)
        {
            if (stopwatch == null)
                throw new ArgumentException("Stopwatch passed cannot be null!");

            return 1000 * stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
        }

        public static double ElapsedMicroseconds(this Stopwatch stopwatch)
        {
            if (stopwatch == null)
                throw new ArgumentException("Stopwatch passed cannot be null!");

            return 1e6 * stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
        }
    }
}
