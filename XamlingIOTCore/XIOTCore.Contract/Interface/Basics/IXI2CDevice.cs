using System;
using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface.Basics
{
    public interface IXI2CDevice : IDisposable
    {
        Task<bool> Init(int address);
        void WriteRead(byte[] writeBuffer, byte[] readBuffer);
        void Read(byte[] buffer);
        bool Write(byte[] buffer);
        bool Write(int value);
        bool SetGPIODirection(byte pin, byte dir);
        bool SetGPIOOn(byte pin);
        bool SetGPIOOff(byte pin);
        bool ReadGPIO(byte pin, out bool value);
    }
}