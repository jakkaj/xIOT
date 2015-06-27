using XCore.RaspberryPI.Interface;
using XIOTCore.Components.Gpio;
using XIOTCore.Contract.Interface;

namespace XCore.RaspberryPI.ExplorerHATPro.Components
{
    public class ExplorerHat_Output1 : XGpioControl, IExplorerHat_Input1
    {
        public ExplorerHat_Output1(IXGpio gpio) : base(gpio)
        {
        }
    }
    public class ExplorerHat_Output2 : XGpioControl, IExplorerHat_Input2
    {
        public ExplorerHat_Output2(IXGpio gpio) : base(gpio)
        {
        }
    }
    public class ExplorerHat_Output3 : XGpioControl, IExplorerHat_Input3
    {
        public ExplorerHat_Output3(IXGpio gpio) : base(gpio)
        {
        }
    }
    public class ExplorerHat_Output4 : XGpioControl, IExplorerHat_Input4
    {
        public ExplorerHat_Output4(IXGpio gpio) : base(gpio)
        {
        }
    }
}
