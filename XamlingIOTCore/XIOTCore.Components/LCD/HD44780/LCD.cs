using System.Threading.Tasks;
using XIOTCore.Components.Util.XamlingCore;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Module;

namespace XIOTCore.Components.LCD.HD44780
{
    public abstract class LCD : ILCD
    {
        protected int _displayFunction;
        protected int _displaycontrol;
        protected int _displaymode;
        protected int _numlines;
        protected int _cols;
        protected BacklightPolarity _polarity;


        private void _command(int value)
        {
            Send(value, Constants.COMMAND);
        }

        protected abstract void Send(int value, int mode);

        public virtual async Task Begin(int cols, int rows, int charSize = Constants.LCD_5x8DOTS)
        {
            if (rows > 1)
            {
                _displayFunction |= Constants.LCD_2LINE;
            }

            _numlines = rows;
            _cols = cols;

            StopwatchDelay.Delay(100);

            if ((_displayFunction & Constants.LCD_8BITMODE) == 0)
            {
                // this is according to the hitachi HD44780 datasheet
                // figure 24, pg 46

                // we start in 8bit mode, try to set 4 bit mode
                Send(0x03, Constants.FOUR_BITS);
                StopwatchDelay.DelayMicroSeconds(4500); // wait min 4.1ms

                // second try
                Send(0x03, Constants.FOUR_BITS);
                StopwatchDelay.DelayMicroSeconds(4500); // wait min 4.1ms

                // third go!
                Send(0x03, Constants.FOUR_BITS);
                StopwatchDelay.DelayMicroSeconds(150);

                // finally, set to 4-bit interface
                Send(0x02, Constants.FOUR_BITS);
            }
            else
            {
                // this is according to the hitachi HD44780 datasheet
                // page 45 figure 23

                // Send function set command sequence
                _command(Constants.FunctionSet | _displayFunction);
                StopwatchDelay.DelayMicroSeconds(4500);  // wait more than 4.1ms

                // second try
                _command(Constants.FunctionSet | _displayFunction);
                StopwatchDelay.DelayMicroSeconds(150);

                // third go
                _command(Constants.FunctionSet | _displayFunction);
            }

            // finally, set # lines, font size, etc.
            _command(Constants.FunctionSet | _displayFunction);

            // turn the display on with no cursor or blinking default
            _displaycontrol = Constants.DisplayOn | Constants.CursorOff | Constants.BlinkOff;
            Display();

            // clear the LCD
            Clear();

            // Initialize to default text direction (for romance languages)
            _displaymode = Constants.EntryLeft | Constants.ShiftDecrement;
            // set the entry mode
            _command(Constants.EntryModeSet | _displaymode);

            BackLight();
        }

        public void Clear()
        {
            _command(Constants.ClearDisplay);
            StopwatchDelay.DelayMicroSeconds(Constants.HomeClearExec);
        }

        public void Home()
        {
            _command(Constants.ReturnHome);
            StopwatchDelay.DelayMicroSeconds(Constants.HomeClearExec);
        }

        public void Display()
        {
            _displaycontrol &= Constants.DisplayOn;
            _command(Constants.DisplayControl | _displaycontrol);
        }

        public void NoDisplay()
        {
            _displaycontrol &= ~Constants.DisplayOn;
            _command(Constants.DisplayControl | _displaycontrol);
        }

        public void Blink()
        {
            _displaycontrol &= Constants.BlinkOn;
            _command(Constants.DisplayControl | _displaycontrol);
        }

        public void NoBlink()
        {
            _displaycontrol &= ~Constants.BlinkOn;
            _command(Constants.DisplayControl | _displaycontrol);
        }

        public void NoCursor()
        {
            _displaycontrol &= ~Constants.CursorOn;
            _command(Constants.DisplayControl | _displaycontrol);
        }

        public void Cursor()
        {
            _displaycontrol &= Constants.CursorOn;
            _command(Constants.DisplayControl | _displaycontrol);
        }

        public void ScollDisplayLeft()
        {
            _command(Constants.CursorShift | Constants.DisplayMove | Constants.MoveLeft);
        }

        public void ScrollDisplayRight()
        {
            _command(Constants.CursorShift | Constants.DisplayMove | Constants.MoveRight);
        }

        public void LeftToRight()
        {
            _displaymode |= Constants.EntryLeft;
            _command(Constants.EntryModeSet | _displaymode);
        }

        public void RightToLeft()
        {
            _displaymode |= ~Constants.EntryLeft;
            _command(Constants.EntryModeSet | _displaymode);
        }

        public void MoveCursorLeft()
        {
            _command(Constants.CursorShift | Constants.CursorMove | Constants.MoveLeft);
        }

        public void MoveCursorRight()
        {
            _command(Constants.CursorShift | Constants.CursorMove | Constants.MoveRight);
        }

        public void AutoScroll()
        {
            _displaymode |= Constants.ShiftIncrement;
            _command(Constants.EntryModeSet | _displaymode);
        }

        public void NoAutoScroll()
        {
            _displaymode |= ~Constants.ShiftIncrement;
            _command(Constants.EntryModeSet | _displaymode);
        }

        public void CreateChar(int location, int[] charMap)
        {
            location &= 0x7;            // we only have 8 locations 0-7

            _command(Constants.SetCGRAMAddr | (location << 3));
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
                _command(Constants.SetDDRAMAddr | (col + row_offsetsLarge[row]));
            }
            else
            {
                _command(Constants.SetDDRAMAddr | (col + row_offsetsDef[row]));
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
                Send(i, Constants.DATA);
            }
           
            return 1;
        }

        public virtual int Write(int value)
        {
            Send(value, Constants.DATA);
            return 1;
        }
    }
}
