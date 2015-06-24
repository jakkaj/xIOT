using XIOTCore.Contract.Interface;

namespace XIOTCore.Components.Gpio
{
    public class XGpioLed : IXGpioLed
    {
        private readonly IXGpio _gpio;

        public XGpioLed(IXGpio gpio)
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
    }
}
