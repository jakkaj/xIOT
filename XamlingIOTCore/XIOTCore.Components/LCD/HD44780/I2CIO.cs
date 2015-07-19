using System;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;

namespace XIOTCore.Components.LCD.HD44780
{
    public class I2CIO
    {
        int _shadow;      // Shadow output
        int _dirMask;     // Direction mask
        
        private bool _initialised;

        private IXI2CDevice _i2cDevice;

        private const int ConnectionSpeed = 40000; // Hz
        private const int LatencyTimer = 255; // Hz


        public I2CIO(IXI2CDevice i2cDevice)
        {
            _i2cDevice = i2cDevice;
             
            _dirMask = 0xFF;    // mark all as INPUTs
            _shadow = 0x0;     // no values set
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
            _shadow = Convert.ToInt32(b[0]);
            _initialised = true;

            return true;
        } 

        public void PinMode(int pin, int dir)
        {
            if (_initialised)
            {
                if (Constants.OUTPUT == dir)
                {
                    _dirMask &= ~(1 << pin);
                }
                else
                {
                    _dirMask |= (1 << pin);
                }
            }
        }

        public void PortMode(int dir)
        {
            if (_initialised)
            {
                if (dir == Constants.INPUT)
                {
                    _dirMask = 0xFF;
                }
                else
                {
                    _dirMask = 0x00;
                }
            }
        }
       
        public int Write(int value)
        {
            if (_initialised)
            {
                // Only write HIGH the values of the ports that have been initialised as
                // outputs updating the output shadow of the device
                _shadow = (value & ~(_dirMask));

                var result = _i2cDevice.Write(_shadow);
                return result == true ? 1 : 0;
            }
            return 1;
        }
    }
}
