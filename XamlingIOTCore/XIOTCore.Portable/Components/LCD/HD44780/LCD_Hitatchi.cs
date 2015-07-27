using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Portable.Util.XamlingCore;

namespace XIOTCore.Portable.Components.LCD.HD44780
{
    public abstract class LCD_Hitatchi : ILCD_Hitatchi
    {
        protected int _displayFunction;
        protected int _displaycontrol;
        protected int _displaymode;
        protected int _numlines;
        protected int _cols;
        protected BacklightPolarity _polarity;


        private void _command(int value)
        {
            Send(value, LCDConstants.COMMAND);
        }

        protected abstract void Send(int value, int mode);

        public virtual async Task Begin(int cols, int rows, int charSize = LCDConstants.LCD_5x8DOTS)
        {
            if (rows > 1)
            {
                _displayFunction |= LCDConstants.LCD_2LINE;
            }

            _numlines = rows;
            _cols = cols;

            StopwatchDelay.Delay(100);

            if ((_displayFunction & LCDConstants.LCD_8BITMODE) == 0)
            {
                // this is according to the hitachi HD44780 datasheet
                // figure 24, pg 46

                // we start in 8bit mode, try to set 4 bit mode
                Send(0x03, LCDConstants.FOUR_BITS);
                StopwatchDelay.DelayMicroSeconds(4500); // wait min 4.1ms

                // second try
                Send(0x03, LCDConstants.FOUR_BITS);
                StopwatchDelay.DelayMicroSeconds(4500); // wait min 4.1ms

                // third go!
                Send(0x03, LCDConstants.FOUR_BITS);
                StopwatchDelay.DelayMicroSeconds(150);

                // finally, set to 4-bit interface
                Send(0x02, LCDConstants.FOUR_BITS);
            }
            else
            {
                // this is according to the hitachi HD44780 datasheet
                // page 45 figure 23

                // Send function set command sequence
                _command(LCDConstants.FunctionSet | _displayFunction);
                StopwatchDelay.DelayMicroSeconds(4500);  // wait more than 4.1ms

                // second try
                _command(LCDConstants.FunctionSet | _displayFunction);
                StopwatchDelay.DelayMicroSeconds(150);

                // third go
                _command(LCDConstants.FunctionSet | _displayFunction);
            }

            // finally, set # lines, font size, etc.
            _command(LCDConstants.FunctionSet | _displayFunction);

            // turn the display on with no cursor or blinking default
            _displaycontrol = LCDConstants.DisplayOn | LCDConstants.CursorOff | LCDConstants.BlinkOff;
            Display();

            // clear the LCD
            Clear();

            // Initialize to default text direction (for romance languages)
            _displaymode = LCDConstants.EntryLeft | LCDConstants.ShiftDecrement;
            // set the entry mode
            _command(LCDConstants.EntryModeSet | _displaymode);

            BackLight();
        }

        public void Clear()
        {
            _command(LCDConstants.ClearDisplay);
            StopwatchDelay.DelayMicroSeconds(LCDConstants.HomeClearExec);
        }

        public void Home()
        {
            _command(LCDConstants.ReturnHome);
            StopwatchDelay.DelayMicroSeconds(LCDConstants.HomeClearExec);
        }

        public void Display()
        {
            _displaycontrol &= LCDConstants.DisplayOn;
            _command(LCDConstants.DisplayControl | _displaycontrol);
        }

        public void NoDisplay()
        {
            _displaycontrol &= ~LCDConstants.DisplayOn;
            _command(LCDConstants.DisplayControl | _displaycontrol);
        }

        public void Blink()
        {
            _displaycontrol &= LCDConstants.BlinkOn;
            _command(LCDConstants.DisplayControl | _displaycontrol);
        }

        public void NoBlink()
        {
            _displaycontrol &= ~LCDConstants.BlinkOn;
            _command(LCDConstants.DisplayControl | _displaycontrol);
        }

        public void NoCursor()
        {
            _displaycontrol &= ~LCDConstants.CursorOn;
            _command(LCDConstants.DisplayControl | _displaycontrol);
        }

        public void Cursor()
        {
            _displaycontrol &= LCDConstants.CursorOn;
            _command(LCDConstants.DisplayControl | _displaycontrol);
        }

        public void ScollDisplayLeft()
        {
            _command(LCDConstants.CursorShift | LCDConstants.DisplayMove | LCDConstants.MoveLeft);
        }

        public void ScrollDisplayRight()
        {
            _command(LCDConstants.CursorShift | LCDConstants.DisplayMove | LCDConstants.MoveRight);
        }

        public void LeftToRight()
        {
            _displaymode |= LCDConstants.EntryLeft;
            _command(LCDConstants.EntryModeSet | _displaymode);
        }

        public void RightToLeft()
        {
            _displaymode |= ~LCDConstants.EntryLeft;
            _command(LCDConstants.EntryModeSet | _displaymode);
        }

        public void MoveCursorLeft()
        {
            _command(LCDConstants.CursorShift | LCDConstants.CursorMove | LCDConstants.MoveLeft);
        }

        public void MoveCursorRight()
        {
            _command(LCDConstants.CursorShift | LCDConstants.CursorMove | LCDConstants.MoveRight);
        }

        public void AutoScroll()
        {
            _displaymode |= LCDConstants.ShiftIncrement;
            _command(LCDConstants.EntryModeSet | _displaymode);
        }

        public void NoAutoScroll()
        {
            _displaymode |= ~LCDConstants.ShiftIncrement;
            _command(LCDConstants.EntryModeSet | _displaymode);
        }

        public void CreateChar(int location, int[] charMap)
        {
            location &= 0x7;            // we only have 8 locations 0-7

            _command(LCDConstants.SetCGRAMAddr | (location << 3));
            StopwatchDelay.DelayMicroSeconds(30);

            for (int i = 0; i < 8; i++)
            {
                Write(charMap[i]);      // call the virtual write method
                StopwatchDelay.DelayMicroSeconds(40);
            }
        }

        public void SetCursor(int col, int row)
        {
            byte[] row_offsetsDef = { 0x00, 0x40, 0x14, 0x54 }; // For regular LCDs
            byte[] row_offsetsLarge = { 0x00, 0x40, 0x10, 0x50 }; // For 16x4 LCDs

            if (row >= _numlines)
            {
                row = _numlines - 1;    // rows start at 0
            }

            // 16x4 LCDs have special memory map layout
            // ----------------------------------------
            if (_cols == 16 && _numlines == 4)
            {
                _command(LCDConstants.SetDDRAMAddr | (col + row_offsetsLarge[row]));
            }
            else
            {
                _command(LCDConstants.SetDDRAMAddr | (col + row_offsetsDef[row]));
            }
        }

        public void BackLight()
        {
            SetBacklight(255);
        }

        public void NoBacklight()
        {
            SetBacklight(0);
        }

        public void On()
        {
            Display();
            BackLight();
        }

        public void Off()
        {
            NoBacklight();
            NoDisplay();
        }

        public virtual void SetBacklightPin(int value, BacklightPolarity pol)
        {
            
        }

        public virtual void SetBacklight(int value)
        {
            
        }

        public virtual int Write(string value)
        {
            foreach (var d in value)
            {
                var i = (int) d;
                var i2 = (uint) d;
                Send(i, LCDConstants.DATA);
            }
           
            return 1;
        }

        public virtual int Write(int value)
        {
            Send(value, LCDConstants.DATA);
            return 1;
        }
    }
}
