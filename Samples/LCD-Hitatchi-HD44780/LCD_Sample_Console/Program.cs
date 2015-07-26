using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Portable.Components.LCD.HD44780;

namespace LCD_Sample_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleLCD = new LcdExamples_FTDI();

            var t = sampleLCD.Init();

            t.Wait();
        }
    }
}
