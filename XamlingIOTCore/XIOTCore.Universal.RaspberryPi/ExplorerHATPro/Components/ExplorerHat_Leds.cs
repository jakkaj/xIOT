using XIOTCore.Contract.Components.GPIO;
using XIOTCore.Contract.Interface.GPIO;
using XIOTCore.Universal.RaspberryPi.Interface;

namespace XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Components
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
