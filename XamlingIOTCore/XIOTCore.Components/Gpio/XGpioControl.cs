using XIOTCore.Contract.Dto;
using XIOTCore.Contract.Interface;

namespace XIOTCore.Components.Gpio
{
    public class XGpioControl : IXGpioControl
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
    }
}
