using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIOTCore_Samples_Console
{
    //Please note - this sample is a bit rough and not intented to be teh *real* sample. 
    //See https://github.com/jakkaj/Xamling-IOT/tree/master/Samples for better stuff :)
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
