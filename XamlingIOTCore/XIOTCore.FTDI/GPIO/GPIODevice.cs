using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.GPIO;
using XIOTCore.FTDI.Contract;

namespace XIOTCore.FTDI.GPIO
{
    public class GPIODevice : IXGpio
    {
        private readonly IXI2CDevice_FTDI _i2CDevice;
        private readonly int _pin;

        public GPIODevice(IXI2CDevice_FTDI i2CDevice, int pin)
        {
            _i2CDevice = i2CDevice;
            _pin = pin;
            _i2CDevice.Init();
        }

        public void SetDirection(XGpioDirection direction)
        {
            _i2CDevice.SetGPIODirection((byte)_pin, direction == XGpioDirection.Input ? (byte) 0 : (byte) 1);
        }

        public void SetLow()
        {
            _i2CDevice.SetGPIOOff((byte)_pin);
        }

        public void SetHigh()
        {
            _i2CDevice.SetGPIOOn((byte)_pin);
        }

        public XPinValue GetValue()
        {
            bool val;
            _i2CDevice.ReadGPIO((byte)_pin, out val);
            return val ? XPinValue.High : XPinValue.Low;
        }
    }
}
