using System;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Portable.Components.LCD.HD44780;

namespace XIOTCore.Portable.Components.OLED.SSD1306
{
    public class OLED_SSD1306_I2CIO : ISimpleWriter
    {
        
        private bool _initialised;

        private IXI2CDevice _i2cDevice;

        public OLED_SSD1306_I2CIO(IXI2CDevice i2cDevice)
        {
            _i2cDevice = i2cDevice;
            _initialised = false;
        }

        public async Task<bool> Init(int address)
        {
            var result = await _i2cDevice.Init(address);

            if (!result)
            {
                return false;
            }

            byte[] b = new byte[1];
            _i2cDevice.Read(b);
            
            _initialised = true;

            return true;
        }

        public bool Write(byte[] value)
        {
            if (_initialised)
            {
                var result = _i2cDevice.Write(value);
                return result;
            }

            return false;
        }

        public bool Write(byte value)
        {
            if (_initialised)
            {
                var result = _i2cDevice.Write(value);
                return result;
            }

            return false;
        }

        public bool Write(byte value1, byte value2)
        {
            if (_initialised)
            {
                var b = new byte[2];
                b[0] = value1;
                b[1] = value2;

                
                var result = _i2cDevice.Write(b);
                return result;
            }

            return false;
        }
    }
}
