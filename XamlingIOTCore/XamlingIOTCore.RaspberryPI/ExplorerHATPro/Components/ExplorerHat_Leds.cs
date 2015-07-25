using XCore.RaspberryPI.Interface;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Windows.Gpio;

namespace XCore.RaspberryPI.ExplorerHATPro.Components
{
    public class ExplorerHat_RedLed : XGpioControl, IExplorerHat_RedLed
    {
        public ExplorerHat_RedLed(IXGpio gpio) : base(gpio)
        {
        }
    }

    public class ExplorerHat_GreenLed : XGpioControl, IExplorerHat_GreenLed
    {
        public ExplorerHat_GreenLed(IXGpio gpio) : base(gpio)
        {
        }
    }
}
