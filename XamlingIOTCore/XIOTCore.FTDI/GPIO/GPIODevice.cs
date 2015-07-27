using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;

namespace XIOTCore.FTDI.GPIO
{
    public class GPIODevice : IXGpioControl
    {
        private readonly IXI2CDevice _i2CDevice;
        private readonly int _pin;

        public GPIODevice(IXI2CDevice i2CDevice, int pin)
        {
            _i2CDevice = i2CDevice;
            _pin = pin;
        }

        public void On()
        {
            _i2CDevice.SetGPIOOn((byte)_pin);
        }

        public void Off()
        {
            _i2CDevice.SetGPIOOff((byte) _pin);
        }

        public bool State
        {
            get
            {
                bool val;
                _i2CDevice.ReadGPIO((byte) _pin, out val);
                return val;

            }
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
            _i2CDevice.SetGPIODirection((byte)_pin, direction == XGpioDirection.Input ? (byte) 0 : (byte) 1);
        }
    }
}
