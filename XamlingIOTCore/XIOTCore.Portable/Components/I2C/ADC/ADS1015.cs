using System;
using System.Threading.Tasks;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.I2C;
using XIOTCore.Portable.Util.XamlingCore;

namespace XIOTCore.Portable.Components.I2C.ADC
{
    public class ADS1015 : IADS1015, IDisposable
    {
        private const int REG_CONV = 0x00;
        private const int REG_CFG = 0x01;


        private readonly IXI2CDevice _i2Cdevice;
        private readonly int _address;

        XAsyncLock _locker = new XAsyncLock();

        public ADS1015(IXI2CDevice i2Cdevice, int address)
        {
            _i2Cdevice = i2Cdevice;
            _address = address;
           
        }

        public async Task<bool> Init()
        {
            return await _i2Cdevice.Init(_address);
        }

        protected async Task<double> GetMillivolts(ushort channel, ushort gain, ushort samplesPerSecond)
        {
            using (var l = await _locker.LockAsync())
            {
                var result = new byte[2];
                var data = new byte[3];

                // Set disable comparator and set "single shot" mode	
                var config = 0x0003 | 0x8000;
                config |= samplesPerSecond;
                config |= channel;
                config |= gain;

                data[0] = REG_CFG;
                data[1] = (byte)((config >> 8) & 0xFF);
                data[2] = (byte)(config & 0xFF);

                _i2Cdevice.Write(data);
              
                await Task.Delay(5);

                _i2Cdevice.WriteRead(new byte[] { (byte)REG_CONV, 0x00 }, result);

                return (ushort)(((result[0] << 8) | result[1]) >> 4) * gain.ForADS1015Scale() / 2048;
            }
        }

        public void Dispose()
        {
            _i2Cdevice.Dispose();
        }
    }
}
