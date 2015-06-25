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
    public class ExplorerHatRedLed : XGpioLed, IExplorerHatRedLed
    {
        public ExplorerHatRedLed(IXGpio gpio) : base(gpio)
        {
        }
    }
}
