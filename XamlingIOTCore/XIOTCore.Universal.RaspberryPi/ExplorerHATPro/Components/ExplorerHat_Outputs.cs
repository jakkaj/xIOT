using XIOTCore.Contract.Components.GPIO;
using XIOTCore.Contract.Interface.GPIO;
using XIOTCore.Universal.RaspberryPi.Interface;

namespace XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Components
{
    public class ExplorerHat_Output1 : XGpioControl, IExplorerHat_Output1
    {
        public ExplorerHat_Output1(IXGpio gpio) : base(gpio)
        {
        }
    }
    public class ExplorerHat_Output2 : XGpioControl, IExplorerHat_Output2
    {
        public ExplorerHat_Output2(IXGpio gpio) : base(gpio)
        {
        }
    }
    public class ExplorerHat_Output3 : XGpioControl, IExplorerHat_Output3
    {
        public ExplorerHat_Output3(IXGpio gpio) : base(gpio)
        {
        }
    }
    public class ExplorerHat_Output4 : XGpioControl, IExplorerHat_Output4
    {
        public ExplorerHat_Output4(IXGpio gpio) : base(gpio)
        {
        }
    }
}
