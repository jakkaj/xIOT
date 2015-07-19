using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.I2c;
using Windows.Security.Authentication.OnlineId;
using XIOTCore.Contract.Interface;

namespace XIOTCore.Components.I2C
{
    public class WindowsI2CDevice : IXI2CDevice
    {
        private I2cDevice _i2Cdevice;

        public async Task<bool> Init(int address)
        {
            return await Init(address, null);
        }

        public async Task<bool> Init(int address, string controllerName)
        {
            if (_i2Cdevice != null)
            {
                return true;
            }

            _i2Cdevice = await I2CDeviceCache.GetDevice(address, controllerName);

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

        public void Write(byte[] buffer)
        {
            _i2Cdevice.Write(buffer);
        }

        public void Dispose()
        {
            _i2Cdevice.Dispose();
        }
    }
}
