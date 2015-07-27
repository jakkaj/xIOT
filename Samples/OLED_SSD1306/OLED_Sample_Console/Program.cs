using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLED_Sample_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample = new OLEDExamples_FTDI();

            sample.Init().Wait();
        }
    }
}
