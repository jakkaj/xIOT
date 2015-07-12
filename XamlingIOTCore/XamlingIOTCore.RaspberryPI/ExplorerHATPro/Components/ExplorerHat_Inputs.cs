using System;
using XCore.RaspberryPI.Interface;
using XIOTCore.Components.Gpio;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Basics;

namespace XCore.RaspberryPI.ExplorerHATPro.Components
{
    public class ExplorerHat_Input : XGpioControl
    {
        public ExplorerHat_Input(IXGpio gpio) : base(gpio)
        {
        }

        public new void On()
        {
            throw new InvalidOperationException("Cannot toggle input IO");
        }

        public new void Off()
        {
            throw new InvalidOperationException("Cannot toggle input IO");
        }
    }

    public class ExplorerHat_Input1 : ExplorerHat_Input, IExplorerHat_Input1
    {
        public ExplorerHat_Input1(IXGpio gpio) : base(gpio)
        {
        }


    }
    public class ExplorerHat_Input2 : ExplorerHat_Input, IExplorerHat_Input2
    {
        public ExplorerHat_Input2(IXGpio gpio) : base(gpio)
        {
        }
    }
    public class ExplorerHat_Input3 : ExplorerHat_Input, IExplorerHat_Input3
    {
        public ExplorerHat_Input3(IXGpio gpio) : base(gpio)
        {
        }
    }
    public class ExplorerHat_Input4 : ExplorerHat_Input, IExplorerHat_Input4
    {
        public ExplorerHat_Input4(IXGpio gpio) : base(gpio)
        {
        }
    }
}
