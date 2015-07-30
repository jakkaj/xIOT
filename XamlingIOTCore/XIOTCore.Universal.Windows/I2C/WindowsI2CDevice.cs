using System;
using System.Threading.Tasks;
using Windows.Devices.I2c;
using XIOTCore.Contract.Interface.Basics;

namespace XIOTCore.Universal.Windows.I2C
{
    public class WindowsI2CDevice : IXI2CDevice
    {
        private I2cDevice _i2Cdevice;

        public async Task<bool> Init(int address)
        {
            if (_i2Cdevice != null)
            {
                return true;
            }

            _i2Cdevice = await I2CDeviceCache.GetDevice(address, "I2C1");

            return _i2Cdevice != null;
        }

        public void WriteRead(byte[] writeBuffer, byte[] readBuffer)
        {
            _i2Cdevice.WriteRead(writeBuffer, readBuffer);
        }

        public void Read(byte[] buffer)
        {
            _i2Cdevice.Read(buffer);
        }

        public bool Write(byte[] buffer)
        {
            _i2Cdevice.Write(buffer);
            return true;
        }

        public bool Write(int value)
        {
            var array = new byte[1];
            array[0] = Convert.ToByte(value);
            _i2Cdevice.Write(array);

            return true;
        }

        public void Dispose()
        {
            _i2Cdevice.Dispose();
        }
    }
}
