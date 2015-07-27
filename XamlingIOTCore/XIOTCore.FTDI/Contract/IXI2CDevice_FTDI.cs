using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract.Interface.Basics;

namespace XIOTCore.FTDI.Contract
{
    public interface IXI2CDevice_FTDI : IXI2CDevice
    {
        bool SetGPIODirection(byte pin, byte dir);
        bool SetGPIOOn(byte pin);
        bool SetGPIOOff(byte pin);
        bool ReadGPIO(byte pin, out bool value);
        Task<bool> Init();
    }
}
