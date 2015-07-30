using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;
using XIOTCore.Portable.Util.XamlingCore;

namespace XIOTCore.Universal.Windows.I2C
{
    internal static class I2CDeviceCache
    {
        private static Dictionary<string, I2cDevice> _deviceCache = new Dictionary<string, I2cDevice>();

        static XAsyncLock _locker = new XAsyncLock();

        public static async Task<I2cDevice> GetDevice(int address, string controllerName)
        {
            var key = $"{address} - {controllerName}";

            if (_deviceCache.ContainsKey(key))
            {
                return _deviceCache[key];
            }

            using (var l = await _locker.LockAsync())
            {
                if (_deviceCache.ContainsKey(key))
                {
                    return _deviceCache[key];
                }

                var settings = new I2cConnectionSettings(address) { BusSpeed = I2cBusSpeed.FastMode };

                var aqs = I2cDevice.GetDeviceSelector(controllerName);

                var dis = await DeviceInformation.FindAllAsync(aqs);
                
                var i2Cdevice = await I2cDevice.FromIdAsync(dis[0].Id, settings);       
                
                if (i2Cdevice == null)
                {
                    return null;
                }

                _deviceCache.Add(key, i2Cdevice);

                return i2Cdevice;
            }
        }
    }
}
