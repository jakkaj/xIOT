using XCore.RaspberryPI.Interface;
using XIOTCore.Components.Gpio;
using XIOTCore.Contract.Interface;

namespace XCore.RaspberryPI.ExplorerHATPro.Components
{
    public class ExplorerHat_RedLed : XGpioLed, IExplorerHat_RedLed
    {
        public ExplorerHat_RedLed(IXGpio gpio) : base(gpio)
        {
        }
    }
}
