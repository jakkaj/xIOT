using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.GPIO;

namespace XIOTCore.Contract.Components.GPIO
{
    public class XGpioControl : IXGpioControl, IXGpio_0, IXGpio_1, IXGpio_2, IXGpio_3, IXGpio_4, IXGpio_5, IXGpio_6, IXGpio_7, IXGpio_8, IXGpio_9, IXGpio_10
    {
        private readonly IXGpio _gpio;

        public XGpioControl(IXGpio gpio)
        {
            _gpio = gpio;
        }

        public void On()
        {
            _gpio.SetHigh();
        }

        public void Off()
        {
            _gpio.SetLow();
        }

        public bool State
        {
            get { return _gpio.GetValue() == XPinValue.High; }
            set
            {
                if (value)
                {
                    On();
                }
                else
                {
                    Off();
                }
            }
        }

        public void SetDirection(XGpioDirection direction)
        {
            _gpio.SetDirection(direction);
        }
    }
}
