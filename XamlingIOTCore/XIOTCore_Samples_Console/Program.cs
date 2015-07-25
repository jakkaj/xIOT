using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIOTCore_Samples_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleLCD = new LCD.LcdExamples_FTDI();

            var t = sampleLCD.Init();

            t.Wait();
        }
    }
}
