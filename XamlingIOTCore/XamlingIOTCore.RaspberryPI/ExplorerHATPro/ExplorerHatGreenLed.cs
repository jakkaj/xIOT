using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.RaspberryPI.Interface;
using XIOTCore.Components.Gpio;
using XIOTCore.Contract.Interface;

namespace XCore.RaspberryPI.ExplorerHATPro
{
    public class ExplorerHatGreenLed : XGpioLed, IExplorerHatGreenLed
    {
        public ExplorerHatGreenLed(IXGpio gpio) : base(gpio)
        {
        }
    }
}
