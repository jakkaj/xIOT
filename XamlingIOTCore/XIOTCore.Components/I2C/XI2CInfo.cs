using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.I2C;

namespace XIOTCore.Components.I2C
{
    public class XI2CInfo : IXI2CInfo
    {
        public async Task GetAllDevices()
        {
            var aqs = I2cDevice.GetDeviceSelector("I2C1");

            var dis = await DeviceInformation.FindAllAsync(aqs);

            var d2 = dis;
        }
    }
}
