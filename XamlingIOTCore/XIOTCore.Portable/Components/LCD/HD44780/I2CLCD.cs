using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.Module;

namespace XIOTCore.Portable.Components.LCD.HD44780
{
    public class I2CLCD : LCD, II2CLCD
    {
        private readonly IXI2CDevice _i2CDevice;
        private const int LCD_NOBACKLIGHT = 0x00;
        private const int LCD_BACKLIGHT =0xff;

        private const int EN = 6;
        private const int RW = 5;
        private const int RS = 4;
        private const int D4 = 0;
        private const int D5 = 1;
        private const int D6 = 2;
        private const int D7 = 3;

        private int _address;
        private int _backlightPinMask;
        private int _backlightStsMask;

        private static I2CIO _i2cio;

        private int _en;
        private int _rw;
        private int _rs;
        int[] _dataPins = new int[4];

       
        public I2CLCD(IXI2CDevice i2CDevice, int address)
        {
            _i2CDevice = i2CDevice;
            _config(address, EN, RW, RS, D4, D5, D6, D7);
        }

        public I2CLCD(IXI2CDevice i2CDevice, int address, int backlightPin, BacklightPolarity pol)
        {
            _i2CDevice = i2CDevice;
            _config(address, EN, RW, RS, D4, D5, D6, D7);
            SetBacklightPin(backlightPin, pol);
        }

        public I2CLCD(IXI2CDevice i2CDevice, int address, int en, int rw, int rs)
        {
            _i2CDevice = i2CDevice;
            _config(address, en, rw, rs, D4, D5, D6, D7);
        }

        public I2CLCD(IXI2CDevice i2CDevice, int address, int en, int rw, int rs, int backlightPin, BacklightPolarity pol)
        {
            _i2CDevice = i2CDevice;
            _config(address, en, rw, rs, D4, D5, D6, D7);
            SetBacklightPin(backlightPin, pol);
        }

        public I2CLCD(IXI2CDevice i2CDevice, int address, int en, int rw, int rs, int d4, int d5, int d6, int d7)
        {
            _i2CDevice = i2CDevice;
            _config(address, en, rw, rs, d4, d5, d6, d7);
        }

        public I2CLCD(IXI2CDevice i2CDevice, int address, int en, int rw, int rs, int d4, int d5, int d6, int d7, int backlightPin, BacklightPolarity pol)
        {
            _i2CDevice = i2CDevice;
            _config(address, en, rw, rs, d4, d5, d6, d7);
            SetBacklightPin(backlightPin, pol);
        }

        private async Task<bool> _init()
        {
            if (_i2cio == null)
            {
                _i2cio = new I2CIO(_i2CDevice);
                if (await _i2cio.Init(_address))
                {
                    _i2cio.PortMode(LCDConstants.OUTPUT);  // Set the entire IO extender to OUTPUT
                    _displayFunction = LCDConstants.LCD_4BITMODE | LCDConstants.LCD_1LINE | LCDConstants.LCD_5x8DOTS;
                    _i2cio.Write(0);  // Set the entire port to LOW
                }
            }
            return true;
        }

        private void _config(int address, int en, int rw, int rs, int d4, int d5, int d6, int d7)
        {
            _address = address;

            _backlightPinMask = 0;
            _backlightStsMask = LCD_NOBACKLIGHT;
            _polarity = BacklightPolarity.Positive;

            _en = (1 << en);
            _rw = (1 << rw);
            _rs = (1 << rs);

            // Initialise pin mapping
            _dataPins[0] = (1 << d4);
            _dataPins[1] = (1 << d5);
            _dataPins[2] = (1 << d6);
            _dataPins[3] = (1 << d7);

        }

        private void write4bits(int value, int mode)
        {
            int pinMapValue = 0;

            // Map the value to LCD pin mapping
            // --------------------------------
            for (int i = 0; i < 4; i++)
            {
                if ((value & 0x1) == 1)
                {
                    pinMapValue |= _dataPins[i];
                }
                value = (value >> 1);
            }

            // Is it a command or data
            // -----------------------
            if (mode == LCDConstants.DATA)
            {
                mode = _rs;
            }

            //_notpulse(value, mode);

            pinMapValue |= mode | _backlightStsMask;
            _pulseEnable(pinMapValue);
        }
        private void _pulseEnable(int data)
        {
            _i2cio.Write(data | _en);   // En HIGH
            _i2cio.Write(data & ~_en);  // En LOW
        }

        public override async Task Begin(int cols, int rows, int charSize = LCDConstants.LCD_5x8DOTS)
        {
            await _init();
            await base.Begin(cols, rows, charSize);
        }


        protected override void Send(int value, int mode)
        {
            if (mode == LCDConstants.FOUR_BITS)
            {
                write4bits((value & 0x0F), LCDConstants.COMMAND);
            }
            else
            {
                write4bits((value >> 4), mode);
                write4bits((value & 0x0F), mode);
            }
        }

        public override void SetBacklight(int value)
        {
            if (_backlightPinMask != 0x0)
            {
                // Check for polarity to configure mask accordingly
                // ----------------------------------------------------------
                if (((_polarity == BacklightPolarity.Positive) && (value > 0)) ||
                     ((_polarity == BacklightPolarity.Negative) && (value == 0)))
                {
                    _backlightStsMask = _backlightPinMask & LCD_BACKLIGHT;
                }
                else
                {
                    _backlightStsMask = _backlightPinMask & LCD_NOBACKLIGHT;
                }

                if (_i2cio != null)
                {
                    _i2cio.Write(_backlightStsMask);
                }
               
            }
        }

        public override void SetBacklightPin(int value, BacklightPolarity pol)
        {
            _backlightPinMask = (1 << value);
            _polarity = pol;
            SetBacklight(LCDConstants.BackLightOff);
        }

     
    }
}
