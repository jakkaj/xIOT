using XCore.RaspberryPI.Interface;
using XIOTCore.Components.Gpio;
using XIOTCore.Contract.Interface;

namespace XCore.RaspberryPI.ExplorerHATPro.Components
{
    public class ExplorerHat_GreenLed : XGpioLed, IExplorerHat_GreenLed
    {
        public ExplorerHat_GreenLed(IXGpio gpio) : base(gpio)
        {
        }
    }
}
