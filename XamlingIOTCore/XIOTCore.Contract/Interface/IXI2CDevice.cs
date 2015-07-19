using System;
using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface
{
    public interface IXI2CDevice : IDisposable
    {
        Task<bool> Init(int address);
        void WriteRead(byte[] writeBuffer, byte[] readBuffer);
        void Read(byte[] buffer);
        bool Write(byte[] buffer);
        bool Write(int value);
    }
}