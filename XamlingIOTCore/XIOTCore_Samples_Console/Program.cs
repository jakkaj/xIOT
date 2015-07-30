using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore_Samples_Console.GPIO;
using XIOTCore_Samples_Console.LCD;
using XIOTCore_Samples_Console.OLED;

namespace XIOTCore_Samples_Console
{
    //Please note - this sample is a bit rough and not intented to be teh *real* sample. 
    //See https://github.com/jakkaj/Xamling-IOT/tree/master/Samples for better stuff :)
    class Program
    {
        static void Main(string[] args)
        {
            var sample = new LcdExamples_FTDI();
            //var sample = new OLEDExamples_FTDI();
            //var sample2 = new GPIOExamples_FTDI();
            //sample2.Init().Wait();
            var t = sample.Init();

            t.Wait();

            //var sampleLCD = new LCD.LcdExamples_FTDI();

            //var t = sampleLCD.Init();

            //t.Wait();
        }
    }
}
