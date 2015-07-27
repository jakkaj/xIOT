using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Contract.Interface.Devices;

namespace XIOTCore.Portable.Components.LCD.HD44780
{
    public class LCD_Hitatchi_I2C : LCD_Hitatchi, ILCD_Hitatchi_I2C
    {
        private readonly IXI2CDevice _i2CDevice;
        private const int LCD_NOBACKLIGHT = 0x00;
        private const int LCD_BACKLIGHT =0xff;

        private int _address;
        private int _backlightPinMask;
        private int _backlightStsMask;

        private static LCD_Hitachi_I2CIO _lcdHitachiI2Cio;

        private int _en;
        private int _rw;
        private int _rs;
        int[] _dataPins = new int[4];

       
        public LCD_Hitatchi_I2C(IXI2CDevice i2CDevice)
        {
            _i2CDevice = i2CDevice;
        }
       
        public  async Task<bool> Init(int address, int cols, int rows, int charSize = LCDConstants.LCD_5x8DOTS,
            int en = 6, int rw = 5, 
            int rs = 4, int d4 = 0, 
            int d5 = 1, int d6 = 2, 
            int d7 = 3,
            int? backlightPin = null, BacklightPolarity? polarity = null)
        {
            _address = address;

            if (_lcdHitachiI2Cio == null)
            {
                _lcdHitachiI2Cio = new LCD_Hitachi_I2CIO(_i2CDevice);
                if (await _lcdHitachiI2Cio.Init(_address))
                {
                    _lcdHitachiI2Cio.PortMode(LCDConstants.OUTPUT);  // Set the entire IO extender to OUTPUT
                    _displayFunction = LCDConstants.LCD_4BITMODE | LCDConstants.LCD_1LINE | LCDConstants.LCD_5x8DOTS;
                    _lcdHitachiI2Cio.Write(0);  // Set the entire port to LOW
                }
            }

            await Begin(cols, rows, charSize);

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

            if (backlightPin != null && polarity != null)
            {
                SetBacklightPin(backlightPin.Value, polarity.Value);
            }

            return true;
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
            _lcdHitachiI2Cio.Write(data | _en);   // En HIGH
            _lcdHitachiI2Cio.Write(data & ~_en);  // En LOW
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

                if (_lcdHitachiI2Cio != null)
                {
                    _lcdHitachiI2Cio.Write(_backlightStsMask);
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
